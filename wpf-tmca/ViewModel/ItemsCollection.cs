using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.ViewModel
{
    public class ItemsCollection : ObservableCollection<ItemViewModel>
    {

        new public void Add(ItemViewModel item)
        {
            base.Add(item);
        }

        new public bool Remove(ItemViewModel item)
        {
            return base.Remove(item);
        }

        new public void SetItem(int index, ItemViewModel item)
        {
            base.SetItem(index, item);
        }

        new public void InsertItem(int index, ItemViewModel item)
        {
            base.InsertItem(index, item);
        }

    }
}
