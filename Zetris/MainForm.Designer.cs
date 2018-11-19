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
            this.valueMisaMinoLevel = new System.Windows.Forms.ComboBox();
            this.valueMisaMinoStyle = new System.Windows.Forms.ComboBox();
            this.valueMisaMinoState = new System.Windows.Forms.Label();
            this.labelMisaMino = new System.Windows.Forms.Label();
            this.labelMisaMinoStyle = new System.Windows.Forms.Label();
            this.labelMisaMinoLevel = new System.Windows.Forms.Label();
            this.labelZetris = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.labelMisakamm = new System.Windows.Forms.Label();
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
            this.ScanTimer.Interval = 3;
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
            this.buttonGamepadDisconnect.Location = new System.Drawing.Point(5, 65);
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
            this.valueGamepadInputs.Location = new System.Drawing.Point(7, 23);
            this.valueGamepadInputs.Name = "valueGamepadInputs";
            this.valueGamepadInputs.Size = new System.Drawing.Size(146, 38);
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
            this.valueInstructions.Location = new System.Drawing.Point(5, 77);
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
            this.panelGamepad.Location = new System.Drawing.Point(9, 10);
            this.panelGamepad.Name = "panelGamepad";
            this.panelGamepad.Size = new System.Drawing.Size(158, 94);
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
            this.buttonGamepadConnect.Location = new System.Drawing.Point(81, 65);
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
            this.panelGame.Location = new System.Drawing.Point(9, 110);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(158, 87);
            this.panelGame.TabIndex = 11;
            // 
            // valueGameRunning
            // 
            this.valueGameRunning.BackColor = System.Drawing.Color.Transparent;
            this.valueGameRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueGameRunning.Location = new System.Drawing.Point(71, 5);
            this.valueGameRunning.Name = "valueGameRunning";
            this.valueGameRunning.Size = new System.Drawing.Size(82, 13);
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
            this.panel1.Controls.Add(this.labelMisakamm);
            this.panel1.Controls.Add(this.valueMisaMinoLevel);
            this.panel1.Controls.Add(this.valueMisaMinoStyle);
            this.panel1.Controls.Add(this.valueMisaMinoState);
            this.panel1.Controls.Add(this.labelMisaMino);
            this.panel1.Controls.Add(this.valueInstructions);
            this.panel1.Controls.Add(this.labelMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMinoLevel);
            this.panel1.Location = new System.Drawing.Point(9, 203);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 134);
            this.panel1.TabIndex = 11;
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
            // valueMisaMinoState
            // 
            this.valueMisaMinoState.BackColor = System.Drawing.Color.Transparent;
            this.valueMisaMinoState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueMisaMinoState.Location = new System.Drawing.Point(87, 5);
            this.valueMisaMinoState.Name = "valueMisaMinoState";
            this.valueMisaMinoState.Size = new System.Drawing.Size(66, 13);
            this.valueMisaMinoState.TabIndex = 1;
            this.valueMisaMinoState.Text = "?";
            this.valueMisaMinoState.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // labelZetris
            // 
            this.labelZetris.BackColor = System.Drawing.Color.Transparent;
            this.labelZetris.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZetris.Location = new System.Drawing.Point(60, 6);
            this.labelZetris.Name = "labelZetris";
            this.labelZetris.Size = new System.Drawing.Size(39, 13);
            this.labelZetris.TabIndex = 1;
            this.labelZetris.Text = "Zetris";
            this.labelZetris.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel2.Controls.Add(this.labelDeveloper);
            this.panel2.Controls.Add(this.labelZetris);
            this.panel2.Location = new System.Drawing.Point(9, 343);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 42);
            this.panel2.TabIndex = 13;
            // 
            // labelDeveloper
            // 
            this.labelDeveloper.BackColor = System.Drawing.Color.Transparent;
            this.labelDeveloper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDeveloper.Location = new System.Drawing.Point(12, 22);
            this.labelDeveloper.Name = "labelDeveloper";
            this.labelDeveloper.Size = new System.Drawing.Size(135, 13);
            this.labelDeveloper.TabIndex = 2;
            this.labelDeveloper.Text = "Developed by mat1jaczyyy";
            this.labelDeveloper.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelMisakamm
            // 
            this.labelMisakamm.AutoSize = true;
            this.labelMisakamm.BackColor = System.Drawing.Color.Transparent;
            this.labelMisakamm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMisakamm.Location = new System.Drawing.Point(1, 115);
            this.labelMisakamm.Name = "labelMisakamm";
            this.labelMisakamm.Size = new System.Drawing.Size(156, 13);
            this.labelMisakamm.TabIndex = 2;
            this.labelMisakamm.Text = "AI by misakamm and jezevec10";
            this.labelMisakamm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(489, 395);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelGamepad);
            this.Controls.Add(this.board2);
            this.Controls.Add(this.board1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Zetris Console";
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
        private System.Windows.Forms.Label valueMisaMinoState;
        private System.Windows.Forms.Label labelMisaMino;
        private System.Windows.Forms.Label labelMisaMinoStyle;
        private System.Windows.Forms.Label labelMisaMinoLevel;
        private System.Windows.Forms.ComboBox valueMisaMinoStyle;
        private System.Windows.Forms.ComboBox valueMisaMinoLevel;
        private System.Windows.Forms.Label labelZetris;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelMisakamm;
        private System.Windows.Forms.Label labelDeveloper;
    }
}

