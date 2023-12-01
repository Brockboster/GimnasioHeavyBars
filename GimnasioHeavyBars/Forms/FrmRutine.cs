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
    public partial class FrmRutine : Form
    {

        private Logic.Models.Exercise MyExercise { get; set; }
        private Logic.Models.Routine MyRoutine { get; set; }

        private List<RoutineDetail> MyRoutineDetail { get; set; } = new List<RoutineDetail>();

        private DataTable ExerciseList { get; set; }

        public FrmRutine()
        {
            InitializeComponent();
            MyRoutine = new Logic.Models.Routine();
            MyRoutineDetail = new List<RoutineDetail>();
            MyExercise = new Logic.Models.Exercise();
        }

        #region Cargar dificultad
        //Cargamos la lista de dificultad 
        private void Difficulty()
        {
            Logic.Models.Difficulty difficulty = new Logic.Models.Difficulty();

            DataTable dt = new DataTable();
            dt = difficulty.List();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbDifficulty.ValueMember = "ID";
                CbDifficulty.DisplayMember = "Descrip";
                CbDifficulty.DataSource = dt;
                CbDifficulty.SelectedIndex = -1;

            }
        }


        #endregion

        #region Cargar Listas de ejercicios
        //Cargamos la lista de usuarios y los mostramos dependiendo de  su estado activo o inactivo 
        private void ExercisesList()
        {
            ExerciseList = new DataTable();

            ExerciseList = MyExercise.ListExercise();

            DgExerciseList.DataSource = ExerciseList;
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



                }
            }


        }


        #endregion


        #region limpiar 
        private void clear()

        {
            TxtExerciseID.Clear();
            TxtExerciseName.Clear();
            NMReps.Value = 0;
            NmSets.Value = 0;
            TxtRutineID.Clear();
            TxtRutineName.Clear();
            CbDifficulty.SelectedIndex = -1;
            Txthoras.Value = 0;
            Txtminutos.Value = 0;
            TxtDescription.Clear();
            DgExercise.ClearSelection();
            DgExerciseList.ClearSelection();


        }
        #endregion

        #region Botones


        private void BtnaddExercise_Click(object sender, EventArgs e)
        {
            //Agregamos los ejercicios al datagridview para poder crear la rutina
            {
                // Verificar si se ha seleccionado un ejercicio en DgExerciseList
                if (DgExerciseList.SelectedRows.Count > 0 && !string.IsNullOrEmpty(TxtExerciseID.Text.Trim()) &&
               !string.IsNullOrEmpty(TxtExerciseName.Text.Trim()))
                {
                    // Obtener la información del ejercicio seleccionado en DgExerciseList
                    DataGridViewRow ejercicioSeleccionado = DgExerciseList.SelectedRows[0];
                    int exerciseID = Convert.ToInt32(ejercicioSeleccionado.Cells["CExerciseID"].Value);
                    string exerciseName = ejercicioSeleccionado.Cells["CNameExercise"].Value.ToString();

                    // Obtener las repeticiones y series del ejercicio desde los NumericUpDown
                    int reps = Convert.ToInt32(NMReps.Value);
                    int sets = Convert.ToInt32(NmSets.Value);

                    // Verificar si el ejercicio ya existe en DgExercise
                    bool ejercicioExistente = false;
                    foreach (DataGridViewRow filaEjercicio in DgExercise.Rows)
                    {
                        if ((int)filaEjercicio.Cells["ExerciseID"].Value == exerciseID)
                        {
                            // Ejercicio ya existe, actualizar repeticiones y series
                            int repsExistente = (int)filaEjercicio.Cells["Reps"].Value;
                            int setsExistente = (int)filaEjercicio.Cells["Sets"].Value;

                            filaEjercicio.Cells["Reps"].Value = repsExistente + reps;
                            filaEjercicio.Cells["Sets"].Value = setsExistente + sets;

                            ejercicioExistente = true;
                            break;
                        }
                    }

                    if (!ejercicioExistente)
                    {
                        // Ejercicio no existe, agregar nueva fila a DgExercise
                        DgExercise.Rows.Add(exerciseID, exerciseName, reps, sets);
                    }

                    // Limpiar los controles después de agregar el ejercicio
                    cleancontrolers();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un ejercicio de la lista.");
                }
            }
        }


        private void cleancontrolers()
        {
            //Limpiamos los datos del datagridview que contiene los ejercicios
            TxtExerciseID.Clear();
            TxtExerciseName.Clear();
            NMReps.Value = 0;
            NmSets.Value = 0;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //Funciona para eliminar algun ejercicio que ya hemos agregado al datagrid de la rutina
            if (DgExercise.SelectedRows.Count == 1)
            {
                DataGridViewRow filaSeleccionada = DgExercise.SelectedRows[0];
                int exerciseID = Convert.ToInt32(filaSeleccionada.Cells["ExerciseID"].Value);
                int reps = Convert.ToInt32(filaSeleccionada.Cells["Reps"].Value);
                int sets = Convert.ToInt32(filaSeleccionada.Cells["Sets"].Value);

                DgExercise.Rows.Remove(filaSeleccionada);

                cleancontrolers();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //Funciona para agregar la rutina con los diferentes datos 
            if (!string.IsNullOrEmpty(TxtRutineName.Text.Trim()))
            {
                MyRoutine = new Logic.Models.Routine();
                MyRoutine.RoutineName = TxtRutineName.Text.Trim();
                MyRoutine.Description = TxtDescription.Text.Trim();
                MyRoutine.Difficulty.DifficultyID = Convert.ToInt32(CbDifficulty.SelectedValue);
                foreach (DataGridViewRow row in DgExercise.Rows)
                {
                    RoutineDetail newdetail = new RoutineDetail();
                    newdetail.Reps = Convert.ToInt32(row.Cells["ItemID"].Value);
                    newdetail.Sets = Convert.ToInt32(row.Cells["ItemID"].Value);
                    

                }


            }
            else
            {

            }


        }

        private void BtnClean_Click(object sender, EventArgs e)
        {
            //Limpiamos con el boton
            clear();
        }


        #endregion

        private void FrmRutine_Load(object sender, EventArgs e)
        {
            ExercisesList();
            Difficulty();
        }

        private void DgExerciseList_CellClick_1(object sender, DataGridViewCellEventArgs e)
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



                }
            }
        }

        private void BtnaddExercise_Click_1(object sender, EventArgs e)
        {
            //Agregamos los ejercicios al datagridview para poder crear la rutina
            {
                // Verificar si se ha seleccionado un ejercicio en DgExerciseList
                if (DgExerciseList.SelectedRows.Count > 0 && !string.IsNullOrEmpty(TxtExerciseID.Text.Trim()) &&
               !string.IsNullOrEmpty(TxtExerciseName.Text.Trim()))
                {
                    // Obtener la información del ejercicio seleccionado en DgExerciseList
                    DataGridViewRow ejercicioSeleccionado = DgExerciseList.SelectedRows[0];
                    int exerciseID = Convert.ToInt32(ejercicioSeleccionado.Cells["CExerciseID"].Value);
                    string exerciseName = ejercicioSeleccionado.Cells["CNameExercise"].Value.ToString();

                    // Obtener las repeticiones y series del ejercicio desde los NumericUpDown
                    int reps = Convert.ToInt32(NMReps.Value);
                    int sets = Convert.ToInt32(NmSets.Value);

                    // Verificar si el ejercicio ya existe en DgExercise
                    bool ejercicioExistente = false;
                    foreach (DataGridViewRow filaEjercicio in DgExercise.Rows)
                    {
                        if ((int)filaEjercicio.Cells["ExerciseID"].Value == exerciseID)
                        {
                            // Ejercicio ya existe, actualizar repeticiones y series
                            int repsExistente = (int)filaEjercicio.Cells["Reps"].Value;
                            int setsExistente = (int)filaEjercicio.Cells["Sets"].Value;

                            filaEjercicio.Cells["Reps"].Value = repsExistente + reps;
                            filaEjercicio.Cells["Sets"].Value = setsExistente + sets;

                            ejercicioExistente = true;
                            break;
                        }
                    }

                    if (!ejercicioExistente)
                    {
                        // Ejercicio no existe, agregar nueva fila a DgExercise
                        DgExercise.Rows.Add(exerciseID, exerciseName, reps, sets);
                    }

                    // Limpiar los controles después de agregar el ejercicio
                    cleancontrolers();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un ejercicio de la lista.");
                }
            }
        }

        private void BtnAdd_Click_1(object sender, EventArgs e)
        {
            //Funciona para agregar la rutina con los diferentes datos 
            if (!string.IsNullOrEmpty(TxtRutineName.Text.Trim()) && (CbDifficulty.SelectedIndex > -1) && (Txtminutos.Value > -1))
            {
                int horas = (int)Txthoras.Value;
                int minutos = (int)Txtminutos.Value;

                int duraciontotalenminutos = horas * 60 + minutos;

                MyRoutine = new Logic.Models.Routine();
                MyRoutine.RoutineName = TxtRutineName.Text.Trim();
                MyRoutine.Description = TxtDescription.Text.Trim();
                MyRoutine.Difficulty.DifficultyID = Convert.ToInt32(CbDifficulty.SelectedValue);
                MyRoutine.RoutineDuration = duraciontotalenminutos;
                foreach (DataGridViewRow row in DgExercise.Rows)
                {
                    RoutineDetail newdetail = new RoutineDetail();

                    newdetail.Sets = Convert.ToInt32(row.Cells["Sets"].Value);
                    newdetail.Reps = Convert.ToInt32(row.Cells["Reps"].Value);
                    newdetail.MyExercise.ExerciseID = Convert.ToInt32(row.Cells["ExerciseID"].Value);
                    MyRoutine.MyRoutineDetail.Add(newdetail);
                }
                if (MyRoutine.Add())
                {
                    MessageBox.Show("Rutina guardada correctamente", ":)", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("la rutina no se guardado de forma correcta!", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
            else
            {
                MessageBox.Show("Faltan algunos  Datos para guardar la rutina", ":Faltante de datos", MessageBoxButtons.OK);
            }
        }
    }
}
