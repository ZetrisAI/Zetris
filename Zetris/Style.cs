using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MisaMinoNET;

namespace Zetris {
    public class Style {
        string _name;
        public string Name {
            get => _name;
            set {
                _name = value;
                Preferences.Save();
            }
        }

        public MisaMinoParameters Parameters { get; private set; }

        public int GetParameter(int index) => Parameters.ToArray()[index];
        public void SetParameter(int index, int value) {
            int[] arr = Parameters.ToArray();
            arr[index] = value;
            Parameters = MisaMinoParameters.FromArray(arr);

            if (Preferences.CurrentStyle == this) Bot.UpdateConfig();

            Preferences.Save();
        }

        public Style Clone() => new Style($"{Name} (Copy)", MisaMinoParameters.FromArray(Parameters.ToArray()));

        public Style(string name, MisaMinoParameters parameters = null) {
            Name = name;
            Parameters = parameters?? new MisaMinoParameters();
        }

        public override string ToString() => Name;
    }
}
