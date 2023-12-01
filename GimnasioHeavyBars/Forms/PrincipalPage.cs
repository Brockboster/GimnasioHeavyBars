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
    public partial class PrincipalPage : Form
    {
        //Creamos un contador para estableces valores y poder cerrar los formularios mas antiguos 
        private int formCount = 0; // Inicializa el contador de formularios abiertos
        private int maxFormCount = 3;
        public PrincipalPage()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        //Cierra la aplicacion 
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //minimiza la aplicacion
        private void BtnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Cierra los formularios antiguos para acelerar la aplicacion
        private void Cerrador()
        {
            foreach (Form form in this.MdiChildren)
            {
                if (!form.Visible)
                {
                    form.Close();
                    return;
                }
            }
        }
        //Creamos un contador para establecer un tiempo exacto 
        private Timer timer;
        private void PrincipalPage_Load(object sender, EventArgs e)
        {
            Fecha.Text = DateTime.Now.ToLongDateString();
            timer = new Timer();
            timer.Interval = 60000; // este intervalo se establece en  milisegundos (1 minuto = 60000 ms)
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            ActualizarFecha();

            switch (Globals.MyGlobalUser.MyUserRole.UserRoleID)
            {
                case 1:
                    //Es un admin no se debe hacer bloqueo por el momento
                    break;
                case 2:
                    //Es personal lo cual se registren ciertos datos

                    BtnMe.Visible = false;
                    break;
                case 3:
                    //Es cliente lo cual se registren ciertos datos
                    BtnUsuarios.Visible = false;
                    BtnCoachs.Visible = false;
                    BtnExercise.Visible = false;
                    BtnRutine.Visible = false;

                    break;
            }
        }
        //actualizamos el tiempo actual
        private void Timer_Tick(object sender, EventArgs e)
        {
            ActualizarFecha();
        }
        //actualizamos la fecha actual 
        private void ActualizarFecha()
        {
            Fecha1.Text = DateTime.Now.ToString("hh:mm tt");
        }
        //se establece y se abre el formualario de los usuarios en el panel visible 
        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            FrmUsers Users = new FrmUsers();
            Users.TopLevel = false;
            Users.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(Users);
            Users.Show();
        }

        private void BtnMe_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            FrmMe me = new FrmMe();
            me.TopLevel = false;
            me.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(me);
            me.Show();
        }

       

        private void exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Application.Exit();
        }

        private void BtnExercise_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            FrmExercise exercise = new FrmExercise();
            exercise.TopLevel = false;
            exercise.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(exercise);
            exercise.Show();
        }

        private void BtnCoachs_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            FrmCoaches Coaches = new FrmCoaches();
            Coaches.TopLevel = false;
            Coaches.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(Coaches);
            Coaches.Show();
        }

        private void Btntraining_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            Frmtraining training = new Frmtraining();
            training.TopLevel = false;
            training.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(training);
            training.Show();
        }

        private void BtnRutine_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            FrmRutine schedule = new FrmRutine();
            schedule.TopLevel = false;
            schedule.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(schedule);
            schedule.Show();
        }

        private void BtnEntrenamiento_Click(object sender, EventArgs e)
        {
            if (formCount >= maxFormCount)
            {
                Cerrador();
            }
            FrmMyTrainings  MyTrainings = new FrmMyTrainings();
            MyTrainings.TopLevel = false;
            MyTrainings.Size = PanelVisible.Size;
            PanelVisible.Controls.Clear();
            PanelVisible.Controls.Add(MyTrainings);
            MyTrainings.Show();
        }
    }
}
