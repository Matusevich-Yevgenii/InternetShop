using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Data
{
    public class AmountOfProduct
    {
        public Product Product { get; set; }

        public int Amount { get; set; }

        public AmountOfProduct(Product p, int a)
        {
            Product = p;
            Amount = a;
        }
    }
}
