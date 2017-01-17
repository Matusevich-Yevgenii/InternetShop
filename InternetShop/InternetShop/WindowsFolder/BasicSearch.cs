using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InternetShop.WindowsFolder.Memento;

namespace InternetShop.WindowsFolder
{
    public abstract class BasicSearch
    {
        protected string _searchText;
        protected Caretaker _caretaker;
        public SearchWindow _form;
        protected BasicSearch(string searchText, Caretaker login, SearchWindow _form = null) { }

        public void SearchWithShow()
        {
            ShowSomeForm();
            SearchElements();
        }

        public void Search()
        {
            SearchElements();
        }

        protected abstract void SearchElements();
        protected abstract void ShowSomeForm();
    }
}
