using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Labb3Prog
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
        }

        private void UserManager_CurrentUserChanged()
        {
            if (UserManager.IsAdminLoggedIn)
            {
                AdminTab.Visibility = Visibility.Visible;
                ShopTab.Visibility = Visibility.Visible;
                LoginTab.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShopTab.Visibility = Visibility.Visible;
                AdminTab.Visibility = Visibility.Collapsed;
                LoginTab.Visibility = Visibility.Collapsed;
            }
            if(UserManager.CurrentUser == null)
            {
                Window_Loaded(new object(), new RoutedEventArgs());
            }
        }
   

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            await UserManager.SaveUsersToFile();
            await ProductManager.SaveProductsToFile();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await UserManager.LoadUsersFromFile();
            await ProductManager.LoadProductsFromFile();

            AdminTab.Visibility = Visibility.Collapsed;
            ShopTab.Visibility = Visibility.Collapsed;
            LoginTab.Visibility = Visibility.Visible;
        }
    }
}
