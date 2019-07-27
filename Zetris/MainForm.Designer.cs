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
            this.valueSpeed = new System.Windows.Forms.Label();
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
            this.labelMemoryScan = new System.Windows.Forms.Label();
            this.labelFrames = new System.Windows.Forms.Label();
            this.labelSkipped = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.valueFinderSolved = new System.Windows.Forms.Label();
            this.valueFinderEnable = new System.Windows.Forms.CheckBox();
            this.labelFinder = new System.Windows.Forms.Label();
            this.panelGamepad.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
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
            resources.ApplyResources(this.valueInstructions, "valueInstructions");
            this.valueInstructions.Name = "valueInstructions";
            // 
            // panelGamepad
            // 
            resources.ApplyResources(this.panelGamepad, "panelGamepad");
            this.panelGamepad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelGamepad.Controls.Add(this.valueSpeed);
            this.panelGamepad.Controls.Add(this.buttonGamepad);
            this.panelGamepad.Controls.Add(this.labelGamepad);
            this.panelGamepad.Controls.Add(this.valueGamepadInputs);
            this.panelGamepad.Controls.Add(this.labelSpeed);
            this.panelGamepad.Name = "panelGamepad";
            // 
            // valueSpeed
            // 
            resources.ApplyResources(this.valueSpeed, "valueSpeed");
            this.valueSpeed.BackColor = System.Drawing.Color.Transparent;
            this.valueSpeed.Name = "valueSpeed";
            this.valueSpeed.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.valueSpeed_MouseWheel);
            // 
            // buttonGamepad
            // 
            resources.ApplyResources(this.buttonGamepad, "buttonGamepad");
            this.buttonGamepad.BackColor = System.Drawing.Color.Transparent;
            this.buttonGamepad.Name = "buttonGamepad";
            this.buttonGamepad.Click += new System.EventHandler(this.buttonGamepad_Click);
            // 
            // labelGamepad
            // 
            resources.ApplyResources(this.labelGamepad, "labelGamepad");
            this.labelGamepad.BackColor = System.Drawing.Color.Transparent;
            this.labelGamepad.Name = "labelGamepad";
            // 
            // valueGamepadInputs
            // 
            resources.ApplyResources(this.valueGamepadInputs, "valueGamepadInputs");
            this.valueGamepadInputs.Name = "valueGamepadInputs";
            // 
            // labelSpeed
            // 
            resources.ApplyResources(this.labelSpeed, "labelSpeed");
            this.labelSpeed.Name = "labelSpeed";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel1.Controls.Add(this.valueGameState);
            this.panel1.Controls.Add(this.valueMisaMinoLevel);
            this.panel1.Controls.Add(this.valueMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMino);
            this.panel1.Controls.Add(this.valueInstructions);
            this.panel1.Controls.Add(this.labelMisaMinoStyle);
            this.panel1.Controls.Add(this.labelMisaMinoLevel);
            this.panel1.Name = "panel1";
            // 
            // valueGameState
            // 
            resources.ApplyResources(this.valueGameState, "valueGameState");
            this.valueGameState.Name = "valueGameState";
            // 
            // valueMisaMinoLevel
            // 
            resources.ApplyResources(this.valueMisaMinoLevel, "valueMisaMinoLevel");
            this.valueMisaMinoLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueMisaMinoLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueMisaMinoLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueMisaMinoLevel.FormattingEnabled = true;
            this.valueMisaMinoLevel.Items.AddRange(new object[] {
            resources.GetString("valueMisaMinoLevel.Items"),
            resources.GetString("valueMisaMinoLevel.Items1"),
            resources.GetString("valueMisaMinoLevel.Items2"),
            resources.GetString("valueMisaMinoLevel.Items3"),
            resources.GetString("valueMisaMinoLevel.Items4"),
            resources.GetString("valueMisaMinoLevel.Items5"),
            resources.GetString("valueMisaMinoLevel.Items6"),
            resources.GetString("valueMisaMinoLevel.Items7"),
            resources.GetString("valueMisaMinoLevel.Items8"),
            resources.GetString("valueMisaMinoLevel.Items9")});
            this.valueMisaMinoLevel.Name = "valueMisaMinoLevel";
            this.valueMisaMinoLevel.SelectedIndexChanged += new System.EventHandler(this.valueMisaMino_SelectedIndexChanged);
            // 
            // valueMisaMinoStyle
            // 
            resources.ApplyResources(this.valueMisaMinoStyle, "valueMisaMinoStyle");
            this.valueMisaMinoStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueMisaMinoStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueMisaMinoStyle.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueMisaMinoStyle.FormattingEnabled = true;
            this.valueMisaMinoStyle.Items.AddRange(new object[] {
            resources.GetString("valueMisaMinoStyle.Items"),
            resources.GetString("valueMisaMinoStyle.Items1"),
            resources.GetString("valueMisaMinoStyle.Items2"),
            resources.GetString("valueMisaMinoStyle.Items3"),
            resources.GetString("valueMisaMinoStyle.Items4"),
            resources.GetString("valueMisaMinoStyle.Items5")});
            this.valueMisaMinoStyle.Name = "valueMisaMinoStyle";
            this.valueMisaMinoStyle.SelectedIndexChanged += new System.EventHandler(this.valueMisaMino_SelectedIndexChanged);
            // 
            // labelMisaMino
            // 
            resources.ApplyResources(this.labelMisaMino, "labelMisaMino");
            this.labelMisaMino.BackColor = System.Drawing.Color.Transparent;
            this.labelMisaMino.Name = "labelMisaMino";
            // 
            // labelMisaMinoStyle
            // 
            resources.ApplyResources(this.labelMisaMinoStyle, "labelMisaMinoStyle");
            this.labelMisaMinoStyle.Name = "labelMisaMinoStyle";
            // 
            // labelMisaMinoLevel
            // 
            resources.ApplyResources(this.labelMisaMinoLevel, "labelMisaMinoLevel");
            this.labelMisaMinoLevel.Name = "labelMisaMinoLevel";
            // 
            // valuePuzzleLeague
            // 
            resources.ApplyResources(this.valuePuzzleLeague, "valuePuzzleLeague");
            this.valuePuzzleLeague.Name = "valuePuzzleLeague";
            this.valuePuzzleLeague.UseVisualStyleBackColor = true;
            this.valuePuzzleLeague.CheckedChanged += new System.EventHandler(this.valuePuzzleLeague_CheckedChanged);
            // 
            // labelDeveloper
            // 
            resources.ApplyResources(this.labelDeveloper, "labelDeveloper");
            this.labelDeveloper.Name = "labelDeveloper";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel3.Controls.Add(this.valuePuzzleLeague);
            this.panel3.Controls.Add(this.labelConfig);
            this.panel3.Name = "panel3";
            // 
            // labelConfig
            // 
            resources.ApplyResources(this.labelConfig, "labelConfig");
            this.labelConfig.BackColor = System.Drawing.Color.Transparent;
            this.labelConfig.Name = "labelConfig";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel2.Controls.Add(this.valueFrametime);
            this.panel2.Controls.Add(this.valueSkipped);
            this.panel2.Controls.Add(this.labelMemoryScan);
            this.panel2.Controls.Add(this.labelFrames);
            this.panel2.Controls.Add(this.labelSkipped);
            this.panel2.Name = "panel2";
            // 
            // valueFrametime
            // 
            resources.ApplyResources(this.valueFrametime, "valueFrametime");
            this.valueFrametime.Name = "valueFrametime";
            // 
            // valueSkipped
            // 
            resources.ApplyResources(this.valueSkipped, "valueSkipped");
            this.valueSkipped.Name = "valueSkipped";
            // 
            // labelMemoryScan
            // 
            resources.ApplyResources(this.labelMemoryScan, "labelMemoryScan");
            this.labelMemoryScan.BackColor = System.Drawing.Color.Transparent;
            this.labelMemoryScan.Name = "labelMemoryScan";
            // 
            // labelFrames
            // 
            resources.ApplyResources(this.labelFrames, "labelFrames");
            this.labelFrames.Name = "labelFrames";
            // 
            // labelSkipped
            // 
            resources.ApplyResources(this.labelSkipped, "labelSkipped");
            this.labelSkipped.Name = "labelSkipped";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel4.Controls.Add(this.valueFinderSolved);
            this.panel4.Controls.Add(this.valueFinderEnable);
            this.panel4.Controls.Add(this.labelFinder);
            this.panel4.Name = "panel4";
            // 
            // valueFinderSolved
            // 
            resources.ApplyResources(this.valueFinderSolved, "valueFinderSolved");
            this.valueFinderSolved.Name = "valueFinderSolved";
            // 
            // valueFinderEnable
            // 
            resources.ApplyResources(this.valueFinderEnable, "valueFinderEnable");
            this.valueFinderEnable.Checked = true;
            this.valueFinderEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.valueFinderEnable.Name = "valueFinderEnable";
            this.valueFinderEnable.UseVisualStyleBackColor = true;
            this.valueFinderEnable.CheckedChanged += new System.EventHandler(this.valueFinderEnable_CheckedChanged);
            // 
            // labelFinder
            // 
            resources.ApplyResources(this.labelFinder, "labelFinder");
            this.labelFinder.BackColor = System.Drawing.Color.Transparent;
            this.labelFinder.Name = "labelFinder";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelGamepad);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
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
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
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
        private System.Windows.Forms.Label labelMemoryScan;
        private System.Windows.Forms.Label valueSkipped;
        private System.Windows.Forms.Label labelSkipped;
        private System.Windows.Forms.Label valueGamepadInputs;
        private System.Windows.Forms.Label valueFrametime;
        private System.Windows.Forms.Label labelFrames;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label valueSpeed;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox valueFinderEnable;
        private System.Windows.Forms.Label labelFinder;
        private System.Windows.Forms.Label valueFinderSolved;
    }
}

