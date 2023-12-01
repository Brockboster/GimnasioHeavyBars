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
    public partial class Frmtraining : Form
    {
        private Logic.Models.User MyUser { get; set; }
        private Logic.Models.Training MyTraining { get; set; }

        private Logic.Models.Routine MyRoutine { get; set; }


        public Frmtraining()
        {
            InitializeComponent();
            MyRoutine = new Logic.Models.Routine();
            MyUser = new Logic.Models.User();
            MyTraining = new Logic.Models.Training();
        }

        private void Frmtraining_Load(object sender, EventArgs e)
        {
            UsersList();
            RoutineList();
            TrainingtypeList();
            Coach();
        }

        #region cargar 
        private void UsersList()
        {
            Logic.Models.User list = new Logic.Models.User();

            DataTable dt = new DataTable();
            dt = list.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbUser.ValueMember = "ID";
                CbUser.DisplayMember = "Descrip";
                CbUser.DataSource = dt;
                CbUser.SelectedIndex = -1;

            }
        }
        private void RoutineList()
        {
            Logic.Models.Routine list = new Logic.Models.Routine();

            DataTable dt = new DataTable();
            dt = list.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbRutina.ValueMember = "ID";
                CbRutina.DisplayMember = "Descrip";
                CbRutina.DataSource = dt;
                CbRutina.SelectedIndex = -1;

            }
        }
        private void TrainingtypeList()
        {
            Logic.Models.TrainingType list = new Logic.Models.TrainingType();

            DataTable dt = new DataTable();
            dt = list.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbTrainingType.ValueMember = "ID";
                CbTrainingType.DisplayMember = "Descrip";
                CbTrainingType.DataSource = dt;
                CbTrainingType.SelectedIndex = -1;

            }
        }
        private void Coach()
        {
            Logic.Models.Coach list = new Logic.Models.Coach();

            DataTable dt = new DataTable();
            dt = list.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                cbentrenador.ValueMember = "ID";
                cbentrenador.DisplayMember = "Descrip";
                cbentrenador.DataSource = dt;
                cbentrenador.SelectedIndex = -1;

            }
        }




        #endregion


        private void Clean()
        {
            TxtNotes.Clear();
            CbRutina.SelectedIndex  =-1;
            CbUser.SelectedIndex = -1;
            CbTrainingType.SelectedIndex = -1;
            cbentrenador.SelectedIndex = -1;


        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CbTrainingType.SelectedIndex > -1 && CbRutina.SelectedIndex > -1 && CbUser.SelectedIndex > -1 
                )
            {
                MyTraining.Notes = TxtNotes.Text;
                MyTraining.MYUser.UserID = Convert.ToInt32(CbUser.SelectedValue);
                MyTraining.MYtrainingType.TrainingTypeID= Convert.ToInt32(CbTrainingType.SelectedValue);
                MyTraining.MyCoach.CoachID = Convert.ToInt32(cbentrenador.SelectedValue);
                MyTraining.MyRoutine.RoutineID= Convert.ToInt32(CbRutina.SelectedValue);
                MyTraining.Date = dateTimePicker1.Value;
                if (MyTraining.add())
                {
                    MessageBox.Show("El entrenamiento ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                    Clean();
                    UsersList();
                }
                else
                {
                    MessageBox.Show("Entrenamiento no ha sido agregado", ":C", MessageBoxButtons.OK);
                    Clean();
                    UsersList();
                }



            }
            else
            {
                MessageBox.Show("Primero debes de llenar los diferentes campos ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void BtnClean_Click(object sender, EventArgs e)
        {
            Clean();
        }
    }
}
