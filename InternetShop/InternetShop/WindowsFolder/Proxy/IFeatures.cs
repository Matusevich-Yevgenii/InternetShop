using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShop.Data;

namespace InternetShop.WindowsFolder.Proxy
{
    interface IFeatures
    {
        List<Categories> Request();
    }
}
