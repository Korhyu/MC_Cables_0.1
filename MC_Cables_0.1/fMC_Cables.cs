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

        bool IndicarCambios = false;

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

        public void CargarCables()
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    //Obtengo los datos de los cables y destino
                    sqlConnection.Open();
                    String Query = "SELECT * FROM Cables INNER JOIN Equipos ON Cables.DestinoID = Equipos.EquipoID INNER JOIN DatosCables ON Cables.FormacionID = DatosCables.FormacionID WHERE Cables.ProyectoID = " + ProyectoID + ";";
                    sda = new MySqlDataAdapter(Query, sqlConnection);
                    sda.Fill(dt);
                    sqlConnection.Close();
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            //Elaboro y cargo la formacion del cable
            dt.Columns.Add("Formacion");
            foreach (DataRow Fila in dt.Rows)
            {
                Fila["Formacion"] = ArmadoFormacion(Fila);
            }
            
            //Cargo los datos en el dgv
            dgvMC.DataSource = dt;

        }

        private int ConfigurarDataGridView()
        {
            //Creo las columnas Nuevas
            dgvMC.Columns.Add("Modificada", "Modificada");
            dgvMC.Columns.Add("Corriente", "Corriente");
            dgvMC.Columns.Add("DVNormal", "DV Normal [%]");
            dgvMC.Columns.Add("DVArranque", "DV Arranque [%]");

            //Oculto columnas
            dgvMC.Columns["CableID"].Visible = false;
            dgvMC.Columns["ProyectoID"].Visible = false;
            dgvMC.Columns["EquipoID"].Visible = false;
            dgvMC.Columns["OrigenID"].Visible = false;
            dgvMC.Columns["TAG"].Visible = false;
            dgvMC.Columns["DestinoID"].Visible = false;
            dgvMC.Columns["FormacionID"].Visible = false;
            dgvMC.Columns["FormacionID1"].Visible = false;
            dgvMC.Columns["Modificada"].Visible = false;
            dgvMC.Columns["Tipo"].Visible = false;

            //Elimino columnas repetidas
            dgvMC.Columns.Remove("ProyectoID1");
            dgvMC.Columns.Remove("UltimaModific");

            //Cambio Headers
            dgvMC.Columns["Corriente"].HeaderText = "I [A]";
            dgvMC.Columns["Tension"].HeaderText = "V [V]";
            dgvMC.Columns["CantidadFases"].HeaderText = "Fases";
            dgvMC.Columns["TipoTension"].HeaderText = "AC/DC";
            dgvMC.Columns["TAG"].HeaderText = "Destino";
            dgvMC.Columns["UltimaModific1"].HeaderText = "Destino";


            //Ordeno las columnas
            int i = 0;
            dgvMC.Columns["Rev"].DisplayIndex = i++;
            dgvMC.Columns["NCable"].DisplayIndex = i++;
            dgvMC.Columns["OrigenCB"].DisplayIndex = i++;
            dgvMC.Columns["DestinoCB"].DisplayIndex = i++;
            dgvMC.Columns["PotenciaActiva"].DisplayIndex = i++;
            dgvMC.Columns["Corriente"].DisplayIndex = i++;
            dgvMC.Columns["Tension"].DisplayIndex = i++;
            dgvMC.Columns["Cosfi"].DisplayIndex = i++;
            dgvMC.Columns["CantidadFases"].DisplayIndex = i++;
            dgvMC.Columns["Longitud"].DisplayIndex = i++;
            dgvMC.Columns["TipoCanalizacion"].DisplayIndex = i++;
            dgvMC.Columns["CablesXFase"].DisplayIndex = i++;
            dgvMC.Columns["CantidadPolos"].DisplayIndex = i++;
            dgvMC.Columns["Seccion"].DisplayIndex = i++;
            dgvMC.Columns["CorrienteAdm"].DisplayIndex = i++;
            dgvMC.Columns["Resistencia"].DisplayIndex = i++;
            dgvMC.Columns["Reactancia"].DisplayIndex = i++;
            dgvMC.Columns["TipoTension"].DisplayIndex = i++;
            dgvMC.Columns["Formacion"].DisplayIndex = i++;
            dgvMC.Columns["DVNP"].DisplayIndex = i++;
            dgvMC.Columns["DVNormal"].DisplayIndex = i++;
            dgvMC.Columns["CosfiArranque"].DisplayIndex = i++;
            dgvMC.Columns["CorrienteArranque"].DisplayIndex = i++;
            dgvMC.Columns["DVAP"].DisplayIndex = i++;
            dgvMC.Columns["DVArranque"].DisplayIndex = i++;



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
            dgvMC.Columns["DVNormal"].DefaultCellStyle.Format = "N3";
            dgvMC.Columns["DVArranque"].DefaultCellStyle.Format = "N3";


            //Ancho de Columnas
            //dgvMC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMC.AutoResizeColumns();
            dgvMC.AllowUserToResizeColumns = true;
            dgvMC.AllowUserToOrderColumns = true;
            

            /*
            dgvMC.Columns["Aux"].DisplayIndex = dgvMC.Columns["TipoOperacion"].DisplayIndex;
            dgvMC.Columns["TipoOperacion"].Name = "Eliminar";
            dgvMC.Columns["Aux"].Name = "TipoOperacion";
            */
            
            return 0;
            
        }

        private void ComboBoxOrigenDestino()
        {
            //Columnas con Combo Box
            var Origen = new DataGridViewComboBoxColumn();
            var Destino = new DataGridViewComboBoxColumn();
            List<String> Origenes = new List<String>();
            List<String> Destinos = new List<String>();

            Origen.HeaderText = "TAG Origen";
            Origen.Name = "OrigenCB";
            Destino.HeaderText = "TAG Destino";
            Destino.Name = "DestinoCB";

            //Cargo los Origenes en la lista
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    //Lista de Origenes
                    String Query = "SELECT EquipoID,TAG FROM Equipos WHERE ProyectoID =" + ProyectoID + ";";
                    MySqlCommand sqlCmd = new MySqlCommand(Query, sqlConnection);
                    sqlConnection.Open();
                    MySqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                        Origenes.Add(sqlReader["TAG"].ToString());
                    sqlReader.Close();
                    Origenes.Sort();
                    Origen.DataSource = Origenes;

                    //Lista de Destinos
                    Query = "SELECT EquipoID,TAG FROM Equipos WHERE ProyectoID =" + ProyectoID + ";";
                    sqlCmd = new MySqlCommand(Query, sqlConnection);
                    sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                        Destinos.Add(sqlReader["TAG"].ToString());
                    sqlReader.Close();
                    Destinos.Sort();
                    Destino.DataSource = Destinos;

                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
            
            dgvMC.Columns.Add(Origen);
            dgvMC.Columns.Add(Destino);

            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    //Obtengo el TAG de origen
                    int i = 0;
                    foreach (DataGridViewRow Fila in dgvMC.Rows)
                    {
                        if ( dgvMC.Rows.Count > i+1 )
                        {
                            String Query = "SELECT EquipoID,TAG FROM Equipos WHERE EquipoID = " + Fila.Cells["OrigenID"].Value.ToString() + ";";
                            MySqlCommand sqlCmd = new MySqlCommand(Query, sqlConnection);
                            sqlConnection.Open();
                            MySqlDataReader sqlReader = sqlCmd.ExecuteReader();
                            sqlReader.Read();
                            Fila.Cells["OrigenCB"].Value = sqlReader["TAG"].ToString();
                            Fila.Cells["OrigenID"].Value = sqlReader["EquipoID"].ToString();

                            sqlReader.Close();
                            sqlConnection.Close();

                            Query = "SELECT EquipoID,TAG FROM Equipos WHERE EquipoID = " + Fila.Cells["DestinoID"].Value.ToString() + ";";
                            sqlCmd = new MySqlCommand(Query, sqlConnection);
                            sqlConnection.Open();
                            sqlReader = sqlCmd.ExecuteReader();
                            sqlReader.Read();
                            Fila.Cells["DestinoCB"].Value = sqlReader["TAG"].ToString();
                            Fila.Cells["DestinoID"].Value = sqlReader["EquipoID"].ToString();

                            sqlReader.Close();
                            sqlConnection.Close();
                        }
                        i++;
                    }
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

            /*
            foreach (DataGridViewRow fila in dgvMC.Rows)
            {
                if (fila.Cells["TipoOperacion"].Value != null)
                    fila.Cells["Aux"].Value = fila.Cells["TipoOperacion"].Value;
            }
            */
        }

        private float CalcularCorriente(DataGridViewRow fila)
        {
            int IT = fila.Cells["Tension"].ColumnIndex;             //Indice Tension
            int IP = fila.Cells["PotenciaActiva"].ColumnIndex;      //Indice Potencia
            int IC = fila.Cells["Cosfi"].ColumnIndex;               //Indice Coseno Fi
            int IF = fila.Cells["CantidadFases"].ColumnIndex;       //Indice Cantidad de Fases
            int II = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente Activa
            int IR = fila.Cells["Rendimiento"].ColumnIndex;         //Indice Rendimiento


            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (fila.Cells[IT].Value != null)
            {
                if (fila.Cells[IP].Value != null)
                {
                    if (fila.Cells[IC].Value != null)
                    {
                        if (fila.Cells[IF].Value != null)
                        {
                            if (fila.Cells[IR].Value != null)
                            {
                                float k;

                                if (Convert.ToInt32(fila.Cells[IF].Value) == 3)
                                    k = (float)Math.Sqrt(3);
                                else
                                    k = 1;

                                float Potencia = (float)fila.Cells[IP].Value;
                                float Cosfi = (float)fila.Cells[IC].Value;
                                float Tension = (float)fila.Cells[IT].Value;
                                float Rendimiento = (float)fila.Cells[IR].Value;

                                float Corriente = Potencia * 1000 / (Tension * Cosfi * k * Rendimiento / 100);

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
            else return 0;
        }

        private float CalcularCorrienteArranque(DataGridViewRow fila)
        {
            int IC = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente
            int IA = fila.Cells["CorrienteArranque"].ColumnIndex;   //Indice Corriente Arranque

            float Corriente = ValorDesdeIndice(fila, fila.Cells["Corriente"].ColumnIndex);
            float Indice = ValorDesdeIndice(fila, fila.Cells["CorrienteArranque"].ColumnIndex);

            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (Corriente != 0 && Indice != 0)
            {
                return Corriente = (float)fila.Cells[IC].Value * (float)fila.Cells[IA].Value;
            }
            return 0;
        }

        private String ArmadoFormacion (DataRow Fila)
        {
            String Formacion;

            Formacion = Fila["CablesXFase"].ToString() + "x";


            /*
            switch (Fila["CablesXFase"])
            {
                case 1:
                    Formacion = "1x";
                    break;
                case 2:
                    Formacion = "1x";
                    break;
                case 3:
                    Formacion = "1x";
                    break;
                case 4:
                    Formacion = "1x";
                    break;
                case 5:
                    Formacion = "1x";
                    break;
            }
            */

            return Formacion;
        }

        private void recalcularCorrientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcularCorrientes();
        }

        private void CalcularCorrientes()
        {
            foreach (DataGridViewRow Fila in dgvMC.Rows)
            {
                var Corriente = CalcularCorriente(Fila);
                if (Corriente != 0)
                {
                    Fila.Cells["Corriente"].Value = CalcularCorriente(Fila);
                }
            }
        }

        private void CalcularCaidas()
        {
            int i = 0;
            foreach (DataGridViewRow Fila in dgvMC.Rows)
            {
                if (dgvMC.Rows.Count > i + 1)
                {
                    float Caida = CalcularCaida(Fila);
                    if (Caida != 0)
                    {
                        Fila.Cells["DVNormal"].Value = Caida;
                    }

                    Caida = CalcularCaidaArranque(Fila);
                    if (Caida != 0)
                    {
                        Fila.Cells["DVArranque"].Value = Caida;
                    }

                    ColorearCaidas(Fila);
                }
                i++;
            }
        }

        private float ValorDesdeIndice(DataGridViewRow fila, int i)
        {
            float Valor;

            if (!String.IsNullOrEmpty(fila.Cells[i].Value.ToString()))
                Valor = (float)fila.Cells[i].Value;
            else
                Valor = 0;

            return Valor;
        }

        private float CalcularCaida(DataGridViewRow fila)
        {
            int IC = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente
            int IL = fila.Cells["Longitud"].ColumnIndex;            //Indice Longitud
            int IR = fila.Cells["Resistencia"].ColumnIndex;         //Indice Resistencia
            int IX = fila.Cells["Reactancia"].ColumnIndex;          //Indice Reactancia
            int IF = fila.Cells["CantidadFases"].ColumnIndex;       //Indice Cantidad de Fases
            int IV = fila.Cells["Tension"].ColumnIndex;             //Indice Tension
            int ICf = fila.Cells["Cosfi"].ColumnIndex;              //Indice Coseno Fi

            float Corriente = ValorDesdeIndice(fila, IC);
            float Longitud = ValorDesdeIndice(fila, IL);
            float Resistencia = ValorDesdeIndice(fila, IR);
            float Reactancia = ValorDesdeIndice(fila, IX);
            float Cosfi = ValorDesdeIndice(fila, ICf);
            float Tension = ValorDesdeIndice(fila, IV);
            float SenFi = (float)Math.Sqrt(1 - (Cosfi * Cosfi));
            int CantFases = Convert.ToInt32(fila.Cells[IF].Value);


            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (Corriente != 0 && Longitud != 0 && Tension != 0)
            {
                if (Resistencia != 0 && Reactancia != 0)
                {
                    if (CantFases != 0 && Cosfi != 0)
                    {
                        float k, DV;

                        //Consigo el k de la carga
                        if (CantFases == 3)
                            k = (float)Math.Sqrt(3);
                        else
                            k = 1;


                        DV = k * Corriente * (Longitud/1000) * (Resistencia * Cosfi + Reactancia * SenFi) / Tension;

                        return DV * 100;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;

        }

        private float CalcularCaidaArranque(DataGridViewRow fila)
        {
            int IC = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente
            int IL = fila.Cells["Longitud"].ColumnIndex;            //Indice Longitud
            int IR = fila.Cells["Resistencia"].ColumnIndex;         //Indice Resistencia
            int IX = fila.Cells["Reactancia"].ColumnIndex;          //Indice Reactancia
            int IF = fila.Cells["CantidadFases"].ColumnIndex;       //Indice Cantidad de Fases
            int IV = fila.Cells["Tension"].ColumnIndex;             //Indice Tension
            int ICf = fila.Cells["CosfiArranque"].ColumnIndex;      //Indice Coseno Fi

            float Corriente = CalcularCorrienteArranque(fila);
            float Longitud = ValorDesdeIndice(fila, IL);
            float Resistencia = ValorDesdeIndice(fila, IR);
            float Reactancia = ValorDesdeIndice(fila, IX);
            float Cosfi = ValorDesdeIndice(fila, ICf);
            float Tension = ValorDesdeIndice(fila, IV);
            float SenFi = (float)Math.Sqrt(1 - (Cosfi * Cosfi));
            int CantFases = Convert.ToInt32(fila.Cells[IF].Value);


            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (Corriente != 0 && Longitud != 0 && Tension != 0)
            {
                if (Resistencia != 0 && Reactancia != 0)
                {
                    if (CantFases != 0 && Cosfi != 0)
                    {
                        float k, DV;

                        //Consigo el k de la carga
                        if (CantFases == 3)
                            k = (float)Math.Sqrt(3);
                        else
                            k = 1;

                        DV = k * Corriente * (Longitud / 1000) * (Resistencia * Cosfi + Reactancia * SenFi) / Tension;

                        return DV * 100;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        }

        private void ColorearCaidas(DataGridViewRow Fila)
        {
            if (!String.IsNullOrEmpty(Fila.Cells["DVNormal"].Value.ToString()))
            {
                if ((float)Fila.Cells["DVNormal"].Value <= (float)Fila.Cells["DVNP"].Value)
                {
                    dgvMC.Rows[Fila.Index].Cells["DVNormal"].Style.BackColor = Color.Green;
                }
                else
                {
                    dgvMC.Rows[Fila.Index].Cells["DVNormal"].Style.BackColor = Color.Red;
                }
            }

            /*
            if (!String.IsNullOrEmpty(Fila.Cells["DVArranque"].Value.ToString()))
            {
                if ((float)Fila.Cells["DVArranque"].Value <= (float)Fila.Cells["DVAP"].Value)
                {
                    dgvMC.Rows[Fila.Index].Cells["DVArranque"].Style.BackColor = Color.Green;
                }
                else
                {
                    dgvMC.Rows[Fila.Index].Cells["DVArranque"].Style.BackColor = Color.Red;
                }
            }
            */
        }

        private void comboBoxOrigenDestinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComboBoxOrigenDestino();
        }

        private void fMC_Cables_Shown(object sender, EventArgs e)
        {
            ComboBoxOrigenDestino();
            ConfigurarDataGridView();
            CalcularCorrientes();
            CalcularCaidas();

            IndicarCambios = true;
        }

        private void RecalcularFila(DataGridViewRow Fila)
        {
            Fila.Cells["Corriente"].Value = CalcularCorriente(Fila);
            Fila.Cells["DVNormal"].Value = CalcularCaida(Fila);
            Fila.Cells["DVArranque"].Value = CalcularCaidaArranque(Fila);

            ColorearCaidas(Fila);
        }

        private void eliminarCableToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dgvMC_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (IndicarCambios == true)
            {
                DataGridViewRow Fila = dgvMC.Rows[e.RowIndex];
                RecalcularFila(Fila);
            }
        }
            
    }
}
