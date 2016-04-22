using System.Collections.Generic;

namespace InternetShop.Data
{
    public class Product
    {
        public static List<Product> ListProducts = new List<Product>(); 

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Warranty { get; set; }
        public string Descriptions { get; set; }

        public Product() { }

        public Product(string name, string price, string warranty, string desctiptions)
        {
            Name = name;
            Price = price;
            Warranty = warranty;
            Descriptions = desctiptions;
            ListProducts.Add(this);
        }
    }

    public class ProductList : List<Product>
    {
        public ProductList()
        {
            for (int i = 0; i < 100; i++)
            {
                this.Add(new Product(i + "fds", (i + i * 3) + "Fds", "gfg", "gfd"));
            }
        }
    }
}
