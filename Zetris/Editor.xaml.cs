using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

using MahApps.Metro.Controls;

namespace Zetris {
    public partial class Editor {
        public static bool FreezeEvents = true;

        public Editor(Window owner) {
            Owner = owner;

            InitializeComponent();

            FreezeEvents = false;

            foreach (Style style in Preferences.Styles)
                StyleList.Items.Add(style);

            StyleList.SelectedIndex = Preferences.StyleIndex;

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":

                    break;
                    
                case "ja":

                    break;
                    
                default:

                    break;
            }
        }

        void StyleListChanged(object sender, SelectionChangedEventArgs e) {
            for (int i = 0; i < 17; i++)
                ((Dial)Layout.Children[i]).RawValue = Preferences.Styles[StyleList.SelectedIndex].GetParameter(i);
        }

        void ParameterChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Styles[StyleList.SelectedIndex].SetParameter(Layout.Children.IndexOf(sender), (int)NewValue);
        }
    }
}
