using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    public class Line : ILine
    {
        public int fromPoint { get; set; }
        public string Label { get; set; }
        public int toPoint { get; set; }
        public ELine Type { get; set; }
    }
}
