using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Configuration;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для EditProducts.xaml
    /// </summary>
    public partial class EditProducts : Window
    {
        DataSet ds;
        string strName, imageName;
        string constr = ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString;

        public EditProducts()
        {
            InitializeComponent();
        }
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.ShowDialog();
                {
                    strName = fldlg.SafeFileName;
                    imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    image1.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            insertImageData();
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

        private void insertImageData()
        {
            try
            {
                if (imageName != "")
                {
                    //Initialize a file stream to read the image file
                    FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];

                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, System.Convert.ToInt32(fs.Length));
                    //Close a file stream
                    fs.Close();

                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string sql = "insert into CarTable(name, model, image, price, warranty, descriptions) values(@name, @model, @image, @price, @warranty, @descriptions)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            //Pass byte array into database
                            cmd.Parameters.Add(new SqlParameter("@name", TbName.Text));
                            cmd.Parameters.Add(new SqlParameter("@model", TbModel.Text));
                            cmd.Parameters.Add(new SqlParameter("@image", imgByteArr));
                            cmd.Parameters.Add(new SqlParameter("@price", TbPrice.Text));
                            cmd.Parameters.Add(new SqlParameter("@warranty", TbWarranty.Text));
                            cmd.Parameters.Add(new SqlParameter("@descriptions", TbDescription.Text));
                            int result = cmd.ExecuteNonQuery();
                            if (result == 1)
                            {
                                MessageBox.Show("added successfully.");
                                //BindImageList();
                            }
                        }
                        conn.Close();
                    }
                }
                TbName.Text = TbModel.Text = TbPrice.Text = TbWarranty.Text = TbDescription.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void BindImageList()
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(constr))
        //        {
        //            conn.Open();

        //            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM CarTable", conn))
        //            {
        //                ds = new DataSet("myDataSet");
        //                adapter.Fill(ds);
        //                DataTable dt = ds.Tables[0];

        //                cbImages.Items.Clear();

        //                foreach (DataRow dr in dt.Rows)
        //                    cbImages.Items.Add(dr["id"].ToString());

        //                cbImages.SelectedIndex = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

    }
}
