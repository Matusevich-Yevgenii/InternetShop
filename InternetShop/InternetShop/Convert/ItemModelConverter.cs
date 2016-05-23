using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InternetShop.Convert
{
    class ItemModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
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
                            if (System.Convert.ToInt32(value) == (int)reader["id"])
                            {

                                return (string)reader["model"];
                            }
                            /* (new Product((string)reader["name"], (string)reader["model"],
                                 (byte[])reader["image"], (string)reader["price"],
                                 (string)reader["warranty"], (string)reader["descriptions"])
                             { Id = (int)reader["id"] });*/
                        }
                    }
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
