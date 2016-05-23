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

        public ProductWindow(Product product = null, Product[] prd = null)
        {
            InitializeComponent();
            this._product = product;
            this._prd = prd;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var t = new SearchWindow();
            t.Show();
            Close();
        }

        private void BtnBucket_Click(object sender, RoutedEventArgs e)
        {
            var t = new BucketWindow();
            t.Show();
            Close();
        }

        private void BtnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            var t = new ShowProductsWindow();
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
            }
        }
    }
}
