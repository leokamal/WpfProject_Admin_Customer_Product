using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Prog.DataModels.Products
{
    public abstract class Product
    {
        public string Name { get; set; }

        public double Price { get; set; }

        protected Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
