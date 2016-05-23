using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Data
{
    public class Bucket
    {
        public int ProdId { get; set; }

        public int Amount { get; set; }

     //   public string TotalPrice { get; set; }

        public Bucket(int _amount)
        {
            Amount = _amount;
            //TotalPrice = (Convert.ToInt32(TotalPrice) + Convert.ToInt32(price)).ToString();
        }
    }
    public class BucketList : List<Bucket>
    {
        public BucketList()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
            {
                conn.Open();
                var sql = new SqlCommand("SELECT * FROM Bucket", conn);
                using (var reader = sql.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            this.Add(new Bucket((int)reader["amount"])
                            { ProdId = (int)reader["idProd"] });
                        }
                    }
                }
            }
        }
    }
}
