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
    public partial class fBalanceCargas : Form
    {
        String strConnection = "Server=ING114;Port=3306;Database=dbthor;Uid=usuario;Pwd=usuario123;";
        String NombreProyecto;
        String ProyectoID;
        String EscenarioID;

        MySqlDataAdapter sda;
        DataTable dt = new DataTable();


        public fBalanceCargas()
        {
            InitializeComponent();
        }

        public fBalanceCargas(String Usuario, String Proyecto)
        {
            InitializeComponent();

            try
            {
                dgvCargas.DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
                if (String.IsNullOrEmpty(Proyecto))
                    Proyecto = "No hay proyecto";
                lbUsuario.Text = "Usuario: " + Usuario.ToString();
                lbProyecto.Text = "Proyecto: " + Proyecto.ToString();

                NombreProyecto = Proyecto.ToString();

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

                //Pido la lista de Escenarios
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    string lectura = null;
                    List<String> EscenariosID = new List<String>();
                    List<String> listaEsc = new List<String>();

                    //Obtengo los ID de escenarios
                    MySqlCommand cmd = new MySqlCommand(String.Format("SELECT EscenarioID FROM ListaCargas WHERE ProyectoID =  '{0}'", ProyectoID), sqlConnection);
                    sqlConnection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var aux = reader.GetString(0);
                        if (!EscenariosID.Contains(aux))
                            EscenariosID.Add(aux);
                    }
                    reader.Close();
                    sqlConnection.Close();

                    //Obtengo los nombres de los escenarios
                    sqlConnection.Open();
                    sda = new MySqlDataAdapter("SELECT NombreEscenario, EscenarioID FROM Escenarios ", sqlConnection);
                    sda.Fill(dt);
                    foreach (DataRow fila in dt.Rows)
                    {
                        foreach (String Id in EscenariosID)
                            if (fila["EscenarioID"].ToString() == Id)
                                if (!listaEsc.Contains(fila["NombreEscenario"].ToString()))
                                    listaEsc.Add(fila["NombreEscenario"].ToString());

                    }
                    reader.Close();
                    sqlConnection.Close();

                    listaEsc.Add(" -- Seleccionar Escenario -- ");
                    listaEsc.Sort();
                    cbEscenario.DataSource = listaEsc;
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
}

        private void cbEscenario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cbEscenario.Text != " -- Seleccionar Escenario -- ")
            {
                
                dgvCargas.Columns.Clear();
                dgvCargas.DataSource = null;

                try
                {
                    using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                    {


                        String Query = @"SELECT ListaCargas.*, Equipos.TAG, Equipos.Tipo, Equipos.PotenciaActiva, Equipos.Tension, Equipos.Cosfi, Equipos.CantidadFases, Equipos.TipoTension, Equipos.Descripcion
                                    FROM ListaCargas, Equipos 
                                    WHERE ListaCargas.EquipoID = Equipos.EquipoID
                                    AND ListaCargas.EscenarioID = '{0}';";

                        //Obtengo los ID del escenario elegido
                        MySqlCommand cmd = new MySqlCommand(String.Format("SELECT EscenarioID FROM Escenarios WHERE NombreEscenario =  '{0}'", cbEscenario.Text), sqlConnection);
                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            EscenarioID = reader.GetString(0);
                        }
                        reader.Close();
                        sqlConnection.Close();

                        sda = new MySqlDataAdapter(String.Format(Query, EscenarioID), sqlConnection);
                        dt = new DataTable();
                        sda.Fill(dt);

                        dgvCargas.DataSource = dt;

                        ConfigurarDataGridView();
                    }
                }
                catch (Exception exc) { MessageBox.Show(exc.Message); }

                foreach (DataGridViewRow Fila in dgvCargas.Rows)
                {
                    float Corriente = CalcularCorriente(Fila);

                    if (Corriente != 0)
                        dgvCargas.Rows[Fila.Index].Cells["Corriente"].Value = Corriente;
                    else
                        dgvCargas.Rows[Fila.Index].Cells["Corriente"].Value = null;

                }
            }
        }

        private int ConfigurarDataGridView()
        {
            //Creo las columnas Nuevas
            dgvCargas.Columns.Add("Modificada", "Modificada");
            dgvCargas.Columns.Add("Corriente", "Corriente");
            dgvCargas.Columns["Corriente"].DisplayIndex = 12;


            //Oculto columnas
            dgvCargas.Columns["CargaID"].Visible = false;
            dgvCargas.Columns["ProyectoID"].Visible = false;
            dgvCargas.Columns["EquipoID"].Visible = false;
            dgvCargas.Columns["EscenarioID"].Visible = false;
            dgvCargas.Columns["Modificada"].Visible = false;


            //Hago solo lectura
            dgvCargas.Columns["CargaID"].ReadOnly = true;
            dgvCargas.Columns["ProyectoID"].ReadOnly = true;
            dgvCargas.Columns["EquipoID"].ReadOnly = true;
            dgvCargas.Columns["EscenarioID"].ReadOnly = true;
            dgvCargas.Columns["TAG"].ReadOnly = true;
            dgvCargas.Columns["Tipo"].ReadOnly = true;
            dgvCargas.Columns["CantidadFases"].ReadOnly = true;
            dgvCargas.Columns["Tension"].ReadOnly = true;
            dgvCargas.Columns["Cosfi"].ReadOnly = true;
            dgvCargas.Columns["PotenciaActiva"].ReadOnly = true;

            return 0;
        }

        private float CalcularCorriente(DataGridViewRow fila)
        {
            int IT = fila.Cells["Tension"].ColumnIndex;             //Indice Tension
            int IP = fila.Cells["PotenciaActiva"].ColumnIndex;      //Indice Potencia
            int IC = fila.Cells["Cosfi"].ColumnIndex;               //Indice Coseno Fi
            int IF = fila.Cells["CantidadFases"].ColumnIndex;       //Indice Cantidad de Fases
            int II = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente Activa


            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (fila.Cells[IT].Value != DBNull.Value)
            {
                if (fila.Cells[IP].Value != DBNull.Value)
                {
                    if (fila.Cells[IC].Value != DBNull.Value)
                    {
                        if (fila.Cells[IF].Value != DBNull.Value)
                        {
                            float k;

                            if (Convert.ToInt32(fila.Cells[IF].Value) == 3)
                            {
                                k = (float)Math.Sqrt(3);
                            }
                            else
                            {
                                k = 1;
                            }

                            float Tension = (float)fila.Cells[IT].Value;
                            float Potencia = (float)fila.Cells[IP].Value;
                            float Cosfi = (float)fila.Cells[IC].Value;

                            float Corriente = Potencia * 1000 / (Tension * Cosfi * k);

                            return Corriente;
                        }
                        else return 0;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        }

    }
}



/*
            dgvCargas.Columns["CargaID"].Visible = false;
            dgvCargas.Columns["ProyectoID"].Visible = false;
            dgvCargas.Columns["EquipoID"].Visible = false;
            dgvCargas.Columns["EscenarioID"].Visible = false;
            dgvCargas.Columns["Modificada"].Visible = false;
            dgvCargas.Columns["EquipoID1"].Visible = false;
            dgvCargas.Columns["ProyectoID1"].Visible = false;
            dgvCargas.Columns["Rev1"].Visible = false;
            dgvCargas.Columns["CosfiArranque"].Visible = false;
            dgvCargas.Columns["CorrienteArranque"].Visible = false;
            dgvCargas.Columns["Rendimiento"].Visible = false;
            dgvCargas.Columns["Ubicacion"].Visible = false;
            dgvCargas.Columns["PID"].Visible = false;
            dgvCargas.Columns["UltimaModific1"].Visible = false;
            
            dgvCargas.Columns["CargaID"].ReadOnly = true;
            dgvCargas.Columns["ProyectoID"].ReadOnly = true;
            dgvCargas.Columns["EquipoID"].ReadOnly = true;
            dgvCargas.Columns["EscenarioID"].ReadOnly = true;
            dgvCargas.Columns["TAG"].ReadOnly = true;
            dgvCargas.Columns["Tipo"].ReadOnly = true;
            dgvCargas.Columns["CantidadFases"].ReadOnly = true;
            dgvCargas.Columns["Tension"].ReadOnly = true;
            dgvCargas.Columns["Cosfi"].ReadOnly = true;
            dgvCargas.Columns["PotenciaActiva"].ReadOnly = true;

            /*
            dgvCargas.Columns["EquipoID"].ReadOnly = true;
            dgvCargas.Columns["ProyectoID"].ReadOnly = true;

            dgvCargas.Columns["Corriente"].DisplayIndex = 9;

            dgvCargas.Columns["Corriente"].DefaultCellStyle.Format = "N3";
            dgvCargas.Columns["PotenciaActiva"].DefaultCellStyle.Format = "N3";

            var nuevaCol = new DataGridViewComboBoxColumn();
            List<String> Tipos = new List<String>();
            Tipos.AddRange(new List<String> { "Generador", "Transformador", "Tablero", "Caja", "Motor", "Otro" });

            nuevaCol.HeaderText = "Tipo Carga";
            nuevaCol.Name = "TipoCB";
            nuevaCol.DataSource = Tipos;
            dgvCargas.Columns.Add(nuevaCol);

            foreach (DataGridViewRow fila in dgvCargas.Rows)
            {
                if (fila.Cells["Tipo"].Value != null)
                    fila.Cells["TipoCB"].Value = fila.Cells["Tipo"].Value;
            }

            dgvCargas.Columns["TipoCB"].DisplayIndex = dgvCargas.Columns["Tipo"].DisplayIndex;
            dgvCargas.Columns["Tipo"].Name = "Aux";
            dgvCargas.Columns["Aux"].Visible = true;
            //dgvEquipos.Columns["Aux"].DisplayIndex = 16;
            //dgvEquipos.Columns.Remove(dgvEquipos.Columns["Tipo"]);
            dgvCargas.Columns["TipoCB"].Name = "Tipo";

            */
