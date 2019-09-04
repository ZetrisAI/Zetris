using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MisaMinoNET;

namespace Zetris {
    public class Style {
        public static Style Current;

        string _name;
        public string Name {
            get => _name;
            set {
                _name = value;

                if (Preferences.Freeze) return;

                Preferences.Save();
            }
        }

        public MisaMinoParameters Parameters { get; private set; }

        public int GetParameter(int index) => Parameters.ToArray()[index];
        public void SetParameter(int index, int value) {
            if (Preferences.Freeze) return;

            int[] arr = Parameters.ToArray();
            arr[index] = value;
            Parameters = MisaMinoParameters.FromArray(arr);

            Preferences.Save();
        }

        int _speed;
        public int Speed {
            get => _speed;
            set {
                _speed = Math.Max(10, Math.Min(100, value));

                if (Preferences.Freeze) return;

                Preferences.Save();
            }
        }

        int _previews;
        public int Previews {
            get => _previews;
            set {
                _previews = Math.Max(1, Math.Min(18, value));

                if (Preferences.Freeze) return;

                Preferences.Save();
            }
        }

        bool _hold;
        public bool HoldAllowed {
            get => _hold;
            set {
                _hold = value;

                if (Preferences.Freeze) return;

                Preferences.Save();
            }
        }

        bool _pc;
        public bool PerfectClear {
            get => _pc;
            set {
                _pc = value;

                if (Preferences.Freeze) return;

                Preferences.Save();
            }
        }

        public Style(string name, MisaMinoParameters parameters = null, int speed = 100, int previews = 18, bool hold = true, bool pc = false) {
            Name = name;
            Parameters = parameters?? new MisaMinoParameters();
            Speed = speed;
            Previews = previews;
            HoldAllowed = hold;
            PerfectClear = pc;
        }
    }
}
