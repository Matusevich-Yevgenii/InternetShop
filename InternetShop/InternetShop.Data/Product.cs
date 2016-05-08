using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace InternetShop.Data
{
    public class Product
    {
        public static List<Product> ListProducts = new List<Product>(); 

        public int? Id { get; set; }

        public string Name { get; set; }

        public Product() { }

        public Product(string name)
        {
            Name = name;
            ListProducts.Add(this);
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
                            this.Add(new Product((string)reader["name"])
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
