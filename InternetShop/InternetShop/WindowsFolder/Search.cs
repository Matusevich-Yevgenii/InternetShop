using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InternetShop.Data;
using InternetShop.WindowsFolder.Memento;

namespace InternetShop.WindowsFolder
{
    public class Search : BasicSearch
    {
        public Search(string searchText, Caretaker login = null, SearchWindow form = null) : base(searchText, login, form)
        {
            _searchText = searchText;
            _caretaker = login;
            _form = form;
        }

        protected override void ShowSomeForm()
        {
            _form = new SearchWindow(_caretaker);
            //if (_login != "")
            //{
            //    _form = new SearchWindow(_searchText, _login);
            //}
            _form.Show();
        }

        protected override void SearchElements()
        {
            if (_searchText != "")
            {
                _form.lbProducts.Items.Clear();
                using (
                    var conn =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings[
                                "InternetShop.Properties.Settings.DbCarConnectionString"].ConnectionString))
                {
                    conn.Open();
                    var sql = new SqlCommand("SELECT * FROM CarTable", conn);
                    using (var reader = sql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (((string)reader["name"]).ToLower().Contains(_searchText.ToLower())
                                    || ((string)reader["model"]).ToLower().Contains(_searchText.ToLower()))
                                {
                                    _form.lbProducts.Items.Add(new Product((string)reader["name"], (string)reader["model"],
                                        (byte[])reader["image"], (string)reader["price"],
                                        (string)reader["warranty"], (string)reader["descriptions"])
                                    { Id = (int)reader["id"] });
                                }
                                /* (new Product((string)reader["name"], (string)reader["model"],
                                 (byte[])reader["image"], (string)reader["price"],
                                 (string)reader["warranty"], (string)reader["descriptions"])
                             { Id = (int)reader["id"] });*/
                            }
                        }
                    }
                    conn.Close();
                }
            }
        }
    }
}
