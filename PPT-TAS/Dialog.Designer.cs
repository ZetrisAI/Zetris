namespace PPT_TAS {
    partial class Dialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dialog));
            this.valueX = new System.Windows.Forms.NumericUpDown();
            this.valueR = new System.Windows.Forms.NumericUpDown();
            this.valueHold = new System.Windows.Forms.CheckBox();
            this.labelX = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.labelHold = new System.Windows.Forms.Label();
            this.canvasBoard = new System.Windows.Forms.PictureBox();
            this.canvasHold = new System.Windows.Forms.PictureBox();
            this.canvasQueue = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.valueX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvasBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvasHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvasQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // valueX
            // 
            this.valueX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.valueX.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueX.Location = new System.Drawing.Point(164, 504);
            this.valueX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.valueX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.valueX.Name = "valueX";
            this.valueX.ReadOnly = true;
            this.valueX.Size = new System.Drawing.Size(29, 16);
            this.valueX.TabIndex = 0;
            this.valueX.TabStop = false;
            this.valueX.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.valueX.ValueChanged += new System.EventHandler(this.valueX_ValueChanged);
            // 
            // valueR
            // 
            this.valueR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.valueR.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueR.Location = new System.Drawing.Point(164, 525);
            this.valueR.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.valueR.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.valueR.Name = "valueR";
            this.valueR.ReadOnly = true;
            this.valueR.Size = new System.Drawing.Size(29, 16);
            this.valueR.TabIndex = 0;
            this.valueR.TabStop = false;
            this.valueR.ValueChanged += new System.EventHandler(this.valueR_ValueChanged);
            // 
            // valueHold
            // 
            this.valueHold.AutoSize = true;
            this.valueHold.Enabled = false;
            this.valueHold.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueHold.Location = new System.Drawing.Point(170, 546);
            this.valueHold.Name = "valueHold";
            this.valueHold.Size = new System.Drawing.Size(15, 14);
            this.valueHold.TabIndex = 2;
            this.valueHold.TabStop = false;
            this.valueHold.UseVisualStyleBackColor = true;
            this.valueHold.CheckedChanged += new System.EventHandler(this.valueHold_CheckedChanged);
            // 
            // labelX
            // 
            this.labelX.Location = new System.Drawing.Point(87, 504);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(57, 14);
            this.labelX.TabIndex = 15;
            this.labelX.Text = "X Position:";
            // 
            // labelR
            // 
            this.labelR.Location = new System.Drawing.Point(87, 525);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(57, 14);
            this.labelR.TabIndex = 15;
            this.labelR.Text = "Rotation:";
            // 
            // labelHold
            // 
            this.labelHold.Location = new System.Drawing.Point(87, 546);
            this.labelHold.Name = "labelHold";
            this.labelHold.Size = new System.Drawing.Size(57, 14);
            this.labelHold.TabIndex = 15;
            this.labelHold.Text = "Use Hold:";
            // 
            // canvasBoard
            // 
            this.canvasBoard.BackColor = System.Drawing.Color.Transparent;
            this.canvasBoard.Location = new System.Drawing.Point(38, 12);
            this.canvasBoard.Name = "canvasBoard";
            this.canvasBoard.Size = new System.Drawing.Size(200, 480);
            this.canvasBoard.TabIndex = 16;
            this.canvasBoard.TabStop = false;
            // 
            // canvasHold
            // 
            this.canvasHold.BackColor = System.Drawing.Color.Transparent;
            this.canvasHold.Location = new System.Drawing.Point(12, 12);
            this.canvasHold.Name = "canvasHold";
            this.canvasHold.Size = new System.Drawing.Size(20, 10);
            this.canvasHold.TabIndex = 20;
            this.canvasHold.TabStop = false;
            // 
            // canvasQueue
            // 
            this.canvasQueue.BackColor = System.Drawing.Color.Transparent;
            this.canvasQueue.Location = new System.Drawing.Point(244, 12);
            this.canvasQueue.Name = "canvasQueue";
            this.canvasQueue.Size = new System.Drawing.Size(20, 480);
            this.canvasQueue.TabIndex = 21;
            this.canvasQueue.TabStop = false;
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(276, 573);
            this.Controls.Add(this.canvasQueue);
            this.Controls.Add(this.canvasHold);
            this.Controls.Add(this.canvasBoard);
            this.Controls.Add(this.labelHold);
            this.Controls.Add(this.labelR);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.valueHold);
            this.Controls.Add(this.valueR);
            this.Controls.Add(this.valueX);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(780, 70);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dialog_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Dialog_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.valueX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvasBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvasHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvasQueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown valueX;
        private System.Windows.Forms.NumericUpDown valueR;
        private System.Windows.Forms.CheckBox valueHold;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label labelHold;
        private System.Windows.Forms.PictureBox canvasBoard;
        private System.Windows.Forms.PictureBox canvasHold;
        private System.Windows.Forms.PictureBox canvasQueue;
    }
}