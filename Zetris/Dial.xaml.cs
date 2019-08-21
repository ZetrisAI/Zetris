using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zetris {
    public partial class Dial: UserControl {
        public delegate void DialChangedEventHandler(double NewValue);
        public event DialChangedEventHandler Changed;

        const double width = 43, height = 39;
        const double radius = 18, stroke = 7;
        const double strokeHalf = stroke / 2;

        const double angle_start = 4 * Math.PI / 3;
        const double angle_end = -1 * Math.PI / 3;
        const double angle_center = Math.PI / 2;

        double ToValue(double rawValue) => (rawValue - _min) / (_max - _min);
        double ToRawValue(double value) => _min + (_max - _min) * value;

        double _min = 0;
        public double Minimum {
            get => _min;
            set {
                if (_min != value) {
                    _min = value;
                    Value = ToValue(_raw);
                }
            }
        }

        double _max = 100;
        public virtual double Maximum {
            get => _max;
            set {
                if (_max != value) {
                    _max = value;
                    Value = ToValue(_raw);
                }
            }
        }

        bool _valuechanging = false;
        double _value = 0.5;
        public double Value {
            get => _value;
            set {
                value = Math.Max(0, Math.Min(1, value));
                if (!_valuechanging && value != _value) {
                    _valuechanging = true;

                    _value = value;
                    RawValue = ToRawValue(_value);
                    DrawArcMain();

                    _valuechanging = false;
                }
            }
        }

        bool _rawchanging = false;
        double _raw = 50;
        public double RawValue {
            get => _raw;
            set {
                value = Math.Round(Math.Max(_min, Math.Min(_max, value)));
                if (!_rawchanging && _raw != value) {
                    _rawchanging = true;

                    _raw = value;
                    Value = ToValue(_raw);
                    Display.Text = ValueString;

                    Changed?.Invoke(_raw);

                    _rawchanging = false;
                }
            }
        }

        double _default = 50;
        public double Default {
            get => _default;
            set {
                Value = ToValue(_raw = _default = Math.Max(_min, Math.Min(_max, value)));
            }
        }

        string _title = "Dial";
        public string Title {
            get => _title;
            set => TitleText.Text = _title = value;
        }

        string _unit = "";
        public string Unit {
            get => _unit;
            set {
                _unit = value;
                DrawArcMain();
            }
        }

        bool _enabled = true;
        public bool Enabled {
            get => _enabled;
            set {
                _enabled = value;

                this.Focus();
                DrawArcMain();
            }
        }

        double _scale = 1;
        public double Scale {
            get => _scale;
            set {
                value = Math.Max(0, Math.Min(1, value));
                if (value != _scale) {
                    _scale = value;

                    ArcCanvas.Width = width * _scale;
                    ArcCanvas.Height = height * _scale;

                    DrawArcBase();
                    DrawArcMain();
                }
            }
        }

        bool _fillstart = true;
        public bool FillStart {
            get => _fillstart;
            set {
                if (value != _fillstart) {
                    _fillstart = value;

                    DrawArcMain();
                }
            }
        }

        string ValueString => $"{RawValue}{Unit}";

        void DrawArc(Path Arc, double value, bool overrideBase) {
            double angle_starting = FillStart? angle_start : angle_start - Math.Abs(angle_end - angle_start) * value * 0.9;

            double x_start = (radius * (Math.Cos(angle_starting) + 1) + strokeHalf) * _scale;
            double y_start = (radius * (-Math.Sin(angle_starting) + 1) + strokeHalf) * _scale;

            double angle_point = angle_start - Math.Abs(angle_end - angle_start) * (1 - (1 - value) * 0.9);

            double x_end = (radius * (Math.Cos(angle_point) + 1) + strokeHalf) * _scale;
            double y_end = (radius * (-Math.Sin(angle_point) + 1) + strokeHalf) * _scale;

            double angle = (angle_starting - angle_point) / Math.PI * 180;

            int large = Convert.ToInt32(angle > 180);
            int direction = Convert.ToInt32(angle > 0);

            Arc.StrokeThickness = stroke * _scale;
            if (!overrideBase) {
                Arc.Stroke = new SolidColorBrush(Enabled? Color.FromArgb(0xFF, 0xBE, 0x17, 0x07) : Color.FromArgb(0xFF, 0x30, 0x30, 0x30));
                Display.Text = ValueString;
            }

            Arc.Data = Geometry.Parse(String.Format("M {0},{1} A {2},{2} {3} {4} {5} {6},{7}",
                x_start.ToString(),
                y_start.ToString(),
                (radius * _scale).ToString(),
                angle.ToString(),
                large,
                direction,
                x_end.ToString(),
                y_end.ToString()
            ));
        }

        void DrawArcBase() => DrawArc(ArcBase, 1, true);

        void DrawArcMain() => DrawArc(ArcMain, _value, false);

        public Dial() {
            InitializeComponent();
            DrawArcBase();
        }

        bool mouseHeld = false;
        double oldValue;
        double lastY;

        void Down(object sender, MouseButtonEventArgs e) {
            if (!Enabled || e.ChangedButton != MouseButton.Left) return;

            mouseHeld = true;
            e.MouseDevice.Capture(ArcCanvas);

            lastY = e.GetPosition(ArcCanvas).Y;
            oldValue = RawValue;

            ArcCanvas.Cursor = Cursors.SizeNS;
        }

        void Up(object sender, MouseButtonEventArgs e) {
            if (!Enabled || e.ChangedButton != MouseButton.Left) return;
            
            mouseHeld = false;
            e.MouseDevice.Capture(null);

            Changed?.Invoke(RawValue);

            ArcCanvas.Cursor = Cursors.Hand;
        }

        void Move(object sender, MouseEventArgs e) {
            if (!Enabled || !mouseHeld) return;
            
            double Y = e.GetPosition(ArcCanvas).Y;
            
            Value += (lastY - Y) / 200;
            lastY = Y;
        }
    }
}
