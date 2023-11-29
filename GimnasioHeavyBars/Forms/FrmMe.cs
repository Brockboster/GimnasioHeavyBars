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
    public partial class FrmMe : Form
    {
        #region Atributos 
        private Logic.Models.User MyUser { get; set; }
        #endregion


        #region Ctor
        public FrmMe()
        {
            InitializeComponent();
            MyUser = new Logic.Models.User();
        }
        #endregion

        #region load 
        private void FrmMe_Load(object sender, EventArgs e)
        {
            loaduser();
            Question();
        }
        #endregion

        #region Traer usuario 

        //Mediante el ID con quien hicimos login podemos  traer los datos del usuario
        private void loaduser()
        {
            int IdUSer = Globals.MyGlobalUser.UserID;
            MyUser.UserID = IdUSer;
            MyUser = MyUser.UserSearchID();

            if (MyUser != null && MyUser.UserID > 0)
            {
                TxtUserID.Text = Convert.ToString(MyUser.UserID);
                txtEmail.Text = MyUser.UserEmail;
                TxtUserName.Text = MyUser.UserName;
                TxtHeight.Text = Convert.ToString(MyUser.Height);
                TxtWeigth.Text = Convert.ToString(MyUser.Weight);
                TxtPhoneNumber.Text = Convert.ToString(MyUser.PhoneNumber);
                TxtMembership.Text = MyUser.membership.MembershipName;
                TxtendMembership.Text = Convert.ToString(MyUser.DateFinal);
                txtRegisterDay.Text = Convert.ToString(MyUser.RegisterDay);
                txtDatePrice.Text = Convert.ToString(MyUser.DateStart);
                TxtPrice.Text = Convert.ToString(MyUser.membership.price);

            }






        }
        #endregion


        #region Cargar lista de membresias 
        private void Question()
        {
            Logic.Models.Question question = new Logic.Models.Question();

            DataTable dt = new DataTable();
            dt = question.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbQuestion.ValueMember = "QuestionID";
                CbQuestion.DisplayMember = "QuestionName";
                CbQuestion.DataSource = dt;
                CbQuestion.SelectedIndex = -1;

            }


        }
        #endregion

        #region limpiar 
        private void Clean()
        {
            TxtUserName.Clear();
            TxtHeight.Clear();
            TxtPrice.Clear();
            txtRegisterDay.Clear();
            txtDatePrice.Clear();
            TxtPhoneNumber.Clear();
            txtEmail.Clear();
            TxtWeigth.Clear();



        }
        #endregion


        #region boton Editar
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && !string.IsNullOrEmpty(TxtWeigth.Text.Trim()))
            {
                MyUser.UserName = TxtUserName.Text.Trim();
                MyUser.PhoneNumber = Convert.ToInt32(TxtPhoneNumber.Text.Trim());
                MyUser.Weight = Convert.ToDecimal(TxtWeigth.Text.Trim());
                MyUser.Height = Convert.ToDecimal(TxtHeight.Text.Trim());
                MyUser.UserEmail = txtEmail.Text.Trim();



                DialogResult Answer = MessageBox.Show("¿Este u otro usuario  tiene registrado este numero o Correo electronico, aun deseas modificarlo?", "???",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Answer == DialogResult.Yes)
                {

                    if (MyUser.UserUpdates())
                    {
                        MessageBox.Show("El usuario ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                        Clean();
                        loaduser();


                    }
                    else
                    {
                        MessageBox.Show("El usuario no ha sido modificado correctamente", ":C", MessageBoxButtons.OK);
                        Clean();
                        loaduser();

                    }
                }

            }

            else
            {
                MessageBox.Show("El usuario no ha sido modificado", "!", MessageBoxButtons.OK);




            }
        }


        #endregion

        #region CellClick
        private void TxtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void TxtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros y Puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Verificar que no haya más de un punto decimal en el texto
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void TxtWeigth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros y Puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Verificar que no haya más de un punto decimal en el texto
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void TxtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion


        #region Edit password
        private void BtnEditPassword_Click(object sender, EventArgs e)
        {
            if (CbQuestion.SelectedIndex > -1 &&
     !string.IsNullOrEmpty(txtAnswer.Text.Trim()) &&
     !string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                int userId = Globals.MyGlobalUser.UserID;
                MyUser.UserID = userId;
                MyUser = MyUser.CheckUser();
                string realAnswer = MyUser.answer;
                int idRealQuestion = MyUser.question.QuestionID;

                int idQuestion = -1;
                string answer = string.Empty;

                if (CbQuestion.SelectedIndex > -1)
                {
                    idQuestion = Convert.ToInt32(CbQuestion.SelectedValue);
                    answer = txtAnswer.Text.Trim();
                }

                if (idRealQuestion == idQuestion && !string.IsNullOrEmpty(realAnswer) && realAnswer == answer)
                //validacion que la respuesta y la pregunta sean la misma que lo registrado anteriormente 
                {
                    MyUser.UserID = Globals.MyGlobalUser.UserID;
                    MyUser.UserPassword = txtNewPassword.Text.Trim();
                    MyUser.UpdateUserPassword();
                    MessageBox.Show("La contraseña se actualizo correctamente", "=)", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("La pregunta o respuesta no coinciden con los datos registrados", "Error de Validación", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (CbQuestion.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar una pregunta");
                    CbQuestion.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAnswer.Text.Trim()))
                {
                    MessageBox.Show("Debes escribir una respuesta");
                    txtAnswer.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                {
                    MessageBox.Show("Debes escribir una nueva contraseña");
                    txtNewPassword.Focus();
                    return;
                }
            }
        }

        #endregion

        #region Salir del formulario 
        private void BtnSkipeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
