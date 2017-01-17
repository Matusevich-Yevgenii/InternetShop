using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using InternetShop.WindowsFolder.Proxy;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для ShowProductsWindow.xaml
    /// </summary>
    public partial class ShowProductsWindow : Window, IFeatures
    {
        private Caretaker _caretaker;
        private IFeatures _features;

        public ShowProductsWindow(Caretaker caretaker = null, FeaturesOperator features = null)
        {
            InitializeComponent();
            _caretaker = caretaker;
            _features = features;

            if (features != null)
                TvCategory.ItemsSource = Request();
            else
                TvCategory.ItemsSource = CategoryCreator.GetCreatorList();

            if(_caretaker.Memento.State != null && _caretaker.Memento.State != "")
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

                var t = new ProductWindow((Product)lbProducts.SelectedItem, prd, _caretaker);
                //if (_caretaker.Memento.State != null)
                //{
                //    t = new ProductWindow((Product)lbProducts.SelectedItem, prd, _caretaker);
                //}
                t.Show();
                Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
        
        public List<Categories> Request()
        {
            return _features.Request();
        }
    }
}
