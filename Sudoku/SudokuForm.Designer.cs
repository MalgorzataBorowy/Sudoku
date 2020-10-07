namespace Sudoku
{
    partial class SudokuForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Check = new System.Windows.Forms.Button();
            this.btn_Solve = new System.Windows.Forms.Button();
            this.btn_NewGame = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gbGame = new System.Windows.Forms.GroupBox();
            this.gbSolver = new System.Windows.Forms.GroupBox();
            this.rbGame = new System.Windows.Forms.RadioButton();
            this.rbSolver = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbGame.SuspendLayout();
            this.gbSolver.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 450);
            this.panel1.TabIndex = 0;
            // 
            // btn_Check
            // 
            this.btn_Check.Location = new System.Drawing.Point(6, 107);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(142, 37);
            this.btn_Check.TabIndex = 1;
            this.btn_Check.Text = "Check";
            this.btn_Check.UseVisualStyleBackColor = true;
            this.btn_Check.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btn_Solve
            // 
            this.btn_Solve.Location = new System.Drawing.Point(6, 150);
            this.btn_Solve.Name = "btn_Solve";
            this.btn_Solve.Size = new System.Drawing.Size(142, 37);
            this.btn_Solve.TabIndex = 2;
            this.btn_Solve.Text = "Solve";
            this.btn_Solve.UseVisualStyleBackColor = true;
            this.btn_Solve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btn_NewGame
            // 
            this.btn_NewGame.Location = new System.Drawing.Point(6, 21);
            this.btn_NewGame.Name = "btn_NewGame";
            this.btn_NewGame.Size = new System.Drawing.Size(142, 37);
            this.btn_NewGame.TabIndex = 3;
            this.btn_NewGame.Text = "New Game";
            this.btn_NewGame.UseVisualStyleBackColor = true;
            this.btn_NewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(6, 64);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(142, 37);
            this.btn_Clear.TabIndex = 4;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Your time:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Blank";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBlank_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(78, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 37);
            this.button2.TabIndex = 8;
            this.button2.Text = "Solve";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSolveCustom_Click);
            // 
            // gbGame
            // 
            this.gbGame.Controls.Add(this.btn_NewGame);
            this.gbGame.Controls.Add(this.btn_Check);
            this.gbGame.Controls.Add(this.label2);
            this.gbGame.Controls.Add(this.btn_Solve);
            this.gbGame.Controls.Add(this.label1);
            this.gbGame.Controls.Add(this.btn_Clear);
            this.gbGame.Location = new System.Drawing.Point(498, 71);
            this.gbGame.Name = "gbGame";
            this.gbGame.Size = new System.Drawing.Size(153, 265);
            this.gbGame.TabIndex = 9;
            this.gbGame.TabStop = false;
            this.gbGame.Text = "Game";
            // 
            // gbSolver
            // 
            this.gbSolver.Controls.Add(this.button1);
            this.gbSolver.Controls.Add(this.button2);
            this.gbSolver.Location = new System.Drawing.Point(498, 71);
            this.gbSolver.Name = "gbSolver";
            this.gbSolver.Size = new System.Drawing.Size(153, 69);
            this.gbSolver.TabIndex = 10;
            this.gbSolver.TabStop = false;
            this.gbSolver.Text = "Solver";
            // 
            // rbGame
            // 
            this.rbGame.AutoSize = true;
            this.rbGame.Checked = true;
            this.rbGame.Location = new System.Drawing.Point(6, 21);
            this.rbGame.Name = "rbGame";
            this.rbGame.Size = new System.Drawing.Size(67, 21);
            this.rbGame.TabIndex = 11;
            this.rbGame.TabStop = true;
            this.rbGame.Text = "Game";
            this.rbGame.UseVisualStyleBackColor = true;
            this.rbGame.CheckedChanged += new System.EventHandler(this.rbModeChanged);
            // 
            // rbSolver
            // 
            this.rbSolver.AutoSize = true;
            this.rbSolver.Location = new System.Drawing.Point(79, 21);
            this.rbSolver.Name = "rbSolver";
            this.rbSolver.Size = new System.Drawing.Size(69, 21);
            this.rbSolver.TabIndex = 12;
            this.rbSolver.Text = "Solver";
            this.rbSolver.UseVisualStyleBackColor = true;
            this.rbSolver.CheckedChanged += new System.EventHandler(this.rbModeChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbGame);
            this.groupBox3.Controls.Add(this.rbSolver);
            this.groupBox3.Location = new System.Drawing.Point(498, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 53);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select mode";
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 476);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbSolver);
            this.Controls.Add(this.gbGame);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SudokuForm";
            this.Text = "Sudoku";
            this.gbGame.ResumeLayout(false);
            this.gbGame.PerformLayout();
            this.gbSolver.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.Button btn_Solve;
        private System.Windows.Forms.Button btn_NewGame;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox gbGame;
        private System.Windows.Forms.GroupBox gbSolver;
        private System.Windows.Forms.RadioButton rbGame;
        private System.Windows.Forms.RadioButton rbSolver;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

