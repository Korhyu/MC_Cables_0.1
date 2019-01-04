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
    public partial class fNuevoEscenario : Form
    {
        String strConnection = "Server=ING114;Port=3306;Database=dbthor;Uid=usuario;Pwd=usuario123;";
        String NombreProyecto;
        String NombreUsuario;
        String ProyectoID;


        public fNuevoEscenario()
        {
            InitializeComponent();
        }

        public fNuevoEscenario(String Usuario, String Proyecto, String ID)
        {
            InitializeComponent();

            if (String.IsNullOrEmpty(Proyecto))
                Proyecto = "No hay proyecto";
            lbUsuario.Text = "Usuario: " + Usuario.ToString();
            lbProyecto.Text = "Proyecto: " + Proyecto.ToString();

            ProyectoID = ID;
            NombreUsuario = Usuario.ToString();
            NombreProyecto = Proyecto.ToString();
        }


        private void btAgregar_Click(object sender, EventArgs e)
        {
            EnviarQuery(NuevaLinea());
            Close();
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

        public String NuevaLinea()
        {
            String Query;

            Query = "'" + tbNombre.Text.ToString() + "'";
            Query = "INSERT INTO Escenarios (NombreEscenario,ProyectoID) VALUES (" + Query + "," + ProyectoID + ");";

            return Query;
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            fBalanceCargas aux = (fBalanceCargas)this.Owner;
            aux.RecargarEscenario();
            Close();
        }
    }
}
