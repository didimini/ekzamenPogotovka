using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using экзаменПодготовка;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        MainWindow exam = new MainWindow();
        [TestMethod]
        public void TestMethod1()
        {
            // вызов метода, который вы хотите протестировать
            /*bool result = exam.IsTextBoxEmpty(" ");

            // проверка результата
            Assert.IsFalse(result, "ожидалось, что валидация не пройдет из-за пустых полей");*/

            Assert.AreEqual(exam.IsTextBoxEmpty(" "), false);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // вызов метода, который хотим протестировать
            bool result = exam.IsInputValid("абвгд");

            // проверка результата
            Assert.IsFalse(result, "ожидалось ,что валидация не пройдет из-за букв");
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(exam.ComboBoxProverka(), false);
        }

        [TestMethod]
        public void TestComboBoxProverkaWhenItemSelected()
        {
            // Предполагаем, что в вашем ComboBox есть элемент с текстом "Значение 1"

            Assert.AreEqual(exam.IsComboBoxItemSelected(exam.CB1, "Значени 1"), false);


            /* // Устанавливаем SelectedItem в выбранный элемент
             exam.CB.SelectedItem = itemToSelect;

             // Вызываем метод ComboBoxProverka и ожидаем, что результат будет true
             bool result = exam.ComboBoxProverka();

             Assert.IsTrue(result, "Ожидалось, что результат ComboBoxProverka будет true, так как элемент выбран.");*/
        } 

    }
}
