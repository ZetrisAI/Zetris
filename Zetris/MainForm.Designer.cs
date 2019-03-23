namespace Zetris
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ScanTimer = new System.Windows.Forms.Timer(this.components);
            this.valueInstructions = new System.Windows.Forms.Label();
            this.panelGamepad = new System.Windows.Forms.Panel();
            this.buttonGamepad = new System.Windows.Forms.Label();
            this.labelGamepad = new System.Windows.Forms.Label();
            this.valueGamepadInputs = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.valueGameState = new System.Windows.Forms.Label();
            this.valueMisaMinoLevel = new System.Windows.Forms.ComboBox();
            this.valueMisaMinoStyle = new System.Windows.Forms.ComboBox();
            this.labelMisaMino = new System.Windows.Forms.Label();
            this.labelMisaMinoStyle = new System.Windows.Forms.Label();
            this.labelMisaMinoLevel = new System.Windows.Forms.Label();
            this.valuePuzzleLeague = new System.Windows.Forms.CheckBox();
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelConfig = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.valueFrametime = new System.Windows.Forms.Label();
            this.valueSkipped = new System.Windows.Forms.Label();
            this.labelTimings = new System.Windows.Forms.Label();
            this.labelFrames = new System.Windows.Forms.Label();
            this.labelSkipped = new System.Windows.Forms.Label();
            this.valueSpeed = new System.Windows.Forms.Label();
            this.panelGamepad.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanTimer
            // 
            this.ScanTimer.Enabled = true;
            this.ScanTimer.Interval = 1;
            this.ScanTimer.Tick += new System.EventHandler(this.Loop);
            // 
            // valueInstructions
            // 
            this.valueInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.valueInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueInstructions.Location = new System.Drawing.Point(5, 96);
            this.valueInstructions.Name = "valueInstructions";
            this.valueInstructions.Size = new System.Drawing.Size(148, 32);
            this.valueInstructions.TabIndex = 1;
            this.valueInstructions.Text = "?";
            this.valueInstructions.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panelGamepad
            // 
            this.panelGamepad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelGamepad.Controls.Add(this.valueSpeed);
            this.panelGamepad.Controls.Add(this.buttonGamepad);
            this.panelGamepad.Controls.Add(this.labelGamepad);
            this.panelGamepad.Controls.Add(this.valueGamepadInputs);
            this.panelGamepad.Controls.Add(this.labelSpeed);
            this.panelGamepad.Location = new System.Drawing.Point(9, 148);
            this.panelGamepad.Name = "panelGamepad";
            this.panelGamepad.Size = new System.Drawing.Size(158, 62);
            this.panelGamepad.TabIndex = 10;
            // 
            // buttonGamepad
            // 
            this.buttonGamepad.BackColor = System.Drawing.Color.Transparent;
            this.buttonGamepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGamepad.Location = new System.Drawing.Point(71, 5);
            this.buttonGamepad.Name = "buttonGamepad";
            this.buttonGamepad.Size = new System.Drawing.Size(82, 13);
            this.buttonGamepad.TabIndex = 1;
            this.buttonGamepad.Text = "?";
            this.buttonGamepad.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.buttonGamepad.Click += new System.EventHandler(this.buttonGamepad_Click);
            // 
            // labelGamepad
            // 
            this.labelGamepad.AutoSize = true;
            this.labelGamepad.BackColor = System.Drawing.Color.Transparent;
            this.labelGamepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGamepad.Location = new System.Drawing.Point(5, 5);
            this.labelGamepad.Name = "labelGamepad";
            this.labelGamepad.Size = new System.Drawing.Size(60, 13);
            this.labelGamepad.TabIndex = 1;
            this.labelGamepad.Text = "Gamepad";
            // 
            // valueGamepadInputs
            // 
            this.valueGamepadInputs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.valueGamepadInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGamepadInputs.Location = new System.Drawing.Point(6, 42);
            this.valueGamepadInputs.Name = "valueGamepadInputs";
            this.valueGamepadInputs.Size = new System.Drawing.Size(146, 13);
            this.valueGamepadInputs.TabIndex = 1;
            this.valueGamepadInputs.Text = "?";
            this.valueGamepadInputs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(6, 23);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelSpeed.TabIndex = 4;
            this.labelSpeed.Text = "Speed:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel1.Controls.Add(this.valueGameState);
            this.panel1.Controls.Add(this.valueMisaMinoLevel);
            this.panel1.Controls.Add(this.valueMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMino);
            this.panel1.Controls.Add(this.valueInstructions);
            this.panel1.Controls.Add(this.labelMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMinoLevel);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 132);
            this.panel1.TabIndex = 11;
            // 
            // valueGameState
            // 
            this.valueGameState.Location = new System.Drawing.Point(8, 74);
            this.valueGameState.Name = "valueGameState";
            this.valueGameState.Size = new System.Drawing.Size(145, 13);
            this.valueGameState.TabIndex = 15;
            this.valueGameState.Text = "?";
            this.valueGameState.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // valueMisaMinoLevel
            // 
            this.valueMisaMinoLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueMisaMinoLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueMisaMinoLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.valueMisaMinoLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueMisaMinoLevel.FormattingEnabled = true;
            this.valueMisaMinoLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.valueMisaMinoLevel.Location = new System.Drawing.Point(47, 50);
            this.valueMisaMinoLevel.MaxDropDownItems = 5;
            this.valueMisaMinoLevel.Name = "valueMisaMinoLevel";
            this.valueMisaMinoLevel.Size = new System.Drawing.Size(106, 21);
            this.valueMisaMinoLevel.TabIndex = 12;
            this.valueMisaMinoLevel.SelectedIndexChanged += new System.EventHandler(this.valueMisaMino_SelectedIndexChanged);
            // 
            // valueMisaMinoStyle
            // 
            this.valueMisaMinoStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueMisaMinoStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueMisaMinoStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.valueMisaMinoStyle.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueMisaMinoStyle.FormattingEnabled = true;
            this.valueMisaMinoStyle.Items.AddRange(new object[] {
            "T-Spin+",
            "T-Spin",
            "Combo",
            "No Hold",
            "4-Wide",
            "Downstack"});
            this.valueMisaMinoStyle.Location = new System.Drawing.Point(47, 26);
            this.valueMisaMinoStyle.MaxDropDownItems = 5;
            this.valueMisaMinoStyle.Name = "valueMisaMinoStyle";
            this.valueMisaMinoStyle.Size = new System.Drawing.Size(106, 21);
            this.valueMisaMinoStyle.TabIndex = 12;
            this.valueMisaMinoStyle.SelectedIndexChanged += new System.EventHandler(this.valueMisaMino_SelectedIndexChanged);
            // 
            // labelMisaMino
            // 
            this.labelMisaMino.AutoSize = true;
            this.labelMisaMino.BackColor = System.Drawing.Color.Transparent;
            this.labelMisaMino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMisaMino.Location = new System.Drawing.Point(5, 5);
            this.labelMisaMino.Name = "labelMisaMino";
            this.labelMisaMino.Size = new System.Drawing.Size(76, 13);
            this.labelMisaMino.TabIndex = 1;
            this.labelMisaMino.Text = "MisaMino AI";
            // 
            // labelMisaMinoStyle
            // 
            this.labelMisaMinoStyle.AutoSize = true;
            this.labelMisaMinoStyle.Location = new System.Drawing.Point(5, 30);
            this.labelMisaMinoStyle.Name = "labelMisaMinoStyle";
            this.labelMisaMinoStyle.Size = new System.Drawing.Size(33, 13);
            this.labelMisaMinoStyle.TabIndex = 4;
            this.labelMisaMinoStyle.Text = "Style:";
            // 
            // labelMisaMinoLevel
            // 
            this.labelMisaMinoLevel.AutoSize = true;
            this.labelMisaMinoLevel.Location = new System.Drawing.Point(5, 54);
            this.labelMisaMinoLevel.Name = "labelMisaMinoLevel";
            this.labelMisaMinoLevel.Size = new System.Drawing.Size(36, 13);
            this.labelMisaMinoLevel.TabIndex = 4;
            this.labelMisaMinoLevel.Text = "Level:";
            // 
            // valuePuzzleLeague
            // 
            this.valuePuzzleLeague.AutoSize = true;
            this.valuePuzzleLeague.Location = new System.Drawing.Point(5, 21);
            this.valuePuzzleLeague.Name = "valuePuzzleLeague";
            this.valuePuzzleLeague.Size = new System.Drawing.Size(151, 17);
            this.valuePuzzleLeague.TabIndex = 2;
            this.valuePuzzleLeague.Text = "Auto Puzzle League Menu";
            this.valuePuzzleLeague.UseVisualStyleBackColor = true;
            this.valuePuzzleLeague.CheckedChanged += new System.EventHandler(this.valuePuzzleLeague_CheckedChanged);
            // 
            // labelDeveloper
            // 
            this.labelDeveloper.Location = new System.Drawing.Point(0, 0);
            this.labelDeveloper.Name = "labelDeveloper";
            this.labelDeveloper.Size = new System.Drawing.Size(100, 23);
            this.labelDeveloper.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel3.Controls.Add(this.valuePuzzleLeague);
            this.panel3.Controls.Add(this.labelConfig);
            this.panel3.Location = new System.Drawing.Point(9, 216);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(158, 41);
            this.panel3.TabIndex = 10;
            // 
            // labelConfig
            // 
            this.labelConfig.AutoSize = true;
            this.labelConfig.BackColor = System.Drawing.Color.Transparent;
            this.labelConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfig.Location = new System.Drawing.Point(5, 5);
            this.labelConfig.Name = "labelConfig";
            this.labelConfig.Size = new System.Drawing.Size(82, 13);
            this.labelConfig.TabIndex = 1;
            this.labelConfig.Text = "Configuration";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel2.Controls.Add(this.valueFrametime);
            this.panel2.Controls.Add(this.valueSkipped);
            this.panel2.Controls.Add(this.labelTimings);
            this.panel2.Controls.Add(this.labelFrames);
            this.panel2.Controls.Add(this.labelSkipped);
            this.panel2.Location = new System.Drawing.Point(9, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 60);
            this.panel2.TabIndex = 10;
            // 
            // valueFrametime
            // 
            this.valueFrametime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueFrametime.Location = new System.Drawing.Point(59, 40);
            this.valueFrametime.Name = "valueFrametime";
            this.valueFrametime.Size = new System.Drawing.Size(92, 13);
            this.valueFrametime.TabIndex = 2;
            this.valueFrametime.Text = "?";
            this.valueFrametime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueSkipped
            // 
            this.valueSkipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSkipped.Location = new System.Drawing.Point(87, 23);
            this.valueSkipped.Name = "valueSkipped";
            this.valueSkipped.Size = new System.Drawing.Size(64, 13);
            this.valueSkipped.TabIndex = 2;
            this.valueSkipped.Text = "?";
            this.valueSkipped.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTimings
            // 
            this.labelTimings.AutoSize = true;
            this.labelTimings.BackColor = System.Drawing.Color.Transparent;
            this.labelTimings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimings.Location = new System.Drawing.Point(5, 5);
            this.labelTimings.Name = "labelTimings";
            this.labelTimings.Size = new System.Drawing.Size(50, 13);
            this.labelTimings.TabIndex = 1;
            this.labelTimings.Text = "Timings";
            // 
            // labelFrames
            // 
            this.labelFrames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrames.Location = new System.Drawing.Point(5, 40);
            this.labelFrames.Name = "labelFrames";
            this.labelFrames.Size = new System.Drawing.Size(146, 13);
            this.labelFrames.TabIndex = 1;
            this.labelFrames.Text = "Frametime:";
            // 
            // labelSkipped
            // 
            this.labelSkipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkipped.Location = new System.Drawing.Point(5, 23);
            this.labelSkipped.Name = "labelSkipped";
            this.labelSkipped.Size = new System.Drawing.Size(146, 13);
            this.labelSkipped.TabIndex = 1;
            this.labelSkipped.Text = "Frames Skipped:";
            // 
            // valueSpeed
            // 
            this.valueSpeed.BackColor = System.Drawing.Color.Transparent;
            this.valueSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSpeed.Location = new System.Drawing.Point(120, 23);
            this.valueSpeed.Name = "valueSpeed";
            this.valueSpeed.Size = new System.Drawing.Size(33, 13);
            this.valueSpeed.TabIndex = 5;
            this.valueSpeed.Text = "100%";
            this.valueSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.valueSpeed.MouseWheel += new System.Windows.Forms.MouseEventHandler(valueSpeed_MouseWheel);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(177, 333);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelGamepad);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelGamepad.ResumeLayout(false);
            this.panelGamepad.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label valueInstructions;
        private System.Windows.Forms.Panel panelGamepad;
        private System.Windows.Forms.Label labelGamepad;
        private System.Windows.Forms.Label buttonGamepad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelMisaMino;
        private System.Windows.Forms.Label labelMisaMinoStyle;
        private System.Windows.Forms.Label labelMisaMinoLevel;
        private System.Windows.Forms.ComboBox valueMisaMinoStyle;
        private System.Windows.Forms.ComboBox valueMisaMinoLevel;
        private System.Windows.Forms.CheckBox valuePuzzleLeague;
        private System.Windows.Forms.Label labelDeveloper;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelConfig;
        private System.Windows.Forms.Label valueGameState;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTimings;
        private System.Windows.Forms.Label valueSkipped;
        private System.Windows.Forms.Label labelSkipped;
        private System.Windows.Forms.Label valueGamepadInputs;
        private System.Windows.Forms.Label valueFrametime;
        private System.Windows.Forms.Label labelFrames;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label valueSpeed;
    }
}

