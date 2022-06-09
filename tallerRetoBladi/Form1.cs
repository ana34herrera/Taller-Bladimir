using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace tallerRetoBladi
{
    public partial class FrmLogin : Form
    {
        public static string cadena = "Sever=;Database=;User Id=USENA;Password=12345;";
        public static string msError = "";
        public static SqlConnection conection = new SqlConnection(cadena);
        public static DataTable dt = new DataTable();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
            txtPassword.Focus();
        }

        private void btnInicioSesion_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("ingrese los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                conection.Open();
                DataTable dt = new DataTable();
                dt = ModuloLogin.Func_TraerDatos(TxtUsuario.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el usuario", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string pass = dt.Rows[0]["PASSWORD_USUARIO"].ToString();
                    string rol = dt.Rows[0]["ROL_USUARIO"].ToString();
                    string NOM = dt.Rows[0]["NOMBRE_USUARIO"].ToString();
                    string APE = dt.Rows[0]["APELLIDO_USUARIO"].ToString();
                    //comparo
                    if (TxtContraseña.Text == pass)
                    {
                        FrmMenuPrincipal f = new FrmMenuPrincipal();
                        f.Lbl_Usuario_Nombre.Text = NOM;
                        f.Lbl_Usuario_Apellido.Text = APE;
                        if (rol == "Administrador")
                        {
                            f.BtnUsuarios.Enabled = true;
                        }
                        else
                        {
                            f.BtnUsuarios.Enabled = false;
                        }
                        this.Hide();
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Más fácil te sabes la contraseña de tu ex :V", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
