using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShop.Data;

namespace InternetShop.WindowsFolder.Proxy
{
    public class FeaturesOperator : IFeatures
    {
        public List<Categories> Request()
        {
            return CategoryCreator.GetCreatorList();
        }
    }
}
