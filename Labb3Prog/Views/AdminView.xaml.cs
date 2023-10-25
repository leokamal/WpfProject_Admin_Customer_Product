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
            ProductManager.ProductListChanged += OnProductListChanged;
        }

        private void OnProductListChanged()
        {
            this.ProdList.ItemsSource = null;
            this.ProdList.ItemsSource = ProductManager.Products;
        }

        private void UserManager_CurrentUserChanged()
        {
           // MessageBox.Show("Current User changed to : "+UserManager.CurrentUser.Name);
        }

        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      //      MessageBox.Show(ProdList.SelectedItem.ToString());
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Products product = new Products(txtProduct.Text.ToString(),double.Parse(txtPrice.Text.ToString()));
            try
            {
                ProductManager.AddProduct(product);
               
        }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
}

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Check if we are not Select a product;
            if (this.ProdList.SelectedItem == null)
                return;

            Product productSelected = (Product)ProdList.SelectedItem;
            MessageBox.Show(productSelected.Name);
           
                ProductManager.RemoveProduct(productSelected);
            }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProdList.ItemsSource = ProductManager.Products;
        }
    }
}
