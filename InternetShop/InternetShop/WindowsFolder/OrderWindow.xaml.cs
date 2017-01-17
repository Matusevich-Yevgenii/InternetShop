using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Caretaker _caretaker;
        private int userId;
        public OrderWindow(Caretaker l)
        {
            InitializeComponent();
            _caretaker = l;
            if (_caretaker.Memento.State != null)
            {
                using (
                    var conn =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings[
                                "InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
                {
                    conn.Open();
                    var sql = new SqlCommand("SELECT * FROM Users", conn);
                    using (var reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader["email"] as string == _caretaker.Memento.State)
                                {
                                    userId = (int) reader["Id"];
                                }
                                /* (new Product((string)reader["name"], (string)reader["model"],
                             (byte[])reader["image"], (string)reader["price"],
                             (string)reader["warranty"], (string)reader["descriptions"])
                         { Id = (int)reader["id"] });*/
                            }
                        }
                    }
                    conn.Close();
                }
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            new Search(TbSearch.Text, _caretaker).SearchWithShow();
            Close();
        }

        private void BtnBucket_Click(object sender, RoutedEventArgs e)
        {
            var t = new BucketWindow(_caretaker);
            //if (login!=null)
            //{
            //    t = new BucketWindow(login);
            //}
            t.Show();
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (TbCity.Text!="" && TbAddress.Text!="" && TbTelephone.Text!="")
            {
                if (_caretaker.Memento.State == null)
                {
                    MessageBox.Show("Pls, sign in");
                    return;
                }
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string sql = "insert into OrderTable(userId, date, city, address, phone) values (@userId, @date, @city, @address, @phone)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        Bucket[] b = new Bucket[lbProducts.Items.Count];
                        for (var i = 0; i < lbProducts.Items.Count; i++)
                        {
                            b[i] = (Bucket)lbProducts.Items[i];
                        }
                        var o = new Order(b, DateTime.Now);
                        //Pass byte array into database
                        cmd.Parameters.Add(new SqlParameter("@userId", userId));
                        cmd.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@city", TbCity.Text));
                        cmd.Parameters.Add(new SqlParameter("@address", TbAddress.Text));
                        cmd.Parameters.Add(new SqlParameter("@phone", TbTelephone.Text));
                        int result = cmd.ExecuteNonQuery();
                        if (result != 1)
                        {
                            MessageBox.Show("error");
                        }
                    }
                    conn.Close();
                }
                //using (
                //    var conn =
                //        new SqlConnection(
                //            ConfigurationManager.ConnectionStrings[
                //                "InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
                //{
                //    conn.Open();
                //    string sql = "insert into Order (date, city, address, telephone) values (@date, @city, @address, @telephone)";
                //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                //    {
                //        Bucket[] b = new Bucket[lbProducts.Items.Count];
                //        for(var i = 0; i < lbProducts.Items.Count; i++)
                //        {
                //            b[i] = (Bucket)lbProducts.Items[i];
                //        }
                //        var o = new Order(b, DateTime.Now);
                //        //Pass byte array into database

                //        cmd.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                //        cmd.Parameters.Add(new SqlParameter("@city", TbCity.Text));
                //        cmd.Parameters.Add(new SqlParameter("@address", TbAddress.Text));
                //        cmd.Parameters.Add(new SqlParameter("@telephone", TbTelephone.Text));
                //        cmd.Parameters.Add(new SqlParameter("@id", o.id));
                //       // cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = o.id;
                //        try
                //        {
                //            int result = cmd.ExecuteNonQuery();
                //            if (result == 1)
                //            {
                //                MessageBox.Show("added successfully.");
                //                //BindImageList();
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("error" + ex.ToString());
                //            throw;
                //        }

                //    }
                //    conn.Close();
                //}


                // Pattern Chain of Responsibility
                Handler h1 = new HandlerServer1();
                Handler h2 = new HandlerServer2();
                try
                {
                    h1.Successor = h2; // If h1 not success use h2 
                    
                    h1.HandleRequest(1, userId, DateTime.Now, TbCity.Text, TbAddress.Text, TbTelephone.Text);
                }
                catch (Exception ex)
                {
                    try
                    {
                        h1.HandleRequest(2, userId, DateTime.Now, TbCity.Text, TbAddress.Text, TbTelephone.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Servers didn't find");
                    }
                }
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
