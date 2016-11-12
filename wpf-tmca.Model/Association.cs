using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    public class Association : IAssociation
    {
        public int fromPoint { get; set; }
        public string Label { get; set; }
        public int toPoint { get; set; }
        public EAssociation Type { get; set; }
    }
}
