namespace PPTMonitor
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
            this.buttonRehook = new System.Windows.Forms.Label();
            this.valuePlayers = new System.Windows.Forms.Label();
            this.board2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelInputs = new System.Windows.Forms.Label();
            this.board1 = new System.Windows.Forms.PictureBox();
            this.labelMisaMino = new System.Windows.Forms.Label();
            this.labelHoldPTR = new System.Windows.Forms.Label();
            this.labelFrames = new System.Windows.Forms.Label();
            this.labelPiece = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.board2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanTimer
            // 
            this.ScanTimer.Enabled = true;
            this.ScanTimer.Interval = 3;
            this.ScanTimer.Tick += new System.EventHandler(this.AILoop);
            // 
            // buttonRehook
            // 
            this.buttonRehook.AutoSize = true;
            this.buttonRehook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonRehook.Location = new System.Drawing.Point(12, 386);
            this.buttonRehook.Name = "buttonRehook";
            this.buttonRehook.Size = new System.Drawing.Size(98, 13);
            this.buttonRehook.TabIndex = 0;
            this.buttonRehook.Text = "Rehook to Process";
            this.buttonRehook.Click += new System.EventHandler(this.buttonRehook_Click);
            // 
            // valuePlayers
            // 
            this.valuePlayers.Location = new System.Drawing.Point(162, 7);
            this.valuePlayers.Name = "valuePlayers";
            this.valuePlayers.Size = new System.Drawing.Size(23, 13);
            this.valuePlayers.TabIndex = 4;
            this.valuePlayers.Text = "0";
            // 
            // board2
            // 
            this.board2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board2.Location = new System.Drawing.Point(175, 23);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(150, 360);
            this.board2.TabIndex = 7;
            this.board2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(19, 24);
            this.button1.TabIndex = 9;
            this.button1.Text = "RE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 402);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(19, 24);
            this.button2.TabIndex = 9;
            this.button2.Text = "RE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelInputs
            // 
            this.labelInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInputs.Location = new System.Drawing.Point(12, 408);
            this.labelInputs.Name = "labelInputs";
            this.labelInputs.Size = new System.Drawing.Size(316, 14);
            this.labelInputs.TabIndex = 1;
            this.labelInputs.Text = "?";
            this.labelInputs.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // board1
            // 
            this.board1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board1.Location = new System.Drawing.Point(12, 23);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(150, 360);
            this.board1.TabIndex = 7;
            this.board1.TabStop = false;
            // 
            // labelMisaMino
            // 
            this.labelMisaMino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMisaMino.Location = new System.Drawing.Point(12, 429);
            this.labelMisaMino.Name = "labelMisaMino";
            this.labelMisaMino.Size = new System.Drawing.Size(316, 17);
            this.labelMisaMino.TabIndex = 1;
            this.labelMisaMino.Text = "?";
            this.labelMisaMino.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelHoldPTR
            // 
            this.labelHoldPTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoldPTR.Location = new System.Drawing.Point(130, 386);
            this.labelHoldPTR.Name = "labelHoldPTR";
            this.labelHoldPTR.Size = new System.Drawing.Size(198, 17);
            this.labelHoldPTR.TabIndex = 1;
            this.labelHoldPTR.Text = "?";
            this.labelHoldPTR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelFrames
            // 
            this.labelFrames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrames.Location = new System.Drawing.Point(201, 5);
            this.labelFrames.Name = "labelFrames";
            this.labelFrames.Size = new System.Drawing.Size(124, 23);
            this.labelFrames.TabIndex = 1;
            this.labelFrames.Text = "?";
            this.labelFrames.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPiece
            // 
            this.labelPiece.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPiece.Location = new System.Drawing.Point(111, 386);
            this.labelPiece.Name = "labelPiece";
            this.labelPiece.Size = new System.Drawing.Size(13, 17);
            this.labelPiece.TabIndex = 1;
            this.labelPiece.Text = "?";
            this.labelPiece.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(340, 454);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.board2);
            this.Controls.Add(this.board1);
            this.Controls.Add(this.valuePlayers);
            this.Controls.Add(this.labelPiece);
            this.Controls.Add(this.labelFrames);
            this.Controls.Add(this.labelHoldPTR);
            this.Controls.Add(this.labelMisaMino);
            this.Controls.Add(this.labelInputs);
            this.Controls.Add(this.buttonRehook);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "PPT Memory Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label buttonRehook;
        private System.Windows.Forms.Label valuePlayers;
        private System.Windows.Forms.PictureBox board2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelInputs;
        private System.Windows.Forms.PictureBox board1;
        private System.Windows.Forms.Label labelMisaMino;
        private System.Windows.Forms.Label labelHoldPTR;
        private System.Windows.Forms.Label labelFrames;
        private System.Windows.Forms.Label labelPiece;
    }
}

