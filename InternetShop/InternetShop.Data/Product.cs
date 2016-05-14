using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace InternetShop.Data
{
    public class Product
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public byte[] Image { get; set; }

        public string Price { get; set; }

        public string Warranty { get; set; }

        public string Descriptions { get; set; }

        public Product() { }

        public Product(string name, string model, byte[] image, string price, string warranty, string desctiptions)
        {
            Name = name;
            Model = model;
            Image = image;
            Price = price;
            Warranty = warranty;
            Descriptions = desctiptions;
        }
    }

    public class ProductList : List<Product>
    {
        public ProductList()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
            {
                conn.Open();
                var sql = new SqlCommand("SELECT * FROM CarTable", conn);
                using (var reader = sql.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            this.Add(new Product((string)reader["name"], (string)reader["model"],
                                (byte[])reader["image"], (string)reader["price"],
                                (string)reader["warranty"], (string)reader["descriptions"])
                            { Id = (int)reader["id"] });
                        }
                    }
                }
            }
            //
            //for (int i = 0; i < 100; i++)
            //{
            //    this.Add(new Product(i + "fds", (i + i * 3) + "Fds", "gfg", "gfg", "gfd"));
            //}
        }
    }
}
