using Labb3Prog.DataModels.Products;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;

namespace Labb3Prog.Managers
{

    public static class ProductManager
    {
        private static readonly List<Product> _products = new List<Product>();

        public static IEnumerable<Product> Products => _products;


        // Skicka detta efter att produktlistan ändrats eller lästs in
        public  static  event Action ProductListChanged ;

        public static void AddProduct(Product product)
        {
            if (product != null)
            {
                Product p = Products.Where(s => s.Name == product.Name && s.Price == product.Price).FirstOrDefault();
                if (p != null)
                    throw new Exception("this product is already exist!");

                Products p1 = new Products(product.Name, product.Price);
                _products.Add(p1);
                ProductListChanged?.Invoke();
            }
        }

        public static void RemoveProduct(Product product)
        {
            _products.Remove(product);
            ProductListChanged?.Invoke();
        }

        public static async Task SaveProductsToFile()
        {
            try
            {
                string productFilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "products.json");
                using (StreamWriter writer = new StreamWriter(productFilePath))
                {
                    string productJson = JsonConvert.SerializeObject(ProductManager.Products, Formatting.Indented);
                    await writer.WriteAsync(productJson);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product list: " + ex.Message);
            }
        }

        public static async Task LoadProductsFromFile()
        {
            try
            {
                string productFilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "products.json");

                using (StreamReader reader = new StreamReader(productFilePath))
                {
                    string productJson = await reader.ReadToEndAsync();
                    _products.Clear();
                    _products.AddRange(JsonConvert.DeserializeObject<List<Products>>(productJson) ?? new List<Products>());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product list: " + ex.Message);
            }
        }
    }
    }
