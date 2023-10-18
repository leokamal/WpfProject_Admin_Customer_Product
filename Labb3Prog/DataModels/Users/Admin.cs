using Labb3Prog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Prog.DataModels.Users
{
    public class Admin : User
    {
        public override UserTypes Type { get; }

        public Admin(string name, string password) : base(name, password)
        {
            this.Type = UserTypes.Admin;
        }

    }
}
