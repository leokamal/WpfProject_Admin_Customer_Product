using Labb3Prog.DataModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Prog.Managers
{

    public static class ProductManager
    {
        private static readonly IEnumerable<Product> _products = new List<Product>();
        public static IEnumerable<Product> Products => _products;

        // Skicka detta efter att produktlistan ändrats eller lästs in
        public static event Action ProductListChanged;

        public static void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public static void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public static async Task SaveProductsToFile()
        {
            throw new NotImplementedException();
        }

        public static async Task LoadProductsFromFile()
        {
            throw new NotImplementedException();
        }
    }
    }
