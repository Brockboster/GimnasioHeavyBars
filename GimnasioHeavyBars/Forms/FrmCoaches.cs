using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic.Models;

namespace GimnasioHeavyBars.Forms
{
    public partial class FrmCoaches : Form
    {
        #region Atributos 
        private DataTable CoachsList { get; set; }
        private Coach MyCoach { get; set; }
        #endregion

        #region ctor
        public FrmCoaches()
        {
            MyCoach = new Coach();
            InitializeComponent();
        }
        #endregion

        #region Evento load 
        private void FrmCoaches_Load(object sender, EventArgs e)
        {
            CoachList();
            Coachtype();

        }
        #endregion

        #region Limpiador 
        private void Clean()
        {
            TxtSearch.Clear();
            TxtCoachID.Clear();
            TxtCoachName.Clear();
            TxtPhoneNumber.Clear();
            txtEmail.Clear();
            CbType.SelectedIndex = -1;
        }
        #endregion

        #region Botones

        //Cerrar Formulario
        private void BtnSkipeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //boton agregar 
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCoachName.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && CbType.SelectedIndex > -1)
            {

                bool Emailok;
                bool Phoneok;

                MyCoach = new Logic.Models.Coach();

                MyCoach.CoachName = TxtCoachName.Text.Trim();
                MyCoach.CoachPhoneNumber = Convert.ToInt32(TxtPhoneNumber.Text.Trim());
                MyCoach.CoachEmail = txtEmail.Text.Trim();
                MyCoach.MyCoachtipe.CoachTypeID = Convert.ToInt32(CbType.SelectedValue);




                Emailok = MyCoach.ConsultCoachEmail();
                Phoneok = MyCoach.ConsultPhonenumber();
                if (Emailok == false && Phoneok == false)
                {
                    string msg = string.Format("¿Estás seguro de agregar al usuario {0}?", MyCoach.CoachName);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {

                        bool ok = MyCoach.Add();

                        if (ok)
                        {
                            MessageBox.Show("Entrenador guardado correctamente!", "confirmación", MessageBoxButtons.OK);

                            Clean();

                            CoachList();

                        }
                        else
                        {
                            MessageBox.Show("El Entrenador no se guardado de forma correcta!", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }



                    }


                }
                else
                {

                    if (Emailok)
                    {
                        MessageBox.Show("Ya existe un Entrenador con el correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }
                    if (Phoneok)
                    {
                        MessageBox.Show("Ya existe un Entrenador con el numero telefonico digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                }


            }
            else
            {
                if (string.IsNullOrEmpty(TxtCoachName.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar un nombre para el cliente", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    TxtCoachName.Focus();

                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Debes digitar un email  para el entrenador", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    txtEmail.Focus();

                }
                if (string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    MessageBox.Show("Debes ingresar un numero correspondiente al  entrenador ", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    TxtPhoneNumber.Focus();

                }
                if (CbType.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar el tipo de entrenador", "Error de Validacion ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    CbType.Focus();

                }
            }
        }
        //boton eliminar 
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCoachName.Text.Trim()) &&
             !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && CbType.SelectedIndex > -1)
            {


                DialogResult r = MessageBox.Show("¿Estas seguro de Eliminar al entrenador ?"
                                                 , MyCoach.CoachName,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyCoach.Delete())
                    {
                        MessageBox.Show("El entrenador" + MyCoach.CoachName + "ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);

                    }
                    Clean();
                    CoachList();

                }

            }
            else
            {
                MessageBox.Show("Primero debes de seleccionar un entrenador de la lista para eliminar", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //boton editar
        private void BtnEdit_Click(object sender, EventArgs e)
        {

            bool Emailok;
            bool Phoneok;

            if (!string.IsNullOrEmpty(TxtCoachName.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && CbType.SelectedIndex > -1)
            {

                MyCoach.CoachName = TxtCoachName.Text.Trim();
                MyCoach.CoachPhoneNumber = Convert.ToInt32(TxtPhoneNumber.Text.Trim());
                MyCoach.CoachEmail = txtEmail.Text.Trim();
                MyCoach.MyCoachtipe.CoachTypeID = Convert.ToInt32(CbType.SelectedValue);

                Emailok = MyCoach.ConsultCoachEmail();
                Phoneok = MyCoach.ConsultPhonenumber();
                if (Emailok == false && Phoneok == false)
                {
                    DialogResult Answer = MessageBox.Show("¿Este u otro Entrenador  tiene registrado este numero o Correo electronico, aun deseas modificarlo?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (Answer == DialogResult.Yes)
                    {

                        if (MyCoach.Update())
                        {
                            MessageBox.Show("El Entrenador " + MyCoach.CoachName + "ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            Clean();
                            CoachList();
                        }
                        else
                        {
                            MessageBox.Show("El Entrenador no ha sido modificado correctamente", ":C", MessageBoxButtons.OK);
                            Clean();
                            CoachList();
                        }
                    }

                }

                else
                {
                    DialogResult Answer = MessageBox.Show("¿Está seguro de modificar este entrenador ?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (Answer == DialogResult.Yes)
                    {

                        if (MyCoach.Update())
                        {
                            MessageBox.Show("El Cliente ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            Clean();
                            CoachList();
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Primero debes de seleccionar un entrenador de la lista para poder modificarlo ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        // boton limpiar
        private void BtnClean_Click(object sender, EventArgs e)
        {
            Clean();
        }
        //boton inactivar 
        private void BtnInactive_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCoachName.Text.Trim()) &&
              !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && CbType.SelectedIndex > -1)
            {
                DialogResult r = MessageBox.Show("¿Está seguro de Inactivar al Entrenador " + MyCoach.CoachName + "?",
                                                                   "???",
                                                                   MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyCoach.Inactivate())
                    {
                        MessageBox.Show("El Entrenador " + MyCoach.CoachName + " ha sido inactivado correctamente.", "!!!", MessageBoxButtons.OK);
                        Clean();
                        CoachList();
                    }

                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar un Entrenador  de la lista para poder activarlo.", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //boton activar
        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCoachName.Text.Trim()) &&
               !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()) && CbType.SelectedIndex > -1)
            {
                DialogResult r = MessageBox.Show("¿Está seguro de Activar al Entrenador " + MyCoach.CoachName + "?",
                                                                   "???",
                                                                   MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyCoach.Activate())
                    {
                        MessageBox.Show("El Entrenador " + MyCoach.CoachName + " ha sido activado correctamente.", "!!!", MessageBoxButtons.OK);
                        Clean();
                        CoachList();
                    }

                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar un Entrenador  de la lista para poder activarlo.", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }





        #endregion

        #region Listas 
        private void CoachList()
        {
            CoachsList = new DataTable();

            string searchfilter = "";
            if (!string.IsNullOrEmpty(TxtSearch.Text.Trim()) && TxtSearch.Text.Count() >= 3)
            {

                searchfilter = TxtSearch.Text.Trim();
            }


            if (CBActive.Checked)
            {

                CoachsList = MyCoach.ActiveList(searchfilter);
            }
            else
            {
                CoachsList = MyCoach.InactiveList(searchfilter);
            }
            DgCoach.DataSource = CoachsList;
        }

        //llenamos el combobox con la informacion del role de usuario
        private void Coachtype()
        {
            Logic.Models.CoachType Type = new Logic.Models.CoachType();

            DataTable dt = new DataTable();
            dt = Type.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbType.ValueMember = "ID";
                CbType.DisplayMember = "Descrip";
                CbType.DataSource = dt;
                CbType.SelectedIndex = -1;

            }
        }



        #endregion

        #region Keypress 
        private void TxtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                BtnInactive.Visible = true;
                btnActivate.Visible = false;
            }
            else
            {
                btnActivate.Visible = true;
                BtnInactive.Visible = false;
            }
            CoachList();

        }
        #endregion

        #region Cellclick
        private void DgCoach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgCoach.SelectedRows.Count == 1)
            {

                DataGridViewRow Row = DgCoach.SelectedRows[0];


                int IDCoach = Convert.ToInt32(Row.Cells["CCoachID"].Value);

                MyCoach = new Logic.Models.Coach();


                MyCoach.CoachID = IDCoach;



                MyCoach = MyCoach.CoachSearchID();

                if (MyCoach != null && MyCoach.CoachID > 0)
                {
                    TxtCoachID.Text = Convert.ToString(MyCoach.CoachID);
                    txtEmail.Text = MyCoach.CoachEmail;
                    TxtPhoneNumber.Text = Convert.ToString(MyCoach.CoachPhoneNumber);
                    CbType.SelectedValue = MyCoach.CoachID;
                    TxtCoachName.Text = MyCoach.CoachName;
                }
            }
        }

        #endregion

    }
}
