using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MC_Cables_0._1
{
    public partial class fNuevaCarga : Form
    {
        String strConnection = "Server=ING114;Port=3306;Database=dbthor;Uid=usuario;Pwd=usuario123;";
        String NombreProyecto;
        String ProyectoID;
        String EscID;
        String EquipoID;

        
        public Equipo EquipoResultado = new Equipo();

        DataSet dsEquipos = new DataSet();
        List<String> listaEquipos = new List<String>();


        public fNuevaCarga()
        {
            InitializeComponent();
        }

        public fNuevaCarga(String Usuario, String Proyecto, String EscenarioID)
        {
            InitializeComponent();

            EscID = EscenarioID;

            this.DialogResult = DialogResult.Cancel;
            if (String.IsNullOrEmpty(Proyecto))
                Proyecto = "No hay proyecto";
            lbUsuario.Text = "Usuario: " + Usuario;
            lbProyecto.Text = "Proyecto: " + Proyecto;

            NombreProyecto = Proyecto;

            try
            {
                //Consigo el ProyectID
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    string lectura = null;
                    MySqlCommand cmd = new MySqlCommand(String.Format("SELECT ProyectoID FROM Proyectos WHERE NombreProyecto =  '{0}'", NombreProyecto), sqlConnection);
                    sqlConnection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lectura += reader.GetString(0);
                    }
                    reader.Close();
                    sqlConnection.Close();

                    ProyectoID = lectura;
                }

                //Pido la lista de Equipos
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    List<String> EscenariosID = new List<String>();
                    List<String> listaEsc = new List<String>();

                    //Obtengo los ID de escenarios
                    MySqlCommand cmd = new MySqlCommand(String.Format("SELECT * FROM Equipos WHERE ProyectoID =  '{0}'", ProyectoID), sqlConnection);
                    sqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dsEquipos);
                    sqlConnection.Close();
                    dsEquipos.Tables[0].DefaultView.Sort = "TAG";
                }

                foreach (DataRow Fila in dsEquipos.Tables[0].Rows)
                {
                    if (!String.IsNullOrEmpty(Fila["Tag"].ToString()))
                        listaEquipos.Add(Fila["Tag"].ToString());
                }
                listaEquipos.Sort();
                cbEquipos.DataSource = listaEquipos;
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private void cbEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            int Indice = cb.SelectedIndex;
            String EquipoSeleccionado = listaEquipos[Indice].ToString();

            foreach (DataRow Fila in dsEquipos.Tables[0].Rows)
            {
                if (Fila["TAG"].ToString() == EquipoSeleccionado)
                {
                    EquipoID = Fila["EquipoID"].ToString();
                }
            }


            //Calculo la corriente
            var PA = Convert.ToInt32(dsEquipos.Tables[0].Rows[Indice]["PotenciaActiva"]);
            var TE = Convert.ToInt32(dsEquipos.Tables[0].Rows[Indice]["Tension"]);
            var CF = Convert.ToInt32(dsEquipos.Tables[0].Rows[Indice]["CantidadFases"]);
            var CO = Convert.ToInt32(dsEquipos.Tables[0].Rows[Indice]["Cosfi"]);

            float k;

            if (CF == 3)
                k = (float)Math.Sqrt(3);
            else
                k = 1;

            var Corriente = PA * 1000 / (TE * CO * k);

            //Cargo los valores
            lbRev.Text = "Rev: " + dsEquipos.Tables[0].Rows[Indice]["Rev"].ToString();
            lbPotencia.Text = "Potencia Activa: " + dsEquipos.Tables[0].Rows[Indice]["PotenciaActiva"].ToString();
            lbCosfi.Text = "Coseno Fi: " + dsEquipos.Tables[0].Rows[Indice]["Cosfi"].ToString();
            lbIArranque.Text = "Corriente Arranque: " + dsEquipos.Tables[0].Rows[Indice]["CorrienteArranque"].ToString();
            lbTension.Text = "Tension: " + dsEquipos.Tables[0].Rows[Indice]["Tension"].ToString();
            lbCosFiArr.Text = "Coseno Fi Arranque: " + dsEquipos.Tables[0].Rows[Indice]["CosfiArranque"].ToString();
            lbCorriente.Text = "Corriente: " + Corriente.ToString("#.##");
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult Res = new DialogResult();
            Res = MessageBox.Show("¿Agregar equipo seleccionado?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Res == DialogResult.Yes)
            {
                String Query = NuevaLinea();

                try
                {
                    using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                    {
                        MySqlCommand sqlCmd = new MySqlCommand(Query, sqlConnection);
                        sqlConnection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
                catch (Exception exc) { MessageBox.Show(exc.Message); }

                fBalanceCargas aux = (fBalanceCargas)this.Owner;
                aux.RecargarEscenario();
            }
            Close();
        }

        private void btAgregar_Click(object sender, EventArgs e)
        {
            String Query = NuevaLinea();

            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(Query, sqlConnection);
                    sqlConnection.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            fBalanceCargas aux = (fBalanceCargas) this.Owner;
            aux.RecargarEscenario();
        }

        public String NuevaLinea()
        {
            String Columna = "INSERT INTO ListaCargas (ProyectoID, EquipoID, EscenarioID) ";
            String Valores = "VALUES ('" + ProyectoID.ToString() + "', '" + EquipoID.ToString() + "', '" + EscID.ToString() + "')";

            String Mensaje = Columna + Valores;

            return Mensaje;
        }
    }
}



