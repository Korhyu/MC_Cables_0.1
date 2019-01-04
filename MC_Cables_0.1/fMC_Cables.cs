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
    public partial class fMC_Cables : Form
    {
        String strConnection = "Server=ING114;Port=3306;Database=dbthor;Uid=usuario;Pwd=usuario123;";
        String NombreProyecto;
        String NombreUsuario;
        String ProyectoID;

        MySqlDataAdapter sda;
        DataTable dt = new DataTable();

        public fMC_Cables()
        {
            InitializeComponent();
        }

        public fMC_Cables(String Usuario, String Proyecto)
        {
            InitializeComponent();

            try
            {
                dgvMC.DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
                if (String.IsNullOrEmpty(Proyecto))
                    Proyecto = "No hay proyecto";
                lbUsuario.Text = "Usuario: " + Usuario.ToString();
                lbProyecto.Text = "Proyecto: " + Proyecto.ToString();

                NombreUsuario = Usuario.ToString();

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

            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            CargarCables();

        }

        private void CargarCables()
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    //Obtengo los nombres de los escenarios
                    sqlConnection.Open();
                    String Query = "SELECT * FROM Cables INNER JOIN Equipos ON Cables.DestinoID = Equipos.EquipoID INNER JOIN DatosCables ON Cables.FormacionID = DatosCables.FormacionID WHERE Cables.ProyectoID = " + ProyectoID + ";";
                    sda = new MySqlDataAdapter(Query, sqlConnection);
                    sda.Fill(dt);
                    sqlConnection.Close();

                    //Pido el TAG de origen y los datos de la formacion
                    dt.Columns.Add("Origen");
                    foreach (DataRow Fila in dt.Rows)
                    {
                        Query = "SELECT * FROM Equipos WHERE EquipoID = " + Fila["OrigenID"].ToString() + ";";
                        MySqlCommand sqlCmd = new MySqlCommand(Query, sqlConnection);
                        sqlConnection.Open();
                        MySqlDataReader sqlReader = sqlCmd.ExecuteReader();
                        sqlReader.Read();
                        Fila["Origen"] = sqlReader["TAG"].ToString();

                        sqlReader.Close();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            dgvMC.DataSource = dt;
            ConfigurarDataGridView();
        }


        private int ConfigurarDataGridView()
        {
            //Creo las columnas Nuevas
            dgvMC.Columns.Add("Modificada", "Modificada");
            dgvMC.Columns.Add("Corriente", "Corriente");
            dgvMC.Columns["Corriente"].DisplayIndex = 12;
            dgvMC.Columns["Rev"].DisplayIndex = 1;
            dgvMC.Columns["TAG"].DisplayIndex = 1;

            //Oculto columnas
            dgvMC.Columns["CableID"].Visible = false;
            dgvMC.Columns["ProyectoID"].Visible = false;
            dgvMC.Columns["EquipoID"].Visible = false;
            dgvMC.Columns["OrigenID"].Visible = false;
            dgvMC.Columns["DestinoID"].Visible = false;
            dgvMC.Columns["FormacionID"].Visible = false;
            dgvMC.Columns["Modificada"].Visible = false;
            dgvMC.Columns["Tipo"].Visible = false;

            //Cambio Headers
            dgvMC.Columns["Corriente"].HeaderText = "I [A]";
            dgvMC.Columns["Tension"].HeaderText = "V [V]";
            dgvMC.Columns["CantidadFases"].HeaderText = "Fases";
            dgvMC.Columns["TipoTension"].HeaderText = "AC/DC";
            //dgvMC.Columns["Origen"].HeaderText = "Origen";
            dgvMC.Columns["TAG1"].HeaderText = "Destino";

            //Hago solo lectura
            //dgvMC.Columns["CargaID"].ReadOnly = true;
            dgvMC.Columns["ProyectoID"].ReadOnly = true;
            dgvMC.Columns["EquipoID"].ReadOnly = true;
            dgvMC.Columns["TAG"].ReadOnly = true;
            dgvMC.Columns["Tipo"].ReadOnly = true;
            dgvMC.Columns["CantidadFases"].ReadOnly = true;
            dgvMC.Columns["Tension"].ReadOnly = true;
            dgvMC.Columns["Cosfi"].ReadOnly = true;
            dgvMC.Columns["PotenciaActiva"].ReadOnly = true;
            dgvMC.Columns["Corriente"].ReadOnly = true;
            dgvMC.Columns["OrigenID"].ReadOnly = true;
            dgvMC.Columns["DestinoID"].ReadOnly = true;

            //Decimales
            dgvMC.Columns["Corriente"].DefaultCellStyle.Format = "N3";
            dgvMC.Columns["PotenciaActiva"].DefaultCellStyle.Format = "N3";
            
            
            //Ancho de Columnas
            //dgvMC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMC.AutoResizeColumns();
            dgvMC.AllowUserToResizeColumns = true;
            dgvMC.AllowUserToOrderColumns = true;

            /*
            //Columnas con Combo Box
            var nuevaCol = new DataGridViewComboBoxColumn();
            List<String> Tipos = new List<String>();
            Tipos.AddRange(new List<String> { "Continua", "Intermitente", "Reserva", "Stand By", "Otro" });
            
            nuevaCol.HeaderText = "Tipo Operacion";
            nuevaCol.Name = "Aux";
            nuevaCol.DataSource = Tipos;
            dgvMC.Columns.Add(nuevaCol);
            

            foreach (DataGridViewRow fila in dgvMC.Rows)
            {
                if (fila.Cells["TipoOperacion"].Value != null)
                    fila.Cells["Aux"].Value = fila.Cells["TipoOperacion"].Value;
            }


            dgvMC.Columns["Aux"].DisplayIndex = dgvMC.Columns["TipoOperacion"].DisplayIndex;
            dgvMC.Columns["TipoOperacion"].Name = "Eliminar";
            dgvMC.Columns["Aux"].Name = "TipoOperacion";
            */
            
            return 0;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
