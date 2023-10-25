using Labb3Prog.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Prog.DataModels.Users
{
    public abstract class User
    {
        public string Name { get; }

        [JsonProperty("Password")] // Specify the JSON property name
        public string Password { get; }

        public abstract UserTypes Type { get; }

        protected User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public bool Authenticate(string password)
        {
            return Password.Equals(password);
        }

       


    }
}
