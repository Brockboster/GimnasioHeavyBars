namespace GimnasioHeavyBars.Forms
{
    partial class FrmMyTrainings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DgTraininglist = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CTrainingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRoutineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRoutineDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNameExercise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CReps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgTraininglist)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgTraininglist
            // 
            this.DgTraininglist.AllowUserToAddRows = false;
            this.DgTraininglist.AllowUserToDeleteRows = false;
            this.DgTraininglist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgTraininglist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DgTraininglist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CTrainingID,
            this.CRoutineName,
            this.CDate,
            this.CRoutineDuration,
            this.CNameExercise,
            this.CSets,
            this.CReps});
            this.DgTraininglist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgTraininglist.Location = new System.Drawing.Point(3, 3);
            this.DgTraininglist.Name = "DgTraininglist";
            this.DgTraininglist.ReadOnly = true;
            this.DgTraininglist.RowHeadersVisible = false;
            this.DgTraininglist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgTraininglist.Size = new System.Drawing.Size(1251, 774);
            this.DgTraininglist.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.60556F));
            this.tableLayoutPanel1.Controls.Add(this.DgTraininglist, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1257, 780);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // CTrainingID
            // 
            this.CTrainingID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CTrainingID.DataPropertyName = "TrainingID";
            this.CTrainingID.HeaderText = "Cod.Entrenamiento";
            this.CTrainingID.Name = "CTrainingID";
            this.CTrainingID.ReadOnly = true;
            // 
            // CRoutineName
            // 
            this.CRoutineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CRoutineName.DataPropertyName = "RoutineName";
            this.CRoutineName.HeaderText = "Nombre de la rutina";
            this.CRoutineName.Name = "CRoutineName";
            this.CRoutineName.ReadOnly = true;
            // 
            // CDate
            // 
            this.CDate.DataPropertyName = "Date";
            this.CDate.HeaderText = "Fecha";
            this.CDate.Name = "CDate";
            this.CDate.ReadOnly = true;
            // 
            // CRoutineDuration
            // 
            this.CRoutineDuration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CRoutineDuration.DataPropertyName = "RoutineDuration";
            this.CRoutineDuration.HeaderText = "Duracion";
            this.CRoutineDuration.Name = "CRoutineDuration";
            this.CRoutineDuration.ReadOnly = true;
            // 
            // CNameExercise
            // 
            this.CNameExercise.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNameExercise.DataPropertyName = "NameExercise";
            this.CNameExercise.HeaderText = "Ejercicio";
            this.CNameExercise.Name = "CNameExercise";
            this.CNameExercise.ReadOnly = true;
            // 
            // CSets
            // 
            this.CSets.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CSets.DataPropertyName = "Sets";
            this.CSets.HeaderText = "Series";
            this.CSets.Name = "CSets";
            this.CSets.ReadOnly = true;
            // 
            // CReps
            // 
            this.CReps.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CReps.DataPropertyName = "Reps";
            this.CReps.HeaderText = "Repeticiones";
            this.CReps.Name = "CReps";
            this.CReps.ReadOnly = true;
            // 
            // FrmMyTrainings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 780);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMyTrainings";
            this.Text = "FrmMyTrainings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMyTrainings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgTraininglist)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgTraininglist;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTrainingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRoutineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRoutineDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNameExercise;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSets;
        private System.Windows.Forms.DataGridViewTextBoxColumn CReps;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}