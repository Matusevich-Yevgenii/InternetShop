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
    }
}
