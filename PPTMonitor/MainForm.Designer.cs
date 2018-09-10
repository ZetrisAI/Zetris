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
            this.labelCurrentRating = new System.Windows.Forms.Label();
            this.labelStartingRating = new System.Windows.Forms.Label();
            this.labelRatingDifference = new System.Windows.Forms.Label();
            this.buttonRehook = new System.Windows.Forms.Label();
            this.valueRatingDifference = new System.Windows.Forms.Label();
            this.valueCurrentRating = new System.Windows.Forms.Label();
            this.valueStartingRating = new System.Windows.Forms.Label();
            this.buttonResetPuzzle = new System.Windows.Forms.Label();
            this.horiDelimiter1 = new System.Windows.Forms.PictureBox();
            this.label2PPlayer2Rating = new System.Windows.Forms.Label();
            this.label4PPlayer3Rating = new System.Windows.Forms.Label();
            this.label4PPlayer4Rating = new System.Windows.Forms.Label();
            this.value4PPlayer3Rating = new System.Windows.Forms.Label();
            this.value4PPlayer4Rating = new System.Windows.Forms.Label();
            this.value2PPlayer2Rating = new System.Windows.Forms.Label();
            this.labelWins = new System.Windows.Forms.Label();
            this.labelLosses = new System.Windows.Forms.Label();
            this.vertDelimiter1 = new System.Windows.Forms.PictureBox();
            this.valueLosses = new System.Windows.Forms.Label();
            this.valueWins = new System.Windows.Forms.Label();
            this.labelPuzzle = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label2PScore = new System.Windows.Forms.Label();
            this.value2PScore1 = new System.Windows.Forms.Label();
            this.value2PScore2 = new System.Windows.Forms.Label();
            this.labelDelimiter1 = new System.Windows.Forms.Label();
            this.label2PSets = new System.Windows.Forms.Label();
            this.value2PSets1 = new System.Windows.Forms.Label();
            this.value2PSets2 = new System.Windows.Forms.Label();
            this.labelDelimiter2 = new System.Windows.Forms.Label();
            this.vertDelimiter2 = new System.Windows.Forms.PictureBox();
            this.label2PTotal = new System.Windows.Forms.Label();
            this.value2PTotal1 = new System.Windows.Forms.Label();
            this.value2PTotal2 = new System.Windows.Forms.Label();
            this.labelDelimiter3 = new System.Windows.Forms.Label();
            this.label4PPlayer2Rating = new System.Windows.Forms.Label();
            this.label4PSets = new System.Windows.Forms.Label();
            this.label4PScore = new System.Windows.Forms.Label();
            this.label4PTotal = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.value4PPlayer2Rating = new System.Windows.Forms.Label();
            this.value4PScore1 = new System.Windows.Forms.Label();
            this.value4PTotal1 = new System.Windows.Forms.Label();
            this.value4PSets1 = new System.Windows.Forms.Label();
            this.value4PScore2 = new System.Windows.Forms.Label();
            this.value4PTotal2 = new System.Windows.Forms.Label();
            this.value4PSets2 = new System.Windows.Forms.Label();
            this.labelDelimiter4 = new System.Windows.Forms.Label();
            this.labelDelimiter10 = new System.Windows.Forms.Label();
            this.labelDelimiter7 = new System.Windows.Forms.Label();
            this.vertDelimiter3 = new System.Windows.Forms.PictureBox();
            this.horiDelimiter2 = new System.Windows.Forms.PictureBox();
            this.value4PScore3 = new System.Windows.Forms.Label();
            this.value4PTotal3 = new System.Windows.Forms.Label();
            this.value4PSets3 = new System.Windows.Forms.Label();
            this.value4PScore4 = new System.Windows.Forms.Label();
            this.value4PTotal4 = new System.Windows.Forms.Label();
            this.value4PSets4 = new System.Windows.Forms.Label();
            this.labelDelimiter6 = new System.Windows.Forms.Label();
            this.labelDelimiter12 = new System.Windows.Forms.Label();
            this.labelDelimiter9 = new System.Windows.Forms.Label();
            this.labelDelimiter5 = new System.Windows.Forms.Label();
            this.labelDelimiter11 = new System.Windows.Forms.Label();
            this.labelDelimiter8 = new System.Windows.Forms.Label();
            this.horiDelimiter3 = new System.Windows.Forms.PictureBox();
            this.label2PPlayer1Rating = new System.Windows.Forms.Label();
            this.value2PPlayer1Rating = new System.Windows.Forms.Label();
            this.buttonPopoutPuzzle = new System.Windows.Forms.Label();
            this.buttonReset2P = new System.Windows.Forms.Label();
            this.buttonPopout2P = new System.Windows.Forms.Label();
            this.label4PPlayer1Rating = new System.Windows.Forms.Label();
            this.value4PPlayer1Rating = new System.Windows.Forms.Label();
            this.buttonReset4P = new System.Windows.Forms.Label();
            this.buttonPopout4P = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter3)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanTimer
            // 
            this.ScanTimer.Enabled = true;
            this.ScanTimer.Interval = 500;
            this.ScanTimer.Tick += new System.EventHandler(this.ScanTimer_Tick);
            // 
            // labelCurrentRating
            // 
            this.labelCurrentRating.AutoSize = true;
            this.labelCurrentRating.Location = new System.Drawing.Point(6, 53);
            this.labelCurrentRating.Name = "labelCurrentRating";
            this.labelCurrentRating.Size = new System.Drawing.Size(78, 13);
            this.labelCurrentRating.TabIndex = 1;
            this.labelCurrentRating.Text = "Current Rating:";
            // 
            // labelStartingRating
            // 
            this.labelStartingRating.AutoSize = true;
            this.labelStartingRating.Location = new System.Drawing.Point(6, 31);
            this.labelStartingRating.Name = "labelStartingRating";
            this.labelStartingRating.Size = new System.Drawing.Size(80, 13);
            this.labelStartingRating.TabIndex = 1;
            this.labelStartingRating.Text = "Starting Rating:";
            // 
            // labelRatingDifference
            // 
            this.labelRatingDifference.AutoSize = true;
            this.labelRatingDifference.Location = new System.Drawing.Point(6, 75);
            this.labelRatingDifference.Name = "labelRatingDifference";
            this.labelRatingDifference.Size = new System.Drawing.Size(93, 13);
            this.labelRatingDifference.TabIndex = 1;
            this.labelRatingDifference.Text = "Rating Difference:";
            // 
            // buttonRehook
            // 
            this.buttonRehook.AutoSize = true;
            this.buttonRehook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonRehook.Location = new System.Drawing.Point(103, 332);
            this.buttonRehook.Name = "buttonRehook";
            this.buttonRehook.Size = new System.Drawing.Size(98, 13);
            this.buttonRehook.TabIndex = 0;
            this.buttonRehook.Text = "Rehook to Process";
            this.buttonRehook.Click += new System.EventHandler(this.buttonRehook_Click);
            // 
            // valueRatingDifference
            // 
            this.valueRatingDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueRatingDifference.Location = new System.Drawing.Point(95, 75);
            this.valueRatingDifference.Name = "valueRatingDifference";
            this.valueRatingDifference.Size = new System.Drawing.Size(49, 13);
            this.valueRatingDifference.TabIndex = 1;
            this.valueRatingDifference.Text = "?";
            this.valueRatingDifference.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueCurrentRating
            // 
            this.valueCurrentRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueCurrentRating.Location = new System.Drawing.Point(83, 53);
            this.valueCurrentRating.Name = "valueCurrentRating";
            this.valueCurrentRating.Size = new System.Drawing.Size(61, 13);
            this.valueCurrentRating.TabIndex = 1;
            this.valueCurrentRating.Text = "?";
            this.valueCurrentRating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueStartingRating
            // 
            this.valueStartingRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueStartingRating.Location = new System.Drawing.Point(83, 31);
            this.valueStartingRating.Name = "valueStartingRating";
            this.valueStartingRating.Size = new System.Drawing.Size(61, 13);
            this.valueStartingRating.TabIndex = 1;
            this.valueStartingRating.Text = "?";
            this.valueStartingRating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonResetPuzzle
            // 
            this.buttonResetPuzzle.AutoSize = true;
            this.buttonResetPuzzle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonResetPuzzle.Location = new System.Drawing.Point(12, 9);
            this.buttonResetPuzzle.Name = "buttonResetPuzzle";
            this.buttonResetPuzzle.Size = new System.Drawing.Size(35, 13);
            this.buttonResetPuzzle.TabIndex = 0;
            this.buttonResetPuzzle.Text = "Reset";
            this.buttonResetPuzzle.Click += new System.EventHandler(this.buttonResetPuzzle_Click);
            // 
            // horiDelimiter1
            // 
            this.horiDelimiter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.horiDelimiter1.Location = new System.Drawing.Point(5, 100);
            this.horiDelimiter1.Name = "horiDelimiter1";
            this.horiDelimiter1.Size = new System.Drawing.Size(290, 2);
            this.horiDelimiter1.TabIndex = 2;
            this.horiDelimiter1.TabStop = false;
            // 
            // label2PPlayer2Rating
            // 
            this.label2PPlayer2Rating.AutoSize = true;
            this.label2PPlayer2Rating.Location = new System.Drawing.Point(6, 166);
            this.label2PPlayer2Rating.Name = "label2PPlayer2Rating";
            this.label2PPlayer2Rating.Size = new System.Drawing.Size(82, 13);
            this.label2PPlayer2Rating.TabIndex = 1;
            this.label2PPlayer2Rating.Text = "Player 2 Rating:";
            // 
            // label4PPlayer3Rating
            // 
            this.label4PPlayer3Rating.AutoSize = true;
            this.label4PPlayer3Rating.Location = new System.Drawing.Point(6, 277);
            this.label4PPlayer3Rating.Name = "label4PPlayer3Rating";
            this.label4PPlayer3Rating.Size = new System.Drawing.Size(82, 13);
            this.label4PPlayer3Rating.TabIndex = 1;
            this.label4PPlayer3Rating.Text = "Player 3 Rating:";
            // 
            // label4PPlayer4Rating
            // 
            this.label4PPlayer4Rating.AutoSize = true;
            this.label4PPlayer4Rating.Location = new System.Drawing.Point(6, 298);
            this.label4PPlayer4Rating.Name = "label4PPlayer4Rating";
            this.label4PPlayer4Rating.Size = new System.Drawing.Size(82, 13);
            this.label4PPlayer4Rating.TabIndex = 1;
            this.label4PPlayer4Rating.Text = "Player 4 Rating:";
            // 
            // value4PPlayer3Rating
            // 
            this.value4PPlayer3Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PPlayer3Rating.Location = new System.Drawing.Point(83, 277);
            this.value4PPlayer3Rating.Name = "value4PPlayer3Rating";
            this.value4PPlayer3Rating.Size = new System.Drawing.Size(60, 13);
            this.value4PPlayer3Rating.TabIndex = 1;
            this.value4PPlayer3Rating.Text = "?";
            this.value4PPlayer3Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PPlayer4Rating
            // 
            this.value4PPlayer4Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PPlayer4Rating.Location = new System.Drawing.Point(83, 298);
            this.value4PPlayer4Rating.Name = "value4PPlayer4Rating";
            this.value4PPlayer4Rating.Size = new System.Drawing.Size(60, 13);
            this.value4PPlayer4Rating.TabIndex = 1;
            this.value4PPlayer4Rating.Text = "?";
            this.value4PPlayer4Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value2PPlayer2Rating
            // 
            this.value2PPlayer2Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PPlayer2Rating.Location = new System.Drawing.Point(95, 166);
            this.value2PPlayer2Rating.Name = "value2PPlayer2Rating";
            this.value2PPlayer2Rating.Size = new System.Drawing.Size(48, 13);
            this.value2PPlayer2Rating.TabIndex = 1;
            this.value2PPlayer2Rating.Text = "?";
            this.value2PPlayer2Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelWins
            // 
            this.labelWins.AutoSize = true;
            this.labelWins.Location = new System.Drawing.Point(185, 43);
            this.labelWins.Name = "labelWins";
            this.labelWins.Size = new System.Drawing.Size(34, 13);
            this.labelWins.TabIndex = 1;
            this.labelWins.Text = "Wins:";
            // 
            // labelLosses
            // 
            this.labelLosses.AutoSize = true;
            this.labelLosses.Location = new System.Drawing.Point(185, 65);
            this.labelLosses.Name = "labelLosses";
            this.labelLosses.Size = new System.Drawing.Size(43, 13);
            this.labelLosses.TabIndex = 1;
            this.labelLosses.Text = "Losses:";
            // 
            // vertDelimiter1
            // 
            this.vertDelimiter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.vertDelimiter1.Location = new System.Drawing.Point(150, 31);
            this.vertDelimiter1.Name = "vertDelimiter1";
            this.vertDelimiter1.Size = new System.Drawing.Size(2, 60);
            this.vertDelimiter1.TabIndex = 3;
            this.vertDelimiter1.TabStop = false;
            // 
            // valueLosses
            // 
            this.valueLosses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLosses.Location = new System.Drawing.Point(225, 65);
            this.valueLosses.Name = "valueLosses";
            this.valueLosses.Size = new System.Drawing.Size(40, 13);
            this.valueLosses.TabIndex = 1;
            this.valueLosses.Text = "0";
            this.valueLosses.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueWins
            // 
            this.valueWins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueWins.Location = new System.Drawing.Point(225, 43);
            this.valueWins.Name = "valueWins";
            this.valueWins.Size = new System.Drawing.Size(40, 13);
            this.valueWins.TabIndex = 1;
            this.valueWins.Text = "0";
            this.valueWins.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPuzzle
            // 
            this.labelPuzzle.AutoSize = true;
            this.labelPuzzle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelPuzzle.Location = new System.Drawing.Point(111, 9);
            this.labelPuzzle.Name = "labelPuzzle";
            this.labelPuzzle.Size = new System.Drawing.Size(77, 13);
            this.labelPuzzle.TabIndex = 1;
            this.labelPuzzle.Text = "Puzzle League";
            this.labelPuzzle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label21.Location = new System.Drawing.Point(125, 110);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "2P Battle";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2PScore
            // 
            this.label2PScore.AutoSize = true;
            this.label2PScore.Location = new System.Drawing.Point(183, 134);
            this.label2PScore.Name = "label2PScore";
            this.label2PScore.Size = new System.Drawing.Size(38, 13);
            this.label2PScore.TabIndex = 1;
            this.label2PScore.Text = "Score:";
            // 
            // value2PScore1
            // 
            this.value2PScore1.AutoSize = true;
            this.value2PScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PScore1.Location = new System.Drawing.Point(230, 134);
            this.value2PScore1.Name = "value2PScore1";
            this.value2PScore1.Size = new System.Drawing.Size(14, 13);
            this.value2PScore1.TabIndex = 1;
            this.value2PScore1.Text = "?";
            this.value2PScore1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value2PScore2
            // 
            this.value2PScore2.AutoSize = true;
            this.value2PScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PScore2.Location = new System.Drawing.Point(251, 134);
            this.value2PScore2.Name = "value2PScore2";
            this.value2PScore2.Size = new System.Drawing.Size(14, 13);
            this.value2PScore2.TabIndex = 1;
            this.value2PScore2.Text = "?";
            // 
            // labelDelimiter1
            // 
            this.labelDelimiter1.AutoSize = true;
            this.labelDelimiter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter1.Location = new System.Drawing.Point(242, 134);
            this.labelDelimiter1.Name = "labelDelimiter1";
            this.labelDelimiter1.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter1.TabIndex = 1;
            this.labelDelimiter1.Text = "-";
            this.labelDelimiter1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2PSets
            // 
            this.label2PSets.AutoSize = true;
            this.label2PSets.Location = new System.Drawing.Point(183, 155);
            this.label2PSets.Name = "label2PSets";
            this.label2PSets.Size = new System.Drawing.Size(31, 13);
            this.label2PSets.TabIndex = 1;
            this.label2PSets.Text = "Sets:";
            // 
            // value2PSets1
            // 
            this.value2PSets1.AutoSize = true;
            this.value2PSets1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PSets1.Location = new System.Drawing.Point(230, 155);
            this.value2PSets1.Name = "value2PSets1";
            this.value2PSets1.Size = new System.Drawing.Size(14, 13);
            this.value2PSets1.TabIndex = 1;
            this.value2PSets1.Text = "?";
            this.value2PSets1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value2PSets2
            // 
            this.value2PSets2.AutoSize = true;
            this.value2PSets2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PSets2.Location = new System.Drawing.Point(251, 155);
            this.value2PSets2.Name = "value2PSets2";
            this.value2PSets2.Size = new System.Drawing.Size(14, 13);
            this.value2PSets2.TabIndex = 1;
            this.value2PSets2.Text = "?";
            // 
            // labelDelimiter2
            // 
            this.labelDelimiter2.AutoSize = true;
            this.labelDelimiter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter2.Location = new System.Drawing.Point(242, 155);
            this.labelDelimiter2.Name = "labelDelimiter2";
            this.labelDelimiter2.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter2.TabIndex = 1;
            this.labelDelimiter2.Text = "-";
            this.labelDelimiter2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // vertDelimiter2
            // 
            this.vertDelimiter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.vertDelimiter2.Location = new System.Drawing.Point(150, 131);
            this.vertDelimiter2.Name = "vertDelimiter2";
            this.vertDelimiter2.Size = new System.Drawing.Size(2, 60);
            this.vertDelimiter2.TabIndex = 3;
            this.vertDelimiter2.TabStop = false;
            // 
            // label2PTotal
            // 
            this.label2PTotal.AutoSize = true;
            this.label2PTotal.Location = new System.Drawing.Point(183, 176);
            this.label2PTotal.Name = "label2PTotal";
            this.label2PTotal.Size = new System.Drawing.Size(34, 13);
            this.label2PTotal.TabIndex = 1;
            this.label2PTotal.Text = "Total:";
            // 
            // value2PTotal1
            // 
            this.value2PTotal1.AutoSize = true;
            this.value2PTotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PTotal1.Location = new System.Drawing.Point(230, 176);
            this.value2PTotal1.Name = "value2PTotal1";
            this.value2PTotal1.Size = new System.Drawing.Size(14, 13);
            this.value2PTotal1.TabIndex = 1;
            this.value2PTotal1.Text = "?";
            this.value2PTotal1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value2PTotal2
            // 
            this.value2PTotal2.AutoSize = true;
            this.value2PTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PTotal2.Location = new System.Drawing.Point(251, 176);
            this.value2PTotal2.Name = "value2PTotal2";
            this.value2PTotal2.Size = new System.Drawing.Size(14, 13);
            this.value2PTotal2.TabIndex = 1;
            this.value2PTotal2.Text = "?";
            // 
            // labelDelimiter3
            // 
            this.labelDelimiter3.AutoSize = true;
            this.labelDelimiter3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter3.Location = new System.Drawing.Point(242, 176);
            this.labelDelimiter3.Name = "labelDelimiter3";
            this.labelDelimiter3.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter3.TabIndex = 1;
            this.labelDelimiter3.Text = "-";
            this.labelDelimiter3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4PPlayer2Rating
            // 
            this.label4PPlayer2Rating.AutoSize = true;
            this.label4PPlayer2Rating.Location = new System.Drawing.Point(6, 256);
            this.label4PPlayer2Rating.Name = "label4PPlayer2Rating";
            this.label4PPlayer2Rating.Size = new System.Drawing.Size(82, 13);
            this.label4PPlayer2Rating.TabIndex = 1;
            this.label4PPlayer2Rating.Text = "Player 2 Rating:";
            // 
            // label4PSets
            // 
            this.label4PSets.AutoSize = true;
            this.label4PSets.Location = new System.Drawing.Point(167, 265);
            this.label4PSets.Name = "label4PSets";
            this.label4PSets.Size = new System.Drawing.Size(31, 13);
            this.label4PSets.TabIndex = 1;
            this.label4PSets.Text = "Sets:";
            // 
            // label4PScore
            // 
            this.label4PScore.AutoSize = true;
            this.label4PScore.Location = new System.Drawing.Point(167, 244);
            this.label4PScore.Name = "label4PScore";
            this.label4PScore.Size = new System.Drawing.Size(38, 13);
            this.label4PScore.TabIndex = 1;
            this.label4PScore.Text = "Score:";
            // 
            // label4PTotal
            // 
            this.label4PTotal.AutoSize = true;
            this.label4PTotal.Location = new System.Drawing.Point(167, 286);
            this.label4PTotal.Name = "label4PTotal";
            this.label4PTotal.Size = new System.Drawing.Size(34, 13);
            this.label4PTotal.TabIndex = 1;
            this.label4PTotal.Text = "Total:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label38.Location = new System.Drawing.Point(125, 211);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(50, 13);
            this.label38.TabIndex = 1;
            this.label38.Text = "4P Battle";
            this.label38.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // value4PPlayer2Rating
            // 
            this.value4PPlayer2Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PPlayer2Rating.Location = new System.Drawing.Point(83, 256);
            this.value4PPlayer2Rating.Name = "value4PPlayer2Rating";
            this.value4PPlayer2Rating.Size = new System.Drawing.Size(60, 13);
            this.value4PPlayer2Rating.TabIndex = 1;
            this.value4PPlayer2Rating.Text = "?";
            this.value4PPlayer2Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PScore1
            // 
            this.value4PScore1.AutoSize = true;
            this.value4PScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PScore1.Location = new System.Drawing.Point(209, 244);
            this.value4PScore1.Name = "value4PScore1";
            this.value4PScore1.Size = new System.Drawing.Size(14, 13);
            this.value4PScore1.TabIndex = 1;
            this.value4PScore1.Text = "?";
            this.value4PScore1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PTotal1
            // 
            this.value4PTotal1.AutoSize = true;
            this.value4PTotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PTotal1.Location = new System.Drawing.Point(209, 286);
            this.value4PTotal1.Name = "value4PTotal1";
            this.value4PTotal1.Size = new System.Drawing.Size(14, 13);
            this.value4PTotal1.TabIndex = 1;
            this.value4PTotal1.Text = "?";
            this.value4PTotal1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PSets1
            // 
            this.value4PSets1.AutoSize = true;
            this.value4PSets1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PSets1.Location = new System.Drawing.Point(209, 265);
            this.value4PSets1.Name = "value4PSets1";
            this.value4PSets1.Size = new System.Drawing.Size(14, 13);
            this.value4PSets1.TabIndex = 1;
            this.value4PSets1.Text = "?";
            this.value4PSets1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PScore2
            // 
            this.value4PScore2.AutoSize = true;
            this.value4PScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PScore2.Location = new System.Drawing.Point(230, 244);
            this.value4PScore2.Name = "value4PScore2";
            this.value4PScore2.Size = new System.Drawing.Size(14, 13);
            this.value4PScore2.TabIndex = 1;
            this.value4PScore2.Text = "?";
            // 
            // value4PTotal2
            // 
            this.value4PTotal2.AutoSize = true;
            this.value4PTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PTotal2.Location = new System.Drawing.Point(230, 286);
            this.value4PTotal2.Name = "value4PTotal2";
            this.value4PTotal2.Size = new System.Drawing.Size(14, 13);
            this.value4PTotal2.TabIndex = 1;
            this.value4PTotal2.Text = "?";
            // 
            // value4PSets2
            // 
            this.value4PSets2.AutoSize = true;
            this.value4PSets2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PSets2.Location = new System.Drawing.Point(230, 265);
            this.value4PSets2.Name = "value4PSets2";
            this.value4PSets2.Size = new System.Drawing.Size(14, 13);
            this.value4PSets2.TabIndex = 1;
            this.value4PSets2.Text = "?";
            // 
            // labelDelimiter4
            // 
            this.labelDelimiter4.AutoSize = true;
            this.labelDelimiter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter4.Location = new System.Drawing.Point(221, 244);
            this.labelDelimiter4.Name = "labelDelimiter4";
            this.labelDelimiter4.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter4.TabIndex = 1;
            this.labelDelimiter4.Text = "-";
            this.labelDelimiter4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter10
            // 
            this.labelDelimiter10.AutoSize = true;
            this.labelDelimiter10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter10.Location = new System.Drawing.Point(221, 286);
            this.labelDelimiter10.Name = "labelDelimiter10";
            this.labelDelimiter10.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter10.TabIndex = 1;
            this.labelDelimiter10.Text = "-";
            this.labelDelimiter10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter7
            // 
            this.labelDelimiter7.AutoSize = true;
            this.labelDelimiter7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter7.Location = new System.Drawing.Point(221, 265);
            this.labelDelimiter7.Name = "labelDelimiter7";
            this.labelDelimiter7.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter7.TabIndex = 1;
            this.labelDelimiter7.Text = "-";
            this.labelDelimiter7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // vertDelimiter3
            // 
            this.vertDelimiter3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.vertDelimiter3.Location = new System.Drawing.Point(150, 233);
            this.vertDelimiter3.Name = "vertDelimiter3";
            this.vertDelimiter3.Size = new System.Drawing.Size(2, 80);
            this.vertDelimiter3.TabIndex = 3;
            this.vertDelimiter3.TabStop = false;
            // 
            // horiDelimiter2
            // 
            this.horiDelimiter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.horiDelimiter2.Location = new System.Drawing.Point(5, 200);
            this.horiDelimiter2.Name = "horiDelimiter2";
            this.horiDelimiter2.Size = new System.Drawing.Size(290, 2);
            this.horiDelimiter2.TabIndex = 2;
            this.horiDelimiter2.TabStop = false;
            // 
            // value4PScore3
            // 
            this.value4PScore3.AutoSize = true;
            this.value4PScore3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PScore3.Location = new System.Drawing.Point(251, 244);
            this.value4PScore3.Name = "value4PScore3";
            this.value4PScore3.Size = new System.Drawing.Size(14, 13);
            this.value4PScore3.TabIndex = 1;
            this.value4PScore3.Text = "?";
            this.value4PScore3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PTotal3
            // 
            this.value4PTotal3.AutoSize = true;
            this.value4PTotal3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PTotal3.Location = new System.Drawing.Point(251, 286);
            this.value4PTotal3.Name = "value4PTotal3";
            this.value4PTotal3.Size = new System.Drawing.Size(14, 13);
            this.value4PTotal3.TabIndex = 1;
            this.value4PTotal3.Text = "?";
            this.value4PTotal3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PSets3
            // 
            this.value4PSets3.AutoSize = true;
            this.value4PSets3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PSets3.Location = new System.Drawing.Point(251, 265);
            this.value4PSets3.Name = "value4PSets3";
            this.value4PSets3.Size = new System.Drawing.Size(14, 13);
            this.value4PSets3.TabIndex = 1;
            this.value4PSets3.Text = "?";
            this.value4PSets3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // value4PScore4
            // 
            this.value4PScore4.AutoSize = true;
            this.value4PScore4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PScore4.Location = new System.Drawing.Point(272, 244);
            this.value4PScore4.Name = "value4PScore4";
            this.value4PScore4.Size = new System.Drawing.Size(14, 13);
            this.value4PScore4.TabIndex = 1;
            this.value4PScore4.Text = "?";
            // 
            // value4PTotal4
            // 
            this.value4PTotal4.AutoSize = true;
            this.value4PTotal4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PTotal4.Location = new System.Drawing.Point(272, 286);
            this.value4PTotal4.Name = "value4PTotal4";
            this.value4PTotal4.Size = new System.Drawing.Size(14, 13);
            this.value4PTotal4.TabIndex = 1;
            this.value4PTotal4.Text = "?";
            // 
            // value4PSets4
            // 
            this.value4PSets4.AutoSize = true;
            this.value4PSets4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PSets4.Location = new System.Drawing.Point(272, 265);
            this.value4PSets4.Name = "value4PSets4";
            this.value4PSets4.Size = new System.Drawing.Size(14, 13);
            this.value4PSets4.TabIndex = 1;
            this.value4PSets4.Text = "?";
            // 
            // labelDelimiter6
            // 
            this.labelDelimiter6.AutoSize = true;
            this.labelDelimiter6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter6.Location = new System.Drawing.Point(263, 244);
            this.labelDelimiter6.Name = "labelDelimiter6";
            this.labelDelimiter6.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter6.TabIndex = 1;
            this.labelDelimiter6.Text = "-";
            this.labelDelimiter6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter12
            // 
            this.labelDelimiter12.AutoSize = true;
            this.labelDelimiter12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter12.Location = new System.Drawing.Point(263, 286);
            this.labelDelimiter12.Name = "labelDelimiter12";
            this.labelDelimiter12.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter12.TabIndex = 1;
            this.labelDelimiter12.Text = "-";
            this.labelDelimiter12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter9
            // 
            this.labelDelimiter9.AutoSize = true;
            this.labelDelimiter9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter9.Location = new System.Drawing.Point(263, 265);
            this.labelDelimiter9.Name = "labelDelimiter9";
            this.labelDelimiter9.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter9.TabIndex = 1;
            this.labelDelimiter9.Text = "-";
            this.labelDelimiter9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter5
            // 
            this.labelDelimiter5.AutoSize = true;
            this.labelDelimiter5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter5.Location = new System.Drawing.Point(242, 244);
            this.labelDelimiter5.Name = "labelDelimiter5";
            this.labelDelimiter5.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter5.TabIndex = 1;
            this.labelDelimiter5.Text = "-";
            this.labelDelimiter5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter11
            // 
            this.labelDelimiter11.AutoSize = true;
            this.labelDelimiter11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter11.Location = new System.Drawing.Point(242, 286);
            this.labelDelimiter11.Name = "labelDelimiter11";
            this.labelDelimiter11.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter11.TabIndex = 1;
            this.labelDelimiter11.Text = "-";
            this.labelDelimiter11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDelimiter8
            // 
            this.labelDelimiter8.AutoSize = true;
            this.labelDelimiter8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter8.Location = new System.Drawing.Point(242, 265);
            this.labelDelimiter8.Name = "labelDelimiter8";
            this.labelDelimiter8.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter8.TabIndex = 1;
            this.labelDelimiter8.Text = "-";
            this.labelDelimiter8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // horiDelimiter3
            // 
            this.horiDelimiter3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.horiDelimiter3.Location = new System.Drawing.Point(5, 322);
            this.horiDelimiter3.Name = "horiDelimiter3";
            this.horiDelimiter3.Size = new System.Drawing.Size(290, 2);
            this.horiDelimiter3.TabIndex = 2;
            this.horiDelimiter3.TabStop = false;
            // 
            // label2PPlayer1Rating
            // 
            this.label2PPlayer1Rating.AutoSize = true;
            this.label2PPlayer1Rating.Location = new System.Drawing.Point(6, 145);
            this.label2PPlayer1Rating.Name = "label2PPlayer1Rating";
            this.label2PPlayer1Rating.Size = new System.Drawing.Size(82, 13);
            this.label2PPlayer1Rating.TabIndex = 1;
            this.label2PPlayer1Rating.Text = "Player 1 Rating:";
            // 
            // value2PPlayer1Rating
            // 
            this.value2PPlayer1Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value2PPlayer1Rating.Location = new System.Drawing.Point(95, 145);
            this.value2PPlayer1Rating.Name = "value2PPlayer1Rating";
            this.value2PPlayer1Rating.Size = new System.Drawing.Size(48, 13);
            this.value2PPlayer1Rating.TabIndex = 1;
            this.value2PPlayer1Rating.Text = "?";
            this.value2PPlayer1Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonPopoutPuzzle
            // 
            this.buttonPopoutPuzzle.AutoSize = true;
            this.buttonPopoutPuzzle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonPopoutPuzzle.Location = new System.Drawing.Point(244, 9);
            this.buttonPopoutPuzzle.Name = "buttonPopoutPuzzle";
            this.buttonPopoutPuzzle.Size = new System.Drawing.Size(44, 13);
            this.buttonPopoutPuzzle.TabIndex = 0;
            this.buttonPopoutPuzzle.Text = "Pop out";
            // 
            // buttonReset2P
            // 
            this.buttonReset2P.AutoSize = true;
            this.buttonReset2P.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonReset2P.Location = new System.Drawing.Point(12, 110);
            this.buttonReset2P.Name = "buttonReset2P";
            this.buttonReset2P.Size = new System.Drawing.Size(35, 13);
            this.buttonReset2P.TabIndex = 0;
            this.buttonReset2P.Text = "Reset";
            // 
            // buttonPopout2P
            // 
            this.buttonPopout2P.AutoSize = true;
            this.buttonPopout2P.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonPopout2P.Location = new System.Drawing.Point(244, 110);
            this.buttonPopout2P.Name = "buttonPopout2P";
            this.buttonPopout2P.Size = new System.Drawing.Size(44, 13);
            this.buttonPopout2P.TabIndex = 0;
            this.buttonPopout2P.Text = "Pop out";
            // 
            // label4PPlayer1Rating
            // 
            this.label4PPlayer1Rating.AutoSize = true;
            this.label4PPlayer1Rating.Location = new System.Drawing.Point(6, 235);
            this.label4PPlayer1Rating.Name = "label4PPlayer1Rating";
            this.label4PPlayer1Rating.Size = new System.Drawing.Size(82, 13);
            this.label4PPlayer1Rating.TabIndex = 1;
            this.label4PPlayer1Rating.Text = "Player 1 Rating:";
            // 
            // value4PPlayer1Rating
            // 
            this.value4PPlayer1Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value4PPlayer1Rating.Location = new System.Drawing.Point(83, 235);
            this.value4PPlayer1Rating.Name = "value4PPlayer1Rating";
            this.value4PPlayer1Rating.Size = new System.Drawing.Size(60, 13);
            this.value4PPlayer1Rating.TabIndex = 1;
            this.value4PPlayer1Rating.Text = "?";
            this.value4PPlayer1Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonReset4P
            // 
            this.buttonReset4P.AutoSize = true;
            this.buttonReset4P.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonReset4P.Location = new System.Drawing.Point(12, 211);
            this.buttonReset4P.Name = "buttonReset4P";
            this.buttonReset4P.Size = new System.Drawing.Size(35, 13);
            this.buttonReset4P.TabIndex = 0;
            this.buttonReset4P.Text = "Reset";
            // 
            // buttonPopout4P
            // 
            this.buttonPopout4P.AutoSize = true;
            this.buttonPopout4P.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonPopout4P.Location = new System.Drawing.Point(244, 211);
            this.buttonPopout4P.Name = "buttonPopout4P";
            this.buttonPopout4P.Size = new System.Drawing.Size(44, 13);
            this.buttonPopout4P.TabIndex = 0;
            this.buttonPopout4P.Text = "Pop out";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(300, 353);
            this.Controls.Add(this.vertDelimiter3);
            this.Controls.Add(this.vertDelimiter2);
            this.Controls.Add(this.vertDelimiter1);
            this.Controls.Add(this.horiDelimiter3);
            this.Controls.Add(this.horiDelimiter2);
            this.Controls.Add(this.horiDelimiter1);
            this.Controls.Add(this.labelDelimiter8);
            this.Controls.Add(this.labelDelimiter9);
            this.Controls.Add(this.labelDelimiter7);
            this.Controls.Add(this.labelDelimiter2);
            this.Controls.Add(this.labelDelimiter11);
            this.Controls.Add(this.labelDelimiter12);
            this.Controls.Add(this.labelDelimiter10);
            this.Controls.Add(this.labelDelimiter3);
            this.Controls.Add(this.labelDelimiter5);
            this.Controls.Add(this.labelDelimiter6);
            this.Controls.Add(this.labelDelimiter4);
            this.Controls.Add(this.labelDelimiter1);
            this.Controls.Add(this.value4PSets4);
            this.Controls.Add(this.value4PSets2);
            this.Controls.Add(this.value2PSets2);
            this.Controls.Add(this.value4PTotal4);
            this.Controls.Add(this.value4PTotal2);
            this.Controls.Add(this.value2PTotal2);
            this.Controls.Add(this.value4PScore4);
            this.Controls.Add(this.value4PScore2);
            this.Controls.Add(this.value2PScore2);
            this.Controls.Add(this.value4PSets3);
            this.Controls.Add(this.value4PSets1);
            this.Controls.Add(this.value2PSets1);
            this.Controls.Add(this.value4PTotal3);
            this.Controls.Add(this.value4PTotal1);
            this.Controls.Add(this.value2PTotal1);
            this.Controls.Add(this.value4PScore3);
            this.Controls.Add(this.value4PScore1);
            this.Controls.Add(this.value2PScore1);
            this.Controls.Add(this.value4PPlayer1Rating);
            this.Controls.Add(this.value4PPlayer2Rating);
            this.Controls.Add(this.value2PPlayer1Rating);
            this.Controls.Add(this.value2PPlayer2Rating);
            this.Controls.Add(this.value4PPlayer4Rating);
            this.Controls.Add(this.valueWins);
            this.Controls.Add(this.valueStartingRating);
            this.Controls.Add(this.valueLosses);
            this.Controls.Add(this.valueCurrentRating);
            this.Controls.Add(this.value4PPlayer3Rating);
            this.Controls.Add(this.labelLosses);
            this.Controls.Add(this.labelWins);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.labelPuzzle);
            this.Controls.Add(this.labelStartingRating);
            this.Controls.Add(this.valueRatingDifference);
            this.Controls.Add(this.label4PPlayer4Rating);
            this.Controls.Add(this.label4PTotal);
            this.Controls.Add(this.label4PPlayer3Rating);
            this.Controls.Add(this.label4PScore);
            this.Controls.Add(this.label2PTotal);
            this.Controls.Add(this.label4PSets);
            this.Controls.Add(this.label2PScore);
            this.Controls.Add(this.label4PPlayer1Rating);
            this.Controls.Add(this.label4PPlayer2Rating);
            this.Controls.Add(this.label2PSets);
            this.Controls.Add(this.label2PPlayer1Rating);
            this.Controls.Add(this.label2PPlayer2Rating);
            this.Controls.Add(this.labelRatingDifference);
            this.Controls.Add(this.labelCurrentRating);
            this.Controls.Add(this.buttonRehook);
            this.Controls.Add(this.buttonPopout4P);
            this.Controls.Add(this.buttonPopout2P);
            this.Controls.Add(this.buttonPopoutPuzzle);
            this.Controls.Add(this.buttonReset4P);
            this.Controls.Add(this.buttonReset2P);
            this.Controls.Add(this.buttonResetPuzzle);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "PPT Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer ScanTimer;
        private System.Windows.Forms.Label labelCurrentRating;
        private System.Windows.Forms.Label labelStartingRating;
        private System.Windows.Forms.Label labelRatingDifference;
        private System.Windows.Forms.Label buttonRehook;
        private System.Windows.Forms.Label valueRatingDifference;
        private System.Windows.Forms.Label valueCurrentRating;
        private System.Windows.Forms.Label valueStartingRating;
        private System.Windows.Forms.Label buttonResetPuzzle;
        private System.Windows.Forms.PictureBox horiDelimiter1;
        private System.Windows.Forms.Label label2PPlayer2Rating;
        private System.Windows.Forms.Label label4PPlayer3Rating;
        private System.Windows.Forms.Label label4PPlayer4Rating;
        private System.Windows.Forms.Label value4PPlayer3Rating;
        private System.Windows.Forms.Label value4PPlayer4Rating;
        private System.Windows.Forms.Label value2PPlayer2Rating;
        private System.Windows.Forms.Label labelWins;
        private System.Windows.Forms.Label labelLosses;
        private System.Windows.Forms.PictureBox vertDelimiter1;
        private System.Windows.Forms.Label valueLosses;
        private System.Windows.Forms.Label valueWins;
        private System.Windows.Forms.Label labelPuzzle;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label2PScore;
        private System.Windows.Forms.Label value2PScore1;
        private System.Windows.Forms.Label value2PScore2;
        private System.Windows.Forms.Label labelDelimiter1;
        private System.Windows.Forms.Label label2PSets;
        private System.Windows.Forms.Label value2PSets1;
        private System.Windows.Forms.Label value2PSets2;
        private System.Windows.Forms.Label labelDelimiter2;
        private System.Windows.Forms.PictureBox vertDelimiter2;
        private System.Windows.Forms.Label label2PTotal;
        private System.Windows.Forms.Label value2PTotal1;
        private System.Windows.Forms.Label value2PTotal2;
        private System.Windows.Forms.Label labelDelimiter3;
        private System.Windows.Forms.Label label4PPlayer2Rating;
        private System.Windows.Forms.Label label4PSets;
        private System.Windows.Forms.Label label4PScore;
        private System.Windows.Forms.Label label4PTotal;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label value4PPlayer2Rating;
        private System.Windows.Forms.Label value4PScore1;
        private System.Windows.Forms.Label value4PTotal1;
        private System.Windows.Forms.Label value4PSets1;
        private System.Windows.Forms.Label value4PScore2;
        private System.Windows.Forms.Label value4PTotal2;
        private System.Windows.Forms.Label value4PSets2;
        private System.Windows.Forms.Label labelDelimiter4;
        private System.Windows.Forms.Label labelDelimiter10;
        private System.Windows.Forms.Label labelDelimiter7;
        private System.Windows.Forms.PictureBox vertDelimiter3;
        private System.Windows.Forms.PictureBox horiDelimiter2;
        private System.Windows.Forms.Label value4PScore3;
        private System.Windows.Forms.Label value4PTotal3;
        private System.Windows.Forms.Label value4PSets3;
        private System.Windows.Forms.Label value4PScore4;
        private System.Windows.Forms.Label value4PTotal4;
        private System.Windows.Forms.Label value4PSets4;
        private System.Windows.Forms.Label labelDelimiter6;
        private System.Windows.Forms.Label labelDelimiter12;
        private System.Windows.Forms.Label labelDelimiter9;
        private System.Windows.Forms.Label labelDelimiter5;
        private System.Windows.Forms.Label labelDelimiter11;
        private System.Windows.Forms.Label labelDelimiter8;
        private System.Windows.Forms.PictureBox horiDelimiter3;
        private System.Windows.Forms.Label label2PPlayer1Rating;
        private System.Windows.Forms.Label value2PPlayer1Rating;
        private System.Windows.Forms.Label buttonPopoutPuzzle;
        private System.Windows.Forms.Label buttonReset2P;
        private System.Windows.Forms.Label buttonPopout2P;
        private System.Windows.Forms.Label label4PPlayer1Rating;
        private System.Windows.Forms.Label value4PPlayer1Rating;
        private System.Windows.Forms.Label buttonReset4P;
        private System.Windows.Forms.Label buttonPopout4P;
    }
}

