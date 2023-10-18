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
using Labb3Prog.DataModels.Users;

namespace Labb3Prog.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        
        public LoginView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
        }

        private void UserManager_CurrentUserChanged()
        {
            MessageBox.Show("Current User changed to : " + UserManager.CurrentUser.Name);
        }

        private  void  LoginBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string loginName = this.LoginName.Text;
            string loginPassword = this.LoginPwd.Password;
            User user = UserManager.LogIn(loginName, loginPassword);
            if(user !=null)
            MessageBox.Show("Login "+loginName +" with Succesfully");
            else
                MessageBox.Show("failed to login !!");
        }

        private void RegisterAdminBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string loginName = this.RegisterName.Text;
            string loginPassword = this.RegisterPwd.Password;
            

            if (UserManager.RegisterAdmin(loginName, loginPassword))
                MessageBox.Show("Register " + loginName + " as Admin with Successfuly");
            else
                MessageBox.Show("failed to register !!");

        }

        private void RegisterCustomerBtn_OnClickmerBtn_Click(object sender, RoutedEventArgs e)
        {
            string loginName = this.RegisterName.Text;
            string loginPassword = this.RegisterPwd.Password;
            

            if (UserManager.RegisterCustomer(loginName, loginPassword))
                MessageBox.Show("Register " + loginName + " as Customer with Successfuly");
            else
                MessageBox.Show("failed to register !!");
        }
    }
}
