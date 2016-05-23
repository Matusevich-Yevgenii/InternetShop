using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (TbEmail.Text.Length == 0)
            {
                Errormessage.Text = "Enter an email.";
                TbEmail.Focus();
            }
            else if (!Regex.IsMatch(TbEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Errormessage.Text = "Enter a valid email.";
                TbEmail.Select(0, TbEmail.Text.Length);
                TbEmail.Focus();
            }
            else
            {
                string email = TbEmail.Text;
                string password = Passb.Password;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Users where email='" + email + "'  and password='" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string username = dataSet.Tables[0].Rows[0]["name"].ToString() + " " + dataSet.Tables[0].Rows[0]["surname"].ToString();
                    var t = new ShowProductsWindow();
                    //welcome.TextBlockName.Text = username;//Sending value from one form to another form.
                    t.Show();
                    Close();
                }
                else
                {
                    Errormessage.Text = "Sorry! Please enter existing emailid/password.";
                }
                con.Close();
            }
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var t = new RegisterWindow();
            t.Show();
            Close();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var t = new SearchWindow(TbSearch.Text);
            t.Show();
            Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            var t = new ShowProductsWindow();
            t.Show();
            Close();
        }
    }
}
