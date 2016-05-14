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
using InternetShop.Data;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для ShowProductsWindow.xaml
    /// </summary>
    public partial class ShowProductsWindow : Window
    {
        public ShowProductsWindow()
        {
            InitializeComponent();
            TvCategory.ItemsSource = CategoryCreator.GetCreatorList();
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

        private void lbProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbProducts.SelectedItem != null)
            {
                var r = new Random();
                var random = new int[4];
                var prd = new Product[4];

                for (int i = 0; i < 4; i++)
                {
                    int count = 0;
                    random[i] = r.Next(0, lbProducts.Items.Count);
                    for (int j = 0; j < i; j++)
                    {
                        if (random[i] == random[j] || (Product)lbProducts.Items[random[i]] == (Product)lbProducts.SelectedItem) count++;
                    }
                    if (count == 0)
                    {
                        prd[i] = (Product)lbProducts.Items[random[i]];
                    }
                    else
                    {
                        i--;
                    }
                }

                var t = new ProductWindow((Product)lbProducts.SelectedItem, prd);
                t.Show();
                Close();
            }
        }
    }
}
