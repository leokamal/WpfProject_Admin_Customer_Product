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
        private static IEnumerable<Product> _products = new List<Product>();
        public static IEnumerable<Product> Products { get { return _products; } set { _products = value; } }
        

        // Skicka detta efter att produktlistan ändrats eller lästs in
        public static event Action ProductListChanged = OnProductListChange;

        public static void AddProduct(Product product)
        {
            _products.ToList().Add(product);
            ProductListChanged?.Invoke();
        }
        public static void OnProductListChange()
        {
           MessageBox.Show("Change product List");

        }

        public static void RemoveProduct(Product product)
        {
            _products.ToList().Remove(product);
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
                    ProductManager.Products = JsonConvert.DeserializeObject<List<Product>>(productJson) ?? new List<Product>();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product list: " + ex.Message);
            }
        }
    }
    }
