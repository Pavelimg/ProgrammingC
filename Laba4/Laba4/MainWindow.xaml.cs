using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Security.Claims;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace FruitCounter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            name1.Content = "user1";
            name2.Content = "user2";
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            ResetColors();


            if (!ValidateField(apples1, "поле яблок")) isValid = false;
            if (!ValidateField(pears1, "поле груш")) isValid = false;
            if (!ValidateField(apples2, "поле яблок")) isValid = false;
            if (!ValidateField(pears2, "поле груш")) isValid = false;

            if (isValid)
            {
                LockFields(true);
                ShowResult();
            }
        }

        private bool ValidateField(TextBox field, string fieldName)
        {
            if (field.Text.Contains(" "))
            {
                field.Background = Brushes.LightCoral;
                MessageBox.Show($"Пробелы запрещены в поле: {fieldName}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(field.Text))
            {
                field.Background = Brushes.LightCoral;
                MessageBox.Show($"Поле '{fieldName}' не заполнено");
                return false;
            }


            if (!CustomParse(field.Text, out int result))
            {
                field.Background = Brushes.LightCoral;
                MessageBox.Show($"Некорректное значение в поле: {fieldName}\nДопустимы только целые положительные числа без значимых нулей");
                return false;
            }

            field.Background = Brushes.White;
            return true;
        }

        private bool CustomParse(string input, out int result)
        {
            result = 0;

            // Запрет ведущих нулей
            if (input.Length > 1 && input.StartsWith("0"))
                return false;

            // Проверка, что все символы - цифры
            if (!input.All(char.IsDigit))
                return false;

            return int.TryParse(input, out result);
        }

        private void ShowResult()
        {
            int totalApples1 = Parse(apples1);
            int totalPears1 = Parse(pears1);
            int totalApples2 = Parse(apples2);
            int totalPears2 = Parse(pears2);

            int totalFruits1 = totalApples1 + totalPears1;
            int totalFruits2 = totalApples2 + totalPears2;

            tbResult.Text = $"Всего яблок: {totalApples1 + totalApples2}\nГруш: {totalPears1 + totalPears2}";
            tbResult.Visibility = Visibility.Visible;

            if (totalFruits1 > totalFruits2)
            {
                text1.Content = $"Больше всего фруктов у пользователя {name1.Content}: {totalFruits1}";
            }
            else if (totalFruits2 > totalFruits1)
            {
                text1.Content = $"Больше всего фруктов у пользователя {name2.Content}: {totalFruits2}";
            }
            else
            {
                text1.Content = $"Количество фруктов одинаково: {totalFruits1}";
            }

        }

        private int Parse(TextBox field) =>
            int.TryParse(field.Text, out int val) ? val : 0;

        private void LockFields(bool isLocked)
        {
            apples1.IsEnabled = !isLocked;
            pears1.IsEnabled = !isLocked;
            apples2.IsEnabled = !isLocked;
            pears2.IsEnabled = !isLocked;
        }

        private void ResetColors()
        {
            apples1.Background = Brushes.White;
            pears1.Background = Brushes.White;
            apples2.Background = Brushes.White;
            pears2.Background = Brushes.White;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            apples1.Text = "";
            pears1.Text = "";
            apples2.Text = "";
            pears2.Text = "";

            LockFields(false);
            tbResult.Visibility = Visibility.Collapsed;
            text1.Visibility = Visibility.Collapsed;
            ResetColors();
        }

        private void ResetFieldColor(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
                textBox.Background = Brushes.White;
        }
    }
}

