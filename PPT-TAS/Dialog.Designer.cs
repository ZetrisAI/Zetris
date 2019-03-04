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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.HeldPiece = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.valueX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeldPiece)).BeginInit();
            this.SuspendLayout();
            // 
            // valueX
            // 
            this.valueX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.valueX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.valueX.ForeColor = System.Drawing.Color.Gainsboro;
            this.valueX.Location = new System.Drawing.Point(353, 9);
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
            this.valueR.Location = new System.Drawing.Point(353, 30);
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
            this.valueHold.Location = new System.Drawing.Point(359, 52);
            this.valueHold.Name = "valueHold";
            this.valueHold.Size = new System.Drawing.Size(15, 14);
            this.valueHold.TabIndex = 2;
            this.valueHold.TabStop = false;
            this.valueHold.UseVisualStyleBackColor = true;
            this.valueHold.CheckedChanged += new System.EventHandler(this.valueHold_CheckedChanged);
            // 
            // labelX
            // 
            this.labelX.Location = new System.Drawing.Point(286, 9);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(57, 14);
            this.labelX.TabIndex = 15;
            this.labelX.Text = "X Position:";
            // 
            // labelR
            // 
            this.labelR.Location = new System.Drawing.Point(286, 30);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(57, 14);
            this.labelR.TabIndex = 15;
            this.labelR.Text = "Rotation:";
            // 
            // labelHold
            // 
            this.labelHold.Location = new System.Drawing.Point(286, 51);
            this.labelHold.Name = "labelHold";
            this.labelHold.Size = new System.Drawing.Size(57, 14);
            this.labelHold.TabIndex = 15;
            this.labelHold.Text = "Use Hold:";
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(48, 13);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(200, 480);
            this.canvas.TabIndex = 16;
            this.canvas.TabStop = false;
            // 
            // HeldPiece
            // 
            this.HeldPiece.BackColor = System.Drawing.Color.DimGray;
            this.HeldPiece.Location = new System.Drawing.Point(13, 25);
            this.HeldPiece.Name = "HeldPiece";
            this.HeldPiece.Size = new System.Drawing.Size(20, 10);
            this.HeldPiece.TabIndex = 20;
            this.HeldPiece.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Hold:";
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(394, 503);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HeldPiece);
            this.Controls.Add(this.canvas);
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
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeldPiece)).EndInit();
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
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.PictureBox HeldPiece;
        private System.Windows.Forms.Label label1;
    }
}