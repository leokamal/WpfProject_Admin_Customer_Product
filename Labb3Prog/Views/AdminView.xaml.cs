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
using Labb3Prog.Managers;
using Labb3Prog.DataModels.Products;

namespace Labb3Prog.Views
{
    /// <summary>
    /// Logique d'interaction pour AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            this.ProdList.ItemsSource = ProductManager.Products;
        }

        private void UserManager_CurrentUserChanged()
        {
            MessageBox.Show("Current User changed to : "+UserManager.CurrentUser.Name);
        }

        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      //      MessageBox.Show(ProdList.SelectedItem.ToString());
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Product product = new Product(txtProduct.Text.ToString(),double.Parse(txtPrice.Text.ToString()));
            ProductManager.AddProduct(product);
            this.ProdList.ItemsSource = ProductManager.Products;
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Product productSelected = (Product)ProdList.SelectedItem;
            MessageBox.Show(productSelected.Name);
            ProductManager.RemoveProduct(productSelected);
            this.ProdList.ItemsSource = ProductManager.Products;
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.CurrentUser = null;
            MessageBox.Show("User " + UserManager.CurrentUser.Name + " Logged out");
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProdList.ItemsSource = ProductManager.Products;
        }
    }
}
