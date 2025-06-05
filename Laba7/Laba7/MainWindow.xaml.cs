using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace лаба7
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private byte _redValue = 255;
        private byte _greenValue = 255;
        private byte _blueValue = 255;

        private double _opacityValue = 100; 
        private int _randomClickCount;
        private const int MaxRandomClicks = 10;

        public byte RedValue
        {
            get => _redValue;
            set
            {
                byte validatedValue = ValidateColorValue(value);
                if (_redValue != validatedValue)
                {
                    _redValue = validatedValue;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RectangleColor));
                    OnPropertyChanged(nameof(LabelForegroundColor));
                }
            }
        }

        public byte GreenValue
        {
            get => _greenValue;
            set
            {
                byte validatedValue = ValidateColorValue(value);
                if (_greenValue != validatedValue)
                {
                    _greenValue = validatedValue;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RectangleColor));
                    OnPropertyChanged(nameof(LabelForegroundColor));
                }
            }
        }

        public byte BlueValue
        {
            get => _blueValue;
            set
            {
                byte validatedValue = ValidateColorValue(value);
                if (_blueValue != validatedValue)
                {
                    _blueValue = validatedValue;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RectangleColor));
                    OnPropertyChanged(nameof(LabelForegroundColor));
                }
            }
        }

        private byte ValidateColorValue(byte value)
        {
            if (value >= 250)
            {
                // Для значений от 250 до 255 округляем до ближайшего кратного 5
                return (byte)(Math.Round(value / 5.0) * 5);
            }
            else
            {
                // Для остальных значений округляем до ближайшего кратного 25
                return (byte)(Math.Round(value / 25.0) * 25);
            }
        }

        public double OpacityValue
        {
            get => _opacityValue;
            set
            {
                if (_opacityValue != value)
                {
                    _opacityValue = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RectangleOpacity));
                    OnPropertyChanged(nameof(ScrollBarValue)); // Обновляем значение для ползунка
                    CheckRandomState();
                }
            }
        }

        public double RectangleOpacity => _opacityValue / 100.0;

        public SolidColorBrush RectangleColor =>
            new SolidColorBrush(System.Windows.Media.Color.FromRgb(RedValue, GreenValue, BlueValue));

        public SolidColorBrush LabelForegroundColor =>
            new SolidColorBrush(System.Windows.Media.Color.FromRgb(
                (byte)(255 - RedValue),
                (byte)(255 - GreenValue),
                (byte)(255 - BlueValue)));

        public bool IsRandomEnabled => _randomClickCount < MaxRandomClicks && OpacityValue >= 25;

        public double ScrollBarValue 
        {
            get => 100 - _opacityValue;
            set => OpacityValue = 100 - value; // Обратное преобразование
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CheckRandomState()
        {
            OnPropertyChanged(nameof(IsRandomEnabled));
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            _randomClickCount++;
            var rnd = new Random();

            RedValue = GenerateRandomColorValue(rnd);
            GreenValue = GenerateRandomColorValue(rnd);
            BlueValue = GenerateRandomColorValue(rnd);

            OpacityValue = rnd.Next(0, 5) * 25;

            CheckRandomState();
        }

        private byte GenerateRandomColorValue(Random rnd)
        {
            int randomStep = rnd.Next(0, 11); 
            if (randomStep == 10)
            {
                return (byte)(250 + rnd.Next(0, 6)); // 250, 251, 252, 253, 254, 255
            }
            else
            {
                return (byte)(randomStep * 25);
            }
        }

        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta > 0 ? -25 : 25; // Движение колесика мыши
            OpacityValue = Math.Max(0, Math.Min(OpacityValue + delta, 100));

            e.Handled = true;
        }

        private void ScrollBar_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                var grid = (Grid)((FrameworkElement)sender).Parent;
                grid.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.PreviewMouseWheelEvent,
                    Source = sender
                });
                e.Handled = true;
            }
        }

        private void Label_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Определяем направление прокрутки
            var delta = e.Delta > 0 ? -25 : 25; // Движение колесика мыши

            // Изменяем значение прозрачности
            OpacityValue = Math.Max(0, Math.Min(OpacityValue + delta, 100));

            // Помечаем событие как обработанное
            e.Handled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}