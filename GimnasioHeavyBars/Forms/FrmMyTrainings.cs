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
    public partial class FrmMyTrainings : Form
    {
        private DataTable trainingList { get; set; }
        private Logic.Models.Training Mytraining { get; set; }

        public FrmMyTrainings()
        {
            Mytraining = new Logic.Models.Training();
            InitializeComponent();


        }

        private void UsersList()
        {
            trainingList = new DataTable();

            int IDUser = Globals.MyGlobalUser.UserID;
             
                trainingList = Mytraining.listar(IDUser);
          
            DgTraininglist.DataSource = trainingList;
        }

        private void FrmMyTrainings_Load(object sender, EventArgs e)
        {
            UsersList();
        }
    }
}
