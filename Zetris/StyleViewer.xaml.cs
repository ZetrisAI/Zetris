using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zetris {
    public partial class StyleViewer: ListBoxItem {
        public Style CustomStyle { get; private set; }
        Editor _editor;

        public StyleViewer(Style style, Editor editor) {
            Style = new System.Windows.Style(GetType(), (System.Windows.Style)FindResource(typeof(ListBoxItem)));

            InitializeComponent();

            Content = (CustomStyle = style).ToString();
            _editor = editor;
        }

        void New(object sender, RoutedEventArgs e) => _editor.New(this);

        void Duplicate(object sender, RoutedEventArgs e) => _editor.Duplicate(this);

        void Rename(object sender, RoutedEventArgs e) {

        }

        void Delete(object sender, RoutedEventArgs e) => _editor.Delete(this);

        void Import(object sender, RoutedEventArgs e) => _editor.Import(this);

        void Export(object sender, RoutedEventArgs e) => _editor.Export(this);
    }
}
