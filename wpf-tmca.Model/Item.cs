using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace wpf_tmca.Model
{
    public class Item : IItem
    {
        private static int _itemNumber;

        public List<string> Context { get; set; }
        public double Height { get; set; }
        public int ItemNumber { get; set; } = _itemNumber++;
        public EItem Type { get; set; }
        public double Width { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
