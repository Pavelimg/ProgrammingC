using System.Windows;

namespace _3
{
    public partial class MainWindow : Window
    {
        private Thickness initialPosition;
        private const double StepFactor = 0.5;
        private const double LabelHeight = 30;
        private double Step => LabelHeight * StepFactor;
        private const double LabelWidth = LabelHeight;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            double centerX = (mainGrid.ActualWidth - LabelWidth) / 2;
            double centerY = (mainGrid.ActualHeight - LabelHeight) / 2;
            labelPoint.Margin = new Thickness(centerX, centerY, 0, 0);
            initialPosition = labelPoint.Margin;
            UpdateButtonsStateUp();
            UpdateButtonsStateLeft();
        }


        private bool CanMoveUp()
        {
            return labelPoint.Margin.Top - Step >= 0;
        }

        private bool CanMoveDown()
        {
            return labelPoint.Margin.Top + LabelHeight + Step <= mainGrid.ActualHeight;
        }


        private bool CanMoveLeft()
        {
            return labelPoint.Margin.Left - Step >= 0;
        }

        private bool CanMoveRight()
        {
            return labelPoint.Margin.Left + LabelWidth + Step <= mainGrid.ActualWidth;
        }


        private void UpdateButtonsStateLeft()
        {

            btnLeft.IsEnabled = CanMoveLeft();
            btnRight.IsEnabled = CanMoveRight();
        }


        private void UpdateButtonsStateUp()
        {
            btnUp.IsEnabled = CanMoveUp();
            btnDown.IsEnabled = CanMoveDown();

        }


        private void MovePoint(int deltaX, int deltaY)
        {
            labelPoint.Margin = new Thickness(
                labelPoint.Margin.Left + deltaX,
                labelPoint.Margin.Top + deltaY,
                0,
                0
            );
            //UpdateButtonsState(); 
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MovePoint(0, -(int)Step);
            UpdateButtonsStateUp();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MovePoint(0, (int)Step);
            UpdateButtonsStateUp();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MovePoint(-(int)Step, 0);
            UpdateButtonsStateLeft();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MovePoint((int)Step, 0);
            UpdateButtonsStateLeft();
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var current = labelPoint.Margin;
            var deltaX = current.Left - initialPosition.Left;
            var deltaY = current.Top - initialPosition.Top;

            MessageBox.Show(
                $"Положение ({current.Left:F0};{current.Top:F0})\n" +
                $"Отклонение ({deltaX:+0;-0;0};{deltaY:+0;-0;0})\n" +
                $"Текущий шаг: {Step:F1}",
                "Информация о положении",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}
