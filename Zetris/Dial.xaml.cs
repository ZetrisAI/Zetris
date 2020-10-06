using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zetris {
    partial class Dial: UserControl {
        public delegate void DialChangedEventHandler(Dial sender, double NewValue);
        public event DialChangedEventHandler Changed;

        const double width = 43, height = 39;
        const double radius = 18, stroke = 7;
        const double strokeHalf = stroke / 2;

        const double angle_start = 4 * Math.PI / 3;
        const double angle_end = -1 * Math.PI / 3;

        double ToValue(double rawValue) => Math.Pow((rawValue - _min) / (_max - _min), 1 / _exp);
        double ToRawValue(double value) => _min + (_max - _min) * Math.Pow(value, _exp);

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

        double _exp = 1;
        public double Exponent {
            get => _exp;
            set {
                if (_exp != value) {
                    _exp = value;
                    Value = ToValue(_raw);
                }
            }
        }

        string _maxoverride = "";
        public string MaximumOverride {
            get => _maxoverride;
            set {
                if (_maxoverride != value) {
                    _maxoverride = value;
                    DrawArcMain();
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

                    Changed?.Invoke(this, _raw);

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

        bool _precise = true;
        public bool AllowPrecise {
            get => _precise;
            set {
                if (!(_precise = value) && Input.IsEnabled)
                    InputLostFocus(null, null);
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

        string ValueString => (MaximumOverride != "" && RawValue == Maximum)? MaximumOverride : $"{RawValue}{Unit}";

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
                x_start.ToString(CultureInfo.InvariantCulture),
                y_start.ToString(CultureInfo.InvariantCulture),
                (radius * _scale).ToString(CultureInfo.InvariantCulture),
                angle.ToString(CultureInfo.InvariantCulture),
                large,
                direction,
                x_end.ToString(CultureInfo.InvariantCulture),
                y_end.ToString(CultureInfo.InvariantCulture)
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

            if (e.ClickCount == 2 && AllowPrecise) {
                DisplayPressed(sender, e);
                return;
            }

            // Kill Rename TextBox
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(this), null);
            Keyboard.ClearFocus();

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

            Changed?.Invoke(this, RawValue);

            ArcCanvas.Cursor = Cursors.Hand;
        }

        void Move(object sender, MouseEventArgs e) {
            if (!Enabled || !mouseHeld) return;
            
            double Y = e.GetPosition(ArcCanvas).Y;
            
            Value += (lastY - Y) / 300;
            lastY = Y;
        }

        Action InputUpdate;

        void InputChanged(object sender, TextChangedEventArgs e) {
            string text = Input.Text;

            if (text == null) return;
            if (text == "") return;

            InputUpdate = () => Input.Text = RawValue.ToString();

            if (int.TryParse(text, out int value)) {
                if (Minimum <= value && value <= Maximum) {
                    RawValue = value;
                    InputUpdate = () => Input.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xDC, 0xDC, 0xDC));
                } else {
                    InputUpdate = () => Input.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xBE, 0x17, 0x07));
                }

                InputUpdate += () => {
                    if (value < 0) text = $"-{text.Substring(1).TrimStart('0')}";
                    else if (value > 0) text = text.TrimStart('0');
                    else text = "0";

                    if (Minimum >= 0) {
                        if (value < 0) text = "0";

                    } else {
                        int lower = -(int)Math.Pow(10, ((int)Minimum).ToString().Length - 1) + 1;
                        if (value < lower) text = lower.ToString();
                    }

                    int upper = (int)Math.Pow(10, ((int)Maximum).ToString().Length) - 1;
                    if (value > upper) text = upper.ToString();

                    Input.Text = text;
                };
            }

            if (Minimum < 0 && text == "-") InputUpdate = null;

            Dispatcher.InvokeAsync(() => {
                InputUpdate?.Invoke();
                InputUpdate = null;
            });
        }

        void DisplayPressed(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2 && Enabled && AllowPrecise) {
                Input.Text = RawValue.ToString();

                Input.CaretIndex = Input.Text.Length;
                Input.SelectionStart = 0;
                Input.SelectionLength = Input.Text.Length;

                Input.Opacity = 1;
                Input.IsEnabled = Input.IsHitTestVisible = true;
                Input.Focus();

                e.Handled = true;
            }
        }
        
        void InputLostFocus(object sender, RoutedEventArgs e) {
            Input.Opacity = 0;
            Input.IsEnabled = Input.IsHitTestVisible = false;

            Changed?.Invoke(this, _raw);
        }

        void InputKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter)
                InputLostFocus(null, null);

            e.Handled = true;
        }
    }
}
