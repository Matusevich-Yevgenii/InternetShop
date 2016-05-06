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
        //private void btnShow_Click(object sender, RoutedEventArgs e)
        //{
        //    DataTable dataTable = ds.Tables[0];

        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        if (row[0].ToString() == cbImages.SelectedItem.ToString())
        //        {
        //            //Store binary data read from the database in a byte array
        //            byte[] blob = (byte[])row[3];
        //            MemoryStream stream = new MemoryStream();
        //            stream.Write(blob, 0, blob.Length);
        //            stream.Position = 0;

        //            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
        //            BitmapImage bi = new BitmapImage();
        //            bi.BeginInit();

        //            MemoryStream ms = new MemoryStream();
        //            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //            ms.Seek(0, SeekOrigin.Begin);
        //            bi.StreamSource = ms;
        //            bi.EndInit();
        //            image2.Source = bi;

        //        }
        //    }
        //}
    }
}
