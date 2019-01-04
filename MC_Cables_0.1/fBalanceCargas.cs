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
        String NombreUsuario;
        String ProyectoID;
        String EscenarioID;

        bool CambioCelda = false;
        bool ModificandoCelda = false;
        bool IndicarCambios = false;

        Equipo resNuevaCarga = new Equipo();

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

                //Pido y cargo la Lista de Escenarios
                ListaEscenarios();
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }

        }

        private void ListaEscenarios()
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    List<String> EscenariosID = new List<String>();
                    List<String> listaEsc = new List<String>();

                    /*
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
                    String Query = "SELECT NombreEscenario, EscenarioID FROM Escenarios WHERE ProyectoID =" + ProyectoID + ";";
                    sda = new MySqlDataAdapter(Query, sqlConnection);
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
                    */

                    //Obtengo los nombres de los escenarios
                    sqlConnection.Open();
                    String Query = "SELECT NombreEscenario, EscenarioID FROM Escenarios WHERE ProyectoID =" + ProyectoID + ";";
                    sda = new MySqlDataAdapter(Query, sqlConnection);
                    sda.Fill(dt);
                    foreach (DataRow fila in dt.Rows)
                    {
                        if (!listaEsc.Contains(fila["NombreEscenario"].ToString()))
                            listaEsc.Add(fila["NombreEscenario"].ToString());
                    }
                    sqlConnection.Close();

                    listaEsc.Add(" -- Seleccionar Escenario -- ");
                    listaEsc.Sort();
                    cbEscenario.DataSource = listaEsc;
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private int ConfigurarDataGridView()
        {
            //Creo las columnas Nuevas
            dgvCargas.Columns.Add("Modificada", "Modificada");
            dgvCargas.Columns.Add("Corriente", "Corriente");
            dgvCargas.Columns.Add("PCons", "P Cons [kW]");
            dgvCargas.Columns["Corriente"].DisplayIndex = 12;
            dgvCargas.Columns["PCons"].DisplayIndex = 8;

            //Oculto columnas
            dgvCargas.Columns["CargaID"].Visible = false;
            dgvCargas.Columns["ProyectoID"].Visible = false;
            dgvCargas.Columns["EquipoID"].Visible = false;
            dgvCargas.Columns["EscenarioID"].Visible = false;
            dgvCargas.Columns["Modificada"].Visible = false;
            dgvCargas.Columns["UltimaModific"].Visible = false;
            dgvCargas.Columns["TipoOperacion"].Visible = false;

            //Cambio Headers
            dgvCargas.Columns["FactorSimultaneidad"].HeaderText = "Fs";
            dgvCargas.Columns["FactorCarga"].HeaderText = "Fc";
            dgvCargas.Columns["PotenciaActiva"].HeaderText = "P Activa [kW]";
            dgvCargas.Columns["Corriente"].HeaderText = "I [A]";
            dgvCargas.Columns["Tension"].HeaderText = "V [V]";
            dgvCargas.Columns["Rendimiento"].HeaderText = "Rend. [%]";
            dgvCargas.Columns["CantidadFases"].HeaderText = "Fases";
            dgvCargas.Columns["TipoTension"].HeaderText = "AC/DC";

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
            dgvCargas.Columns["Corriente"].ReadOnly = true;
            dgvCargas.Columns["PCons"].ReadOnly = true;
            
            //Decimales
            dgvCargas.Columns["Corriente"].DefaultCellStyle.Format = "N3";
            dgvCargas.Columns["PotenciaActiva"].DefaultCellStyle.Format = "N3";
            dgvCargas.Columns["PCons"].DefaultCellStyle.Format = "N3";
            
            //Ancho de Columnas
            dgvCargas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCargas.AutoResizeColumns();
            dgvCargas.Columns["PCons"].Width = 110;
            dgvCargas.Columns["PotenciaActiva"].Width = 110;
            dgvCargas.Columns["CantidadFases"].Width = 50;
            dgvCargas.Columns["Rendimiento"].Width = 90;
            dgvCargas.Columns["TipoTension"].Width = 50;
            //dgvCargas.Columns["Descripcion"].Width = 200;
            dgvCargas.AllowUserToResizeColumns = true;
            dgvCargas.AllowUserToOrderColumns = true;


            //Columnas con Combo Box
            var nuevaCol = new DataGridViewComboBoxColumn();
            List<String> Tipos = new List<String>();
            Tipos.AddRange(new List<String> { "Continua", "Intermitente", "Reserva", "Stand By", "Otro" });

            nuevaCol.HeaderText = "Tipo Operacion";
            nuevaCol.Name = "Aux";
            nuevaCol.DataSource = Tipos;
            dgvCargas.Columns.Add(nuevaCol);

            foreach (DataGridViewRow fila in dgvCargas.Rows)
            {
                if (fila.Cells["TipoOperacion"].Value != null)
                    fila.Cells["Aux"].Value = fila.Cells["TipoOperacion"].Value;
            }

            
            dgvCargas.Columns["Aux"].DisplayIndex = dgvCargas.Columns["TipoOperacion"].DisplayIndex;
            dgvCargas.Columns["TipoOperacion"].Name = "Eliminar";
            dgvCargas.Columns["Aux"].Name = "TipoOperacion";
            
            return 0;
        }

        private void cbEscenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarEscenario();

        }

        public void RecargarEscenario()
        {
            if (cbEscenario.SelectedIndex > 0)
            {

                dgvCargas.Columns.Clear();
                dgvCargas.DataSource = null;

                IndicarCambios = false;

                try
                {
                    using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                    {


                        String Query = @"SELECT ListaCargas.*, Equipos.TAG, Equipos.Tipo, Equipos.PotenciaActiva, Equipos.Tension, Equipos.Cosfi, Equipos.Rendimiento, Equipos.CantidadFases, Equipos.TipoTension, Equipos.Descripcion
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


                //Calculo de Corrientes y Potencias
                foreach (DataGridViewRow Fila in dgvCargas.Rows)
                {
                    if (Fila.Cells["CargaID"].Value != null)
                    {
                        float Corriente = CalcularCorriente(Fila);
                        float PotenciaAbs = CalcularPotAbs(Fila);

                        if (Corriente != 0)
                            dgvCargas.Rows[Fila.Index].Cells["Corriente"].Value = Corriente;
                        else
                            dgvCargas.Rows[Fila.Index].Cells["Corriente"].Value = null;

                        if (PotenciaAbs != 0)
                            dgvCargas.Rows[Fila.Index].Cells["PCons"].Value = PotenciaAbs;
                        else
                            dgvCargas.Rows[Fila.Index].Cells["PCons"].Value = null;
                    }
                }

                IndicarCambios = true;
            }
            else
            {
                dgvCargas.DataSource = null;
            }
            dgvCargas.Refresh();

            CalcularTotales();
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
                            if (fila.Cells[IR].Value != DBNull.Value)
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

                                float Corriente = Potencia * 1000 / (Tension * Cosfi * k * Rendimiento/100);

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

        private float CalcularPotAbs(DataGridViewRow fila)
        {
            int IP = fila.Cells["PotenciaActiva"].ColumnIndex;      //Indice Potencia
            int IF = fila.Cells["FactorCarga"].ColumnIndex;         //Indice Factor de Carga
            int IR = fila.Cells["Rendimiento"].ColumnIndex;         //Indice Rendimiento


            //Verifico que esten todos los datos y realizo el calculo de la corriente
            if (fila.Cells[IF].Value != DBNull.Value)
            {
                if (fila.Cells[IP].Value != DBNull.Value)
                {
                    if (fila.Cells[IR].Value != DBNull.Value)
                    {
                        float Potencia = (float)fila.Cells[IP].Value;
                        float FactorC = (float)fila.Cells[IF].Value;
                        float Rendimiento = (float)fila.Cells[IR].Value;

                        float PotenciaAbs = Potencia * FactorC / (Rendimiento / 100);

                        return PotenciaAbs;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        }

        private void dgvCargas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ModificandoCelda == false)
            {
                ModificandoCelda = true;

                if (IndicarCambios == true)
                {
                    int IndiceF = e.RowIndex;
                    int IndiceC = e.ColumnIndex;
                    int IndiceModif = dgvCargas.Columns["Modificada"].Index;

                    dgvCargas.Rows[IndiceF].Cells[IndiceC].Style.BackColor = Color.Yellow;
                    dgvCargas.Rows[IndiceF].Cells[IndiceModif].Value = "Modificada";

                    if(IndiceC == dgvCargas.Columns["FactorCarga"].Index)
                    {
                        var FCActual = (float)dgvCargas.Rows[IndiceF].Cells["FactorCarga"].Value;
                        var Rendimiento = (float)dgvCargas.Rows[IndiceF].Cells["Rendimiento"].Value / 100;

                        /*
                        if (FCActual > Rendimiento)
                        {
                            dgvCargas.Rows[IndiceF].Cells["PCons"].Style.BackColor = Color.Brown;
                            //MessageBox.Show("Si el Factor de carga es superior al rendimiento de la maquina dara por resultado una potencia consumida superior a la mecanica de la maquina", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            //Agrego mensaje de alerta
                            DataGridViewCell cell = this.dgvCargas.Rows[IndiceF].Cells["FactorCarga"];
                            cell.ToolTipText = "El Factor de Carga es superior al Rendimiento";
                            cell = this.dgvCargas.Rows[IndiceF].Cells["PCons"];
                            cell.ToolTipText = "El Factor de Carga es superior al Rendimiento";
                        }
                        */
                    }

                    //Verifico si no debo recalcular la corriente
                    VerificarRecalcular(IndiceF, IndiceC);

                    CambioCelda = true;
                }
                ModificandoCelda = false;
            }
        }

        private void VerificarRecalcular(int IndiceF, int IndiceC)
        {
            //Verifica y recalcula la corriente si se modifica Tension, Potencia, CosenoFi o Rendimiento
            int IT = dgvCargas.Columns["Tension"].Index;             //Indice Tension
            int IP = dgvCargas.Columns["PotenciaActiva"].Index;      //Indice Potencia
            int IC = dgvCargas.Columns["Cosfi"].Index;               //Indice Coseno Fi
            int II = dgvCargas.Columns["Corriente"].Index;           //Indice Corriente Activa
            int IR = dgvCargas.Columns["Rendimiento"].Index;         //Indice Rendimiento
            int IF = dgvCargas.Columns["FactorCarga"].Index;         //Indice Factor de Carga
            int IA = dgvCargas.Columns["PCons"].Index;               //Indice Potencia Absorbida

            if (IndiceC == IT || IndiceC == IP || IndiceC == IC || IndiceC == IR)
            {
                DataGridViewRow Fila = dgvCargas.Rows[IndiceF];
                var Corriente = CalcularCorriente(Fila);
                dgvCargas.Rows[IndiceF].Cells[II].Value = Corriente;
            }

            if (IndiceC == IP || IndiceC == IF || IndiceC == IR)
            {
                DataGridViewRow Fila = dgvCargas.Rows[IndiceF];
                var PotenciaAbs = CalcularPotAbs(Fila);
                dgvCargas.Rows[IndiceF].Cells[IA].Value = PotenciaAbs;
            }

            CalcularTotales();

        }

        private void CalcularTotales()
        {
            float[,] Potencias = new float[3,6] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
            int Aux1, Aux2;

            foreach (DataGridViewRow Fila in dgvCargas.Rows)
            {
                var Tipo = Fila.Cells["TipoOperacion"].Value;

                switch (Tipo)
                {
                    case "Continua":
                        //Potencia Activa Continua
                        Potencias[0, 0] = Potencias[0, 0] + (float)Fila.Cells["PCons"].Value;
                        //Potencia Reactiva Continua
                        Potencias[1, 0] = Potencias[1, 0] + ((float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value) * (float)Math.Sin(Math.Acos((float)Fila.Cells["Cosfi"].Value));
                        //Potencia Aparente Continua
                        Potencias[2, 0] = Potencias[2, 0] + (float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value;
                        break;
                    case "Intermitente":
                        //Potencia Activa Intermitente
                        Potencias[0, 1] = Potencias[0, 1] + (float)Fila.Cells["PCons"].Value;
                        //Potencia Reactiva Intermitente
                        Potencias[1, 1] = Potencias[1, 1] + ((float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value) * (float)Math.Sin(Math.Acos((float)Fila.Cells["Cosfi"].Value));
                        //Potencia Aparente Intermitente
                        Potencias[2, 1] = Potencias[2, 1] + (float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value;
                        break;
                    /*
                    case "Reserva":
                        //Potencia Activa Reserva
                        Potencias[0, 2] = Potencias[0, 2] + (float)Fila.Cells["PCons"].Value;
                        //Potencia Reactiva Reserva
                        Potencias[1, 2] = Potencias[1, 2] + ((float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value) * (float)Math.Sin(Math.Acos((float)Fila.Cells["Cosfi"].Value));
                        //Potencia Aparente Reserva
                        Potencias[2, 2] = Potencias[2, 2] + (float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value;
                        break;
                    */
                    case "Stand By":
                        //Potencia Activa Stand By
                        Potencias[0, 3] = Potencias[0, 3] + (float)Fila.Cells["PCons"].Value;
                        //Potencia Reactiva Stand By
                        Potencias[1, 3] = Potencias[1, 3] + ((float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value) * (float)Math.Sin(Math.Acos((float)Fila.Cells["Cosfi"].Value));
                        //Potencia Aparente Stand By
                        Potencias[2, 3] = Potencias[2, 3] + (float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value;
                        break;
                    case "Otro":
                        //Potencia Activa Otro
                        Potencias[0, 4] = Potencias[0, 4] + (float)Fila.Cells["PCons"].Value;
                        //Potencia Reactiva Otro
                        Potencias[1, 4] = Potencias[1, 4] + ((float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value) * (float)Math.Sin(Math.Acos((float)Fila.Cells["Cosfi"].Value));
                        //Potencia Aparente Otro
                        Potencias[2, 4] = Potencias[2, 4] + (float)Fila.Cells["PCons"].Value / (float)Fila.Cells["Cosfi"].Value;
                        break;
                }
            }

            int MaxFilas = Potencias.GetLength(0)-1;
            int MaxColum = Potencias.GetLength(1)-1;

            for (Aux1 = 0; Aux1 <= MaxFilas; Aux1++)
            {
                //Para el total de las potencias sumo unicamente las potencias Continuas e Intermitentes
                for (Aux2 = 0; Aux2 <= 1; Aux2++)
                {
                    Potencias[Aux1, MaxColum] = Potencias[Aux1, MaxColum] + Potencias[Aux1, Aux2];
                }
            }

            lbPCA.Text = Potencias[0, 0].ToString("#.##");            //Indico Potencias Continuas
            lbPCR.Text = Potencias[1, 0].ToString("#.##");
            lbPCS.Text = Potencias[2, 0].ToString("#.##");
            lbPIA.Text = Potencias[0, 1].ToString("#.##");            //Indico Potencias Intermitentes
            lbPIR.Text = Potencias[1, 1].ToString("#.##");
            lbPIS.Text = Potencias[2, 1].ToString("#.##");
            //lbPRA.Text = Potencias[0, 2].ToString("#.##");            //Indico Potencias Reserva
            //lbPRR.Text = Potencias[1, 2].ToString("#.##");
            //lbPRS.Text = Potencias[2, 2].ToString("#.##");
            lbPSA.Text = Potencias[0, 3].ToString("#.##");            //Indico Potencias Stand By
            lbPSR.Text = Potencias[1, 3].ToString("#.##");
            lbPSS.Text = Potencias[2, 3].ToString("#.##");
            lbTA.Text = Potencias[0, 5].ToString("#.##");             //Indico Potencias Totales
            lbTR.Text = Potencias[1, 5].ToString("#.##");
            lbTS.Text = Potencias[2, 5].ToString("#.##");

        }

        private void SincronizarDB ()
        {
            foreach (DataGridViewRow Fila in dgvCargas.Rows)
            {
                if (Fila.Cells["Modificada"].Value != DBNull.Value)
                {
                    //Si el ID no es null debo actualizar la base de datos
                    if (!String.IsNullOrEmpty(Fila.Cells["CargaID"].ToString()))
                    {
                        using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                        {
                            MySqlCommand sqlCmd = new MySqlCommand("", sqlConnection);
                            sqlConnection.Open();

                            // Creo la query y la cargo en el cmd
                            sqlCmd.CommandText = QueryActualizacion(Fila);
                            sqlCmd.ExecuteNonQuery();

                            sqlConnection.Close();
                        }
                    }
                    //Si el ID esta vacio significa que la fila es nueva y debo crear la misma
                    else
                    {
                        using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                        {
                            MySqlCommand sqlCmd = new MySqlCommand("", sqlConnection);
                            sqlConnection.Open();

                            // Creo la query y la cargo en el cmd
                            sqlCmd.CommandText = NuevaLinea(Fila, "ListaCargas");
                            sqlCmd.ExecuteNonQuery();

                            sqlConnection.Close();
                        }
                    }
                }
            }
            CambioCelda = false;
        }

        public String QueryActualizacion (DataGridViewRow fila)
        {
            String Mensaje = "";
            Mensaje = "UPDATE ListaCargas SET ";
            
            Mensaje += "ProyectoID = '" + ProyectoID + "', ";

            if (!String.IsNullOrEmpty(fila.Cells["EquipoID"].Value.ToString()) )
                Mensaje += "EquipoID = '" + fila.Cells["EquipoID"].Value.ToString() + "', ";

            if (!String.IsNullOrEmpty(fila.Cells["Rev"].Value.ToString()) )
                Mensaje += "Rev = '" + fila.Cells["Rev"].Value.ToString() + "', ";

            if (!String.IsNullOrEmpty(fila.Cells["EscenarioID"].Value.ToString()) )
                Mensaje += "EscenarioID = '" + fila.Cells["EscenarioID"].Value.ToString() + "', ";

            if (!String.IsNullOrEmpty(fila.Cells["FactorSimultaneidad"].Value.ToString()) )
                Mensaje += "FactorSimultaneidad = '" + fila.Cells["FactorSimultaneidad"].Value.ToString().Replace(',', '.') + "', ";

            if (!String.IsNullOrEmpty(fila.Cells["FactorCarga"].Value.ToString()) )
                Mensaje += "FactorCarga = '" + fila.Cells["FactorCarga"].Value.ToString().Replace(',', '.') + "', ";

            if (!String.IsNullOrEmpty(fila.Cells["TipoOperacion"].Value.ToString()) )
                Mensaje += "TipoOperacion = '" + fila.Cells["TipoOperacion"].Value.ToString() + "', ";
/*
            if (!String.IsNullOrEmpty(fila.Cells["Rendimiento"].Value.ToString()) )
                Mensaje += "Rendimiento = '" + fila.Cells["Rendimiento"].Value.ToString() + "', ";
*/
            if ( !String.IsNullOrEmpty(fila.Cells["UltimaModific"].Value.ToString()) )
                Mensaje += "UltimaModific = '" + fila.Cells["UltimaModific"].Value.ToString() + "'  ";

            Mensaje = Mensaje.Remove(Mensaje.Length - 2);
            Mensaje += " WHERE CargaID = " + fila.Cells["CargaID"].Value.ToString() + ";";
        

            return Mensaje;
        }

        public String NuevaLinea(DataGridViewRow fila, String Tabla)
        {
            String Columna = "INSERT INTO " + Tabla + " (ProyectoID, ";
            String Valores = "VALUES ('" + ProyectoID.ToString() + "', '";
            String NombreColumna = "";

            foreach (DataGridViewCell celda in fila.Cells)
            {
                NombreColumna = dgvCargas.Columns[celda.ColumnIndex].Name;

                if (NombreColumna != "Modificada" && NombreColumna != "Corriente" && NombreColumna != "Aux")
                {
                    if (celda.Value != null && !String.IsNullOrEmpty(celda.Value.ToString()))
                    {
                        Columna += dgvCargas.Columns[celda.ColumnIndex].Name + ", ";
                        Valores += celda.Value.ToString().Replace(',', '.') + "', '";
                    }
                }

            }

            Columna = Columna.Remove(Columna.Length - 2);
            Valores = Valores.Remove(Valores.Length - 3);

            String Mensaje = Columna + ") " + Valores + ");";

            return Mensaje;
        }

        private void sincronizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SincronizarDB();
            RecargarEscenario();
        }

        private void agregarCargaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fNuevaCarga frm = new fNuevaCarga(NombreUsuario, NombreProyecto, EscenarioID);
            frm.Owner = this;
            DialogResult res = frm.ShowDialog();
            if(res == DialogResult.OK)
                resNuevaCarga = frm.EquipoResultado;
        }

        private void eliminarCargaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Res = new DialogResult();
            String Query;
            Res = MessageBox.Show("¿Esta seguro que desea eliminar las cargas seleccionadas?", "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Res == DialogResult.Yes)
            {
                foreach (DataGridViewRow Fila in dgvCargas.Rows)
                {
                    if(Fila.Selected == true)
                    {
                        Query = "DELETE FROM ListaCargas WHERE CargaID='";

                        Query = Query + Fila.Cells["CargaID"].Value.ToString() +"'";
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
                RecargarEscenario();
            }
        }

        private void recargarEscenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecargarEscenario();
        }

        private void EnviarQuery(String Query)
        {
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

        private void dgvCargas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Some logic for determining which cell this is in the row
            if ((e.ColumnIndex == this.dgvCargas.Columns["Pcons"].Index))
            {
                // Get a reference to the cell
                DataGridViewCell cell = this.dgvCargas.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Set the tooltip text
                cell.ToolTipText = "La potencia consumida se calcula como: \n Potencia Activa * Rendimiento / Factor de Carga";
            }
        }

        private void nuevoEscenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fNuevoEscenario frm = new fNuevoEscenario(NombreUsuario, NombreProyecto, ProyectoID);
            frm.Owner = this;
            frm.ShowDialog();
            ListaEscenarios();
        }

        private void eliminarEscenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Query = cbEscenario.Text;
            String Aux = "¿Esta seguro que desea eliminar el escenario '" + Query + "'?";
            DialogResult Res = new DialogResult();

            Res = MessageBox.Show(Aux, "¡Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Res == DialogResult.Yes)
            {
                Query = "DELETE FROM Escenarios WHERE NombreEscenario = '" + Query + "';";
                EnviarQuery(Query);
            }
            ListaEscenarios();
        }

        private void recargarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}