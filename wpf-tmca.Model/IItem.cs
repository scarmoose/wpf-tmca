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
        double Height { get; }
        double Weight { get; }
        double X { get; }
        double Y { get; }
        int ItemNumber { get; }
        EItem Type { get; }

        string ToString();
    }
}
