using System.Globalization;

namespace Zetris {
    public partial class Error {
        public Error(string exceptionText) {
            InitializeComponent();

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    General.Text = "";
                    break;
                    
                case "ja":
                    General.Text = "";
                    break;
                    
                default:
                    General.Text = "Zetris has encountered an error. Please report the information below to the developers.";
                    break;
            }

            Exception.Text = $"Version: {App.Version}\n\n{exceptionText}";
        }
    }
}
