using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    class Item : IItem
    {
        private static int _itemNumber;

        public List<string> Context { get; set; }
        public double Height { get; set; }
        public int ItemNumber { get; set; } = _itemNumber;
        public EItem Type { get; set; }
        public double Weight { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string toString() => 
    }
}
