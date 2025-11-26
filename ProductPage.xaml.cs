using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace _41_размер
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                FIOBox.Text = "Вы авторизованы как " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 1: RoleBox.Text = "Роль: Клиент";break;
                    case 2: RoleBox.Text = "Роль: Менеджер";break;
                    case 3:RoleBox.Text = "Роль: Администратор";break;
                }
            }
            else
            {
                FIOBox.Text = "Вы не авторизованы";
                RoleBox.Text = "Роль: Гость";
            }

            var currentProducts = Muzafarov41Entities.GetContext().Product.ToList();
            
            ProductListView.ItemsSource = currentProducts;
            FilterBox.SelectedIndex = 0;

            UpdateProductPage();
        }

        private void UpdateProductPage()
        {

            var currentProducts = Muzafarov41Entities.GetContext().Product.ToList();
            
            ProductsAll.Text = "из " + currentProducts.Count().ToString();   

            if (FilterBox.SelectedIndex == 0)
                currentProducts = currentProducts.Where(p=> Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) <=100).ToList();

            if (FilterBox.SelectedIndex == 1)
                currentProducts = currentProducts.Where(p => Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) < 10).ToList();

            if (FilterBox.SelectedIndex == 2)
                currentProducts = currentProducts.Where(p => Convert.ToInt32(p.ProductDiscountAmount) >= 10 && Convert.ToInt32(p.ProductDiscountAmount) < 15).ToList();

            if (FilterBox.SelectedIndex == 3)
                currentProducts = currentProducts.Where(p => Convert.ToInt32(p.ProductDiscountAmount) >= 15 && Convert.ToInt32(p.ProductDiscountAmount) <= 100).ToList();

            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(SearchBox.Text.ToLower())).ToList();

            ProductsLeft.Text = currentProducts.Count().ToString();
                   
            ProductListView.ItemsSource = currentProducts.ToList();

            if (AscendRbut.IsChecked == true)
            {
                ProductListView.ItemsSource = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }
            if (DescendRBut.IsChecked == true)
            {
                ProductListView.ItemsSource = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProductPage();
        }

        private void FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProductPage();
        }

        private void AscendRbut_Click(object sender, RoutedEventArgs e)
        {
            UpdateProductPage();
        }

        private void DescendRBut_Click(object sender, RoutedEventArgs e)
        {
            UpdateProductPage();
        }
    }
}
