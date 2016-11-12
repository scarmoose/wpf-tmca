using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Model
{
    public class Diagram
    {
        public List<IItem> Items { get; set; }

        //Skal måske bare være Association
        public List<IAssociation> Lines { get; set; }
    }
}
