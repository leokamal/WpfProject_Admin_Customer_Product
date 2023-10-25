using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Prog.DataModels.Products
{
    public class Products : Product
    {
        public int Qte { get; set; } = 1;

        public Products(string name, double price, int qte = 1) : base(name, price)
        {
            this.Qte = qte;
        }
    }
}
