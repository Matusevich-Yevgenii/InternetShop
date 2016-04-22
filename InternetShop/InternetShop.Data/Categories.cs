using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Data
{
    public class Categories
    {
        public string Name { get; set; }
        public List<Traffic> TrafficList { get; set; }
    }

    public class Traffic
    {
        public string TrafficDescription { get; set; }
        public Traffic(string trafic)
        {
            TrafficDescription = trafic;
        }
    }

    public class CategoryCreator
    {
        static public List<Categories> GetCreatorList()
        {
            Categories cA = new Categories();
            cA.Name = "категория A";
            cA.TrafficList = new List<Traffic>()
            {
                new Traffic("мотоцикл"),
                new Traffic("мотороллер"),
                new Traffic("другие мототранспортные средства")
            };

            Categories cB = new Categories();
            cB.Name = "категория B";
            cB.TrafficList = new List<Traffic>()
            {
                new Traffic("автомобили не тяжелей 3.5т и меньше 8 мест")
            };

            Categories cC = new Categories();
            cC.Name = "категория С";
            cC.TrafficList = new List<Traffic>()
            {
                new Traffic("автомобили тяжелей 3.5т")
            };

            Categories cD = new Categories();
            cD.Name = "категория D";
            cD.TrafficList = new List<Traffic>()
            {
                new Traffic("автомобили для перевозки пассажиров")
            };

            Categories cE = new Categories();
            cE.Name = "категория E";
            cE.TrafficList = new List<Traffic>()
            {
                new Traffic("составы транспортных средств с тягачом")
            };

            return new List<Categories>() { cA, cB, cC, cD, cE };
        }
    }
}
