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
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow(string textSearch = null)
        {
            InitializeComponent();
            TbSearch.Text = textSearch;
            Search();
        }

        private void BtnBucket_Click(object sender, RoutedEventArgs e)
        {
            var t = new BucketWindow();
            t.Show();
            Close();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (TbSearch.Text != "")
            {
                lbProducts.Items.Clear();
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
                                if (((string)reader["name"]).ToLower().Contains(TbSearch.Text.ToLower())
                                    || ((string)reader["model"]).ToLower().Contains(TbSearch.Text.ToLower()))
                                {
                                    lbProducts.Items.Add(new Product((string)reader["name"], (string)reader["model"],
                                        (byte[])reader["image"], (string)reader["price"],
                                        (string)reader["warranty"], (string)reader["descriptions"])
                                    { Id = (int)reader["id"] });
                                }
                                /* (new Product((string)reader["name"], (string)reader["model"],
                                 (byte[])reader["image"], (string)reader["price"],
                                 (string)reader["warranty"], (string)reader["descriptions"])
                             { Id = (int)reader["id"] });*/
                            }
                        }
                    }
                }
            }
        }

        private void lbProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbProducts.SelectedItem != null)
            {
                var r = new Random();
                var random = new int[4];
                var prd = new Product[4];
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
                                if (((Product) lbProducts.SelectedItem).Id != (int) reader["id"])
                                {
                                    prd[count] = new Product((string) reader["name"], (string) reader["model"],
                                        (byte[]) reader["image"], (string) reader["price"],
                                        (string) reader["warranty"], (string) reader["descriptions"])
                                    {
                                        Id = (int) reader["id"]
                                    };

                                    count++;
                                }
                            }
                        }
                    }

                }

                var t = new ProductWindow((Product)lbProducts.SelectedItem, prd);
                t.Show();
                Close();
            }
        }
    }
}
