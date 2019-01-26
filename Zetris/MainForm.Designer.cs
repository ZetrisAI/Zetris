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
            this.valueGamepadInputs = new System.Windows.Forms.Label();
            this.valueInstructions = new System.Windows.Forms.Label();
            this.panelGamepad = new System.Windows.Forms.Panel();
            this.buttonGamepad = new System.Windows.Forms.Label();
            this.labelGamepad = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.valueGameState = new System.Windows.Forms.Label();
            this.valueMisaMinoLevel = new System.Windows.Forms.ComboBox();
            this.valueMisaMinoStyle = new System.Windows.Forms.ComboBox();
            this.labelMisaMino = new System.Windows.Forms.Label();
            this.labelMisaMinoStyle = new System.Windows.Forms.Label();
            this.labelMisaMinoLevel = new System.Windows.Forms.Label();
            this.valueSkipped = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTimings = new System.Windows.Forms.Label();
            this.labelFrametime = new System.Windows.Forms.Label();
            this.labelSkipped = new System.Windows.Forms.Label();
            this.valueFrametime = new System.Windows.Forms.Label();
            this.ScanTimer2 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer3 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer4 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer5 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer6 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer7 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer8 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer9 = new System.Windows.Forms.Timer(this.components);
            this.ScanTimer10 = new System.Windows.Forms.Timer(this.components);
            this.panelGamepad.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanTimer
            // 
            this.ScanTimer.Enabled = true;
            this.ScanTimer.Interval = 1;
            this.ScanTimer.Tick += new System.EventHandler(this.Loop);
            // 
            // valueGamepadInputs
            // 
            this.valueGamepadInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGamepadInputs.Location = new System.Drawing.Point(6, 28);
            this.valueGamepadInputs.Name = "valueGamepadInputs";
            this.valueGamepadInputs.Size = new System.Drawing.Size(146, 13);
            this.valueGamepadInputs.TabIndex = 1;
            this.valueGamepadInputs.Text = "?";
            this.valueGamepadInputs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // valueInstructions
            // 
            this.valueInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.valueInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueInstructions.Location = new System.Drawing.Point(5, 95);
            this.valueInstructions.Name = "valueInstructions";
            this.valueInstructions.Size = new System.Drawing.Size(148, 32);
            this.valueInstructions.TabIndex = 1;
            this.valueInstructions.Text = "?";
            this.valueInstructions.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panelGamepad
            // 
            this.panelGamepad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelGamepad.Controls.Add(this.buttonGamepad);
            this.panelGamepad.Controls.Add(this.labelGamepad);
            this.panelGamepad.Controls.Add(this.valueGamepadInputs);
            this.panelGamepad.Location = new System.Drawing.Point(9, 148);
            this.panelGamepad.Name = "panelGamepad";
            this.panelGamepad.Size = new System.Drawing.Size(158, 49);
            this.panelGamepad.TabIndex = 10;
            // 
            // buttonGamepad
            // 
            this.buttonGamepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGamepad.Location = new System.Drawing.Point(71, 5);
            this.buttonGamepad.Name = "buttonGamepad";
            this.buttonGamepad.Size = new System.Drawing.Size(82, 13);
            this.buttonGamepad.TabIndex = 15;
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
            this.valueGameState.Location = new System.Drawing.Point(5, 77);
            this.valueGameState.Name = "valueGameState";
            this.valueGameState.Size = new System.Drawing.Size(148, 13);
            this.valueGameState.TabIndex = 14;
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
            // valueSkipped
            // 
            this.valueSkipped.Location = new System.Drawing.Point(86, 25);
            this.valueSkipped.Name = "valueSkipped";
            this.valueSkipped.Size = new System.Drawing.Size(67, 13);
            this.valueSkipped.TabIndex = 14;
            this.valueSkipped.Text = "?";
            this.valueSkipped.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel2.Controls.Add(this.labelTimings);
            this.panel2.Controls.Add(this.labelFrametime);
            this.panel2.Controls.Add(this.labelSkipped);
            this.panel2.Controls.Add(this.valueFrametime);
            this.panel2.Controls.Add(this.valueSkipped);
            this.panel2.Location = new System.Drawing.Point(9, 203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 65);
            this.panel2.TabIndex = 13;
            // 
            // labelTimings
            // 
            this.labelTimings.AutoSize = true;
            this.labelTimings.BackColor = System.Drawing.Color.Transparent;
            this.labelTimings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimings.Location = new System.Drawing.Point(5, 5);
            this.labelTimings.Name = "labelTimings";
            this.labelTimings.Size = new System.Drawing.Size(50, 13);
            this.labelTimings.TabIndex = 2;
            this.labelTimings.Text = "Timings";
            // 
            // labelFrametime
            // 
            this.labelFrametime.Location = new System.Drawing.Point(5, 43);
            this.labelFrametime.Name = "labelFrametime";
            this.labelFrametime.Size = new System.Drawing.Size(71, 13);
            this.labelFrametime.TabIndex = 14;
            this.labelFrametime.Text = "Frametime:";
            // 
            // labelSkipped
            // 
            this.labelSkipped.Location = new System.Drawing.Point(5, 25);
            this.labelSkipped.Name = "labelSkipped";
            this.labelSkipped.Size = new System.Drawing.Size(89, 13);
            this.labelSkipped.TabIndex = 14;
            this.labelSkipped.Text = "Frames Skipped:";
            // 
            // valueFrametime
            // 
            this.valueFrametime.Location = new System.Drawing.Point(50, 43);
            this.valueFrametime.Name = "valueFrametime";
            this.valueFrametime.Size = new System.Drawing.Size(103, 13);
            this.valueFrametime.TabIndex = 14;
            this.valueFrametime.Text = "?";
            this.valueFrametime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ScanTimer2
            // 
            this.ScanTimer2.Enabled = true;
            this.ScanTimer2.Interval = 1;
            this.ScanTimer2.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer3
            // 
            this.ScanTimer3.Enabled = true;
            this.ScanTimer3.Interval = 1;
            this.ScanTimer3.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer4
            // 
            this.ScanTimer4.Enabled = true;
            this.ScanTimer4.Interval = 1;
            this.ScanTimer4.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer5
            // 
            this.ScanTimer5.Enabled = true;
            this.ScanTimer5.Interval = 1;
            this.ScanTimer5.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer6
            // 
            this.ScanTimer6.Enabled = true;
            this.ScanTimer6.Interval = 1;
            this.ScanTimer6.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer7
            // 
            this.ScanTimer7.Enabled = true;
            this.ScanTimer7.Interval = 1;
            this.ScanTimer7.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer8
            // 
            this.ScanTimer8.Enabled = true;
            this.ScanTimer8.Interval = 1;
            this.ScanTimer8.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer9
            // 
            this.ScanTimer9.Enabled = true;
            this.ScanTimer9.Interval = 1;
            this.ScanTimer9.Tick += new System.EventHandler(this.Loop);
            // 
            // ScanTimer10
            // 
            this.ScanTimer10.Enabled = true;
            this.ScanTimer10.Interval = 1;
            this.ScanTimer10.Tick += new System.EventHandler(this.Loop);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(177, 276);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label valueGamepadInputs;
        private System.Windows.Forms.Label valueInstructions;
        private System.Windows.Forms.Panel panelGamepad;
        private System.Windows.Forms.Label labelGamepad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelMisaMino;
        private System.Windows.Forms.Label labelMisaMinoStyle;
        private System.Windows.Forms.Label labelMisaMinoLevel;
        private System.Windows.Forms.ComboBox valueMisaMinoStyle;
        private System.Windows.Forms.ComboBox valueMisaMinoLevel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label valueSkipped;
        private System.Windows.Forms.Label valueGameState;
        private System.Windows.Forms.Label labelTimings;
        private System.Windows.Forms.Label labelSkipped;
        private System.Windows.Forms.Label labelFrametime;
        private System.Windows.Forms.Label valueFrametime;
        private System.Windows.Forms.Label buttonGamepad;
        private System.Windows.Forms.Timer ScanTimer2;
        private System.Windows.Forms.Timer ScanTimer3;
        private System.Windows.Forms.Timer ScanTimer4;
        private System.Windows.Forms.Timer ScanTimer5;
        private System.Windows.Forms.Timer ScanTimer6;
        private System.Windows.Forms.Timer ScanTimer7;
        private System.Windows.Forms.Timer ScanTimer8;
        private System.Windows.Forms.Timer ScanTimer9;
        private System.Windows.Forms.Timer ScanTimer10;
    }
}

