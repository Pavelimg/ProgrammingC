using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private int counter = 0; // Переменная для хранения значения счётчика
        private const int VariantNumber = 5; // Номер вашего варианта

        public MainWindow()
        {
            InitializeComponent();

            // Настройка свойств Label программно
            ConfigureLabels();
        }

        // Метод для настройки свойств Label
        private void ConfigureLabels()
        {
            // Настройка lbl1
            lbl1.Content = "ХЗ";
            lbl1.FontSize = 16;
            lbl1.Foreground = Brushes.Blue;
            lbl1.FontFamily = new FontFamily("Arial");

            // Настройка lbl2
            lbl2.Content = "ХЗ 2";
            lbl2.FontSize = 18;
            lbl2.Foreground = Brushes.Green;
            lbl2.FontWeight = FontWeights.Bold;

            // Настройка lbl3
            lbl3.Content = "что-то";
            lbl3.FontSize = 14;
            lbl3.Foreground = Brushes.Red;
            lbl3.FontStyle = FontStyles.Italic;
        }

        // Обработчик события нажатия кнопки мыши для всего окна
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int previousCounter = counter; // Сохраняем предыдущее значение счётчика

            if (e.LeftButton == MouseButtonState.Pressed) // ЛКМ
            {
                counter++; // Увеличиваем счётчик на 1
            }
            else if (e.RightButton == MouseButtonState.Pressed) // ПКМ
            {
                counter -= 2; // Уменьшаем счётчик на 2
            }

            // Обновляем значение счётчика с надписью "Счетчик:"
            lblCounter.Content = $"Счетчик: {counter}";

            // Проверка на изменение знака
            if ((previousCounter < 0 && counter >= 0) || (previousCounter >= 0 && counter < 0))
            {
                MessageBox.Show("Счётчик изменил знак!");
            }

            // Проверка на совпадение с номером варианта
            if (counter == VariantNumber)
            {
                MessageBox.Show($"Моя остановачка ({VariantNumber})!");
            }

            // Изменение цвета текста счётчика
            //lblCounter.Foreground = counter < 0 ? Brushes.Red : Brushes.Green;
        }
    }
}
