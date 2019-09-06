using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using MahApps.Metro.Controls;

namespace Zetris {
    public partial class Editor {
        public static bool FreezeEvents = true;

        public Editor(Window owner) {
            Owner = owner;

            InitializeComponent();

            FreezeEvents = false;

            foreach (Style style in Preferences.Styles)
                StyleList.Items.Add(new StyleViewer(style, this));

            StyleList.SelectedIndex = Preferences.StyleIndex;

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    Title = "";
                    break;
                    
                case "ja":
                    Title = "";
                    break;
                    
                default:
                    Title = "Style Editor";
                    break;
            }
        }

        void StyleListChanged(object sender, SelectionChangedEventArgs e) {
            if (StyleList.SelectedIndex == -1) return;

            for (int i = 0; i < 17; i++)
                ((Dial)Layout.Children[i]).RawValue = Preferences.Styles[StyleList.SelectedIndex].GetParameter(i);
        }

        void Insert(int index, Style style) {
            Preferences.Styles.Insert(index, style);
            Preferences.Save();

            StyleList.Items.Insert(index, new StyleViewer(style, this));
            StyleList.SelectedIndex = index;
        }

        void New(int index) =>
            Insert(index, new Style("New Style"));

        public void New(StyleViewer item) =>
            New(StyleList.Items.IndexOf(item));

        public void Duplicate(StyleViewer item) {
            int index = StyleList.Items.IndexOf(item);
            Insert(index, Preferences.Styles[index].Clone());
        }

        public void Delete(StyleViewer item) {
            if (Preferences.Styles.Count <= 1) return;

            int index = StyleList.Items.IndexOf(item);

            Preferences.Styles.RemoveAt(index);
            if (Preferences.StyleIndex >= index) Preferences.StyleIndex--;
            Preferences.Save();
            
            StyleList.Items.RemoveAt(index);
            StyleList.SelectedIndex = Math.Min(index, StyleList.Items.Count - 1);
        }

        void Import(int index) {
            OpenFileDialog ofd = new OpenFileDialog() {
                Filter = "Zetris Style Files|*.zst",
                Title = "Import Zetris Style"
            };

            if (ofd.ShowDialog(Window.GetWindow(this)) == true)
                using (FileStream file = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                    Insert(index, Binary.DecodeStyle(file));
        }

        public void Import(StyleViewer item) =>
            Import(StyleList.Items.IndexOf(item));

        void NewEmpty(object sender, RoutedEventArgs e) =>
            New(Preferences.Styles.Count);

        void ImportEmpty(object sender, RoutedEventArgs e) =>
            Import(Preferences.Styles.Count);

        void ParameterChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Styles[StyleList.SelectedIndex].SetParameter(Layout.Children.IndexOf(sender), (int)NewValue);
        }
    }
}
