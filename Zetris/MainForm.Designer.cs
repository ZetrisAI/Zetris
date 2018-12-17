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
            this.valuePlayers = new System.Windows.Forms.Label();
            this.board2 = new System.Windows.Forms.PictureBox();
            this.buttonGamepadDisconnect = new System.Windows.Forms.Button();
            this.valueGamepadInputs = new System.Windows.Forms.Label();
            this.board1 = new System.Windows.Forms.PictureBox();
            this.valueInstructions = new System.Windows.Forms.Label();
            this.valueMatchFrames = new System.Windows.Forms.Label();
            this.panelGamepad = new System.Windows.Forms.Panel();
            this.valueGamepadState = new System.Windows.Forms.Label();
            this.labelGamepad = new System.Windows.Forms.Label();
            this.buttonGamepadConnect = new System.Windows.Forms.Button();
            this.panelGame = new System.Windows.Forms.Panel();
            this.valueGameRunning = new System.Windows.Forms.Label();
            this.labelGame = new System.Windows.Forms.Label();
            this.labelGlobalFrames = new System.Windows.Forms.Label();
            this.labelMatchFrames = new System.Windows.Forms.Label();
            this.valueGlobalFrames = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.valueDASTapback = new System.Windows.Forms.CheckBox();
            this.labelTimings = new System.Windows.Forms.Label();
            this.valueMisaMinoLevel = new System.Windows.Forms.ComboBox();
            this.valueMisaMinoStyle = new System.Windows.Forms.ComboBox();
            this.labelMisaMino = new System.Windows.Forms.Label();
            this.labelMisaMinoStyle = new System.Windows.Forms.Label();
            this.labelMisaMinoLevel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.valuePuzzleLeague = new System.Windows.Forms.CheckBox();
            this.labelBehavior = new System.Windows.Forms.Label();
            this.labelDeveloper = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.board2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).BeginInit();
            this.panelGamepad.SuspendLayout();
            this.panelGame.SuspendLayout();
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
            // valuePlayers
            // 
            this.valuePlayers.Location = new System.Drawing.Point(110, 30);
            this.valuePlayers.Name = "valuePlayers";
            this.valuePlayers.Size = new System.Drawing.Size(43, 13);
            this.valuePlayers.TabIndex = 4;
            this.valuePlayers.Text = "?";
            this.valuePlayers.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // board2
            // 
            this.board2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board2.Location = new System.Drawing.Point(329, 10);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(150, 375);
            this.board2.TabIndex = 7;
            this.board2.TabStop = false;
            // 
            // buttonGamepadDisconnect
            // 
            this.buttonGamepadDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGamepadDisconnect.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonGamepadDisconnect.Location = new System.Drawing.Point(5, 63);
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
            this.valueGamepadInputs.Location = new System.Drawing.Point(6, 32);
            this.valueGamepadInputs.Name = "valueGamepadInputs";
            this.valueGamepadInputs.Size = new System.Drawing.Size(146, 13);
            this.valueGamepadInputs.TabIndex = 1;
            this.valueGamepadInputs.Text = "?";
            this.valueGamepadInputs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board1.Location = new System.Drawing.Point(173, 10);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(150, 375);
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
            // valueMatchFrames
            // 
            this.valueMatchFrames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueMatchFrames.Location = new System.Drawing.Point(79, 48);
            this.valueMatchFrames.Name = "valueMatchFrames";
            this.valueMatchFrames.Size = new System.Drawing.Size(74, 15);
            this.valueMatchFrames.TabIndex = 1;
            this.valueMatchFrames.Text = "?";
            this.valueMatchFrames.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelGamepad
            // 
            this.panelGamepad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelGamepad.Controls.Add(this.valueGamepadState);
            this.panelGamepad.Controls.Add(this.labelGamepad);
            this.panelGamepad.Controls.Add(this.buttonGamepadConnect);
            this.panelGamepad.Controls.Add(this.buttonGamepadDisconnect);
            this.panelGamepad.Controls.Add(this.valueGamepadInputs);
            this.panelGamepad.Location = new System.Drawing.Point(9, 243);
            this.panelGamepad.Name = "panelGamepad";
            this.panelGamepad.Size = new System.Drawing.Size(158, 92);
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
            this.buttonGamepadConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGamepadConnect.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonGamepadConnect.Location = new System.Drawing.Point(81, 63);
            this.buttonGamepadConnect.Name = "buttonGamepadConnect";
            this.buttonGamepadConnect.Size = new System.Drawing.Size(72, 24);
            this.buttonGamepadConnect.TabIndex = 9;
            this.buttonGamepadConnect.Text = "Connect";
            this.buttonGamepadConnect.UseVisualStyleBackColor = true;
            this.buttonGamepadConnect.Click += new System.EventHandler(this.GamepadConnect);
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelGame.Controls.Add(this.valueGameRunning);
            this.panelGame.Controls.Add(this.labelGame);
            this.panelGame.Controls.Add(this.labelGlobalFrames);
            this.panelGame.Controls.Add(this.valuePlayers);
            this.panelGame.Controls.Add(this.labelMatchFrames);
            this.panelGame.Controls.Add(this.valueGlobalFrames);
            this.panelGame.Controls.Add(this.valueMatchFrames);
            this.panelGame.Controls.Add(this.labelPlayers);
            this.panelGame.Location = new System.Drawing.Point(9, 10);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(158, 87);
            this.panelGame.TabIndex = 11;
            // 
            // valueGameRunning
            // 
            this.valueGameRunning.BackColor = System.Drawing.Color.Transparent;
            this.valueGameRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGameRunning.Location = new System.Drawing.Point(74, 5);
            this.valueGameRunning.Name = "valueGameRunning";
            this.valueGameRunning.Size = new System.Drawing.Size(79, 13);
            this.valueGameRunning.TabIndex = 1;
            this.valueGameRunning.Text = "?";
            this.valueGameRunning.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelGame
            // 
            this.labelGame.AutoSize = true;
            this.labelGame.BackColor = System.Drawing.Color.Transparent;
            this.labelGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGame.Location = new System.Drawing.Point(5, 5);
            this.labelGame.Name = "labelGame";
            this.labelGame.Size = new System.Drawing.Size(67, 13);
            this.labelGame.TabIndex = 1;
            this.labelGame.Text = "PPT Client";
            // 
            // labelGlobalFrames
            // 
            this.labelGlobalFrames.AutoSize = true;
            this.labelGlobalFrames.Location = new System.Drawing.Point(5, 66);
            this.labelGlobalFrames.Name = "labelGlobalFrames";
            this.labelGlobalFrames.Size = new System.Drawing.Size(77, 13);
            this.labelGlobalFrames.TabIndex = 4;
            this.labelGlobalFrames.Text = "Global Frames:";
            // 
            // labelMatchFrames
            // 
            this.labelMatchFrames.AutoSize = true;
            this.labelMatchFrames.Location = new System.Drawing.Point(5, 48);
            this.labelMatchFrames.Name = "labelMatchFrames";
            this.labelMatchFrames.Size = new System.Drawing.Size(77, 13);
            this.labelMatchFrames.TabIndex = 4;
            this.labelMatchFrames.Text = "Match Frames:";
            // 
            // valueGlobalFrames
            // 
            this.valueGlobalFrames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGlobalFrames.Location = new System.Drawing.Point(79, 66);
            this.valueGlobalFrames.Name = "valueGlobalFrames";
            this.valueGlobalFrames.Size = new System.Drawing.Size(74, 15);
            this.valueGlobalFrames.TabIndex = 1;
            this.valueGlobalFrames.Text = "?";
            this.valueGlobalFrames.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(5, 30);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(99, 13);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players Connected:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel1.Controls.Add(this.valueDASTapback);
            this.panel1.Controls.Add(this.labelTimings);
            this.panel1.Controls.Add(this.valueMisaMinoLevel);
            this.panel1.Controls.Add(this.valueMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMino);
            this.panel1.Controls.Add(this.valueInstructions);
            this.panel1.Controls.Add(this.labelMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMinoLevel);
            this.panel1.Location = new System.Drawing.Point(9, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 134);
            this.panel1.TabIndex = 11;
            // 
            // valueDASTapback
            // 
            this.valueDASTapback.AutoSize = true;
            this.valueDASTapback.Checked = true;
            this.valueDASTapback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.valueDASTapback.Location = new System.Drawing.Point(6, 75);
            this.valueDASTapback.Name = "valueDASTapback";
            this.valueDASTapback.Size = new System.Drawing.Size(137, 17);
            this.valueDASTapback.TabIndex = 2;
            this.valueDASTapback.Text = "Optimize DAS Tapback";
            this.valueDASTapback.UseVisualStyleBackColor = true;
            this.valueDASTapback.CheckedChanged += new System.EventHandler(this.valueDASTapback_CheckedChanged);
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel2.Controls.Add(this.valuePuzzleLeague);
            this.panel2.Controls.Add(this.labelBehavior);
            this.panel2.Location = new System.Drawing.Point(9, 343);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 42);
            this.panel2.TabIndex = 13;
            // 
            // valuePuzzleLeague
            // 
            this.valuePuzzleLeague.AutoSize = true;
            this.valuePuzzleLeague.Location = new System.Drawing.Point(6, 22);
            this.valuePuzzleLeague.Name = "valuePuzzleLeague";
            this.valuePuzzleLeague.Size = new System.Drawing.Size(151, 17);
            this.valuePuzzleLeague.TabIndex = 2;
            this.valuePuzzleLeague.Text = "Auto Puzzle League Menu";
            this.valuePuzzleLeague.UseVisualStyleBackColor = true;
            this.valuePuzzleLeague.CheckedChanged += new System.EventHandler(this.valuePuzzleLeague_CheckedChanged);
            // 
            // labelBehavior
            // 
            this.labelBehavior.AutoSize = true;
            this.labelBehavior.BackColor = System.Drawing.Color.Transparent;
            this.labelBehavior.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBehavior.Location = new System.Drawing.Point(5, 5);
            this.labelBehavior.Name = "labelBehavior";
            this.labelBehavior.Size = new System.Drawing.Size(57, 13);
            this.labelBehavior.TabIndex = 1;
            this.labelBehavior.Text = "Behavior";
            // 
            // labelDeveloper
            // 
            this.labelDeveloper.Location = new System.Drawing.Point(0, 0);
            this.labelDeveloper.Name = "labelDeveloper";
            this.labelDeveloper.Size = new System.Drawing.Size(100, 23);
            this.labelDeveloper.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(490, 395);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelGamepad);
            this.Controls.Add(this.board2);
            this.Controls.Add(this.board1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Zetris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).EndInit();
            this.panelGamepad.ResumeLayout(false);
            this.panelGamepad.PerformLayout();
            this.panelGame.ResumeLayout(false);
            this.panelGame.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label valuePlayers;
        private System.Windows.Forms.PictureBox board2;
        private System.Windows.Forms.Button buttonGamepadDisconnect;
        private System.Windows.Forms.Label valueGamepadInputs;
        private System.Windows.Forms.PictureBox board1;
        private System.Windows.Forms.Label valueInstructions;
        private System.Windows.Forms.Label valueMatchFrames;
        private System.Windows.Forms.Panel panelGamepad;
        private System.Windows.Forms.Label labelGamepad;
        private System.Windows.Forms.Label valueGamepadState;
        private System.Windows.Forms.Button buttonGamepadConnect;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Label valueGameRunning;
        private System.Windows.Forms.Label labelGame;
        private System.Windows.Forms.Label labelGlobalFrames;
        private System.Windows.Forms.Label labelMatchFrames;
        private System.Windows.Forms.Label valueGlobalFrames;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelMisaMino;
        private System.Windows.Forms.Label labelMisaMinoStyle;
        private System.Windows.Forms.Label labelMisaMinoLevel;
        private System.Windows.Forms.ComboBox valueMisaMinoStyle;
        private System.Windows.Forms.ComboBox valueMisaMinoLevel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox valuePuzzleLeague;
        private System.Windows.Forms.Label labelBehavior;
        private System.Windows.Forms.CheckBox valueDASTapback;
        private System.Windows.Forms.Label labelDeveloper;
        private System.Windows.Forms.Label labelTimings;
    }
}

