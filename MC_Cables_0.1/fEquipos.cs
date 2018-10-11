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

                //Consigo los datos de la tabla
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    sda = new MySqlDataAdapter(String.Format("SELECT e.* FROM Proyectos p JOIN Equipos e ON p.ProyectoID = e.ProyectoID WHERE p.NombreProyecto =  '{0}'", NombreProyecto), sqlConnection);
                    dt = new DataTable();
                    sda.Fill(dt);

                    dgvEquipos.DataSource = dt;

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
                DataGridViewComboBoxColumn dgcb = new DataGridViewComboBoxColumn();
            }

            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private int ConfigurarDataGridView()
        {
            dgvEquipos.Columns.Add("Modificada", "Modificada");
            dgvEquipos.Columns.Add("Corriente", "Corriente");

            dgvEquipos.Columns["EquipoID"].Visible = true;
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

        private float CalcularPotencia(DataGridViewRow fila)
        {
            int IT = fila.Cells["Tension"].ColumnIndex;             //Indice Tension
            int IP = fila.Cells["PotenciaActiva"].ColumnIndex;      //Indice Potencia
            int IC = fila.Cells["Cosfi"].ColumnIndex;               //Indice Coseno Fi
            int IF = fila.Cells["CantidadFases"].ColumnIndex;       //Indice Cantidad de Fases
            int II = fila.Cells["Corriente"].ColumnIndex;           //Indice Corriente Activa

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

                            if (Convert.ToInt32(fila.Cells[IF].Value) == 3)
                            {
                                k = (float)Math.Sqrt(3);
                            }
                            else
                            {
                                k = 1;
                            }

                            float Tension = (float)fila.Cells[IT].Value;
                            float Corriente = (float)fila.Cells[II].Value;
                            float Cosfi = (float)fila.Cells[IC].Value;

                            float Potencia = Corriente * Tension * Cosfi * k / 1000;

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

            using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
            {
                MySqlCommand sqlCmd = new MySqlCommand("", sqlConnection);
                sqlConnection.Open();

                foreach (DataGridViewRow fila in dgvEquipos.Rows)
                {
                    if (fila.Cells["Modificada"].Value != null)
                    {
                        aux = ArmadoQueryEquipo(fila);
                        if(!String.IsNullOrEmpty(aux))
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
            using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
            {
                sda = new MySqlDataAdapter(String.Format("SELECT * FROM equipos INNER JOIN Proyectos ON Proyectos.NombreProyecto = '{0}'", NombreProyecto), sqlConnection);
                dt = new DataTable();
                sda.Fill(dt);
                dgvEquipos.DataSource = dt;
                dgvEquipos.Columns["EquipoID"].Visible = false;
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

                    //Si se modifico la tension, la potencia o el coseno fi, recalculo la corriente
                    if (dgvEquipos.Rows[IndiceF].Cells[IT].Value != DBNull.Value)
                    {
                        if (dgvEquipos.Rows[IndiceF].Cells[IF].Value != DBNull.Value)
                        {
                            if (dgvEquipos.Rows[IndiceF].Cells[IC].Value != DBNull.Value)
                            {
                                if (dgvEquipos.Rows[IndiceF].Cells[IP].Value != DBNull.Value)
                                {
                                    if (IndiceC == IT || IndiceC == IP || IndiceC == IC || IndiceC == IF)
                                    {
                                        float k;

                                        if (Convert.ToInt32(dgvEquipos.Rows[IndiceF].Cells[IF].Value) == 3)
                                            k = (float)Math.Sqrt(3);
                                        else
                                            k = 1;

                                        float Tension = (float)dgvEquipos.Rows[IndiceF].Cells[IT].Value;
                                        float Potencia = (float)dgvEquipos.Rows[IndiceF].Cells[IP].Value;
                                        float Cosfi = (float)dgvEquipos.Rows[IndiceF].Cells[IC].Value;

                                        float Corriente = Potencia * 1000 / (Tension * Cosfi * k);

                                        dgvEquipos.Rows[IndiceF].Cells[II].Style.BackColor = Color.Yellow;
                                        dgvEquipos.Rows[IndiceF].Cells[II].Value = Corriente;
                                    }
                                }
                                else //Si se modifico la corriente, recalculo la potencia
                                {
                                    if (IndiceC == II)
                                    {
                                        float k;

                                        if (Convert.ToInt32(dgvEquipos.Rows[IndiceF].Cells[IF].Value) == 3)
                                        {
                                            k = (float)Math.Sqrt(3);
                                        }
                                        else
                                        {
                                            k = 1;
                                        }

                                        float Tension = (float)dgvEquipos.Rows[IndiceF].Cells[IT].Value;
                                        float Cosfi = (float)dgvEquipos.Rows[IndiceF].Cells[IC].Value;
                                        float Corriente;
                                        float.TryParse(dgvEquipos.Rows[IndiceF].Cells[II].Value.ToString(), out Corriente);

                                        float Potencia = Corriente * (Tension * Cosfi * k) / 1000;

                                        dgvEquipos.Rows[IndiceF].Cells[IP].Style.BackColor = Color.Yellow;
                                        dgvEquipos.Rows[IndiceF].Cells[IP].Value = Potencia;
                                    }
                                }
                            }
                        }
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
        }

        public String ArmadoQueryEquipo(DataGridViewRow fila)
        {
            String Mensaje = "";

            //Verifico si es una fila nueva o existente
            if (fila.Cells["EquipoID"].Value.ToString() == "")
            {
                NuevaLinea(fila, "equipos");
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

        public String NuevaLinea(DataGridViewRow fila, String Tabla)
        {
            String Columna = "INSERT INTO " + Tabla + " (ProyectoID, ";
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
        

    }
}



/*
Codigo de reserva

    if (IndiceC == IP)
                    {
                        float Corriente = CalcularCorriente(dgvEquipos.Rows[e.RowIndex]);
                        if (Corriente == 0)
                        {
                            dgvEquipos.Rows[IndiceF].Cells[II].Style.BackColor = Color.Yellow;
                            dgvEquipos.Rows[IndiceF].Cells[II].Value = Corriente;
                        }
                    }


                    if (IndiceC == II)
                    {
                        float Potencia = CalcularPotencia(dgvEquipos.Rows[e.RowIndex]);
                        if (Potencia == 0)
                        {
                            dgvEquipos.Rows[IndiceF].Cells[IP].Style.BackColor = Color.Yellow;
                            dgvEquipos.Rows[IndiceF].Cells[IP].Value = Potencia;
                        }
                    }




                if (fila.Cells["TAG"].Value.ToString() != "")
                    Mensaje += "TAG = '" + fila.Cells["TAG"].Value.ToString() + "'";
                else
                    Mensaje += ", TAG = NULL";

                if (fila.Cells["Rev"].Value.ToString() != "")
                    Mensaje += ", Rev = '" + fila.Cells["Rev"].Value.ToString() + "'";
                else
                    Mensaje += ", Rev = NULL";

                if (fila.Cells["Tipo"].Value.ToString() != "")
                    Mensaje += ", Tipo = '" + fila.Cells["Tipo"].Value.ToString() + "'";
                else
                    Mensaje += ", Tipo = NULL";

                if (fila.Cells["CantidadFases"].Value.ToString() != "")
                    Mensaje += ", CantidadFases = '" + fila.Cells["CantidadFases"].Value.ToString() + "'";
                else
                    Mensaje += ", CantidadFases = NULL";

                if (fila.Cells["Tension"].Value.ToString() != "")
                    Mensaje += ", Tension = '" + fila.Cells["Tension"].Value.ToString() + "'";
                else
                    Mensaje += ", Tension = NULL";

                if (fila.Cells["Cosfi"].Value.ToString() != "")
                    Mensaje += ", Cosfi = '" + fila.Cells["Cosfi"].Value.ToString() + "'";
                else
                    Mensaje += ", Cosfi = NULL";

                if (fila.Cells["PotenciaActiva"].Value.ToString() != "")
                    Mensaje += ", PotenciaActiva = '" + fila.Cells["PotenciaActiva"].Value.ToString() + "'";
                else
                    Mensaje += ", PotenciaActiva = NULL";

                if (fila.Cells["CosfiArranque"].Value.ToString() != "")
                    Mensaje += ", CosfiArranque = '" + fila.Cells["CosfiArranque"].Value.ToString() + "'";
                else
                    Mensaje += ", CosfiArranque = NULL";

                if (fila.Cells["CorrienteArranque"].Value.ToString() != "")
                    Mensaje += ", CorrienteArranque = '" + fila.Cells["CorrienteArranque"].Value.ToString() + "'";
                else
                    Mensaje += ", CorrienteArranque = NULL";

                if (fila.Cells["Rendimiento"].Value.ToString() != "")
                    Mensaje += ", Rendimiento = '" + fila.Cells["Rendimiento"].Value.ToString() + "'";
                else
                    Mensaje += ", Rendimiento = NULL";

                if (fila.Cells["Descripcion"].Value.ToString() != "")
                    Mensaje += ", Descripcion = '" + fila.Cells["Descripcion"].Value.ToString() + "'";
                else
                    Mensaje += ", Descripcion = NULL";

                if (fila.Cells["Ubicacion"].Value.ToString() != "")
                    Mensaje += ", Ubicacion = '" + fila.Cells["Ubicacion"].Value.ToString() + "'";
                else
                    Mensaje += ", Ubicacion = NULL";

                if (fila.Cells["PID"].Value.ToString() != "")
                    Mensaje += ", PID = '" + fila.Cells["PID"].Value.ToString() + "'";
                else
                    Mensaje += ", PID = NULL";


                Mensaje += " WHERE EquipoID = " + fila.Cells["EquipoID"].Value.ToString();

                //Reemplazo ',' por '.'
                Mensaje = Mensaje.Replace(",", ".");
                Mensaje = Mensaje.Replace("'. ", "', ");
                Mensaje = Mensaje.Replace("NULL.", "NULL,");









*/
