using System.Globalization;

namespace Zetris {
    public partial class Error {
        public Error(string exceptionText) {
            InitializeComponent();

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    General.Text = "제트리스에 에러가 발생했습니다. 아래의 정보를 개발자에게 신고해 주세요.";
                    break;
                    
                case "ja":
                    General.Text = "エラーが発生しました。以下の情報を開発者に報告してください。";
                    break;
                    
                default:
                    General.Text = "Zetris has encountered an error. Please report the information below to the developers.";
                    break;
            }

            Exception.Text = $"Version: {App.Version}\n\n{exceptionText}";
        }
    }
}
