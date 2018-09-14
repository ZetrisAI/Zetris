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
            this.valueP3Name = new System.Windows.Forms.Label();
            this.valueP4Name = new System.Windows.Forms.Label();
            this.valueP3Rating = new System.Windows.Forms.Label();
            this.valueP4Rating = new System.Windows.Forms.Label();
            this.labelWins = new System.Windows.Forms.Label();
            this.labelLosses = new System.Windows.Forms.Label();
            this.vertDelimiter1 = new System.Windows.Forms.PictureBox();
            this.valueLosses = new System.Windows.Forms.Label();
            this.valueWins = new System.Windows.Forms.Label();
            this.labelPuzzle = new System.Windows.Forms.Label();
            this.valueP2Name = new System.Windows.Forms.Label();
            this.label4PSets = new System.Windows.Forms.Label();
            this.label4PScore = new System.Windows.Forms.Label();
            this.label4PTotal = new System.Windows.Forms.Label();
            this.labelBattle = new System.Windows.Forms.Label();
            this.valueP2Rating = new System.Windows.Forms.Label();
            this.valueScore1 = new System.Windows.Forms.Label();
            this.valueTotal1 = new System.Windows.Forms.Label();
            this.valueSets1 = new System.Windows.Forms.Label();
            this.valueScore2 = new System.Windows.Forms.Label();
            this.valueTotal2 = new System.Windows.Forms.Label();
            this.valueSets2 = new System.Windows.Forms.Label();
            this.labelDelimiter4 = new System.Windows.Forms.Label();
            this.labelDelimiter10 = new System.Windows.Forms.Label();
            this.labelDelimiter7 = new System.Windows.Forms.Label();
            this.vertDelimiter3 = new System.Windows.Forms.PictureBox();
            this.valueScore3 = new System.Windows.Forms.Label();
            this.valueTotal3 = new System.Windows.Forms.Label();
            this.valueSets3 = new System.Windows.Forms.Label();
            this.valueScore4 = new System.Windows.Forms.Label();
            this.valueTotal4 = new System.Windows.Forms.Label();
            this.valueSets4 = new System.Windows.Forms.Label();
            this.labelDelimiter6 = new System.Windows.Forms.Label();
            this.labelDelimiter12 = new System.Windows.Forms.Label();
            this.labelDelimiter9 = new System.Windows.Forms.Label();
            this.labelDelimiter5 = new System.Windows.Forms.Label();
            this.labelDelimiter11 = new System.Windows.Forms.Label();
            this.labelDelimiter8 = new System.Windows.Forms.Label();
            this.horiDelimiter3 = new System.Windows.Forms.PictureBox();
            this.buttonPopoutPuzzle = new System.Windows.Forms.Label();
            this.valueP1Name = new System.Windows.Forms.Label();
            this.valueP1Rating = new System.Windows.Forms.Label();
            this.buttonResetBattle = new System.Windows.Forms.Label();
            this.buttonPopoutBattle = new System.Windows.Forms.Label();
            this.valueP2League = new System.Windows.Forms.Label();
            this.valueP1League = new System.Windows.Forms.Label();
            this.valuePlayers = new System.Windows.Forms.Label();
            this.valueP3League = new System.Windows.Forms.Label();
            this.valueP4League = new System.Windows.Forms.Label();
            this.valueP2Ratio = new System.Windows.Forms.Label();
            this.valueP3Ratio = new System.Windows.Forms.Label();
            this.valueP4Ratio = new System.Windows.Forms.Label();
            this.valueP1Ratio = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter3)).BeginInit();
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
            this.buttonRehook.Location = new System.Drawing.Point(103, 229);
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
            // valueP3Name
            // 
            this.valueP3Name.Location = new System.Drawing.Point(6, 174);
            this.valueP3Name.Name = "valueP3Name";
            this.valueP3Name.Size = new System.Drawing.Size(93, 13);
            this.valueP3Name.TabIndex = 1;
            this.valueP3Name.Text = "Player 3";
            // 
            // valueP4Name
            // 
            this.valueP4Name.Location = new System.Drawing.Point(6, 195);
            this.valueP4Name.Name = "valueP4Name";
            this.valueP4Name.Size = new System.Drawing.Size(93, 13);
            this.valueP4Name.TabIndex = 1;
            this.valueP4Name.Text = "Player 4";
            // 
            // valueP3Rating
            // 
            this.valueP3Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP3Rating.Location = new System.Drawing.Point(83, 174);
            this.valueP3Rating.Name = "valueP3Rating";
            this.valueP3Rating.Size = new System.Drawing.Size(60, 13);
            this.valueP3Rating.TabIndex = 1;
            this.valueP3Rating.Text = "?";
            this.valueP3Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP4Rating
            // 
            this.valueP4Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP4Rating.Location = new System.Drawing.Point(83, 195);
            this.valueP4Rating.Name = "valueP4Rating";
            this.valueP4Rating.Size = new System.Drawing.Size(60, 13);
            this.valueP4Rating.TabIndex = 1;
            this.valueP4Rating.Text = "?";
            this.valueP4Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // valueP2Name
            // 
            this.valueP2Name.Location = new System.Drawing.Point(6, 153);
            this.valueP2Name.Name = "valueP2Name";
            this.valueP2Name.Size = new System.Drawing.Size(93, 13);
            this.valueP2Name.TabIndex = 1;
            this.valueP2Name.Text = "Player 2";
            // 
            // label4PSets
            // 
            this.label4PSets.AutoSize = true;
            this.label4PSets.Location = new System.Drawing.Point(167, 162);
            this.label4PSets.Name = "label4PSets";
            this.label4PSets.Size = new System.Drawing.Size(31, 13);
            this.label4PSets.TabIndex = 1;
            this.label4PSets.Text = "Sets:";
            // 
            // label4PScore
            // 
            this.label4PScore.AutoSize = true;
            this.label4PScore.Location = new System.Drawing.Point(167, 141);
            this.label4PScore.Name = "label4PScore";
            this.label4PScore.Size = new System.Drawing.Size(38, 13);
            this.label4PScore.TabIndex = 1;
            this.label4PScore.Text = "Score:";
            // 
            // label4PTotal
            // 
            this.label4PTotal.AutoSize = true;
            this.label4PTotal.Location = new System.Drawing.Point(167, 183);
            this.label4PTotal.Name = "label4PTotal";
            this.label4PTotal.Size = new System.Drawing.Size(34, 13);
            this.label4PTotal.TabIndex = 1;
            this.label4PTotal.Text = "Total:";
            // 
            // labelBattle
            // 
            this.labelBattle.AutoSize = true;
            this.labelBattle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelBattle.Location = new System.Drawing.Point(125, 108);
            this.labelBattle.Name = "labelBattle";
            this.labelBattle.Size = new System.Drawing.Size(34, 13);
            this.labelBattle.TabIndex = 1;
            this.labelBattle.Text = "Battle";
            this.labelBattle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // valueP2Rating
            // 
            this.valueP2Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Rating.Location = new System.Drawing.Point(83, 153);
            this.valueP2Rating.Name = "valueP2Rating";
            this.valueP2Rating.Size = new System.Drawing.Size(60, 13);
            this.valueP2Rating.TabIndex = 1;
            this.valueP2Rating.Text = "?";
            this.valueP2Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueScore1
            // 
            this.valueScore1.AutoSize = true;
            this.valueScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore1.Location = new System.Drawing.Point(209, 141);
            this.valueScore1.Name = "valueScore1";
            this.valueScore1.Size = new System.Drawing.Size(14, 13);
            this.valueScore1.TabIndex = 1;
            this.valueScore1.Text = "?";
            this.valueScore1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueTotal1
            // 
            this.valueTotal1.AutoSize = true;
            this.valueTotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTotal1.Location = new System.Drawing.Point(209, 183);
            this.valueTotal1.Name = "valueTotal1";
            this.valueTotal1.Size = new System.Drawing.Size(14, 13);
            this.valueTotal1.TabIndex = 1;
            this.valueTotal1.Text = "?";
            this.valueTotal1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueSets1
            // 
            this.valueSets1.AutoSize = true;
            this.valueSets1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSets1.Location = new System.Drawing.Point(209, 162);
            this.valueSets1.Name = "valueSets1";
            this.valueSets1.Size = new System.Drawing.Size(14, 13);
            this.valueSets1.TabIndex = 1;
            this.valueSets1.Text = "?";
            this.valueSets1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueScore2
            // 
            this.valueScore2.AutoSize = true;
            this.valueScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore2.Location = new System.Drawing.Point(230, 141);
            this.valueScore2.Name = "valueScore2";
            this.valueScore2.Size = new System.Drawing.Size(14, 13);
            this.valueScore2.TabIndex = 1;
            this.valueScore2.Text = "?";
            // 
            // valueTotal2
            // 
            this.valueTotal2.AutoSize = true;
            this.valueTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTotal2.Location = new System.Drawing.Point(230, 183);
            this.valueTotal2.Name = "valueTotal2";
            this.valueTotal2.Size = new System.Drawing.Size(14, 13);
            this.valueTotal2.TabIndex = 1;
            this.valueTotal2.Text = "?";
            // 
            // valueSets2
            // 
            this.valueSets2.AutoSize = true;
            this.valueSets2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSets2.Location = new System.Drawing.Point(230, 162);
            this.valueSets2.Name = "valueSets2";
            this.valueSets2.Size = new System.Drawing.Size(14, 13);
            this.valueSets2.TabIndex = 1;
            this.valueSets2.Text = "?";
            // 
            // labelDelimiter4
            // 
            this.labelDelimiter4.AutoSize = true;
            this.labelDelimiter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter4.Location = new System.Drawing.Point(221, 141);
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
            this.labelDelimiter10.Location = new System.Drawing.Point(221, 183);
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
            this.labelDelimiter7.Location = new System.Drawing.Point(221, 162);
            this.labelDelimiter7.Name = "labelDelimiter7";
            this.labelDelimiter7.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter7.TabIndex = 1;
            this.labelDelimiter7.Text = "-";
            this.labelDelimiter7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // vertDelimiter3
            // 
            this.vertDelimiter3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.vertDelimiter3.Location = new System.Drawing.Point(150, 130);
            this.vertDelimiter3.Name = "vertDelimiter3";
            this.vertDelimiter3.Size = new System.Drawing.Size(2, 80);
            this.vertDelimiter3.TabIndex = 3;
            this.vertDelimiter3.TabStop = false;
            // 
            // valueScore3
            // 
            this.valueScore3.AutoSize = true;
            this.valueScore3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore3.Location = new System.Drawing.Point(251, 141);
            this.valueScore3.Name = "valueScore3";
            this.valueScore3.Size = new System.Drawing.Size(14, 13);
            this.valueScore3.TabIndex = 1;
            this.valueScore3.Text = "?";
            this.valueScore3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueTotal3
            // 
            this.valueTotal3.AutoSize = true;
            this.valueTotal3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTotal3.Location = new System.Drawing.Point(251, 183);
            this.valueTotal3.Name = "valueTotal3";
            this.valueTotal3.Size = new System.Drawing.Size(14, 13);
            this.valueTotal3.TabIndex = 1;
            this.valueTotal3.Text = "?";
            this.valueTotal3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueSets3
            // 
            this.valueSets3.AutoSize = true;
            this.valueSets3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSets3.Location = new System.Drawing.Point(251, 162);
            this.valueSets3.Name = "valueSets3";
            this.valueSets3.Size = new System.Drawing.Size(14, 13);
            this.valueSets3.TabIndex = 1;
            this.valueSets3.Text = "?";
            this.valueSets3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueScore4
            // 
            this.valueScore4.AutoSize = true;
            this.valueScore4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore4.Location = new System.Drawing.Point(272, 141);
            this.valueScore4.Name = "valueScore4";
            this.valueScore4.Size = new System.Drawing.Size(14, 13);
            this.valueScore4.TabIndex = 1;
            this.valueScore4.Text = "?";
            // 
            // valueTotal4
            // 
            this.valueTotal4.AutoSize = true;
            this.valueTotal4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTotal4.Location = new System.Drawing.Point(272, 183);
            this.valueTotal4.Name = "valueTotal4";
            this.valueTotal4.Size = new System.Drawing.Size(14, 13);
            this.valueTotal4.TabIndex = 1;
            this.valueTotal4.Text = "?";
            // 
            // valueSets4
            // 
            this.valueSets4.AutoSize = true;
            this.valueSets4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSets4.Location = new System.Drawing.Point(272, 162);
            this.valueSets4.Name = "valueSets4";
            this.valueSets4.Size = new System.Drawing.Size(14, 13);
            this.valueSets4.TabIndex = 1;
            this.valueSets4.Text = "?";
            // 
            // labelDelimiter6
            // 
            this.labelDelimiter6.AutoSize = true;
            this.labelDelimiter6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter6.Location = new System.Drawing.Point(263, 141);
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
            this.labelDelimiter12.Location = new System.Drawing.Point(263, 183);
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
            this.labelDelimiter9.Location = new System.Drawing.Point(263, 162);
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
            this.labelDelimiter5.Location = new System.Drawing.Point(242, 141);
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
            this.labelDelimiter11.Location = new System.Drawing.Point(242, 183);
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
            this.labelDelimiter8.Location = new System.Drawing.Point(242, 162);
            this.labelDelimiter8.Name = "labelDelimiter8";
            this.labelDelimiter8.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter8.TabIndex = 1;
            this.labelDelimiter8.Text = "-";
            this.labelDelimiter8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // horiDelimiter3
            // 
            this.horiDelimiter3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.horiDelimiter3.Location = new System.Drawing.Point(5, 219);
            this.horiDelimiter3.Name = "horiDelimiter3";
            this.horiDelimiter3.Size = new System.Drawing.Size(290, 2);
            this.horiDelimiter3.TabIndex = 2;
            this.horiDelimiter3.TabStop = false;
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
            // valueP1Name
            // 
            this.valueP1Name.Location = new System.Drawing.Point(6, 132);
            this.valueP1Name.Name = "valueP1Name";
            this.valueP1Name.Size = new System.Drawing.Size(93, 13);
            this.valueP1Name.TabIndex = 1;
            this.valueP1Name.Text = "Player 1";
            // 
            // valueP1Rating
            // 
            this.valueP1Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Rating.Location = new System.Drawing.Point(83, 132);
            this.valueP1Rating.Name = "valueP1Rating";
            this.valueP1Rating.Size = new System.Drawing.Size(60, 13);
            this.valueP1Rating.TabIndex = 1;
            this.valueP1Rating.Text = "?";
            this.valueP1Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonResetBattle
            // 
            this.buttonResetBattle.AutoSize = true;
            this.buttonResetBattle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonResetBattle.Location = new System.Drawing.Point(12, 108);
            this.buttonResetBattle.Name = "buttonResetBattle";
            this.buttonResetBattle.Size = new System.Drawing.Size(35, 13);
            this.buttonResetBattle.TabIndex = 0;
            this.buttonResetBattle.Text = "Reset";
            // 
            // buttonPopoutBattle
            // 
            this.buttonPopoutBattle.AutoSize = true;
            this.buttonPopoutBattle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonPopoutBattle.Location = new System.Drawing.Point(244, 108);
            this.buttonPopoutBattle.Name = "buttonPopoutBattle";
            this.buttonPopoutBattle.Size = new System.Drawing.Size(44, 13);
            this.buttonPopoutBattle.TabIndex = 0;
            this.buttonPopoutBattle.Text = "Pop out";
            // 
            // valueP2League
            // 
            this.valueP2League.Location = new System.Drawing.Point(141, 153);
            this.valueP2League.Name = "valueP2League";
            this.valueP2League.Size = new System.Drawing.Size(11, 13);
            this.valueP2League.TabIndex = 4;
            this.valueP2League.Text = "0";
            // 
            // valueP1League
            // 
            this.valueP1League.Location = new System.Drawing.Point(141, 132);
            this.valueP1League.Name = "valueP1League";
            this.valueP1League.Size = new System.Drawing.Size(11, 13);
            this.valueP1League.TabIndex = 4;
            this.valueP1League.Text = "0";
            // 
            // valuePlayers
            // 
            this.valuePlayers.Location = new System.Drawing.Point(265, 229);
            this.valuePlayers.Name = "valuePlayers";
            this.valuePlayers.Size = new System.Drawing.Size(23, 13);
            this.valuePlayers.TabIndex = 4;
            this.valuePlayers.Text = "0";
            // 
            // valueP3League
            // 
            this.valueP3League.Location = new System.Drawing.Point(141, 174);
            this.valueP3League.Name = "valueP3League";
            this.valueP3League.Size = new System.Drawing.Size(11, 13);
            this.valueP3League.TabIndex = 4;
            this.valueP3League.Text = "0";
            // 
            // valueP4League
            // 
            this.valueP4League.Location = new System.Drawing.Point(141, 195);
            this.valueP4League.Name = "valueP4League";
            this.valueP4League.Size = new System.Drawing.Size(11, 13);
            this.valueP4League.TabIndex = 4;
            this.valueP4League.Text = "0";
            // 
            // valueP2Ratio
            // 
            this.valueP2Ratio.Location = new System.Drawing.Point(152, 153);
            this.valueP2Ratio.Name = "valueP2Ratio";
            this.valueP2Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP2Ratio.TabIndex = 4;
            this.valueP2Ratio.Text = "P";
            // 
            // valueP3Ratio
            // 
            this.valueP3Ratio.Location = new System.Drawing.Point(152, 174);
            this.valueP3Ratio.Name = "valueP3Ratio";
            this.valueP3Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP3Ratio.TabIndex = 4;
            this.valueP3Ratio.Text = "P";
            // 
            // valueP4Ratio
            // 
            this.valueP4Ratio.Location = new System.Drawing.Point(152, 195);
            this.valueP4Ratio.Name = "valueP4Ratio";
            this.valueP4Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP4Ratio.TabIndex = 4;
            this.valueP4Ratio.Text = "P";
            // 
            // valueP1Ratio
            // 
            this.valueP1Ratio.Location = new System.Drawing.Point(152, 132);
            this.valueP1Ratio.Name = "valueP1Ratio";
            this.valueP1Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP1Ratio.TabIndex = 4;
            this.valueP1Ratio.Text = "P";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelLog.Location = new System.Drawing.Point(427, 9);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(25, 13);
            this.labelLog.TabIndex = 1;
            this.labelLog.Text = "Log";
            this.labelLog.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log.ForeColor = System.Drawing.Color.Gainsboro;
            this.log.Location = new System.Drawing.Point(301, 31);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(274, 212);
            this.log.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(587, 255);
            this.Controls.Add(this.log);
            this.Controls.Add(this.valueP1Ratio);
            this.Controls.Add(this.valueP1League);
            this.Controls.Add(this.valueP4Ratio);
            this.Controls.Add(this.valuePlayers);
            this.Controls.Add(this.valueP3Ratio);
            this.Controls.Add(this.valueP4League);
            this.Controls.Add(this.valueP2Ratio);
            this.Controls.Add(this.valueP3League);
            this.Controls.Add(this.valueP2League);
            this.Controls.Add(this.valueP4Name);
            this.Controls.Add(this.valueP3Name);
            this.Controls.Add(this.valueP1Name);
            this.Controls.Add(this.valueP2Name);
            this.Controls.Add(this.vertDelimiter3);
            this.Controls.Add(this.vertDelimiter1);
            this.Controls.Add(this.horiDelimiter3);
            this.Controls.Add(this.horiDelimiter1);
            this.Controls.Add(this.labelDelimiter8);
            this.Controls.Add(this.labelDelimiter9);
            this.Controls.Add(this.labelDelimiter7);
            this.Controls.Add(this.labelDelimiter11);
            this.Controls.Add(this.labelDelimiter12);
            this.Controls.Add(this.labelDelimiter10);
            this.Controls.Add(this.labelDelimiter5);
            this.Controls.Add(this.labelDelimiter6);
            this.Controls.Add(this.labelDelimiter4);
            this.Controls.Add(this.valueSets4);
            this.Controls.Add(this.valueSets2);
            this.Controls.Add(this.valueTotal4);
            this.Controls.Add(this.valueTotal2);
            this.Controls.Add(this.valueScore4);
            this.Controls.Add(this.valueScore2);
            this.Controls.Add(this.valueSets3);
            this.Controls.Add(this.valueSets1);
            this.Controls.Add(this.valueTotal3);
            this.Controls.Add(this.valueTotal1);
            this.Controls.Add(this.valueScore3);
            this.Controls.Add(this.valueScore1);
            this.Controls.Add(this.valueP1Rating);
            this.Controls.Add(this.valueP2Rating);
            this.Controls.Add(this.valueP4Rating);
            this.Controls.Add(this.valueWins);
            this.Controls.Add(this.valueStartingRating);
            this.Controls.Add(this.valueLosses);
            this.Controls.Add(this.valueCurrentRating);
            this.Controls.Add(this.valueP3Rating);
            this.Controls.Add(this.labelLosses);
            this.Controls.Add(this.labelWins);
            this.Controls.Add(this.labelBattle);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.labelPuzzle);
            this.Controls.Add(this.labelStartingRating);
            this.Controls.Add(this.valueRatingDifference);
            this.Controls.Add(this.label4PTotal);
            this.Controls.Add(this.label4PScore);
            this.Controls.Add(this.label4PSets);
            this.Controls.Add(this.labelRatingDifference);
            this.Controls.Add(this.labelCurrentRating);
            this.Controls.Add(this.buttonRehook);
            this.Controls.Add(this.buttonPopoutBattle);
            this.Controls.Add(this.buttonPopoutPuzzle);
            this.Controls.Add(this.buttonResetBattle);
            this.Controls.Add(this.buttonResetPuzzle);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "PPT Monitor";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.horiDelimiter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertDelimiter3)).EndInit();
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
        private System.Windows.Forms.Label valueP3Name;
        private System.Windows.Forms.Label valueP4Name;
        private System.Windows.Forms.Label valueP3Rating;
        private System.Windows.Forms.Label valueP4Rating;
        private System.Windows.Forms.Label labelWins;
        private System.Windows.Forms.Label labelLosses;
        private System.Windows.Forms.PictureBox vertDelimiter1;
        private System.Windows.Forms.Label valueLosses;
        private System.Windows.Forms.Label valueWins;
        private System.Windows.Forms.Label labelPuzzle;
        private System.Windows.Forms.Label valueP2Name;
        private System.Windows.Forms.Label label4PSets;
        private System.Windows.Forms.Label label4PScore;
        private System.Windows.Forms.Label label4PTotal;
        private System.Windows.Forms.Label labelBattle;
        private System.Windows.Forms.Label valueP2Rating;
        private System.Windows.Forms.Label valueScore1;
        private System.Windows.Forms.Label valueTotal1;
        private System.Windows.Forms.Label valueSets1;
        private System.Windows.Forms.Label valueScore2;
        private System.Windows.Forms.Label valueTotal2;
        private System.Windows.Forms.Label valueSets2;
        private System.Windows.Forms.Label labelDelimiter4;
        private System.Windows.Forms.Label labelDelimiter10;
        private System.Windows.Forms.Label labelDelimiter7;
        private System.Windows.Forms.PictureBox vertDelimiter3;
        private System.Windows.Forms.Label valueScore3;
        private System.Windows.Forms.Label valueTotal3;
        private System.Windows.Forms.Label valueSets3;
        private System.Windows.Forms.Label valueScore4;
        private System.Windows.Forms.Label valueTotal4;
        private System.Windows.Forms.Label valueSets4;
        private System.Windows.Forms.Label labelDelimiter6;
        private System.Windows.Forms.Label labelDelimiter12;
        private System.Windows.Forms.Label labelDelimiter9;
        private System.Windows.Forms.Label labelDelimiter5;
        private System.Windows.Forms.Label labelDelimiter11;
        private System.Windows.Forms.Label labelDelimiter8;
        private System.Windows.Forms.PictureBox horiDelimiter3;
        private System.Windows.Forms.Label buttonPopoutPuzzle;
        private System.Windows.Forms.Label valueP1Name;
        private System.Windows.Forms.Label valueP1Rating;
        private System.Windows.Forms.Label buttonResetBattle;
        private System.Windows.Forms.Label buttonPopoutBattle;
        private System.Windows.Forms.Label valueP2League;
        private System.Windows.Forms.Label valueP1League;
        private System.Windows.Forms.Label valuePlayers;
        private System.Windows.Forms.Label valueP3League;
        private System.Windows.Forms.Label valueP4League;
        private System.Windows.Forms.Label valueP2Ratio;
        private System.Windows.Forms.Label valueP3Ratio;
        private System.Windows.Forms.Label valueP4Ratio;
        private System.Windows.Forms.Label valueP1Ratio;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.TextBox log;
    }
}

