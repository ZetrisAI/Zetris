namespace PPT_TAS {
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
            this.panelGamepad.SuspendLayout();
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(177, 142);
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
    }
}

