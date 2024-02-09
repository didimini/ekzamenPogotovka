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
        string tarifName;
        double resultMain;
        public ComboBox CB1 { get; set; }
        //public ComboBoxItem itemToSelect { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        // метод провеки является ли текст пустым или состоит только из пробелов
        public bool IsTextBoxEmpty(string text)
        {
            if (calculationTB.Text == null || calculationTB.Text == "")
            {
                return false;
            }
            return true;
        }

        private void calculationBtn_Click(object sender, RoutedEventArgs e)
        {
            double time;

            if (IsTextBoxEmpty(calculationTB.Text))
            {
                MessageBox.Show("Пожалуйста, введите минуты", "Ошибка");
            }
            else
            {
                time = Convert.ToInt32(calculationTB.Text);

                if (tarifOne.IsChecked == true)
                {
                    if (time <= 200)
                    {
                        resultMain = 0.7 * time;
                        resultCost.Content = $"К оплате: {resultMain}";
                        minSvverh.Visibility = Visibility.Collapsed;
                        tarifName = "Тариф 1";
                    }
                    else
                    {
                        resultMain = 140 + 1.6 * (time - 200);
                        resultCost.Content = $"К оплате: {resultMain}";
                        minSvverh.Content = $"Минут свверх установленной нормы {time - 200}";
                        minSvverh.Visibility = Visibility.Visible;
                        tarifName = "Тариф 2";
                    }
                }
                else if (tarifTwo.IsChecked == true)
                {
                    if (time <= 100)
                    {
                        resultMain = 0.3 * time;
                        resultCost.Content = $"К оплате: {resultMain}";
                        minSvverh.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        resultMain = 30 + 1.6 * (time - 100);
                        resultCost.Content = $"К оплате: {resultMain}";
                        minSvverh.Content = $"Минут свверх установленной нормы {time - 100}";
                        minSvverh.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public bool ComboBoxProverka()
        {
            // Проверяем, выбран ли элемент в ComboBox
            return CB.SelectedItem != null;
        }

        public bool IsComboBoxItemSelected(ComboBox comboBox, string itemText)
        {
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
                return selectedComboBoxItem.Content.ToString() == itemText;
            }
            return false;
        }



        public bool IsInputValid(string input)
        {
            // регулярное выражение для проверки ввода только чисел
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(input);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsInputValid(e.Text);
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

        private void cvitocBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущую дату и время в формате
            string dateNow = DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss");

            // Получаем текущий каталог приложения
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Формируем путь к файлу шаблона договора
            string templatePath = System.IO.Path.Combine(currentDirectory, "Chek", "Чек.docx");
            string outputPath = System.IO.Path.Combine(currentDirectory, "Chek", $"Чек_{dateNow}.docx ");

            // Создаем объект приложения Microsoft Word
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            // Открываем шаблон договора
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(templatePath);

            // Заменяем метки в шаблоне значениями
            doc.Content.Find.Execute(FindText: "{Tarif}", ReplaceWith: tarifName.ToString());
            doc.Content.Find.Execute(FindText: "{Result}", ReplaceWith: resultMain.ToString());
            doc.Content.Find.Execute(FindText: "{Date}", ReplaceWith: dateNow.ToString());

            // Сохраняем документ с замененными значениями по указанному пути
            doc.SaveAs2(outputPath);
            // Закрываем документ
            doc.Close();
            // Закрываем приложение Microsoft Word
            wordApp.Quit();

            // Открываем созданный чек
            System.Diagnostics.Process.Start(outputPath);

            // Выводим сообщение об успешном выполнении операции
            MessageBox.Show("Операция успешно выполнена. Чек сохранен.");
        }
    }

}
