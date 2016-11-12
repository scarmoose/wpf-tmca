using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    interface ILine
    {
        int fromPoint { get; }
        int toPoint { get; }
        string Label { get; }
        ELine Type { get; }
    }
}
