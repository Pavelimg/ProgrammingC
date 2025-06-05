using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private List<Brush> colors; // Список цветов
        private int currentColorIndex = 0; // Текущий индекс цвета
        private Random random; // Генератор случайных чисел

        public MainWindow()
        {
            InitializeComponent();
            // Инициализация окна
            main_win.Width = 850;
            main_win.Height = 540;
            main_win.MaxHeight = 800;
            main_win.MaxWidth = 1000;
            main_win.MinHeight = 400;
            main_win.MinWidth = 600;
            main_win.Background = Brushes.Aquamarine;

            // Инициализация списка цветов
            colors = new List<Brush>
            {
                Brushes.Aquamarine,
                Brushes.Peru,
                Brushes.LightPink,
                Brushes.LightBlue,
                Brushes.LightGreen
            };

            currentColorIndex = 0; // Начинаем с первого цвета
            random = new Random(); // Инициализация генератора случайных чисел
        }

        private void main_win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Смена цвета: последовательная (А) или случайная (Б)
            bool isRandom = true; // Переключатель: true - случайно, false - по порядку

            if (isRandom)
            {
                // Случайный выбор цвета
                int randomIndex = random.Next(colors.Count);
                main_win.Background = colors[randomIndex];
            }
            else
            {
                // Последовательное переключение цветов
                currentColorIndex = (currentColorIndex + 1) % colors.Count;
                main_win.Background = colors[currentColorIndex];
            }
        }

        private void main_win_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Разворачиваем окно при правом клике мыши
            main_win.WindowState = WindowState.Maximized;
        }

        private void main_win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {

                // Проверяем ширину и изменяем её на 750, если она находится в диапазоне
                if (main_win.Width >= 600 && main_win.Width <= 1000)
                {  //  минималка + 200 = максималка - 200
                    // текщуее - 800
                    double deltaW = main_win.Width - ((main_win.MaxWidth + main_win.MinWidth) / 2);

                    if (main_win.Width > 800)
                    {
                        main_win.Width = main_win.Width - 2 * deltaW;

                    }
                    else { main_win.Width = main_win.Width - 2 * deltaW; }
                }

                // Проверяем высоту и изменяем её на 750, если она находится в диапазоне
                if (main_win.Height >= 400 && main_win.Height <= 800)
                {
                    double deltaH = main_win.Height - ((main_win.MaxHeight + main_win.MinHeight) / 2);
                    if (main_win.Height > 600)
                    {
                        main_win.Height = main_win.Height - 2 * deltaH;
                    }
                    else { main_win.Height = main_win.Height - 2 * deltaH; }
                }

                if (e.Key == Key.Escape)
                {
                    // Закрываем приложение при нажатии клавиши Escape
                    main_win.Close();
                }
            }
        }
    }
}
