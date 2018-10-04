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
    public partial class LogIn : Form
    {
        String strConnection = "Server=ING114;Port=3306;Database=dbthor;Uid=usuario;Pwd=usuario123;";

        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(strConnection))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("SELECT Nombre,Apellido FROM usuarios", sqlConnection);
                    sqlConnection.Open();
                    MySqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    List<String> listaUsr = new List<String>();
                    listaUsr.Add(" -- Seleccionar Usuario -- ");
                    while (sqlReader.Read())
                        listaUsr.Add(sqlReader["Nombre"].ToString() + " " + sqlReader["Apellido"].ToString());
                    sqlReader.Close();
                    listaUsr.Sort();
                    cbUsuario.DataSource = listaUsr;
                


                    sqlCmd.CommandText = "SELECT NombreProyecto FROM proyectos";
                    sqlReader = sqlCmd.ExecuteReader();

                    List<String> listaPry = new List<String>();

                    listaPry.Add(" -- Seleccionar Proyecto -- ");
                    while (sqlReader.Read())
                        listaPry.Add(sqlReader["NombreProyecto"].ToString());
                    sqlReader.Close();
                    sqlConnection.Close();
                    listaPry.Sort();
                    cbProyecto.DataSource = listaPry;
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( cbUsuario.Text == " -- Seleccionar Usuario -- " || cbProyecto.Text == " -- Seleccionar Proyecto -- ")
            {
                MessageBox.Show("Seleccione un Usuario y un Proyecto");
            }
            else
            {
                fEquipos frm = new fEquipos(cbUsuario.Text, cbProyecto.Text);
                frm.Owner = this;
                frm.ShowDialog();
                //this.Close();
            }

        }

        private void btCargas_Click(object sender, EventArgs e)
        {
            if (cbUsuario.Text == " -- Seleccionar Usuario -- " || cbProyecto.Text == " -- Seleccionar Proyecto -- ")
            {
                MessageBox.Show("Seleccione un Usuario y un Proyecto");
            }
            else
            {
                fBalanceCargas frm = new fBalanceCargas(cbUsuario.Text, cbProyecto.Text);
                frm.Owner = this;
                frm.ShowDialog();
                //this.Close();
            }
        }
    }
}
