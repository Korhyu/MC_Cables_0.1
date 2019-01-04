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
    public partial class fEquipos : Form
    {
        String strConnection = "Server=ING114;Port=3306;Database=dbthor;Uid=usuario;Pwd=usuario123;";
        String NombreProyecto;
        String ProyectoID;

        MySqlDataAdapter sda;
        DataTable dt;

        bool CambioCelda = false;
        bool IndicarCambios = false;
        bool ModificandoCelda = false;

        public fEquipos()
        {
            InitializeComponent();

            lbUsuario.Text = "Usuario: USUARIO NO SELECCIONADO";
            lbProyecto.Text = "Proyecto: PROYECTO NO SELECCIONADO";
        }

        public fEquipos(String Usuario, String Proyecto)
        {
            InitializeComponent();
            dgvEquipos.DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
            if (String.IsNullOrEmpty(Proyecto))
                Proyecto = "No hay proyecto";
            lbUsuario.Text = "Usuario: " + Usuario.ToString();
            lbProyecto.Text = "Proyecto: " + Proyecto.ToString();

            NombreProyecto = Proyecto.ToString();
        }

        private void dgvEquipos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.Value != null)
                if (float.TryParse(e.Value.ToString(), out float aux))
                    e.Value = aux;
        }

        private void fEquipos_Load(object sender, EventArgs e)
        {
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

                RecargarLista();

                DataGridViewComboBoxColumn dgcb = new DataGridViewComboBoxColumn();
            }

            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private int ConfigurarDataGridView()
        {
            dgvEquipos.Columns.Add("Modificada", "Modificada");
            dgvEquipos.Columns.Add("Corriente", "Corriente");

            dgvEquipos.Columns["EquipoID"].Visible = false;
            dgvEquipos.Columns["Modificada"].Visible = false;
            dgvEquipos.Columns["ProyectoID"].Visible = false;

            dgvEquipos.Columns["EquipoID"].ReadOnly = true;
            dgvEquipos.Columns["ProyectoID"].ReadOnly = true;

            dgvEquipos.Columns["Corriente"].DisplayIndex = 9;

            dgvEquipos.Columns["Corriente"].DefaultCellStyle.Format = "N3";
            dgvEquipos.Columns["PotenciaActiva"].DefaultCellStyle.Format = "N3";

            var nuevaCol = new DataGridViewComboBoxColumn();
            List<String> Tipos = new List<String>();
            Tipos.AddRange(new List<String> { "Generador", "Transformador", "Tablero", "Caja", "Motor", "Otro" });

            nuevaCol.HeaderText = "Tipo Carga";
            nuevaCol.Name = "TipoCB";
            nuevaCol.DataSource = Tipos;
            dgvEquipos.Columns.Add(nuevaCol);

            foreach (DataGridViewRow fila in dgvEquipos.Rows)
            {
                if (fila.Cells["Tipo"].Value != null)
                    fila.Cells["TipoCB"].Value = fila.Cells["Tipo"].Value;
            }

            dgvEquipos.Columns["TipoCB"].DisplayIndex = dgvEquipos.Columns["Tipo"].DisplayIndex;
            dgvEquipos.Columns["Tipo"].Name = "Aux";
            dgvEquipos.Columns["Aux"].Visible = false;
            //dgvEquipos.Columns["Aux"].DisplayIndex = 16;
            //dgvEquipos.Columns.Remove(dgvEquipos.Columns["Tipo"]);
            dgvEquipos.Columns["TipoCB"].Name = "Tipo";

            return 0;
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
            if (fila.Cells[IT].Value != DBNull.Value)
            {
                if (fila.Cells[IP].Value != DBNull.Value)
                {
                    if (fila.Cells[IC].Value != DBNull.Value)
                    {
                        if (fila.Cells[IF].Value != DBNull.Value)
                        {
                            float k;
                            float Rendimiento;

                            //Consulto si existe el rendimiento de la carga
                            if (fila.Cells[IR].Value != DBNull.Value)
                                Rendimiento = (float)fila.Cells[IR].Value / 100;
                            else
                                Rendimiento = 1;
                            
                            //Consigo el k de la carga
                            if (Convert.ToInt32(fila.Cells[IF].Value) == 3)
                                k = (float)Math.Sqrt(3);
                            else
                                k = 1;


                            float Tension = (float)fila.Cells[IT].Value;
                            float Potencia = (float)fila.Cells[IP].Value;
                            float Cosfi = (float)fila.Cells[IC].Value;

                            float Corriente = Potencia * 1000 / (Tension * Cosfi * k * Rendimiento);

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

        private float CalcularPotencia(DataGridViewRow fila)
        {
            int IT = fila.Cells["Tension"].ColumnIndex;             //Indice Tension
            int IP = fila.Cells["PotenciaActiva"].ColumnIndex;      //Indice Potencia
            int IC = fila.Cells["Cosfi"].ColumnIndex;               //Indice Coseno Fi
            int IF = fila.Cells["CantidadFases"].ColumnIndex;       //Indice Cantidad de Fases
            int II = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente Activa
            int IR = fila.Cells["Rendimiento"].ColumnIndex;         //Indice Rendimiento

            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (fila.Cells[IT].Value != DBNull.Value)
            {
                if (fila.Cells[IC].Value != DBNull.Value)
                {
                    if (fila.Cells[II].Value != DBNull.Value)
                    {
                        if (fila.Cells[IF].Value != DBNull.Value)
                        {
                            float k;
                            float Rendimiento;
                            float Corriente;

                            //Consulto si existe el rendimiento de la carga
                            if (fila.Cells[IR].Value != DBNull.Value)
                                Rendimiento = (float)fila.Cells[IR].Value / 100;
                            else
                                Rendimiento = 1;

                            //Consigo el k de la carga
                            if (Convert.ToInt32(fila.Cells[IF].Value) == 3)
                                k = (float)Math.Sqrt(3);
                            else
                                k = 1;

                            float Tension = (float)fila.Cells[IT].Value;
                            float.TryParse(fila.Cells[II].Value.ToString(),out Corriente);
                            float Cosfi = (float)fila.Cells[IC].Value;

                            float Potencia = Corriente * Tension * Cosfi * k * Rendimiento / 1000;

                            return Potencia;
                        }
                        else return 0;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        }

        private void SincronizarDB()
        {
            String aux;

            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("", sqlConnection);
                    sqlConnection.Open();

                    foreach (DataGridViewRow fila in dgvEquipos.Rows)
                    {
                        if (fila.Cells["Modificada"].Value != null)
                        {
                            aux = ArmadoQueryEquipo(fila);
                            if (!String.IsNullOrEmpty(aux))
                            {
                                sqlCmd.CommandText = aux;
                                sqlCmd.ExecuteNonQuery();
                            }
                        }
                        //MessageBox.Show(fila.Cells["TAG"].Value.ToString());
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private void EnviarQuery(String Query)
        {
            using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
            {
                MySqlCommand sqlCmd = new MySqlCommand(Query, sqlConnection);
                sqlConnection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        private void btReload_Click(object sender, EventArgs e)
        {
            RecargarLista();
        }

        private void recargarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecargarLista();
        }

        private void RecargarLista()
        {
            //Consigo los datos de la tabla
            using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
            {
                sda = new MySqlDataAdapter(String.Format("SELECT e.* FROM Proyectos p JOIN Equipos e ON p.ProyectoID = e.ProyectoID WHERE p.NombreProyecto =  '{0}'", NombreProyecto), sqlConnection);
                dt = new DataTable();
                sda.Fill(dt);

                dgvEquipos.DataSource = dt;

                //Desabilito la vision de cambios en la tabla
                IndicarCambios = false;

                //Configuracion del DGV
                ConfigurarDataGridView();

                //Si estan todos los datos, calculo la corriente
                foreach (DataGridViewRow fila in dgvEquipos.Rows)
                {
                    if (fila.Cells["EquipoID"].Value != null)
                    {
                        var FilaI = fila.Index;
                        var ColumnaI = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Corriente"]);

                        float Corriente = CalcularCorriente(fila);

                        if (Corriente != 0)
                            dgvEquipos.Rows[FilaI].Cells[ColumnaI].Value = Corriente;
                        else
                            dgvEquipos.Rows[FilaI].Cells[ColumnaI].Value = null;
                    }
                }

                //Habilito la vision de cambios en la tabla
                IndicarCambios = true;
            }
        }

        private void dgvEquipos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ModificandoCelda == false)
            {
                ModificandoCelda = true;

                if (IndicarCambios == true)
                {
                    int IndiceF = e.RowIndex;
                    int IndiceC = e.ColumnIndex;
                    int IndiceModif = dgvEquipos.Columns["Modificada"].Index;

                    CambioCelda = true;
                    dgvEquipos.Rows[IndiceF].Cells[IndiceC].Style.BackColor = Color.Yellow;
                    dgvEquipos.Rows[IndiceF].Cells[IndiceModif].Value = "Modificada";

                    //Calculo los indices a las diferentes columnas
                    int IT = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Tension"]);             //Indice Tension
                    int IP = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["PotenciaActiva"]);      //Indice Potencia
                    int IC = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Cosfi"]);               //Indice Coseno Fi
                    int IF = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["CantidadFases"]);       //Indice Cantidad de Fases
                    int II = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Corriente"]);           //Indice Corriente Activa
                    int IY = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Tipo"]);                //Indice Tipo
                    int IA = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Aux"]);                 //Indice Auxiliar
                    int IR = dgvEquipos.Columns.IndexOf(dgvEquipos.Columns["Rendimiento"]);         //Indice Rendimiento

                    //Si se modifico la tension, la potencia o el coseno fi, recalculo la corriente
                    if (IndiceC == IT || IndiceC == IP || IndiceC == IC || IndiceC == IF || IndiceC == IR)
                    {
                        var Corriente = CalcularCorriente(dgvEquipos.Rows[IndiceF]);
                        dgvEquipos.Rows[IndiceF].Cells[II].Style.BackColor = Color.Yellow;
                        dgvEquipos.Rows[IndiceF].Cells[II].Value = Corriente;
                    }

                    //Si se modifico la Corriente, recalculo la potencia
                    if (IndiceC == II)
                    {
                        var Potencia = CalcularPotencia(dgvEquipos.Rows[IndiceF]);
                        dgvEquipos.Rows[IndiceF].Cells[IP].Style.BackColor = Color.Yellow;
                        dgvEquipos.Rows[IndiceF].Cells[IP].Value = Potencia;
                    }

                    //Si se modifico el tipo, actualizo la columna Aux
                    if (IndiceC == IY)
                    {
                        dgvEquipos.Rows[IndiceF].Cells[IA].Value = dgvEquipos.Rows[IndiceF].Cells[IY].Value;
                    }
                }
                ModificandoCelda = false;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DialogResult Res = new DialogResult();
            Res = MessageBox.Show("Va sincronizar los cambios, reemplazando lo existente. ¿Esta Seguro?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Res == DialogResult.Yes && CambioCelda == true)
            {
                SincronizarDB();
                CambioCelda = false;

                foreach (DataGridViewRow fila in dgvEquipos.Rows)
                {
                    //Insertar funcion para volver al color original la fila
                }
            }
        }

        private void fEquipos_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (CambioCelda == true)
            {
                DialogResult Res = new DialogResult();
                Res = MessageBox.Show("Hay cambios sin guardar. ¿Desea guardarlos?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Res == DialogResult.Yes)
                {
                    SincronizarDB();
                }
            }
            CambioCelda = false;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (CambioCelda == true)
            {
                DialogResult Res = new DialogResult();
                Res = MessageBox.Show("Hay cambios sin guardar. ¿Desea guardarlos?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Res == DialogResult.Yes)
                {
                    SincronizarDB();
                }
            }
            CambioCelda = false;
            //Close();
        }

        public String ArmadoQueryEquipo(DataGridViewRow fila)
        {
            String Mensaje = "";

            //Verifico si es una fila nueva o existente
            if (fila.Cells["EquipoID"].Value.ToString() == "")
            {
                NuevaLinea(fila);
                Mensaje = null;
            }
            else
            {
                Mensaje = "UPDATE equipos SET ";

                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (dgvEquipos.Columns[celda.ColumnIndex].Name != "EquipoID" && dgvEquipos.Columns[celda.ColumnIndex].Name != "ProyectoID")
                    {
                        if (dgvEquipos.Columns[celda.ColumnIndex].Name != "Corriente" && dgvEquipos.Columns[celda.ColumnIndex].Name != "Modificada")
                        {
                            if (dgvEquipos.Columns[celda.ColumnIndex].Name != "Aux")
                            {
                                if (!String.IsNullOrEmpty(celda.Value.ToString()))
                                {
                                    Mensaje += " " + dgvEquipos.Columns[celda.ColumnIndex].Name;
                                    Mensaje += " = '" + celda.Value.ToString().Replace(',', '.') + "', ";
                                }
                            }
                        }
                    }
                }
                Mensaje = Mensaje.Remove(Mensaje.Length - 2);
                Mensaje += " WHERE EquipoID = " + fila.Cells["EquipoID"].Value.ToString() + ";";
            }
            return Mensaje;
        }

        public String NuevaLinea(DataGridViewRow fila)
        {
            String Columna = "INSERT INTO equipos (ProyectoID, ";
            String Valores = "VALUES ('" + ProyectoID.ToString() + "', '";
            String NombreColumna = "";

            foreach (DataGridViewCell celda in fila.Cells)
            {
                NombreColumna = dgvEquipos.Columns[celda.ColumnIndex].Name;

                if (NombreColumna != "Modificada" && NombreColumna != "Corriente" && NombreColumna != "Aux")
                {
                    if (celda.Value != null && !String.IsNullOrEmpty(celda.Value.ToString()))
                    {
                        Columna += dgvEquipos.Columns[celda.ColumnIndex].Name + ", ";
                        Valores += celda.Value.ToString().Replace(',', '.') + "', '";
                    }
                }
                
            }

            Columna = Columna.Remove(Columna.Length - 2);
            Valores = Valores.Remove(Valores.Length - 3);

            String Mensaje = Columna + ") " + Valores + ");";

            return Mensaje;

        }

        private void dgvEquipos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

        }

        private void dgvEquipos_Sorted(object sender, EventArgs e)
        {
            foreach (DataGridViewRow fila in dgvEquipos.Rows)
            {
                if (fila.Cells["Aux"].Value != null)
                    fila.Cells["Tipo"].Value = fila.Cells["Aux"].Value;
            }

        }

        private void dgvEquipos_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int ID = 0;
            int IndiceF = e.Row.Index - 1;

            ID = NuevoID();

            if(ID != 0)
            {
                dgvEquipos.Rows[IndiceF].Cells["EquipoID"].Value = ID;
                dgvEquipos.Rows[IndiceF].Cells["Modificada"].Value = "Modificada";
                EnviarQuery(NuevaLinea(dgvEquipos.Rows[IndiceF]));
            }         
        }

        private int NuevoID ()
        {
            int ID = 0;

            //Consigo el ProyectID
            using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
            {
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT MAX(equipoID) FROM equipos"), sqlConnection);
                sqlConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ID = reader.GetInt32(0) + 1;
                }
                reader.Close();
                sqlConnection.Close();
            }

            return ID;
        }
        
        private void SicronizarLinea(DataGridViewRow fila)
        {

            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {   MySqlCommand sqlCmd = new MySqlCommand("", sqlConnection);
                    sqlConnection.Open();

                    if (fila.Cells["Modificada"].Value != null)
                    {
                        String aux = ArmadoQueryEquipo(fila);
                        if (!String.IsNullOrEmpty(aux))
                        {
                            sqlCmd.CommandText = aux;
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private void guardarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Res = new DialogResult();
            Res = MessageBox.Show("Va sincronizar los cambios, reemplazando lo existente. ¿Esta Seguro?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Res == DialogResult.Yes && CambioCelda == true)
            {
                SincronizarDB();
                CambioCelda = false;

                foreach (DataGridViewRow fila in dgvEquipos.Rows)
                {
                    //Insertar funcion para volver al color original la fila
                }
            }
        }

        private void eliminarFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Res = new DialogResult();
            String Query;
            Res = MessageBox.Show("¿Esta seguro que desea eliminar los equipos seleccionados?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Res == DialogResult.Yes)
            {
                foreach (DataGridViewRow Fila in dgvEquipos.Rows)
                {
                    if (Fila.Selected == true)
                    {
                        Query = "DELETE FROM Equipos WHERE EquipoID='";

                        Query = Query + Fila.Cells["EquipoID"].Value.ToString() + "'";
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
                    }
                }
                RecargarLista();
            }
        }
    }
}