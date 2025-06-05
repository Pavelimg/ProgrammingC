using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> countryCodes = new Dictionary<string, string>
        {
            { "Россия", "+7" },
            { "США", "+1" },
            { "Германия", "+49" }
        };

        private string currentCountryCode = "+7";
        private string currentPhoneType = "Мобильный";
        private const string MobileMask = "+7(000)-000-00-00";
        private const string HomeMask = "+7(0000)-00-00-00";
        private const string MobileMaskBelgium = "+32 (000)-000-000";
        private const string HomeMaskBelgium = "+32(0000)-000-00";
        private bool isPhoneTextBoxFocused = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBox();

            PhoneTextBox.Text = currentCountryCode;
            MonthComboBox.SelectedIndex = 0; // Устанавливаем январь по умолчанию
        }


        private void NameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Формируем текст, который получится после ввода
            string newText = textBox.Text.Remove(textBox.CaretIndex, textBox.SelectionLength)
                                      .Insert(textBox.CaretIndex, e.Text);

            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && c != '-' && c != ' ')
                {
                    e.Handled = true;
                    return;
                }
            }

            if (newText.StartsWith(" "))
            {
                e.Handled = true;
                return;
            }

            if (newText.Contains("  "))
            {
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        // Добавляем обработчик LostFocus для удаления пробелов в конце
        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Text.Trim();
            }
        }


        private void InitializeComboBox()//Заполняет названиями стран из словаря countryCodes
        {
            foreach (var country in countryCodes.Keys)
            {
                CountryComboBox.Items.Add(country);
            }
            CountryComboBox.SelectedIndex = 0;
        }


        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem != null)
            {
                string selectedCountry = CountryComboBox.SelectedItem.ToString();
                currentCountryCode = countryCodes[selectedCountry];//при изменении выбора страны обновляется currentCountryCode
                UpdatePhoneTextBox();//обновл маски телефона
            }
        }

        private void PhoneTypeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhoneTypeListBox.SelectedItem != null)
            {
                ListBoxItem selectedItem = (ListBoxItem)PhoneTypeListBox.SelectedItem;
                currentPhoneType = selectedItem.Content.ToString();
                UpdatePhoneTextBox();
                MessageBox.Show("Выбран тип телефона: " + currentPhoneType);
            }
        }

        private void UpdatePhoneTextBox()
        {
            string phoneText = PhoneTextBox.Text;

            if (!phoneText.StartsWith(currentCountryCode))
            {
                PhoneTextBox.Text = currentCountryCode;
                PhoneTextBox.CaretIndex = PhoneTextBox.Text.Length;
                return;
            }
            string digitsOnly = new string(phoneText.Substring(currentCountryCode.Length).Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(digitsOnly))
            {
                PhoneTextBox.Text = currentCountryCode;
            }
            else
            {
                string mask;
                if (currentCountryCode == "+7")
                {
                    mask = currentPhoneType == "Мобильный" ? MobileMask : HomeMask;
                }
                else if (currentCountryCode == "+49")
                {
                    mask = currentPhoneType == "Мобильный" ? MobileMaskBelgium : HomeMaskBelgium;
                }
                else
                {
                    mask = currentPhoneType == "Мобильный" ? MobileMask : HomeMask; // По умолчанию для других стран
                }
                string maskedText = ApplyMask(digitsOnly, mask);
                PhoneTextBox.Text = maskedText;
            }
            // Устанавливаем курсор в конец текста
            PhoneTextBox.CaretIndex = PhoneTextBox.Text.Length;
        }

        private void PhoneTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isPhoneTextBoxFocused = true;
            UpdatePhoneTextBox();
        }

        private void PhoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            isPhoneTextBoxFocused = false;
            UpdatePhoneTextBox();
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isPhoneTextBoxFocused)
                return;

            string phoneText = PhoneTextBox.Text;

            // Защита кода страны
            if (!phoneText.StartsWith(currentCountryCode))
            {
                PhoneTextBox.Text = currentCountryCode;
                PhoneTextBox.CaretIndex = PhoneTextBox.Text.Length;
                return;
            }

            // Удаляю все символы, кроме цифр, после кода страны
            string digitsOnly = new string(phoneText.Substring(currentCountryCode.Length).Where(char.IsDigit).ToArray());

            // Определяю максимальное количество цифр в зависимости от типа номера и страны
            int maxDigits;
            if (currentCountryCode == "+7")
            {
                maxDigits = currentPhoneType == "Мобильный" ? 10 : 11;
            }
            else if (currentCountryCode == "+49")
            {
                maxDigits = 9;
            }
            else
            {
                maxDigits = 10; // По умолчанию для других стран
            }

            if (digitsOnly.Length > maxDigits)
            {
                digitsOnly = digitsOnly.Substring(0, maxDigits);
            }

            string mask;
            if (currentCountryCode == "+7")
            {
                mask = currentPhoneType == "Мобильный" ? MobileMask : HomeMask;
            }
            else if (currentCountryCode == "+49")
            {
                mask = currentPhoneType == "Мобильный" ? MobileMaskBelgium : HomeMaskBelgium;
            }
            else
            {
                mask = currentPhoneType == "Мобильный" ? MobileMask : HomeMask;
            }

            string maskedText = ApplyMask(digitsOnly, mask);

            PhoneTextBox.Text = maskedText;

            PhoneTextBox.CaretIndex = PhoneTextBox.Text.Length;
        }

        private string ApplyMask(string digits, string mask)
        {
            string result = currentCountryCode;
            int digitIndex = 0;
            for (int i = currentCountryCode.Length; i < mask.Length; i++)
            {
                if (digitIndex >= digits.Length)
                    break;

                if (mask[i] == '0')
                {
                    result += digits[digitIndex];
                    digitIndex++;
                }
                else
                {
                    result += mask[i];
                }
            }

            return result;
        }

        private void PhoneTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Запрет удаление кода страны
            if (e.Key == Key.Back && PhoneTextBox.SelectionStart <= currentCountryCode.Length)
            {
                e.Handled = true;
            }
        }

        private void DateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DayTextBox.Text))
            {
                DayTextBox.BorderBrush = Brushes.Black;
                ErrorTextBlock.Text = string.Empty;
                return;
            }

            if (!int.TryParse(DayTextBox.Text, out int day) || day < 1 || day > GetMaxDaysInMonth())
            {
                DayTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорректный день для выбранного месяца.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DayTextBox.BorderBrush = Brushes.Black;
                ErrorTextBlock.Text = string.Empty;
            }
        }

        private void MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DayTextBox.Text))
            {
                DayTextBox.BorderBrush = Brushes.Black;
                ErrorTextBlock.Text = string.Empty;
                return;
            }

            if (!int.TryParse(DayTextBox.Text, out int day) || day < 1 || day > GetMaxDaysInMonth())
            {
                DayTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорректный день для выбранного месяца.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DayTextBox.BorderBrush = Brushes.Black;
                ErrorTextBlock.Text = string.Empty;
            }
        }


        private void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {//Разрешаю ввод только цифр в поля дня и года
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private int GetMaxDaysInMonth()
        {
            if (MonthComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                return int.Parse(selectedItem.Tag.ToString());
            }
            return 31;
        }


        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }

        private bool TryParseBirthDate(out DateTime birthDate)
        {//Проверка корректности даты
            birthDate = default;

            // Проверка дня
            if (!int.TryParse(DayTextBox.Text, out int day) || day < 1 || day > GetMaxDaysInMonth())
            {
                DayTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return false;
            }
            else
            {
                DayTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            // Проверка года
            if (!int.TryParse(YearTextBox.Text, out int year) || year < 1900 || year > DateTime.Now.Year)
            {
                YearTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return false;
            }
            else
            {
                YearTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            try
            {
                birthDate = new DateTime(year, MonthComboBox.SelectedIndex + 1, day);
                return true;
            }
            catch
            {
                DayTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return false;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Сброс ошибок
            ErrorTextBlock.Text = string.Empty;
            PhoneTextBox.BorderBrush = Brushes.Black;
            PhoneTypeListBox.BorderBrush = Brushes.Black;

            // Проверка обязательных полей
            string country = CountryComboBox.SelectedItem?.ToString();
            string phone = PhoneTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string firstName = FirstNameTextBox.Text;

            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка выбора типа номера
            if (PhoneTypeListBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип номера (мобильный или домашний).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                PhoneTypeListBox.BorderBrush = Brushes.Red;
                return;
            }

            // Проверка количества цифр в номере
            string digitsOnly = new string(phone.Where(char.IsDigit).ToArray());
            int requiredDigits;
            if (currentCountryCode == "+7")
            {
                requiredDigits = 11;
            }
            else if (currentCountryCode == "+49")
            {
                requiredDigits = 11;
            }
            else
            {
                requiredDigits = 11; // По умолчанию для других стран
            }

            if (digitsOnly.Length != requiredDigits)
            {
                MessageBox.Show($"Номер телефона должен содержать ровно {requiredDigits} цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                PhoneTextBox.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                PhoneTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            // Проверка даты рождения
            if (!TryParseBirthDate(out DateTime birthDate))
            {
                MessageBox.Show("Некорректная дата рождения. Пожалуйста, исправьте ошибки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка возраста
            int age = CalculateAge(birthDate);
            if (age < 18 || age > 90)
            {
                MessageBox.Show("Возраст должен быть от 18 до 90 лет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сохранение данных в файл
            string userData = $"Страна: {country}, Тип номера: {currentPhoneType}, Телефон: {phone}, " +
                              $"Фамилия: {lastName}, Имя: {firstName}, Дата рождения: {birthDate.ToShortDateString()}";
            File.AppendAllText("user_data.txt", userData + Environment.NewLine);

            MessageBox.Show("Данные успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
