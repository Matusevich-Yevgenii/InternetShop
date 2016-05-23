using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InternetShop.Data;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var t = new SearchWindow(TbSearch.Text);
            t.Show();
            Close();
        }

        private void BtnBucket_Click(object sender, RoutedEventArgs e)
        {
            var t = new BucketWindow();
            t.Show();
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (TbCity.Text!="" && TbAddress.Text!="" && TbTelephone.Text!="")
            {


                MessageBox.Show("Order is accepted");
            }
            else
            {
                MessageBox.Show("Pls, enter city, address and telephone");
            }
        }

        private void lbProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbProducts.SelectedItem != null)
            {
                var r = new Random();
                var random = new int[4];
                var prd = new Product[4];
                var p = new Product();

                int count = 0;
                using (
                    var conn =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings[
                                "InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
                {
                    conn.Open();
                    var sql = new SqlCommand("SELECT * FROM CarTable", conn);
                    using (var reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (((Bucket)lbProducts.SelectedItem).ProdId == (int)reader["id"])
                                {
                                    p = new Product((string)reader["name"], (string)reader["model"],
                                        (byte[])reader["image"], (string)reader["price"],
                                        (string)reader["warranty"], (string)reader["descriptions"])
                                    {
                                        Id = (int)reader["id"]
                                    };
                                }
                                else
                                {
                                    prd[count] = new Product((string)reader["name"], (string)reader["model"],
                                        (byte[])reader["image"], (string)reader["price"],
                                        (string)reader["warranty"], (string)reader["descriptions"])
                                    {
                                        Id = (int)reader["id"]
                                    };

                                    count++;
                                }
                            }
                        }
                    }

                }

                var t = new ProductWindow(p, prd);
                t.Show();
                Close();
            }
        }
    }
}
