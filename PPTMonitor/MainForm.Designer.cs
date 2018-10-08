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
            this.valueRating = new System.Windows.Forms.Label();
            this.valueP2Name = new System.Windows.Forms.LinkLabel();
            this.valueP2Rating = new System.Windows.Forms.Label();
            this.valueScore1 = new System.Windows.Forms.Label();
            this.valueScore2 = new System.Windows.Forms.Label();
            this.labelDelimiter4 = new System.Windows.Forms.Label();
            this.valueP1Name = new System.Windows.Forms.LinkLabel();
            this.valueP2League = new System.Windows.Forms.Label();
            this.valueP1League = new System.Windows.Forms.Label();
            this.valuePlayers = new System.Windows.Forms.Label();
            this.valueP2Ratio = new System.Windows.Forms.Label();
            this.valueP1Ratio = new System.Windows.Forms.Label();
            this.valueP1Region = new System.Windows.Forms.Label();
            this.valueP2Region = new System.Windows.Forms.Label();
            this.valueP1Rating = new System.Windows.Forms.Label();
            this.valueP1Regional = new System.Windows.Forms.Label();
            this.valueP1Worldwide = new System.Windows.Forms.Label();
            this.valueP2Regional = new System.Windows.Forms.Label();
            this.valueP2Worldwide = new System.Windows.Forms.Label();
            this.board1 = new System.Windows.Forms.PictureBox();
            this.board2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.board2)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanTimer
            // 
            this.ScanTimer.Enabled = true;
            this.ScanTimer.Interval = 500;
            this.ScanTimer.Tick += new System.EventHandler(this.ScanTimer_Tick);
            // 
            // buttonRehook
            // 
            this.buttonRehook.AutoSize = true;
            this.buttonRehook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonRehook.Location = new System.Drawing.Point(121, 499);
            this.buttonRehook.Name = "buttonRehook";
            this.buttonRehook.Size = new System.Drawing.Size(98, 13);
            this.buttonRehook.TabIndex = 0;
            this.buttonRehook.Text = "Rehook to Process";
            this.buttonRehook.Click += new System.EventHandler(this.buttonRehook_Click);
            // 
            // valueRating
            // 
            this.valueRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueRating.Location = new System.Drawing.Point(263, 499);
            this.valueRating.Name = "valueRating";
            this.valueRating.Size = new System.Drawing.Size(61, 13);
            this.valueRating.TabIndex = 1;
            this.valueRating.Text = "?";
            this.valueRating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP2Name
            // 
            this.valueP2Name.Location = new System.Drawing.Point(184, 383);
            this.valueP2Name.Name = "valueP2Name";
            this.valueP2Name.Size = new System.Drawing.Size(93, 13);
            this.valueP2Name.TabIndex = 1;
            this.valueP2Name.TabStop = true;
            this.valueP2Name.Text = "Player 2";
            this.valueP2Name.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.valueP2Name_LinkClicked);
            // 
            // valueP2Rating
            // 
            this.valueP2Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Rating.Location = new System.Drawing.Point(264, 405);
            this.valueP2Rating.Name = "valueP2Rating";
            this.valueP2Rating.Size = new System.Drawing.Size(52, 13);
            this.valueP2Rating.TabIndex = 1;
            this.valueP2Rating.Text = "?";
            this.valueP2Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueScore1
            // 
            this.valueScore1.AutoSize = true;
            this.valueScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore1.Location = new System.Drawing.Point(150, 477);
            this.valueScore1.Name = "valueScore1";
            this.valueScore1.Size = new System.Drawing.Size(14, 13);
            this.valueScore1.TabIndex = 1;
            this.valueScore1.Text = "?";
            this.valueScore1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueScore2
            // 
            this.valueScore2.AutoSize = true;
            this.valueScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore2.Location = new System.Drawing.Point(171, 477);
            this.valueScore2.Name = "valueScore2";
            this.valueScore2.Size = new System.Drawing.Size(14, 13);
            this.valueScore2.TabIndex = 1;
            this.valueScore2.Text = "?";
            // 
            // labelDelimiter4
            // 
            this.labelDelimiter4.AutoSize = true;
            this.labelDelimiter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter4.Location = new System.Drawing.Point(162, 477);
            this.labelDelimiter4.Name = "labelDelimiter4";
            this.labelDelimiter4.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter4.TabIndex = 1;
            this.labelDelimiter4.Text = "-";
            this.labelDelimiter4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // valueP1Name
            // 
            this.valueP1Name.Location = new System.Drawing.Point(19, 383);
            this.valueP1Name.Name = "valueP1Name";
            this.valueP1Name.Size = new System.Drawing.Size(93, 13);
            this.valueP1Name.TabIndex = 1;
            this.valueP1Name.TabStop = true;
            this.valueP1Name.Text = "Player 1";
            this.valueP1Name.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.valueP1Name_LinkClicked);
            // 
            // valueP2League
            // 
            this.valueP2League.Location = new System.Drawing.Point(184, 405);
            this.valueP2League.Name = "valueP2League";
            this.valueP2League.Size = new System.Drawing.Size(74, 13);
            this.valueP2League.TabIndex = 4;
            // 
            // valueP1League
            // 
            this.valueP1League.Location = new System.Drawing.Point(19, 405);
            this.valueP1League.Name = "valueP1League";
            this.valueP1League.Size = new System.Drawing.Size(74, 13);
            this.valueP1League.TabIndex = 4;
            this.valueP1League.Text = "Grand Master";
            // 
            // valuePlayers
            // 
            this.valuePlayers.Location = new System.Drawing.Point(19, 499);
            this.valuePlayers.Name = "valuePlayers";
            this.valuePlayers.Size = new System.Drawing.Size(23, 13);
            this.valuePlayers.TabIndex = 4;
            this.valuePlayers.Text = "0";
            // 
            // valueP2Ratio
            // 
            this.valueP2Ratio.Location = new System.Drawing.Point(303, 383);
            this.valueP2Ratio.Name = "valueP2Ratio";
            this.valueP2Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP2Ratio.TabIndex = 4;
            this.valueP2Ratio.Text = "X";
            // 
            // valueP1Ratio
            // 
            this.valueP1Ratio.Location = new System.Drawing.Point(138, 383);
            this.valueP1Ratio.Name = "valueP1Ratio";
            this.valueP1Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP1Ratio.TabIndex = 4;
            this.valueP1Ratio.Text = "X";
            // 
            // valueP1Region
            // 
            this.valueP1Region.Location = new System.Drawing.Point(19, 427);
            this.valueP1Region.Name = "valueP1Region";
            this.valueP1Region.Size = new System.Drawing.Size(136, 13);
            this.valueP1Region.TabIndex = 4;
            this.valueP1Region.Text = "People\'s Republic of China";
            // 
            // valueP2Region
            // 
            this.valueP2Region.Location = new System.Drawing.Point(184, 427);
            this.valueP2Region.Name = "valueP2Region";
            this.valueP2Region.Size = new System.Drawing.Size(136, 13);
            this.valueP2Region.TabIndex = 4;
            this.valueP2Region.Text = "People\'s Republic of China";
            // 
            // valueP1Rating
            // 
            this.valueP1Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Rating.Location = new System.Drawing.Point(99, 405);
            this.valueP1Rating.Name = "valueP1Rating";
            this.valueP1Rating.Size = new System.Drawing.Size(52, 13);
            this.valueP1Rating.TabIndex = 1;
            this.valueP1Rating.Text = "?";
            this.valueP1Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP1Regional
            // 
            this.valueP1Regional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Regional.Location = new System.Drawing.Point(43, 448);
            this.valueP1Regional.Name = "valueP1Regional";
            this.valueP1Regional.Size = new System.Drawing.Size(44, 13);
            this.valueP1Regional.TabIndex = 6;
            this.valueP1Regional.Text = "?";
            this.valueP1Regional.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP1Worldwide
            // 
            this.valueP1Worldwide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Worldwide.Location = new System.Drawing.Point(106, 448);
            this.valueP1Worldwide.Name = "valueP1Worldwide";
            this.valueP1Worldwide.Size = new System.Drawing.Size(44, 13);
            this.valueP1Worldwide.TabIndex = 6;
            this.valueP1Worldwide.Text = "?";
            this.valueP1Worldwide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP2Regional
            // 
            this.valueP2Regional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Regional.Location = new System.Drawing.Point(208, 448);
            this.valueP2Regional.Name = "valueP2Regional";
            this.valueP2Regional.Size = new System.Drawing.Size(44, 13);
            this.valueP2Regional.TabIndex = 6;
            this.valueP2Regional.Text = "?";
            this.valueP2Regional.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP2Worldwide
            // 
            this.valueP2Worldwide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Worldwide.Location = new System.Drawing.Point(271, 448);
            this.valueP2Worldwide.Name = "valueP2Worldwide";
            this.valueP2Worldwide.Size = new System.Drawing.Size(44, 13);
            this.valueP2Worldwide.TabIndex = 6;
            this.valueP2Worldwide.Text = "?";
            this.valueP2Worldwide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // board1
            // 
            this.board1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board1.Location = new System.Drawing.Point(12, 12);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(150, 360);
            this.board1.TabIndex = 7;
            this.board1.TabStop = false;
            // 
            // board2
            // 
            this.board2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.board2.Location = new System.Drawing.Point(175, 12);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(150, 360);
            this.board2.TabIndex = 7;
            this.board2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(340, 521);
            this.Controls.Add(this.board2);
            this.Controls.Add(this.board1);
            this.Controls.Add(this.valueP2Worldwide);
            this.Controls.Add(this.valueP1Worldwide);
            this.Controls.Add(this.valueP2Regional);
            this.Controls.Add(this.valueP1Regional);
            this.Controls.Add(this.valueP1Ratio);
            this.Controls.Add(this.valueP1League);
            this.Controls.Add(this.valueP2Region);
            this.Controls.Add(this.valueP1Region);
            this.Controls.Add(this.valuePlayers);
            this.Controls.Add(this.valueP2Ratio);
            this.Controls.Add(this.valueP2League);
            this.Controls.Add(this.valueP1Name);
            this.Controls.Add(this.valueP2Name);
            this.Controls.Add(this.labelDelimiter4);
            this.Controls.Add(this.valueScore2);
            this.Controls.Add(this.valueScore1);
            this.Controls.Add(this.valueP1Rating);
            this.Controls.Add(this.valueP2Rating);
            this.Controls.Add(this.valueRating);
            this.Controls.Add(this.buttonRehook);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "PPT Memory Viewer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.board2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label buttonRehook;
        private System.Windows.Forms.Label valueRating;
        private System.Windows.Forms.LinkLabel valueP2Name;
        private System.Windows.Forms.Label valueP2Rating;
        private System.Windows.Forms.Label valueScore1;
        private System.Windows.Forms.Label valueScore2;
        private System.Windows.Forms.Label labelDelimiter4;
        private System.Windows.Forms.LinkLabel valueP1Name;
        private System.Windows.Forms.Label valueP2League;
        private System.Windows.Forms.Label valueP1League;
        private System.Windows.Forms.Label valuePlayers;
        private System.Windows.Forms.Label valueP2Ratio;
        private System.Windows.Forms.Label valueP1Ratio;
        private System.Windows.Forms.Label valueP1Region;
        private System.Windows.Forms.Label valueP2Region;
        private System.Windows.Forms.Label valueP1Rating;
        private System.Windows.Forms.Label valueP1Regional;
        private System.Windows.Forms.Label valueP1Worldwide;
        private System.Windows.Forms.Label valueP2Regional;
        private System.Windows.Forms.Label valueP2Worldwide;
        private System.Windows.Forms.PictureBox board1;
        private System.Windows.Forms.PictureBox board2;
    }
}

