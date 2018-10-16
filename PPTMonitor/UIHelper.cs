using System.Drawing;
using System.Windows.Forms;

namespace PPTMonitor {
    class UIHelper {
        public static readonly string[] Regions = new string[183] {
            "Not Set",
            "Japan (Hokkaido)",
            "Japan (Aomori)",
            "Japan (Iwate)",
            "Japan (Miyagi)",
            "Japan (Akita)",
            "Japan (Yamagata)",
            "Japan (Fukushima)",
            "Japan (Ibaraki)",
            "Japan (Tochigi)",
            "Japan (Gunma)",
            "Japan (Saitama)",
            "Japan (Chiba)",
            "Japan (Tokyo)",
            "Japan (Kanagawa)",
            "Japan (Niigata)",
            "Japan (Toyama)",
            "Japan (Ishikawa)",
            "Japan (Fukui)",
            "Japan (Yamanashi)",
            "Japan (Nagano)",
            "Japan (Gifu)",
            "Japan (Shizuoka)",
            "Japan (Aichi)",
            "Japan (Mie)",
            "Japan (Shiga)",
            "Japan (Kyoto)",
            "Japan (Osaka)",
            "Japan (Hyogo)",
            "Japan (Nara)",
            "Japan (Wakayama)",
            "Japan (Tottori)",
            "Japan (Shimane)",
            "Japan (Okayama)",
            "Japan (Hiroshima)",
            "Japan (Yamaguchi)",
            "Japan (Tokushima)",
            "Japan (Kagawa)",
            "Japan (Ehime)",
            "Japan (Kochi)",
            "Japan (Fukuoka)",
            "Japan (Saga)",
            "Japan (Nagasaki)",
            "Japan (Kumamoto)",
            "Japan (Oita)",
            "Japan (Miyazaki)",
            "Japan (Kagoshima)",
            "Japan (Okinawa)",
            "USA (Alabama)",
            "USA (Alaska)",
            "USA (Arizona)",
            "USA (Arkansas)",
            "USA (California)",
            "USA (Colorado)",
            "USA (Connecticut)",
            "USA (Delaware)",
            "USA (Florida)",
            "USA (Georgia)",
            "USA (Hawaii)",
            "USA (Idaho)",
            "USA (Illinois)",
            "USA (Indiana)",
            "USA (Iowa)",
            "USA (Kansas)",
            "USA (Kentucky)",
            "USA (Louisiana)",
            "USA (Maine)",
            "USA (Maryland)",
            "USA (Massachusetts)",
            "USA (Michigan)",
            "USA (Minnesota)",
            "USA (Mississippi)",
            "USA (Missouri)",
            "USA (Montana)",
            "USA (Nebraska)",
            "USA (Nevada)",
            "USA (New Hampshire)",
            "USA (New Jersey)",
            "USA (New Mexico)",
            "USA (New York)",
            "USA (North Carolina)",
            "USA (North Dakota)",
            "USA (Ohio)",
            "USA (Oklahoma)",
            "USA (Oregon)",
            "USA (Pennsylvania)",
            "USA (Rhode Island)",
            "USA (South Carolina)",
            "USA (South Dakota)",
            "USA (Tennessee)",
            "USA (Texas)",
            "USA (Utah)",
            "USA (Vermont)",
            "USA (Virginia)",
            "USA (Washington)",
            "USA (West Virginia)",
            "USA (Wisconsin)",
            "USA (Wyoming)",
            "USA (Other)",
            "Canada",
            "North America (Other)",
            "Austria",
            "Belgium",
            "Bulgaria",
            "Croatia",
            "Czech Republic",
            "Denmark",
            "Spain",
            "Finland",
            "France",
            "Great Britain",
            "Germany",
            "Greece",
            "Hungary",
            "Ireland",
            "Israel",
            "Italy",
            "Netherlands",
            "Norway",
            "Poland",
            "Portugal",
            "Romania",
            "Russian Federation",
            "Slovenia",
            "Switzerland",
            "Slovakia",
            "Sweden",
            "Turkey",
            "Ukraine",
            "Europe (Other)",
            "Argentina",
            "Bahamas",
            "Bolivia",
            "Brazil",
            "Chile",
            "Colombia",
            "Costa Rica",
            "Cuba",
            "Ecuador",
            "Honduras",
            "Jamaica",
            "Mexico",
            "Paraguay",
            "Peru",
            "Trinidad and Tobago",
            "Uruguay",
            "Latin America (Other)",
            "Algeria",
            "Angola",
            "Côte d'Ivoire",
            "Egypt",
            "Ethiopia",
            "Gambia",
            "Ghana",
            "Guinea",
            "Kenya",
            "Morocco",
            "Nigeria",
            "South Africa",
            "Senegal",
            "Togo",
            "Tunisia",
            "Africa (Other)",
            "Australia",
            "Fiji",
            "New Zealand",
            "Oceania (Other)",
            "People's Republic of China",
            "Hong Kong, China",
            "Indonesia",
            "India",
            "Islamic Republic of Iran",
            "Republic of Korea",
            "Saudi Arabia",
            "Malaysia",
            "Pakistan",
            "Philippines",
            "Singapore",
            "Thailand",
            "United Arab Emirates",
            "Uzbekistan",
            "Taiwan, Republic of China",
            "Asia (Other)"
        };

        public static readonly string[] Leagues = new string[15] {
            "Student",
            "Beginner",
            "Rookie",
            "Amateur",
            "Ace",
            "Wizard",
            "Professional",
            "Elite",
            "Virtuoso",
            "Star",
            "Superstar",
            "Legend",
            "Golden",
            "Platinum",
            "Grand Master"
        };

        public static string getRatioType(int ratio) {
            if (ratio < 34) {
                return "T";
            } else if (ratio > 66) {
                return "P";
            }
            return "X";
        }

        public static Color getTetrominoColor(int x) {
            switch (x) {
                case 0:
                    return Color.FromArgb(0, 255, 0);

                case 1:
                    return Color.FromArgb(255, 0, 0);

                case 2:
                    return Color.FromArgb(0, 0, 255);

                case 3:
                    return Color.FromArgb(255, 63, 0);

                case 4:
                    return Color.FromArgb(63, 0, 255);

                case 5:
                    return Color.FromArgb(255, 255, 0);

                case 6:
                    return Color.FromArgb(0, 255, 255);

                case 7:
                    return Color.FromArgb(239, 206, 26);

                case 9:
                    return Color.FromArgb(255, 255, 255);

                case -1:
                case -2:
                    return Color.Transparent;
            }

            return Color.Transparent;
        }

        public static void updateImage(PictureBox canvas, string prefix, int index) {
            if (index == -1) {
                canvas.BackgroundImage = null;
            } else {
                canvas.BackgroundImage = (Image)(PPTMonitor.Properties.Resources.ResourceManager.GetObject($"{prefix}_{index}"));
            }
        }

        public static void updateVoice(PictureBox canvas, int flag) {
            if (flag == 1) {
                canvas.BackgroundImage = PPTMonitor.Properties.Resources.voice;
            } else {
                canvas.BackgroundImage = null;
            }
        }

        public static void drawBoard(PictureBox canvas, int[,] board) {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            using (Graphics gfx = Graphics.FromImage(canvas.Image)) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 40; j++) {
                        gfx.FillRectangle(new SolidBrush(UIHelper.getTetrominoColor(board[i, j])), i * (canvas.Width / 10), (39 - j) * (canvas.Height / 40), canvas.Width / 10, canvas.Height / 40);
                    }
                }

                gfx.DrawLine(new Pen(Color.Red), 0, canvas.Height / 2, canvas.Width, canvas.Height / 2);
                gfx.Flush();
            }
        }
    }
}
