using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Lab5_Settings_WPF
{
    public partial class MainWindow : Window
    {
        private readonly string settingsPath = "settings.cfg";
        private bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            UpdatePreviewText();
            isInitialized = true;
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                try
                {
                    var settings = File.ReadAllLines(settingsPath);
                    foreach (var line in settings)
                    {
                        var parts = line.Split('=');
                        if (parts.Length != 2) continue;

                        switch (parts[0])
                        {
                            case "Italic": chkItalic.IsChecked = bool.Parse(parts[1]); break;
                            case "Bold": chkBold.IsChecked = bool.Parse(parts[1]); break;
                            case "Underline": chkUnderline.IsChecked = bool.Parse(parts[1]); break;
                            case "Case":
                                radLower.IsChecked = parts[1] == "Lower";
                                radUpper.IsChecked = parts[1] == "Upper";
                                break;
                            case "UpdateMode":
                                radImmediate.IsChecked = parts[1] == "Immediate";
                                radOnClick.IsChecked = parts[1] == "OnClick";
                                break;
                            case "CustomText": txtInput.Text = parts[1]; break;
                        }
                    }
                }
                catch { SetRandomSettings(); }
            }
            else
            {
                SetRandomSettings();
            }
        }

        private void SetRandomSettings()
        {
            var rnd = new Random();
            chkItalic.IsChecked = rnd.Next(2) == 1;
            chkBold.IsChecked = rnd.Next(2) == 1;
            chkUnderline.IsChecked = rnd.Next(2) == 1;
            radLower.IsChecked = rnd.Next(2) == 1;
            radUpper.IsChecked = !radLower.IsChecked;
            radImmediate.IsChecked = rnd.Next(2) == 1;
            radOnClick.IsChecked = !radImmediate.IsChecked;
        }

        private void UpdatePreviewText()
        {
            string inputText = txtInput.Text;

            txtPreview.Text = radLower.IsChecked == true
                ? inputText.ToLower()
                : inputText.ToUpper();

            txtPreview.FontStyle = chkItalic.IsChecked == true
                ? FontStyles.Italic
                : FontStyles.Normal;

            txtPreview.FontWeight = chkBold.IsChecked == true
                ? FontWeights.Bold
                : FontWeights.Normal;

            txtPreview.TextDecorations = chkUnderline.IsChecked == true
                ? TextDecorations.Underline
                : null;
        }

        private void SettingChanged(object sender, RoutedEventArgs e)
        {
            if (!isInitialized) return;

            if (radImmediate.IsChecked == true)
            {
                UpdatePreviewText();
            }

            btnUpdate.IsEnabled = radOnClick.IsChecked == true;
            SaveSettings();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isInitialized) return;

            if (radImmediate.IsChecked == true)
            {
                UpdatePreviewText();
            }
            SaveSettings();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdatePreviewText();
        }

        private void SaveSettings()
        {
            try
            {
                File.WriteAllText(settingsPath,
                    $"Italic={chkItalic.IsChecked}\n" +
                    $"Bold={chkBold.IsChecked}\n" +
                    $"Underline={chkUnderline.IsChecked}\n" +
                    $"Case={(radLower.IsChecked == true ? "Lower" : "Upper")}\n" +
                    $"UpdateMode={(radImmediate.IsChecked == true ? "Immediate" : "OnClick")}\n" +
                    $"CustomText={txtInput.Text}");
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }
    }
}
