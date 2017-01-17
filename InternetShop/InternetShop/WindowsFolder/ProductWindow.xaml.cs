using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private readonly Product _product;
        private Product[] _prd;
        string constr = ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString;
        private string login;
        private Caretaker _caretaker;

        public ProductWindow(Product product = null, Product[] prd = null, Caretaker l = null)
        {
            InitializeComponent();
            this._product = product;
            this._prd = prd;
            _caretaker = l;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            new Search(TbSearch.Text, _caretaker).SearchWithShow();
            Close();
        }

        private void BtnBucket_Click(object sender, RoutedEventArgs e)
        {
            var t = new BucketWindow(_caretaker);
            //if (_caretaker.Memento.State != null)
            //{
            //    t = new BucketWindow(_caretaker);
            //}
            t.Show();
            Close();
        }

        private void BtnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            var t = new ShowProductsWindow(_caretaker);
            //if (_caretaker.Memento.State != null)
            //{
            //    t = new ShowProductsWindow(_caretaker.);
            //}
            t.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridSource.DataContext = _product;

            GridSame1.DataContext = _prd[0];
            GridSame2.DataContext = _prd[1];
            GridSame3.DataContext = _prd[2];
            GridSame4.DataContext = _prd[3];

        }

        private void BtnAddtoBucket_Click(object sender, RoutedEventArgs e)
        {
            //new Bucket(_product, 1, _product.Price);
            //BucketList.Add(new Bucket(_product, 1, _product.Price);
            if (Regex.IsMatch(TbAmount.Text,
                @"^[0-9]+$"))
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = "insert into Bucket(idProd, amount) values(@idProd, @amount)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //Pass byte array into database
                        cmd.Parameters.Add(new SqlParameter("@idProd", _product.Id));
                        cmd.Parameters.Add(new SqlParameter("@amount", System.Convert.ToInt32(TbAmount.Text)));
                        int result = cmd.ExecuteNonQuery();
                        if (result != 1)
                        {
                            MessageBox.Show("error");
                        }
                    }
                    conn.Close();
                }
                MessageBox.Show("Added to bucket");
            }
        }

        private void f(int i)
        {
            var temp = _prd[i];
            _prd[i] = _product;
            var t = new ProductWindow(temp, _prd, _caretaker);
            //if (login!=null)
            //{
            //    t = new ProductWindow(temp, _prd, _caretaker);
            //}
            t.Show();
            Close();
        }

        private void GridSame1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            f(0);
        }

        private void GridSame2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            f(1);
        }

        private void GridSame3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            f(2);
        }

        private void GridSame4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            f(3);
        }
    }
}
