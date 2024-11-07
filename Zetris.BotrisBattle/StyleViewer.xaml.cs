using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Win32;

namespace Zetris.BotrisBattle {
    partial class StyleViewer: ListBoxItem {
        public Style CustomStyle { get; private set; }
        Editor _editor;

        public StyleViewer(Style style, Editor editor) {
            Style = new System.Windows.Style(GetType(), (System.Windows.Style)FindResource(typeof(ListBoxItem)));

            InitializeComponent();

            Text.Text = (CustomStyle = style).ToString();
            _editor = editor;

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    NewItem.Header = "신규 생성";
                    DuplicateItem.Header = "복제하기";
                    RenameItem.Header = "이름 바꾸기";
                    DeleteItem.Header = "삭제";
                    ImportItem.Header = "가져오기";
                    ExportItem.Header = "내보내기";
                    sfdFilter = "Zetris 스타일 파일";
                    sfdTitle = "스타일 내보내기";
                    break;

                case "ja":
                    NewItem.Header = "新規作成";
                    DuplicateItem.Header = "複製";
                    RenameItem.Header = "名前の変更";
                    DeleteItem.Header = "削除";
                    ImportItem.Header = "保存";
                    ExportItem.Header = "読み込み";
                    sfdFilter = "Zetrisのスタイルのファイル";
                    sfdTitle = "エクスポート形式";
                    break;

                default:
                    NewItem.Header = "New";
                    DuplicateItem.Header = "Duplicate";
                    RenameItem.Header = "Rename";
                    DeleteItem.Header = "Delete";
                    ImportItem.Header = "Import";
                    ExportItem.Header = "Export";
                    sfdFilter = "Zetris Style File";
                    sfdTitle = "Export Style";
                    break;
            }
        }

        void StyleDrag(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Released || Input.IsEnabled) return;

            DragDrop.DoDragDrop(this, this, DragDropEffects.Move | DragDropEffects.Copy);
        }

        void StyleDragOver(object sender, DragEventArgs e) {
            if (!e.Data.GetFormats().SequenceEqual(new string[] { "Zetris.StyleViewer" }) || (StyleViewer)e.Data.GetData(typeof(StyleViewer)) == this)
                e.Effects = DragDropEffects.None;
            else if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.Move;

            e.Handled = true;
        }

        void StyleDrop(object sender, DragEventArgs e) =>
            _editor.StyleListDrop(sender, e);

        void StyleDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2) {
                Rename(null, null);
                e.Handled = true;
            }
        }

        void New(object sender, RoutedEventArgs e) => _editor.New(this);

        void Duplicate(object sender, RoutedEventArgs e) => _editor.Duplicate(this);

        public void Rename(object sender, RoutedEventArgs e) {
            Input.Text = CustomStyle.ToString();

            Input.CaretIndex = Input.Text.Length;
            Input.SelectionStart = 0;
            Input.SelectionLength = Input.Text.Length;

            Input.Opacity = 1;
            Input.IsEnabled = Input.IsHitTestVisible = true;
            Input.Focus();
        }

        void Delete(object sender, RoutedEventArgs e) => _editor.Delete(this);

        void Import(object sender, RoutedEventArgs e) => _editor.Import(this);

        string sfdFilter, sfdTitle;

        public void Export(object sender, RoutedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog() {
                Filter = $"{sfdFilter}|*.zst",
                Title = sfdTitle
            };

            if (sfd.ShowDialog(Window.GetWindow(this)) == true)
                File.WriteAllBytes(sfd.FileName, Binary.EncodeStyle(CustomStyle).ToArray());
        }

        void InputLostFocus(object sender, RoutedEventArgs e) {
            Text.Text = CustomStyle.Name = Input.Text;
            Input.Opacity = 0;
            Input.IsEnabled = Input.IsHitTestVisible = false;
        }

        void InputKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter)
                InputLostFocus(null, null);

            e.Handled = true;
        }
    }
}
