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
using InternetShop.WindowsFolder.Memento;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        private Caretaker _caretaker;
        public ResetPassword(Caretaker l)
        {
            InitializeComponent();
            _caretaker = l;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            new Search(TbSearch.Text, _caretaker).SearchWithShow();
            Close();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var t = new MainWindow();
            //if (login!=null)
            //{
            //    t = new MainWindow();
            //}
            t.Show();
            Close();
        }
    }
}
