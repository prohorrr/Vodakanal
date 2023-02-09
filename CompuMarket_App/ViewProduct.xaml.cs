using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using CompuMarket_App.DB;

namespace CompuMarket_App
{
    /// <summary>
    /// Логика взаимодействия для ViewProduct.xaml
    /// </summary>
    public partial class ViewProduct : Page
    {
        private Receipt _currentProduct;
        private Users _currentUser;
        public ViewProduct(Receipt selectedProduct, Users authUser)
        {
            InitializeComponent();
            _currentProduct = selectedProduct;  
            _currentUser = authUser;
            DataContext = _currentUser;  
            DataContext = _currentProduct;
            idClient.Text = _currentProduct.INN.ToString().Trim();
            //if (_currentProduct.isActual == false)    
            //GridEnable.IsEnabled = false;
        }
        private void numberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void OrderClick_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (adressBox.Text.Length < 5)
            {
                errors.AppendLine("(Адресс) Введите корректный адресс");
                adressBox.BorderBrush = Brushes.Red;
            }
            else
                adressBox.BorderBrush = Brushes.Black;

            if (numberBox.Text.Length < 10)
            {
                errors.AppendLine("(Номер) Введите существующий номер");
                numberBox.BorderBrush = Brushes.Red;
            }
            else
                numberBox.BorderBrush = Brushes.Black;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString()); return;
            }
            try
            {
                var neworder = IlyasovaFriendEntitieser.GetContext();
                long numb = Convert.ToInt64(numberBox.Text);
                PaidFor order = new PaidFor(adressBox.Text, numb, _currentProduct, _currentUser);
                neworder.PaidFor.Add(order);  neworder.SaveChanges();
                MessageBox.Show("Заказ добавлен");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
