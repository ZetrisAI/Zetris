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
            this.buttonGamepadDisconnect = new System.Windows.Forms.Button();
            this.valueGamepadInputs = new System.Windows.Forms.Label();
            this.board1 = new System.Windows.Forms.PictureBox();
            this.valueInstructions = new System.Windows.Forms.Label();
            this.panelGamepad = new System.Windows.Forms.Panel();
            this.valueGamepadState = new System.Windows.Forms.Label();
            this.labelGamepad = new System.Windows.Forms.Label();
            this.buttonGamepadConnect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.valueGameState = new System.Windows.Forms.Label();
            this.labelTimings = new System.Windows.Forms.Label();
            this.valueMisaMinoLevel = new System.Windows.Forms.ComboBox();
            this.valueMisaMinoStyle = new System.Windows.Forms.ComboBox();
            this.labelMisaMino = new System.Windows.Forms.Label();
            this.labelMisaMinoStyle = new System.Windows.Forms.Label();
            this.labelMisaMinoLevel = new System.Windows.Forms.Label();
            this.valueDASTapback = new System.Windows.Forms.CheckBox();
            this.valuePuzzleLeague = new System.Windows.Forms.CheckBox();
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.valueDisplayBoard = new System.Windows.Forms.CheckBox();
            this.labelConfig = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).BeginInit();
            this.panelGamepad.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanTimer
            // 
            this.ScanTimer.Enabled = true;
            this.ScanTimer.Interval = 1;
            this.ScanTimer.Tick += new System.EventHandler(this.Loop);
            // 
            // buttonGamepadDisconnect
            // 
            this.buttonGamepadDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGamepadDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGamepadDisconnect.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonGamepadDisconnect.Location = new System.Drawing.Point(5, 44);
            this.buttonGamepadDisconnect.Name = "buttonGamepadDisconnect";
            this.buttonGamepadDisconnect.Size = new System.Drawing.Size(72, 24);
            this.buttonGamepadDisconnect.TabIndex = 9;
            this.buttonGamepadDisconnect.Text = "Disconnect";
            this.buttonGamepadDisconnect.UseVisualStyleBackColor = true;
            this.buttonGamepadDisconnect.Click += new System.EventHandler(this.GamepadDisconnect);
            // 
            // valueGamepadInputs
            // 
            this.valueGamepadInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGamepadInputs.Location = new System.Drawing.Point(6, 24);
            this.valueGamepadInputs.Name = "valueGamepadInputs";
            this.valueGamepadInputs.Size = new System.Drawing.Size(146, 13);
            this.valueGamepadInputs.TabIndex = 1;
            this.valueGamepadInputs.Text = "?";
            this.valueGamepadInputs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board1.Location = new System.Drawing.Point(177, 10);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(130, 325);
            this.board1.TabIndex = 7;
            this.board1.TabStop = false;
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
            this.panelGamepad.Controls.Add(this.valueGamepadState);
            this.panelGamepad.Controls.Add(this.labelGamepad);
            this.panelGamepad.Controls.Add(this.buttonGamepadConnect);
            this.panelGamepad.Controls.Add(this.buttonGamepadDisconnect);
            this.panelGamepad.Controls.Add(this.valueGamepadInputs);
            this.panelGamepad.Location = new System.Drawing.Point(9, 148);
            this.panelGamepad.Name = "panelGamepad";
            this.panelGamepad.Size = new System.Drawing.Size(158, 73);
            this.panelGamepad.TabIndex = 10;
            // 
            // valueGamepadState
            // 
            this.valueGamepadState.BackColor = System.Drawing.Color.Transparent;
            this.valueGamepadState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGamepadState.Location = new System.Drawing.Point(71, 5);
            this.valueGamepadState.Name = "valueGamepadState";
            this.valueGamepadState.Size = new System.Drawing.Size(82, 13);
            this.valueGamepadState.TabIndex = 1;
            this.valueGamepadState.Text = "?";
            this.valueGamepadState.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // buttonGamepadConnect
            // 
            this.buttonGamepadConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGamepadConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGamepadConnect.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonGamepadConnect.Location = new System.Drawing.Point(81, 44);
            this.buttonGamepadConnect.Name = "buttonGamepadConnect";
            this.buttonGamepadConnect.Size = new System.Drawing.Size(72, 24);
            this.buttonGamepadConnect.TabIndex = 9;
            this.buttonGamepadConnect.Text = "Connect";
            this.buttonGamepadConnect.UseVisualStyleBackColor = true;
            this.buttonGamepadConnect.Click += new System.EventHandler(this.GamepadConnect);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel1.Controls.Add(this.valueGameState);
            this.panel1.Controls.Add(this.labelTimings);
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
            // labelTimings
            // 
            this.labelTimings.Location = new System.Drawing.Point(82, 5);
            this.labelTimings.Name = "labelTimings";
            this.labelTimings.Size = new System.Drawing.Size(71, 13);
            this.labelTimings.TabIndex = 14;
            this.labelTimings.Text = "?";
            this.labelTimings.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // valueDASTapback
            // 
            this.valueDASTapback.AutoSize = true;
            this.valueDASTapback.Checked = true;
            this.valueDASTapback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.valueDASTapback.Location = new System.Drawing.Point(5, 57);
            this.valueDASTapback.Name = "valueDASTapback";
            this.valueDASTapback.Size = new System.Drawing.Size(137, 17);
            this.valueDASTapback.TabIndex = 2;
            this.valueDASTapback.Text = "Optimize DAS Tapback";
            this.valueDASTapback.UseVisualStyleBackColor = true;
            this.valueDASTapback.CheckedChanged += new System.EventHandler(this.valueDASTapback_CheckedChanged);
            // 
            // valuePuzzleLeague
            // 
            this.valuePuzzleLeague.AutoSize = true;
            this.valuePuzzleLeague.Location = new System.Drawing.Point(5, 40);
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
            this.panel3.Controls.Add(this.valueDASTapback);
            this.panel3.Controls.Add(this.valuePuzzleLeague);
            this.panel3.Controls.Add(this.valueDisplayBoard);
            this.panel3.Controls.Add(this.labelConfig);
            this.panel3.Location = new System.Drawing.Point(9, 227);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(158, 78);
            this.panel3.TabIndex = 10;
            // 
            // valueDisplayBoard
            // 
            this.valueDisplayBoard.AutoSize = true;
            this.valueDisplayBoard.Location = new System.Drawing.Point(5, 23);
            this.valueDisplayBoard.Name = "valueDisplayBoard";
            this.valueDisplayBoard.Size = new System.Drawing.Size(115, 17);
            this.valueDisplayBoard.TabIndex = 2;
            this.valueDisplayBoard.Text = "Display PPT Board";
            this.valueDisplayBoard.UseVisualStyleBackColor = true;
            this.valueDisplayBoard.CheckedChanged += new System.EventHandler(this.valueDisplayBoard_CheckedChanged);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(177, 345);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelGamepad);
            this.Controls.Add(this.board1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board1)).EndInit();
            this.panelGamepad.ResumeLayout(false);
            this.panelGamepad.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Button buttonGamepadDisconnect;
        private System.Windows.Forms.Label valueGamepadInputs;
        private System.Windows.Forms.PictureBox board1;
        private System.Windows.Forms.Label valueInstructions;
        private System.Windows.Forms.Panel panelGamepad;
        private System.Windows.Forms.Label labelGamepad;
        private System.Windows.Forms.Label valueGamepadState;
        private System.Windows.Forms.Button buttonGamepadConnect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelMisaMino;
        private System.Windows.Forms.Label labelMisaMinoStyle;
        private System.Windows.Forms.Label labelMisaMinoLevel;
        private System.Windows.Forms.ComboBox valueMisaMinoStyle;
        private System.Windows.Forms.ComboBox valueMisaMinoLevel;
        private System.Windows.Forms.CheckBox valuePuzzleLeague;
        private System.Windows.Forms.CheckBox valueDASTapback;
        private System.Windows.Forms.Label labelDeveloper;
        private System.Windows.Forms.Label labelTimings;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox valueDisplayBoard;
        private System.Windows.Forms.Label labelConfig;
        private System.Windows.Forms.Label valueGameState;
    }
}

