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
    public partial class FrmUsers : Form
    {
        #region Atributos 
        private Logic.Models.User MyUser { get; set; }
        private DataTable UserList { get; set; }
        #endregion

        #region Ctor
        public FrmUsers()
        {
            InitializeComponent();
            MyUser = new Logic.Models.User();


        }
        #endregion

        #region Load 
        //Evento load para cargar algunas condicionales 
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            UsersList();
            UsersRole();
            Merbership();
        }
        #endregion

        #region Botones 
        //Boton para Cerrar el formulario
        private void BtnSkipeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //boton agregar
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                bool Emailok;
                bool Phoneok;

                MyUser = new Logic.Models.User();

                MyUser.UserName = TxtUserName.Text.Trim();
                MyUser.PhoneNumber = Convert.ToInt32(TxtPhoneNumber.Text.Trim());
                MyUser.Weight = Convert.ToDecimal(TxtWeigth.Text.Trim());
                MyUser.Height = Convert.ToDecimal(TxtHeight.Text.Trim());
                MyUser.UserEmail = txtEmail.Text.Trim();
                MyUser.UserPassword = TxtPassword.Text.Trim();
                MyUser.MyUserRole.UserRoleID = Convert.ToInt32(CbUserRole.SelectedValue);
                MyUser.membership.DueDate = DateTime.Now;
                MyUser.membership.MerbershipID = Convert.ToInt32(CbMembership.SelectedValue);



                Emailok = MyUser.ConsultUserEmail();
                Phoneok = MyUser.ConsultPhonenumber();
                if (Emailok == false && Phoneok == false)
                {
                    string msg = string.Format("¿Estás seguro de agregar al usuario {0}?", MyUser.UserName);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {

                        bool ok = MyUser.Add();

                        if (ok)
                        {
                            MessageBox.Show("Usuario guardado correctamente!", "confirmación", MessageBoxButtons.OK);

                            Clean();

                            UsersList();

                        }
                        else
                        {
                            MessageBox.Show("El Usuario no se guardado de forma correcta!", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }



                    }


                }
                else
                {

                    if (Emailok)
                    {
                        MessageBox.Show("Ya existe un usuario con el correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }
                    if (Phoneok)
                    {
                        MessageBox.Show("Ya existe un usuario con el numero telefonico digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                }

            }
        }
        //boton eliminar
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
               !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && !string.IsNullOrEmpty(TxtWeigth.Text.Trim())
                     && CbUserRole.SelectedIndex > -1 && CbMembership.SelectedIndex > -1)
            {


                DialogResult r = MessageBox.Show("¿Estas seguro de Eliminar al usuario ?"
                                                 , MyUser.UserName,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyUser.Delete())
                    {
                        MessageBox.Show("El usuario" + MyUser.UserName + "ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);

                    }
                    Clean();
                    UsersList();

                }

            }
            else
            {
                MessageBox.Show("Primero debes de seleccionar un usuario de la lista para eliminar", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        //boton limpiar
        private void BtnClean_Click(object sender, EventArgs e)
        {
            Clean();
        }
        // boton editar
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            {

                bool Emailok;
                bool Phoneok;

                if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && !string.IsNullOrEmpty(TxtWeigth.Text.Trim())
                    && CbUserRole.SelectedIndex > -1 && CbMembership.SelectedIndex > -1)
                {
                    MyUser.UserName = TxtUserName.Text.Trim();
                    MyUser.PhoneNumber = Convert.ToInt32(TxtPhoneNumber.Text.Trim());
                    MyUser.Weight = Convert.ToDecimal(TxtWeigth.Text.Trim());
                    MyUser.Height = Convert.ToDecimal(TxtHeight.Text.Trim());
                    MyUser.UserEmail = txtEmail.Text.Trim();
                    MyUser.UserPassword = TxtPassword.Text.Trim();
                    MyUser.MyUserRole.UserRoleID = Convert.ToInt32(CbUserRole.SelectedValue);
                    MyUser.membership.DueDate = DateTime.Now;
                    MyUser.membership.MerbershipID = Convert.ToInt32(CbMembership.SelectedValue);

                    Emailok = MyUser.ConsultUserEmail();
                    Phoneok = MyUser.ConsultPhonenumber();
                    if (Emailok == false && Phoneok == false)
                    {
                        DialogResult Answer = MessageBox.Show("¿Este u otro usuario  tiene registrado este numero o Correo electronico, aun deseas modificarlo?", "???",
                                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (Answer == DialogResult.Yes)
                        {

                            if (MyUser.Update())
                            {
                                MessageBox.Show("El Entrenador " + MyUser.UserName + "ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                                Clean();
                                UsersList();
                            }
                            else
                            {
                                MessageBox.Show("El usuario" + MyUser.UserName + "no ha sido modificado correctamente", ":C", MessageBoxButtons.OK);
                                Clean();
                                UsersList();
                            }
                        }

                    }

                    else
                    {
                        DialogResult Answer = MessageBox.Show("¿Está seguro de modificar el entrenador " + MyUser.UserName + " ?", "???",
                                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (Answer == DialogResult.Yes)
                        {

                            if (MyUser.Update())
                            {
                                MessageBox.Show("El usuario " + MyUser.UserName + "ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                                Clean();
                                UsersList();
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Primero debes de seleccionar un Usuario de la lista para poder modificarlo ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }


        }

        // boton activar
        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && !string.IsNullOrEmpty(TxtWeigth.Text.Trim())
                    && CbUserRole.SelectedIndex > -1 && CbMembership.SelectedIndex > -1)
            {
                DialogResult r = MessageBox.Show("¿Está seguro de Activar al Usuario " + MyUser.UserName + "?",
                                                                   "???",
                                                                   MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyUser.Activate())
                    {
                        MessageBox.Show("El usuario " + MyUser.UserName + " ha sido activado correctamente.", "!!!", MessageBoxButtons.OK);
                        Clean();
                        UsersList();
                    }

                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar un usuario de la lista para poder activarlo.", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // boton inactivar
        private void Btninactive_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
            !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && !string.IsNullOrEmpty(TxtWeigth.Text.Trim())
                  && CbUserRole.SelectedIndex > -1 && CbMembership.SelectedIndex > -1)
            {
                DialogResult r = MessageBox.Show("¿Está seguro de Activar al Usuario " + MyUser.UserName + "?",
                                                                   "???",
                                                                   MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyUser.Inactivate())
                    {
                        MessageBox.Show("El usuario " + MyUser.UserName + "ha sido Inactivado correctamente.", "!!!", MessageBoxButtons.OK);
                        Clean();
                        UsersList();
                    }

                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar un usuario de la lista para poder inactivarlo.", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        #endregion

        #region Cargar Listas
        //Cargamos la lista de usuarios y los mostramos dependiendo de  su estado activo o inactivo 
        private void UsersList()
        {
            UserList = new DataTable();

            string searchfilter = "";
            if (!string.IsNullOrEmpty(TxtSearch.Text.Trim()) && TxtSearch.Text.Count() >= 3)
            {

                searchfilter = TxtSearch.Text.Trim();
            }


            if (CBActive.Checked)
            {

                UserList = MyUser.ActiveList(searchfilter);
            }
            else
            {
                UserList = MyUser.InactiveList(searchfilter);
            }
            DgUserList.DataSource = UserList;
        }

        //llenamos el combobox con la informacion del role de usuario
        private void UsersRole()
        {
            Logic.Models.UserRole Rol = new Logic.Models.UserRole();

            DataTable dt = new DataTable();
            dt = Rol.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbUserRole.ValueMember = "ID";
                CbUserRole.DisplayMember = "Descrip";
                CbUserRole.DataSource = dt;
                CbUserRole.SelectedIndex = -1;

            }
        }
        private void Merbership()
        {
            Logic.Models.Membership memberships = new Logic.Models.Membership();

            DataTable dt = new DataTable();
            dt = memberships.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbMembership.ValueMember = "ID";
                CbMembership.DisplayMember = "Descrip";
                CbMembership.DataSource = dt;
                CbMembership.SelectedIndex = -1;

            }
        }


        #endregion

        #region Check  
        private void CBActive_CheckedChanged(object sender, EventArgs e)
        {
            CheckStatus();
        }

        private void CheckStatus()
        {
            Clean();
            if (CBActive.Checked)
            {
                Btninactive.Visible = true;
                btnActivate.Visible = false;
            }
            else
            {
                btnActivate.Visible = true;
                Btninactive.Visible = false;
            }
            UsersList();

        }
        #endregion

        #region Limpiar
        private void Clean()
        {
            txtEmail.Clear();
            TxtHeight.Clear();
            TxtPhoneNumber.Clear();
            TxtSearch.Clear();
            TxtPassword.Clear();
            TxtUserID.Clear();
            TxtUserName.Clear();
            TxtWeigth.Clear();
            CbMembership.SelectedIndex = -1;
            CbUserRole.SelectedIndex = -1;

        }
        #endregion

        #region validar login 
        private bool Validate(bool SkipPassword = false)
        {
            bool R = false;
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()) && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && !string.IsNullOrEmpty(TxtWeigth.Text.Trim())
                    && CbUserRole.SelectedIndex > -1 && CbMembership.SelectedIndex > -1)
            {
                if (SkipPassword)
                {
                    R = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                    {
                        R = true;
                    }
                    else
                    {
                        MessageBox.Show("Se Debe Ingresar una contraseña para el usuario", "Faltante de Datos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        TxtPassword.Focus();
                        return false;
                    }
                }


            }
            else
            {
                if (string.IsNullOrEmpty(TxtUserName.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar un nombre para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    TxtUserName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar un email  para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtHeight.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar una altura  para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    TxtHeight.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtWeigth.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar un peso  para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    TxtWeigth.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar un numero telefonico  para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    TxtPhoneNumber.Focus();
                    return false;
                }
                if (CbUserRole.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar un rol para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    CbUserRole.Focus();
                    return false;
                }
                if (CbMembership.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar una membresia para el usuario", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    CbMembership.Focus();
                    return false;
                }

            }
            return R;



        }
        #endregion

        #region keypress
        private void TxtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void TxtWeigth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void TxtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        #endregion

        #region CellClick
        private void DgUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgUserList.SelectedRows.Count == 1)
            {

                DataGridViewRow Row = DgUserList.SelectedRows[0];


                int IDUSER = Convert.ToInt32(Row.Cells["CUserID"].Value);

                MyUser = new Logic.Models.User();


                MyUser.UserID = IDUSER;



                MyUser = MyUser.UserSearchID();

                if (MyUser != null && MyUser.UserID > 0)
                {
                    TxtUserID.Text = Convert.ToString(MyUser.UserID);
                    txtEmail.Text = MyUser.UserEmail;
                    TxtUserName.Text = MyUser.UserName;
                    TxtHeight.Text = Convert.ToString(MyUser.Height);
                    TxtWeigth.Text = Convert.ToString(MyUser.Weight);
                    TxtPhoneNumber.Text = Convert.ToString(MyUser.PhoneNumber);
                    CbMembership.SelectedValue = MyUser.membership.MerbershipID;
                    CbUserRole.SelectedValue = MyUser.MyUserRole.UserRoleID;
                }
            }
        }

        #endregion
    }
}
