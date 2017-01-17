using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Data
{
    public class Order
    {
        public int ?id { get; set; }

        public List<Bucket> bucket = new List<Bucket>();

        public DateTime dateTime { get; set; }

        public Order(Bucket[] b, DateTime d)
        {
            foreach (Bucket t in b)
            {
                bucket.Add(t);
            }
            dateTime = d;
        }
    }
}
