using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    public class Diagram
    {
        public List<IItem> Items { get; set; }

        public List<IAssociation> Association { get; set; }

        
    }
}
