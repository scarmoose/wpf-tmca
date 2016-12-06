using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_tmca.ViewModel;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    public class AddItemCommand : IUndoRedoCommand
    {
        private ItemsCollection items;
        private ItemViewModel item;

        public AddItemCommand(ItemsCollection _items, ItemViewModel _item)
        {
            items = _items;
            item = _item;
        }

        public void Execute()
        {
            items.Add(item);
        }

        public void Unexecute()
        {
            items.Remove(item);
        }
    }
}
