using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace экзаменПодготовка
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculationBtn_Click(object sender, RoutedEventArgs e)
        {
            double time;

            if (calculationTB.Text == null || calculationTB.Text == "")
            {
                MessageBox.Show("Пожжалуйста введите минуты", "Ошибка");
            }
            else
            {
                time = Convert.ToInt32(calculationTB.Text);

                if (tarifOne.IsChecked == true)
                {
                    if (time <= 200)
                    {
                        resultCost.Content = $"К оплате: {0.7 * time}";
                        minSvverh.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        resultCost.Content = $"К оплате: {140 + 1.6 * (time - 200)}";
                        minSvverh.Content = $"Минут свверх установленной нормы {time - 200}";
                        minSvverh.Visibility = Visibility.Visible;
                    }
                }
                else if (tarifTwo.IsChecked == true)
                {
                    if (time <= 100)
                    {
                        resultCost.Content = $"К оплате: {0.3 * time}";
                        minSvverh.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        resultCost.Content = $"К оплате: {30 + 1.6 * (time - 100)}";
                        minSvverh.Content = $"Минут свверх установленной нормы {time - 100}";
                        minSvverh.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Используем регулярное выражение для проверки, что вводимые символы - это числа
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Запрещаем вставку текста с помощью Ctrl+V
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tarifOne.IsChecked = true;
            calculationTB.Text = null;
            resultCost.Content = "";
            minSvverh.Content = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
