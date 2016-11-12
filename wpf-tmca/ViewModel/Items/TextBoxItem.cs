using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel.Items
{
    class TextBoxItem : ItemViewModel
    {
        public TextBoxItem(Item item) : base(item)
        {
            Width = 60;
            Height = 23;
        }

        public TextBoxItem():this(new Item() { Type=EItem.TextBox})
        {

        }
        
    }
}
