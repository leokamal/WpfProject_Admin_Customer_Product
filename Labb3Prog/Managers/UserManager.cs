using Labb3Prog.DataModels.Users;
using Labb3Prog.Enums;
using Labb3Prog.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Labb3Prog.Managers
{
    public static class UserManager
    {
        private static  List<User> _users = new List<User>();
        private static User _currentUser;

        public static List<User> Users { get { return _users; } set { _users = value; } }

        public static User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                CurrentUserChanged?.Invoke();
            }
        }

        public static event Action CurrentUserChanged;

        // Skicka detta efter att användarlistan ändrats eller lästs in
        public static event Action UserListChanged;

        public static bool IsAdminLoggedIn => CurrentUser.Type == UserTypes.Admin;
       // public static bool IsCustomerLoggedIn => CurrentUser.Type == UserTypes.Customer;

        public static bool RegisterAdmin(string name, string password)
        {
            User user = UserManager.Users.Where(s => s.Name == name).FirstOrDefault();
            if (user != null)
                return false;

            UserManager.Users.Add(new Admin(name, password));

            return true;
        }
        public static bool RegisterCustomer(string name, string password)
        {
            User user = UserManager.Users.Where(s => s.Name == name).FirstOrDefault();
            if (user != null)
                return false;

            UserManager.Users.Add(new Customer(name, password));

            return true;
        }

        public static void ChangeCurrentUser(string name, string password, UserTypes type)
        {
           
            if (type == UserTypes.Admin)
                _currentUser = new Admin(name, password);

            if (type == UserTypes.Customer)
                _currentUser = new Customer(name, password);
        }


        public static  User LogIn(string userName, string password)
        {
            User user = UserManager.Users.Where(u => u.Name == userName && u.Authenticate(password)).FirstOrDefault();
           
            return user;
        }

        public static void LogOut()
        {
            throw new NotImplementedException();
        }

        public static async Task SaveUsersToFile()
        {
            try
            {
                string userFilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.json");
                List<User> serializerUser = new List<User>();
                foreach (var user in UserManager.Users)
                {
                  /*  if (user.Type == UserTypes.Admin)
                    {
                        serializerUser.Add( new Admin(user.Name, user.GetPassword()));
                    }
                    else if (user.Type == UserTypes.Customer)
                    {
                        serializerUser.Add(new Customer(user.Name, user.GetPassword()));
                    }*/
                }
                using (StreamWriter writer = new StreamWriter(userFilePath))
                {
                    string userJson = JsonConvert.SerializeObject(UserManager.Users, new StringEnumConverter());
                    await writer.WriteAsync(userJson);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product list: " + ex.Message);
            }
        }

        public static async Task LoadUsersFromFile()
        {
            try
            {
                string userFilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.json");

                using (StreamReader reader = new StreamReader(userFilePath))
                {
                    string userJson = await reader.ReadToEndAsync();
                    UserManager.Users = JsonConvert.DeserializeObject<List<User>>(userJson, new UserConverter()) ?? new List<User>();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user list: " + ex.Message);
            }
        }
    }
}
