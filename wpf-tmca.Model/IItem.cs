using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    public interface IItem
    {
        List<string> Context { get; set; }
        double Height { get; set; }
        double Width { get; set; }
        double X { get; set; }
        double Y { get; set; }
        int ItemNumber { get; }
        EItem Type { get; }

        string ToString();
    }
}
