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
    public partial class FrmExercise : Form
    {
        #region atributos
        private Logic.Models.Exercise MyExercise { get; set; }
        private DataTable ExerciseList { get; set; }
        #endregion

        #region Ctor
        public FrmExercise()
        {
            InitializeComponent();
            MyExercise = new Logic.Models.Exercise();
        }

        #endregion

        #region Load
        private void FrmExercise_Load(object sender, EventArgs e)
        {
            ExercisesList();
        }
        #endregion

        #region Cargar Listas
        //Cargamos la lista de usuarios y los mostramos dependiendo de  su estado activo o inactivo 
        private void ExercisesList()
        {
            ExerciseList = new DataTable();

            ExerciseList = MyExercise.ListExercise();

            DgExerciseList.DataSource = ExerciseList;
        }
        #endregion

        #region Limpiador 
        private void Clean()
        {

            TxtSearch.Clear();
            TxtExerciseID.Clear();
            TxtExerciseName.Clear();
            TxtADescription.Clear();


        }


        #endregion

        #region Cellclick
        private void DgExerciseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgExerciseList.SelectedRows.Count == 1)
            {

                DataGridViewRow Row = DgExerciseList.SelectedRows[0];


                int ID = Convert.ToInt32(Row.Cells["CExerciseID"].Value);

                MyExercise = new Logic.Models.Exercise();


                MyExercise.ExerciseID = ID;



                MyExercise = MyExercise.ExerciseSearchID();

                if (MyExercise != null && MyExercise.ExerciseID > 0)
                {
                    TxtExerciseID.Text = Convert.ToString(MyExercise.ExerciseID);
                    TxtExerciseName.Text = MyExercise.NameExercise;
                    TxtADescription.Text = MyExercise.Description;


                }
            }
        }
        #endregion

        #region Botones 
        private void BtnClean_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void BtnSkipeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            MyExercise = new Logic.Models.Exercise();
            MyExercise.NameExercise = TxtExerciseName.Text.Trim();
            MyExercise.Description = TxtADescription.Text.Trim();

            string msg = string.Format("¿Estás seguro de agregar al usuario {0}?", MyExercise.NameExercise);

            DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {

                bool ok = MyExercise.Add();

                if (ok)
                {
                    MessageBox.Show("Ejercicio correctamente!", "confirmación", MessageBoxButtons.OK);

                    Clean();

                    ExercisesList();

                }
                else
                {
                    MessageBox.Show("El Ejercicio no se guardado de forma correcta!", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtExerciseName.Text.Trim()))
            {
                MyExercise.NameExercise = TxtExerciseName.Text.Trim();
                MyExercise.Description = TxtADescription.Text.Trim();

                if (MyExercise.Update())
                {
                    MessageBox.Show("El ejercicio   ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                    Clean();
                    ExercisesList();
                }
                else
                {
                    MessageBox.Show("El ejercicio no ha sido modificado correctamente", ":C", MessageBoxButtons.OK);
                    Clean();
                    ExercisesList();
                }

            }
            else
            {
                MessageBox.Show("Primero debes de seleccionar un ejercicio de la lista para poder modificarlo ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtExerciseName.Text.Trim()))
            {
                DialogResult r = MessageBox.Show("¿Estas seguro de Eliminar este ejercicio ?"
                                                , MyExercise.NameExercise,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    if (MyExercise.Delete())
                    {
                        MessageBox.Show("El usuario Ejercicio ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);

                    }
                    Clean();
                    ExercisesList();

                }

            }
            else
            {
                MessageBox.Show("Primero debes de seleccionar un ejercicio de la lista para poder eliminarlo ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }

        #endregion

        #region Validaciones
        //validamos la inserccion correcta de datos en los campos respectivos
        private void TxtExerciseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo Solo Acepta Letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #endregion
    }
}
