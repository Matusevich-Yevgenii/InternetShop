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
using System.Windows.Shapes;

namespace InternetShop.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        public void Reset()
        {
            TbEmail.Text = TbLogin.Text = 
                TbName.Text = TbSurname.Text = 
                Passb.Password = 
                ConfirmPassb.Password = "";
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
                var email = TbEmail.Text;
                var login = TbLogin.Text;
                var name = TbName.Text;
                var surname = TbSurname.Text;
                var password = Passb.Password;
                if (Passb.Password.Length == 0)
                {
                    Errormessage.Text = "Enter password.";
                    Passb.Focus();
                }
                else if (ConfirmPassb.Password.Length == 0)
                {
                    Errormessage.Text = "Enter Confirm password.";
                    ConfirmPassb.Focus();
                }
                else if (Passb.Password != ConfirmPassb.Password)
                {
                    Errormessage.Text = "Confirm password must be same as password.";
                    ConfirmPassb.Focus();
                }
                else
                {
                    Errormessage.Text = "";
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Users (email, login,name,surname,password) values('" + email + "','" + login + "','" + name + "','" + surname + "','" + password + "')", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Errormessage.Text = "You have Registered successfully.";
                    Reset();
                }
            }
        }
    }
}
