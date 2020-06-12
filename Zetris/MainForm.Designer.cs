namespace Zetris {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ScanTimer = new System.Windows.Forms.Timer(this.components);
            this.valueGamepadInputs = new System.Windows.Forms.Label();
            this.panelGamepad = new System.Windows.Forms.Panel();
            this.buttonGamepad = new System.Windows.Forms.Label();
            this.labelGamepad = new System.Windows.Forms.Label();
            this.valueSkipped = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTimings = new System.Windows.Forms.Label();
            this.labelFrametime = new System.Windows.Forms.Label();
            this.labelSkipped = new System.Windows.Forms.Label();
            this.valueFrametime = new System.Windows.Forms.Label();
            this.buttonResume = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.checkManual = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelGamepad.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelButtons.SuspendLayout();
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
            // panelGamepad
            // 
            this.panelGamepad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelGamepad.Controls.Add(this.buttonGamepad);
            this.panelGamepad.Controls.Add(this.labelGamepad);
            this.panelGamepad.Controls.Add(this.valueGamepadInputs);
            this.panelGamepad.Location = new System.Drawing.Point(9, 12);
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
            this.panel2.Location = new System.Drawing.Point(9, 67);
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
            // buttonResume
            // 
            this.buttonResume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.buttonResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResume.Location = new System.Drawing.Point(51, 138);
            this.buttonResume.Name = "buttonResume";
            this.buttonResume.Size = new System.Drawing.Size(75, 23);
            this.buttonResume.TabIndex = 14;
            this.buttonResume.Text = "Resume";
            this.buttonResume.UseVisualStyleBackColor = false;
            this.buttonResume.Click += new System.EventHandler(this.buttonResume_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelButtons.Controls.Add(this.button5);
            this.panelButtons.Controls.Add(this.button6);
            this.panelButtons.Controls.Add(this.button7);
            this.panelButtons.Controls.Add(this.button8);
            this.panelButtons.Controls.Add(this.checkManual);
            this.panelButtons.Controls.Add(this.button4);
            this.panelButtons.Controls.Add(this.button3);
            this.panelButtons.Controls.Add(this.button2);
            this.panelButtons.Controls.Add(this.button11);
            this.panelButtons.Controls.Add(this.button9);
            this.panelButtons.Controls.Add(this.button10);
            this.panelButtons.Controls.Add(this.button1);
            this.panelButtons.Location = new System.Drawing.Point(9, 167);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(158, 115);
            this.panelButtons.TabIndex = 15;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(115, 79);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(23, 23);
            this.button5.TabIndex = 17;
            this.button5.Tag = ScpDriverInterface.X360Buttons.A;
            this.button5.Text = "A";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(126, 57);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(23, 23);
            this.button6.TabIndex = 18;
            this.button6.Tag = ScpDriverInterface.X360Buttons.B;
            this.button6.Text = "B";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(104, 57);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(23, 23);
            this.button7.TabIndex = 19;
            this.button7.Tag = ScpDriverInterface.X360Buttons.X;
            this.button7.Text = "X";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(115, 35);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(23, 23);
            this.button8.TabIndex = 20;
            this.button8.Tag = ScpDriverInterface.X360Buttons.Y;
            this.button8.Text = "Y";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // checkManual
            // 
            this.checkManual.AutoSize = true;
            this.checkManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkManual.Location = new System.Drawing.Point(8, 5);
            this.checkManual.Name = "checkManual";
            this.checkManual.Size = new System.Drawing.Size(100, 17);
            this.checkManual.TabIndex = 16;
            this.checkManual.Text = "Manual Input";
            this.checkManual.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(19, 79);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 23);
            this.button4.TabIndex = 14;
            this.button4.Tag = ScpDriverInterface.X360Buttons.Down;
            this.button4.Text = "▼";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(30, 57);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(23, 23);
            this.button3.TabIndex = 14;
            this.button3.Tag = ScpDriverInterface.X360Buttons.Right;
            this.button3.Text = "▶";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(8, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 23);
            this.button2.TabIndex = 14;
            this.button2.Tag = ScpDriverInterface.X360Buttons.Left;
            this.button2.Text = "◀";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(78, 31);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(23, 23);
            this.button11.TabIndex = 14;
            this.button11.Tag = ScpDriverInterface.X360Buttons.RightBumper;
            this.button11.Text = "R";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(56, 31);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(23, 23);
            this.button9.TabIndex = 14;
            this.button9.Tag = ScpDriverInterface.X360Buttons.LeftBumper;
            this.button9.Text = "L";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(67, 69);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(23, 23);
            this.button10.TabIndex = 14;
            this.button10.Tag = ScpDriverInterface.X360Buttons.Start;
            this.button10.Text = "S";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(19, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 14;
            this.button1.Tag = ScpDriverInterface.X360Buttons.Up;
            this.button1.Text = "▲";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(178, 292);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.buttonResume);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelGamepad);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 10);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelGamepad.ResumeLayout(false);
            this.panelGamepad.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label valueGamepadInputs;
        private System.Windows.Forms.Panel panelGamepad;
        private System.Windows.Forms.Label labelGamepad;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label valueSkipped;
        private System.Windows.Forms.Label labelTimings;
        private System.Windows.Forms.Label labelSkipped;
        private System.Windows.Forms.Label labelFrametime;
        private System.Windows.Forms.Label valueFrametime;
        private System.Windows.Forms.Label buttonGamepad;
        private System.Windows.Forms.Button buttonResume;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.CheckBox checkManual;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button1;
    }
}

