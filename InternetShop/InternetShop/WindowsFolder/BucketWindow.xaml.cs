using System;
using System.Collections.Generic;
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

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для BucketWindow.xaml
    /// </summary>
    public partial class BucketWindow : Window
    {
        public BucketWindow()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var t = new SearchWindow();
            t.Show();
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
            var t = new OrderWindow();
            t.Show();
            Close();
        }

        private void BtnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            var t = new ShowProductsWindow();
            t.Show();
            Close();
        }
    }
}
