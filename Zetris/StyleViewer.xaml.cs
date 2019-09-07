using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Microsoft.Win32;

namespace Zetris {
    public partial class StyleViewer: ListBoxItem {
        public Style CustomStyle { get; private set; }
        Editor _editor;

        public StyleViewer(Style style, Editor editor) {
            Style = new System.Windows.Style(GetType(), (System.Windows.Style)FindResource(typeof(ListBoxItem)));

            InitializeComponent();

            Text.Text = (CustomStyle = style).ToString();
            _editor = editor;
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

        void New(object sender, RoutedEventArgs e) => _editor.New(this);

        void Duplicate(object sender, RoutedEventArgs e) => _editor.Duplicate(this);

        public void Rename(object sender, RoutedEventArgs e) {
            Input.Text = CustomStyle.ToString();

            Input.SelectionStart = 0;
            Input.SelectionLength = Input.Text.Length;
            Input.CaretIndex = Input.Text.Length;

            Input.Opacity = 1;
            Input.IsEnabled = Input.IsHitTestVisible = true;
            Input.Focus();
        }

        void Delete(object sender, RoutedEventArgs e) => _editor.Delete(this);

        void Import(object sender, RoutedEventArgs e) => _editor.Import(this);

        public void Export(object sender, RoutedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog() {
                Filter = "Zetris Style Files|*.zst",
                Title = "Export Zetris Style"
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
            if (e.Key == Key.Enter) {
                InputLostFocus(null, null);
                e.Handled = true;
            }
        }
    }
}
