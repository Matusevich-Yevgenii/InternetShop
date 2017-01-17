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
using InternetShop.WindowsFolder.Memento;
using InternetShop.WindowsFolder.Proxy;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Originator originator = new Originator();
        Caretaker caretaker = new Caretaker();
        string constr = ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();

            // reset State 
            originator.State = "";
            caretaker.Memento = originator.CreateMemento();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string sql = "TRUNCATE TABLE Bucket";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
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
                    // FACTORY METHOD and Proxy
                    
                    //for proxy // create some features for some users 
                    var features = new FeaturesOperator();
                    
                    // Pattern MEMENTO  // save state that user is authorizate 
                    originator.State = email;
                    caretaker.Memento = originator.CreateMemento();

                    var t = new ShowProductsWindow(caretaker, features); // Session for concrete creator ( for email and login);       
                    t.Show(); // t.FactoryMethod();
                    

                    //welcome.TextBlockName.Text = username;//Sending value from one form to another form.
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
            var t = new RegisterWindow(caretaker);
            t.Show();
            Close();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            new Search(TbSearch.Text, caretaker).SearchWithShow();
            Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            var t = new ShowProductsWindow(caretaker);

            t.Show();
            Close();
        }

        private void LblForgot_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var t = new ResetPassword(caretaker);
            //if (email!=null)
            //{
            //    t = new ResetPassword(email);
            //}
            t.Show();
            Close();
        }
    }
}
