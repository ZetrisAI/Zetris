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
            this.labelScoreAddress = new System.Windows.Forms.Label();
            this.labelWins = new System.Windows.Forms.Label();
            this.labelLosses = new System.Windows.Forms.Label();
            this.valueLosses = new System.Windows.Forms.Label();
            this.valueWins = new System.Windows.Forms.Label();
            this.labelPuzzle = new System.Windows.Forms.Label();
            this.valueP2Name = new System.Windows.Forms.Label();
            this.label4PSets = new System.Windows.Forms.Label();
            this.label4PScore = new System.Windows.Forms.Label();
            this.label4PTotal = new System.Windows.Forms.Label();
            this.labelPlayerInfo = new System.Windows.Forms.Label();
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
            this.buttonPopoutPuzzle = new System.Windows.Forms.Label();
            this.valueP1Name = new System.Windows.Forms.Label();
            this.buttonResetBattle = new System.Windows.Forms.Label();
            this.buttonPopoutBattle = new System.Windows.Forms.Label();
            this.valueP2League = new System.Windows.Forms.Label();
            this.valueP1League = new System.Windows.Forms.Label();
            this.valuePlayers = new System.Windows.Forms.Label();
            this.valueP2Ratio = new System.Windows.Forms.Label();
            this.valueP1Ratio = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.TextBox();
            this.valueP1Region = new System.Windows.Forms.Label();
            this.valueP2Region = new System.Windows.Forms.Label();
            this.labelDebug = new System.Windows.Forms.Label();
            this.labelPlayerAddress = new System.Windows.Forms.Label();
            this.labelLeagueAddress = new System.Windows.Forms.Label();
            this.valueScoreAddress = new System.Windows.Forms.Label();
            this.valuePlayerAddress = new System.Windows.Forms.Label();
            this.valueLeagueAddress = new System.Windows.Forms.Label();
            this.valueP1Rating = new System.Windows.Forms.Label();
            this.valueP1Regional = new System.Windows.Forms.Label();
            this.valueP1Worldwide = new System.Windows.Forms.Label();
            this.valueP2Regional = new System.Windows.Forms.Label();
            this.valueP2Worldwide = new System.Windows.Forms.Label();
            this.valueP4Rating = new System.Windows.Forms.Label();
            this.valueP3Rating = new System.Windows.Forms.Label();
            this.valueP4Name = new System.Windows.Forms.Label();
            this.valueP3Name = new System.Windows.Forms.Label();
            this.valueP4League = new System.Windows.Forms.Label();
            this.valueP4Ratio = new System.Windows.Forms.Label();
            this.valueP3Region = new System.Windows.Forms.Label();
            this.valueP4Region = new System.Windows.Forms.Label();
            this.valueP3League = new System.Windows.Forms.Label();
            this.valueP3Ratio = new System.Windows.Forms.Label();
            this.valueP3Regional = new System.Windows.Forms.Label();
            this.valueP4Regional = new System.Windows.Forms.Label();
            this.valueP3Worldwide = new System.Windows.Forms.Label();
            this.valueP4Worldwide = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
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
            this.labelCurrentRating.Location = new System.Drawing.Point(6, 57);
            this.labelCurrentRating.Name = "labelCurrentRating";
            this.labelCurrentRating.Size = new System.Drawing.Size(78, 13);
            this.labelCurrentRating.TabIndex = 1;
            this.labelCurrentRating.Text = "Current Rating:";
            // 
            // labelStartingRating
            // 
            this.labelStartingRating.AutoSize = true;
            this.labelStartingRating.Location = new System.Drawing.Point(6, 35);
            this.labelStartingRating.Name = "labelStartingRating";
            this.labelStartingRating.Size = new System.Drawing.Size(80, 13);
            this.labelStartingRating.TabIndex = 1;
            this.labelStartingRating.Text = "Starting Rating:";
            // 
            // labelRatingDifference
            // 
            this.labelRatingDifference.AutoSize = true;
            this.labelRatingDifference.Location = new System.Drawing.Point(6, 79);
            this.labelRatingDifference.Name = "labelRatingDifference";
            this.labelRatingDifference.Size = new System.Drawing.Size(93, 13);
            this.labelRatingDifference.TabIndex = 1;
            this.labelRatingDifference.Text = "Rating Difference:";
            // 
            // buttonRehook
            // 
            this.buttonRehook.AutoSize = true;
            this.buttonRehook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonRehook.Location = new System.Drawing.Point(455, 406);
            this.buttonRehook.Name = "buttonRehook";
            this.buttonRehook.Size = new System.Drawing.Size(98, 13);
            this.buttonRehook.TabIndex = 0;
            this.buttonRehook.Text = "Rehook to Process";
            this.buttonRehook.Click += new System.EventHandler(this.buttonRehook_Click);
            // 
            // valueRatingDifference
            // 
            this.valueRatingDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueRatingDifference.Location = new System.Drawing.Point(95, 79);
            this.valueRatingDifference.Name = "valueRatingDifference";
            this.valueRatingDifference.Size = new System.Drawing.Size(49, 13);
            this.valueRatingDifference.TabIndex = 1;
            this.valueRatingDifference.Text = "?";
            this.valueRatingDifference.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueCurrentRating
            // 
            this.valueCurrentRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueCurrentRating.Location = new System.Drawing.Point(83, 57);
            this.valueCurrentRating.Name = "valueCurrentRating";
            this.valueCurrentRating.Size = new System.Drawing.Size(61, 13);
            this.valueCurrentRating.TabIndex = 1;
            this.valueCurrentRating.Text = "?";
            this.valueCurrentRating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueStartingRating
            // 
            this.valueStartingRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueStartingRating.Location = new System.Drawing.Point(83, 35);
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
            // labelScoreAddress
            // 
            this.labelScoreAddress.Location = new System.Drawing.Point(161, 363);
            this.labelScoreAddress.Name = "labelScoreAddress";
            this.labelScoreAddress.Size = new System.Drawing.Size(75, 13);
            this.labelScoreAddress.TabIndex = 1;
            this.labelScoreAddress.Text = "Score Addr:";
            // 
            // labelWins
            // 
            this.labelWins.AutoSize = true;
            this.labelWins.Location = new System.Drawing.Point(185, 47);
            this.labelWins.Name = "labelWins";
            this.labelWins.Size = new System.Drawing.Size(34, 13);
            this.labelWins.TabIndex = 1;
            this.labelWins.Text = "Wins:";
            // 
            // labelLosses
            // 
            this.labelLosses.AutoSize = true;
            this.labelLosses.Location = new System.Drawing.Point(185, 69);
            this.labelLosses.Name = "labelLosses";
            this.labelLosses.Size = new System.Drawing.Size(43, 13);
            this.labelLosses.TabIndex = 1;
            this.labelLosses.Text = "Losses:";
            // 
            // valueLosses
            // 
            this.valueLosses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLosses.Location = new System.Drawing.Point(225, 69);
            this.valueLosses.Name = "valueLosses";
            this.valueLosses.Size = new System.Drawing.Size(40, 13);
            this.valueLosses.TabIndex = 1;
            this.valueLosses.Text = "0";
            this.valueLosses.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueWins
            // 
            this.valueWins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueWins.Location = new System.Drawing.Point(225, 47);
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
            this.labelPuzzle.Location = new System.Drawing.Point(116, 9);
            this.labelPuzzle.Name = "labelPuzzle";
            this.labelPuzzle.Size = new System.Drawing.Size(77, 13);
            this.labelPuzzle.TabIndex = 1;
            this.labelPuzzle.Text = "Puzzle League";
            this.labelPuzzle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // valueP2Name
            // 
            this.valueP2Name.Location = new System.Drawing.Point(164, 144);
            this.valueP2Name.Name = "valueP2Name";
            this.valueP2Name.Size = new System.Drawing.Size(93, 13);
            this.valueP2Name.TabIndex = 1;
            this.valueP2Name.Text = "Player 2";
            // 
            // label4PSets
            // 
            this.label4PSets.AutoSize = true;
            this.label4PSets.Location = new System.Drawing.Point(8, 384);
            this.label4PSets.Name = "label4PSets";
            this.label4PSets.Size = new System.Drawing.Size(31, 13);
            this.label4PSets.TabIndex = 1;
            this.label4PSets.Text = "Sets:";
            // 
            // label4PScore
            // 
            this.label4PScore.AutoSize = true;
            this.label4PScore.Location = new System.Drawing.Point(8, 363);
            this.label4PScore.Name = "label4PScore";
            this.label4PScore.Size = new System.Drawing.Size(38, 13);
            this.label4PScore.TabIndex = 1;
            this.label4PScore.Text = "Score:";
            // 
            // label4PTotal
            // 
            this.label4PTotal.AutoSize = true;
            this.label4PTotal.Location = new System.Drawing.Point(8, 405);
            this.label4PTotal.Name = "label4PTotal";
            this.label4PTotal.Size = new System.Drawing.Size(34, 13);
            this.label4PTotal.TabIndex = 1;
            this.label4PTotal.Text = "Total:";
            // 
            // labelPlayerInfo
            // 
            this.labelPlayerInfo.AutoSize = true;
            this.labelPlayerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelPlayerInfo.Location = new System.Drawing.Point(120, 116);
            this.labelPlayerInfo.Name = "labelPlayerInfo";
            this.labelPlayerInfo.Size = new System.Drawing.Size(57, 13);
            this.labelPlayerInfo.TabIndex = 1;
            this.labelPlayerInfo.Text = "Player Info";
            this.labelPlayerInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // valueP2Rating
            // 
            this.valueP2Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Rating.Location = new System.Drawing.Point(244, 166);
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
            this.valueScore1.Location = new System.Drawing.Point(51, 363);
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
            this.valueTotal1.Location = new System.Drawing.Point(51, 405);
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
            this.valueSets1.Location = new System.Drawing.Point(51, 384);
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
            this.valueScore2.Location = new System.Drawing.Point(72, 363);
            this.valueScore2.Name = "valueScore2";
            this.valueScore2.Size = new System.Drawing.Size(14, 13);
            this.valueScore2.TabIndex = 1;
            this.valueScore2.Text = "?";
            // 
            // valueTotal2
            // 
            this.valueTotal2.AutoSize = true;
            this.valueTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTotal2.Location = new System.Drawing.Point(72, 405);
            this.valueTotal2.Name = "valueTotal2";
            this.valueTotal2.Size = new System.Drawing.Size(14, 13);
            this.valueTotal2.TabIndex = 1;
            this.valueTotal2.Text = "?";
            // 
            // valueSets2
            // 
            this.valueSets2.AutoSize = true;
            this.valueSets2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSets2.Location = new System.Drawing.Point(72, 384);
            this.valueSets2.Name = "valueSets2";
            this.valueSets2.Size = new System.Drawing.Size(14, 13);
            this.valueSets2.TabIndex = 1;
            this.valueSets2.Text = "?";
            // 
            // labelDelimiter4
            // 
            this.labelDelimiter4.AutoSize = true;
            this.labelDelimiter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter4.Location = new System.Drawing.Point(63, 363);
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
            this.labelDelimiter10.Location = new System.Drawing.Point(63, 405);
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
            this.labelDelimiter7.Location = new System.Drawing.Point(63, 384);
            this.labelDelimiter7.Name = "labelDelimiter7";
            this.labelDelimiter7.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter7.TabIndex = 1;
            this.labelDelimiter7.Text = "-";
            this.labelDelimiter7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // valueScore3
            // 
            this.valueScore3.AutoSize = true;
            this.valueScore3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueScore3.Location = new System.Drawing.Point(93, 363);
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
            this.valueTotal3.Location = new System.Drawing.Point(93, 405);
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
            this.valueSets3.Location = new System.Drawing.Point(93, 384);
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
            this.valueScore4.Location = new System.Drawing.Point(114, 363);
            this.valueScore4.Name = "valueScore4";
            this.valueScore4.Size = new System.Drawing.Size(14, 13);
            this.valueScore4.TabIndex = 1;
            this.valueScore4.Text = "?";
            // 
            // valueTotal4
            // 
            this.valueTotal4.AutoSize = true;
            this.valueTotal4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTotal4.Location = new System.Drawing.Point(114, 405);
            this.valueTotal4.Name = "valueTotal4";
            this.valueTotal4.Size = new System.Drawing.Size(14, 13);
            this.valueTotal4.TabIndex = 1;
            this.valueTotal4.Text = "?";
            // 
            // valueSets4
            // 
            this.valueSets4.AutoSize = true;
            this.valueSets4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSets4.Location = new System.Drawing.Point(114, 384);
            this.valueSets4.Name = "valueSets4";
            this.valueSets4.Size = new System.Drawing.Size(14, 13);
            this.valueSets4.TabIndex = 1;
            this.valueSets4.Text = "?";
            // 
            // labelDelimiter6
            // 
            this.labelDelimiter6.AutoSize = true;
            this.labelDelimiter6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelimiter6.Location = new System.Drawing.Point(105, 363);
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
            this.labelDelimiter12.Location = new System.Drawing.Point(105, 405);
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
            this.labelDelimiter9.Location = new System.Drawing.Point(105, 384);
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
            this.labelDelimiter5.Location = new System.Drawing.Point(84, 363);
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
            this.labelDelimiter11.Location = new System.Drawing.Point(84, 405);
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
            this.labelDelimiter8.Location = new System.Drawing.Point(84, 384);
            this.labelDelimiter8.Name = "labelDelimiter8";
            this.labelDelimiter8.Size = new System.Drawing.Size(11, 13);
            this.labelDelimiter8.TabIndex = 1;
            this.labelDelimiter8.Text = "-";
            this.labelDelimiter8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonPopoutPuzzle
            // 
            this.buttonPopoutPuzzle.AutoSize = true;
            this.buttonPopoutPuzzle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonPopoutPuzzle.Location = new System.Drawing.Point(246, 9);
            this.buttonPopoutPuzzle.Name = "buttonPopoutPuzzle";
            this.buttonPopoutPuzzle.Size = new System.Drawing.Size(44, 13);
            this.buttonPopoutPuzzle.TabIndex = 0;
            this.buttonPopoutPuzzle.Text = "Pop out";
            // 
            // valueP1Name
            // 
            this.valueP1Name.Location = new System.Drawing.Point(6, 144);
            this.valueP1Name.Name = "valueP1Name";
            this.valueP1Name.Size = new System.Drawing.Size(93, 13);
            this.valueP1Name.TabIndex = 1;
            this.valueP1Name.Text = "Player 1";
            // 
            // buttonResetBattle
            // 
            this.buttonResetBattle.AutoSize = true;
            this.buttonResetBattle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonResetBattle.Location = new System.Drawing.Point(12, 116);
            this.buttonResetBattle.Name = "buttonResetBattle";
            this.buttonResetBattle.Size = new System.Drawing.Size(35, 13);
            this.buttonResetBattle.TabIndex = 0;
            this.buttonResetBattle.Text = "Reset";
            // 
            // buttonPopoutBattle
            // 
            this.buttonPopoutBattle.AutoSize = true;
            this.buttonPopoutBattle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(204)))));
            this.buttonPopoutBattle.Location = new System.Drawing.Point(247, 116);
            this.buttonPopoutBattle.Name = "buttonPopoutBattle";
            this.buttonPopoutBattle.Size = new System.Drawing.Size(44, 13);
            this.buttonPopoutBattle.TabIndex = 0;
            this.buttonPopoutBattle.Text = "Pop out";
            // 
            // valueP2League
            // 
            this.valueP2League.Location = new System.Drawing.Point(164, 166);
            this.valueP2League.Name = "valueP2League";
            this.valueP2League.Size = new System.Drawing.Size(74, 13);
            this.valueP2League.TabIndex = 4;
            // 
            // valueP1League
            // 
            this.valueP1League.Location = new System.Drawing.Point(6, 166);
            this.valueP1League.Name = "valueP1League";
            this.valueP1League.Size = new System.Drawing.Size(74, 13);
            this.valueP1League.TabIndex = 4;
            this.valueP1League.Text = "Grand Master";
            // 
            // valuePlayers
            // 
            this.valuePlayers.Location = new System.Drawing.Point(175, 116);
            this.valuePlayers.Name = "valuePlayers";
            this.valuePlayers.Size = new System.Drawing.Size(23, 13);
            this.valuePlayers.TabIndex = 4;
            this.valuePlayers.Text = "0";
            // 
            // valueP2Ratio
            // 
            this.valueP2Ratio.Location = new System.Drawing.Point(283, 144);
            this.valueP2Ratio.Name = "valueP2Ratio";
            this.valueP2Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP2Ratio.TabIndex = 4;
            this.valueP2Ratio.Text = "X";
            // 
            // valueP1Ratio
            // 
            this.valueP1Ratio.Location = new System.Drawing.Point(125, 144);
            this.valueP1Ratio.Name = "valueP1Ratio";
            this.valueP1Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP1Ratio.TabIndex = 4;
            this.valueP1Ratio.Text = "X";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelLog.Location = new System.Drawing.Point(489, 9);
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
            this.log.Location = new System.Drawing.Point(311, 35);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(377, 363);
            this.log.TabIndex = 5;
            // 
            // valueP1Region
            // 
            this.valueP1Region.Location = new System.Drawing.Point(6, 188);
            this.valueP1Region.Name = "valueP1Region";
            this.valueP1Region.Size = new System.Drawing.Size(136, 13);
            this.valueP1Region.TabIndex = 4;
            this.valueP1Region.Text = "People\'s Republic of China";
            // 
            // valueP2Region
            // 
            this.valueP2Region.Location = new System.Drawing.Point(164, 188);
            this.valueP2Region.Name = "valueP2Region";
            this.valueP2Region.Size = new System.Drawing.Size(136, 13);
            this.valueP2Region.TabIndex = 4;
            this.valueP2Region.Text = "People\'s Republic of China";
            // 
            // labelDebug
            // 
            this.labelDebug.AutoSize = true;
            this.labelDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelDebug.Location = new System.Drawing.Point(209, 338);
            this.labelDebug.Name = "labelDebug";
            this.labelDebug.Size = new System.Drawing.Size(39, 13);
            this.labelDebug.TabIndex = 1;
            this.labelDebug.Text = "Debug";
            this.labelDebug.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayerAddress
            // 
            this.labelPlayerAddress.Location = new System.Drawing.Point(161, 384);
            this.labelPlayerAddress.Name = "labelPlayerAddress";
            this.labelPlayerAddress.Size = new System.Drawing.Size(75, 13);
            this.labelPlayerAddress.TabIndex = 1;
            this.labelPlayerAddress.Text = "Player Addr:";
            // 
            // labelLeagueAddress
            // 
            this.labelLeagueAddress.Location = new System.Drawing.Point(161, 405);
            this.labelLeagueAddress.Name = "labelLeagueAddress";
            this.labelLeagueAddress.Size = new System.Drawing.Size(75, 13);
            this.labelLeagueAddress.TabIndex = 1;
            this.labelLeagueAddress.Text = "League Addr:";
            // 
            // valueScoreAddress
            // 
            this.valueScoreAddress.Location = new System.Drawing.Point(227, 363);
            this.valueScoreAddress.Name = "valueScoreAddress";
            this.valueScoreAddress.Size = new System.Drawing.Size(78, 13);
            this.valueScoreAddress.TabIndex = 1;
            this.valueScoreAddress.Text = "0x00000000";
            // 
            // valuePlayerAddress
            // 
            this.valuePlayerAddress.Location = new System.Drawing.Point(227, 384);
            this.valuePlayerAddress.Name = "valuePlayerAddress";
            this.valuePlayerAddress.Size = new System.Drawing.Size(78, 13);
            this.valuePlayerAddress.TabIndex = 1;
            this.valuePlayerAddress.Text = "0x00000000";
            // 
            // valueLeagueAddress
            // 
            this.valueLeagueAddress.Location = new System.Drawing.Point(227, 405);
            this.valueLeagueAddress.Name = "valueLeagueAddress";
            this.valueLeagueAddress.Size = new System.Drawing.Size(78, 13);
            this.valueLeagueAddress.TabIndex = 1;
            this.valueLeagueAddress.Text = "0x00000000";
            // 
            // valueP1Rating
            // 
            this.valueP1Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Rating.Location = new System.Drawing.Point(86, 166);
            this.valueP1Rating.Name = "valueP1Rating";
            this.valueP1Rating.Size = new System.Drawing.Size(52, 13);
            this.valueP1Rating.TabIndex = 1;
            this.valueP1Rating.Text = "?";
            this.valueP1Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP1Regional
            // 
            this.valueP1Regional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Regional.Location = new System.Drawing.Point(30, 209);
            this.valueP1Regional.Name = "valueP1Regional";
            this.valueP1Regional.Size = new System.Drawing.Size(44, 13);
            this.valueP1Regional.TabIndex = 6;
            this.valueP1Regional.Text = "?";
            this.valueP1Regional.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP1Worldwide
            // 
            this.valueP1Worldwide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP1Worldwide.Location = new System.Drawing.Point(93, 209);
            this.valueP1Worldwide.Name = "valueP1Worldwide";
            this.valueP1Worldwide.Size = new System.Drawing.Size(44, 13);
            this.valueP1Worldwide.TabIndex = 6;
            this.valueP1Worldwide.Text = "?";
            this.valueP1Worldwide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP2Regional
            // 
            this.valueP2Regional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Regional.Location = new System.Drawing.Point(188, 209);
            this.valueP2Regional.Name = "valueP2Regional";
            this.valueP2Regional.Size = new System.Drawing.Size(44, 13);
            this.valueP2Regional.TabIndex = 6;
            this.valueP2Regional.Text = "?";
            this.valueP2Regional.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP2Worldwide
            // 
            this.valueP2Worldwide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP2Worldwide.Location = new System.Drawing.Point(251, 209);
            this.valueP2Worldwide.Name = "valueP2Worldwide";
            this.valueP2Worldwide.Size = new System.Drawing.Size(44, 13);
            this.valueP2Worldwide.TabIndex = 6;
            this.valueP2Worldwide.Text = "?";
            this.valueP2Worldwide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP4Rating
            // 
            this.valueP4Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP4Rating.Location = new System.Drawing.Point(244, 263);
            this.valueP4Rating.Name = "valueP4Rating";
            this.valueP4Rating.Size = new System.Drawing.Size(52, 13);
            this.valueP4Rating.TabIndex = 1;
            this.valueP4Rating.Text = "?";
            this.valueP4Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP3Rating
            // 
            this.valueP3Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP3Rating.Location = new System.Drawing.Point(86, 263);
            this.valueP3Rating.Name = "valueP3Rating";
            this.valueP3Rating.Size = new System.Drawing.Size(52, 13);
            this.valueP3Rating.TabIndex = 1;
            this.valueP3Rating.Text = "?";
            this.valueP3Rating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP4Name
            // 
            this.valueP4Name.Location = new System.Drawing.Point(164, 241);
            this.valueP4Name.Name = "valueP4Name";
            this.valueP4Name.Size = new System.Drawing.Size(93, 13);
            this.valueP4Name.TabIndex = 1;
            this.valueP4Name.Text = "Player 4";
            // 
            // valueP3Name
            // 
            this.valueP3Name.Location = new System.Drawing.Point(6, 241);
            this.valueP3Name.Name = "valueP3Name";
            this.valueP3Name.Size = new System.Drawing.Size(93, 13);
            this.valueP3Name.TabIndex = 1;
            this.valueP3Name.Text = "Player 3";
            // 
            // valueP4League
            // 
            this.valueP4League.Location = new System.Drawing.Point(164, 263);
            this.valueP4League.Name = "valueP4League";
            this.valueP4League.Size = new System.Drawing.Size(74, 13);
            this.valueP4League.TabIndex = 4;
            // 
            // valueP4Ratio
            // 
            this.valueP4Ratio.Location = new System.Drawing.Point(283, 241);
            this.valueP4Ratio.Name = "valueP4Ratio";
            this.valueP4Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP4Ratio.TabIndex = 4;
            this.valueP4Ratio.Text = "X";
            // 
            // valueP3Region
            // 
            this.valueP3Region.Location = new System.Drawing.Point(6, 285);
            this.valueP3Region.Name = "valueP3Region";
            this.valueP3Region.Size = new System.Drawing.Size(136, 13);
            this.valueP3Region.TabIndex = 4;
            this.valueP3Region.Text = "People\'s Republic of China";
            // 
            // valueP4Region
            // 
            this.valueP4Region.Location = new System.Drawing.Point(164, 285);
            this.valueP4Region.Name = "valueP4Region";
            this.valueP4Region.Size = new System.Drawing.Size(136, 13);
            this.valueP4Region.TabIndex = 4;
            this.valueP4Region.Text = "People\'s Republic of China";
            // 
            // valueP3League
            // 
            this.valueP3League.Location = new System.Drawing.Point(6, 263);
            this.valueP3League.Name = "valueP3League";
            this.valueP3League.Size = new System.Drawing.Size(74, 13);
            this.valueP3League.TabIndex = 4;
            this.valueP3League.Text = "Grand Master";
            // 
            // valueP3Ratio
            // 
            this.valueP3Ratio.Location = new System.Drawing.Point(125, 241);
            this.valueP3Ratio.Name = "valueP3Ratio";
            this.valueP3Ratio.Size = new System.Drawing.Size(11, 13);
            this.valueP3Ratio.TabIndex = 4;
            this.valueP3Ratio.Text = "X";
            // 
            // valueP3Regional
            // 
            this.valueP3Regional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP3Regional.Location = new System.Drawing.Point(30, 306);
            this.valueP3Regional.Name = "valueP3Regional";
            this.valueP3Regional.Size = new System.Drawing.Size(44, 13);
            this.valueP3Regional.TabIndex = 6;
            this.valueP3Regional.Text = "?";
            this.valueP3Regional.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP4Regional
            // 
            this.valueP4Regional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP4Regional.Location = new System.Drawing.Point(188, 306);
            this.valueP4Regional.Name = "valueP4Regional";
            this.valueP4Regional.Size = new System.Drawing.Size(44, 13);
            this.valueP4Regional.TabIndex = 6;
            this.valueP4Regional.Text = "?";
            this.valueP4Regional.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP3Worldwide
            // 
            this.valueP3Worldwide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP3Worldwide.Location = new System.Drawing.Point(93, 306);
            this.valueP3Worldwide.Name = "valueP3Worldwide";
            this.valueP3Worldwide.Size = new System.Drawing.Size(44, 13);
            this.valueP3Worldwide.TabIndex = 6;
            this.valueP3Worldwide.Text = "?";
            this.valueP3Worldwide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // valueP4Worldwide
            // 
            this.valueP4Worldwide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueP4Worldwide.Location = new System.Drawing.Point(251, 306);
            this.valueP4Worldwide.Name = "valueP4Worldwide";
            this.valueP4Worldwide.Size = new System.Drawing.Size(44, 13);
            this.valueP4Worldwide.TabIndex = 6;
            this.valueP4Worldwide.Text = "?";
            this.valueP4Worldwide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.labelScore.Location = new System.Drawing.Point(60, 338);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(35, 13);
            this.labelScore.TabIndex = 1;
            this.labelScore.Text = "Score";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(701, 429);
            this.Controls.Add(this.valueP4Worldwide);
            this.Controls.Add(this.valueP2Worldwide);
            this.Controls.Add(this.valueP3Worldwide);
            this.Controls.Add(this.valueP1Worldwide);
            this.Controls.Add(this.valueP4Regional);
            this.Controls.Add(this.valueP2Regional);
            this.Controls.Add(this.valueP3Regional);
            this.Controls.Add(this.valueP1Regional);
            this.Controls.Add(this.log);
            this.Controls.Add(this.valueP3Ratio);
            this.Controls.Add(this.valueP1Ratio);
            this.Controls.Add(this.valueP3League);
            this.Controls.Add(this.valueP1League);
            this.Controls.Add(this.valueP4Region);
            this.Controls.Add(this.valueP2Region);
            this.Controls.Add(this.valueP3Region);
            this.Controls.Add(this.valueP1Region);
            this.Controls.Add(this.valuePlayers);
            this.Controls.Add(this.valueP4Ratio);
            this.Controls.Add(this.valueP2Ratio);
            this.Controls.Add(this.valueP4League);
            this.Controls.Add(this.valueP2League);
            this.Controls.Add(this.valueLeagueAddress);
            this.Controls.Add(this.labelLeagueAddress);
            this.Controls.Add(this.valuePlayerAddress);
            this.Controls.Add(this.labelPlayerAddress);
            this.Controls.Add(this.valueScoreAddress);
            this.Controls.Add(this.labelScoreAddress);
            this.Controls.Add(this.valueP3Name);
            this.Controls.Add(this.valueP4Name);
            this.Controls.Add(this.valueP1Name);
            this.Controls.Add(this.valueP2Name);
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
            this.Controls.Add(this.valueP3Rating);
            this.Controls.Add(this.valueScore1);
            this.Controls.Add(this.valueP4Rating);
            this.Controls.Add(this.valueP1Rating);
            this.Controls.Add(this.valueP2Rating);
            this.Controls.Add(this.valueWins);
            this.Controls.Add(this.valueStartingRating);
            this.Controls.Add(this.valueLosses);
            this.Controls.Add(this.valueCurrentRating);
            this.Controls.Add(this.labelLosses);
            this.Controls.Add(this.labelWins);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelDebug);
            this.Controls.Add(this.labelPlayerInfo);
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
        private System.Windows.Forms.Label labelScoreAddress;
        private System.Windows.Forms.Label labelWins;
        private System.Windows.Forms.Label labelLosses;
        private System.Windows.Forms.Label valueLosses;
        private System.Windows.Forms.Label valueWins;
        private System.Windows.Forms.Label labelPuzzle;
        private System.Windows.Forms.Label valueP2Name;
        private System.Windows.Forms.Label label4PSets;
        private System.Windows.Forms.Label label4PScore;
        private System.Windows.Forms.Label label4PTotal;
        private System.Windows.Forms.Label labelPlayerInfo;
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
        private System.Windows.Forms.Label buttonPopoutPuzzle;
        private System.Windows.Forms.Label valueP1Name;
        private System.Windows.Forms.Label buttonResetBattle;
        private System.Windows.Forms.Label buttonPopoutBattle;
        private System.Windows.Forms.Label valueP2League;
        private System.Windows.Forms.Label valueP1League;
        private System.Windows.Forms.Label valuePlayers;
        private System.Windows.Forms.Label valueP2Ratio;
        private System.Windows.Forms.Label valueP1Ratio;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label valueP1Region;
        private System.Windows.Forms.Label valueP2Region;
        private System.Windows.Forms.Label labelDebug;
        private System.Windows.Forms.Label labelPlayerAddress;
        private System.Windows.Forms.Label labelLeagueAddress;
        private System.Windows.Forms.Label valueScoreAddress;
        private System.Windows.Forms.Label valuePlayerAddress;
        private System.Windows.Forms.Label valueLeagueAddress;
        private System.Windows.Forms.Label valueP1Rating;
        private System.Windows.Forms.Label valueP1Regional;
        private System.Windows.Forms.Label valueP1Worldwide;
        private System.Windows.Forms.Label valueP2Regional;
        private System.Windows.Forms.Label valueP2Worldwide;
        private System.Windows.Forms.Label valueP4Rating;
        private System.Windows.Forms.Label valueP3Rating;
        private System.Windows.Forms.Label valueP4Name;
        private System.Windows.Forms.Label valueP3Name;
        private System.Windows.Forms.Label valueP4League;
        private System.Windows.Forms.Label valueP4Ratio;
        private System.Windows.Forms.Label valueP3Region;
        private System.Windows.Forms.Label valueP4Region;
        private System.Windows.Forms.Label valueP3League;
        private System.Windows.Forms.Label valueP3Ratio;
        private System.Windows.Forms.Label valueP3Regional;
        private System.Windows.Forms.Label valueP4Regional;
        private System.Windows.Forms.Label valueP3Worldwide;
        private System.Windows.Forms.Label valueP4Worldwide;
        private System.Windows.Forms.Label labelScore;
    }
}

