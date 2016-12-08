using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel.Items
{
    class TextBoxViewModel : ItemViewModel
    {
        public TextBoxViewModel(Item item) : base(item)
        {
            Width = 60;
            Height = 40;
        }

        public TextBoxViewModel():this(new Item() { Type=EItem.TextBox})
        {

        }
        
    }
}
