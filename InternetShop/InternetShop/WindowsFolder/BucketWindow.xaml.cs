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
using InternetShop.WindowsFolder.Memento;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для BucketWindow.xaml
    /// </summary>
    public partial class BucketWindow : Window
    {
        private Caretaker _caretaker;

        public BucketWindow(Caretaker l)
        {
            InitializeComponent();
            _caretaker = l;

            if (_caretaker.Memento.State != null && _caretaker.Memento.State != "")
                BtnSignIn.Content = _caretaker.Memento.State;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            new Search(TbSearch.Text, _caretaker).SearchWithShow();
            Close();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var t = new MainWindow();
            t.Show();
            Close();
        }

        private void BtnMakeAnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (lbProducts.Items.Count != 0)
            {
                var t = new OrderWindow(_caretaker);
                //if (login!=null)
                //{
                //    t = new OrderWindow(login);
                //}
                t.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Bucket is empty");
            }
        }

        private void BtnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            var t = new ShowProductsWindow(_caretaker);
            //if (login!=null)
            //{
            //   // t = new ShowProductsWindow(login);
            //}
            t.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //lbProducts.ItemsSource = BucketList;
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
                    conn.Close();

                }

                var t = new ProductWindow(p, prd, _caretaker);
                //if (login!=null)
                //{
                //    t = new ProductWindow(p, prd, login);
                //}
                t.Show();
                Close();
            }
        }
    }
}
