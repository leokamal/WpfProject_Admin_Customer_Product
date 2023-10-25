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
using Labb3Prog.DataModels.Products;
using Labb3Prog.Managers;

namespace Labb3Prog.Views
{
    /// <summary>
    /// Logique d'interaction pour ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {
        public ShopView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            ProductManager.ProductListChanged += ProductManager_ProductListChanged;
            
            
        }

        private async void ProductManager_ProductListChanged()
        {
         //   await ProductManager.SaveProductsToFile();
       //     await ProductManager.LoadProductsFromFile();
            this.ProdList.ItemsSource = null;
            this.ProdList.ItemsSource = ProductManager.Products;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProdList.ItemsSource = null;
            this.ProdList.ItemsSource = ProductManager.Products;
        }
        private void UserManager_CurrentUserChanged()
        {
            if (UserManager.CurrentUser != null)
                this.UserName.Text = UserManager.CurrentUser.Name;
            else
            {

                MessageBox.Show("You are Logged Out, pleasse loggin to continue");
                

            }
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.CartList.SelectedItem == null)
            {
                MessageBox.Show("Please Select the product in Cart List to Remove");
                return;
            }

            this.CartList.Items.Remove(this.CartList.SelectedItem);
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string selectedProduct = ProdList.SelectedItem.ToString();
            if (selectedProduct == null)
            {
                MessageBox.Show("Please Select One product in Product List to add");
                return;
            }
            double price = ProductManager.Products.Where(s => s.Name.Equals(selectedProduct.Split(' ')[0])).FirstOrDefault().Price;

            bool productFound = false;
            foreach (string item in CartList.Items)
            {
                string[] parts = item.ToString().Split(' ');
                if (parts.Length >= 2 && parts[1] == selectedProduct.Split(' ')[0])
                {
                    productFound = true;

                    int quantity = int.Parse(parts[0].Trim('x')) + 1;
                    CartList.Items.Remove(item);
                   
                    CartList.Items.Add($"{quantity}x {selectedProduct.Split(' ')[0]} : {price} x {quantity} : {quantity * price}");
                    break;
                }
            }

            if (!productFound)
            {
                // If the product is not in the cart, add it
                CartList.Items.Add($"1x {selectedProduct.Split(' ')[0]} : {price} x {1} : {price}");
            }
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();
        }

        private void CheckoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CartList.Items.Count > 0)
            {
                string checkoutMessage = "Checkout Summary:\n";

                double totalCost = 0;

                foreach (var item in CartList.Items)
                {
                    double price = double.Parse(item.ToString().Split(':')[2].ToString());
                    checkoutMessage += item + "\n";
                    totalCost += price;
                }

                checkoutMessage += $"\nTotal Cost: {totalCost}";

                MessageBox.Show(checkoutMessage, "Checkout", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear the cart after checkout
                CartList.Items.Clear();
            }
            else
            {
                MessageBox.Show("Your cart is empty.", "Checkout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
    }
}
