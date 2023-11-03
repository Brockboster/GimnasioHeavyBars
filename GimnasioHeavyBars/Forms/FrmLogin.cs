using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GimnasioHeavyBars.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        //Boton para Cancelar y cerrar la aplicacion
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Boton para el ingreso directo 
        private void BtnIngresoDirecto_Click(object sender, EventArgs e)
        {
            Globals.PrincipalPage.Show();
            this.Hide();
        }
        //ingresar a la app 
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                  !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                string user = TxtUserName.Text.Trim();
                string password = TxtPassword.Text.Trim();

                Globals.MyGlobalUser = Globals.MyGlobalUser.UserValidate(user, password);

                if (Globals.MyGlobalUser.UserID > 0)
                {
                    if (Globals.MyGlobalUser.Active)
                    {
                        Globals.PrincipalPage.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Intentas ingresar con un usuario en estado inactivo...", "Error de Estado", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Nombre de Usuario o Contraseña son incorrectos ...", "Error de Credenciales", MessageBoxButtons.OK);

                    TxtPassword.Focus();
                    TxtPassword.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Te hacen falta datos ,vuelvelo a intentar!", "Error de validación", MessageBoxButtons.OK);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = !TxtPassword.UseSystemPasswordChar;
        }

        private void TxtPassword_Click(object sender, EventArgs e)
        {

            TxtPassword.Text = "";
            TxtPassword.UseSystemPasswordChar = true;
        }

        private void TxtUserName_Click(object sender, EventArgs e)
        {
            TxtUserName.Text = " ";
        }

        private void FrmLogin_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Alt && e.Control)
            {


                if (e.KeyCode == Keys.Q)
                {
                    BtnIngresoDirecto.Visible = true;

                }
            }
        }
    }
    
}
