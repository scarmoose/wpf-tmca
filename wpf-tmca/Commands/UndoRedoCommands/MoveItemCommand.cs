using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpf_tmca.ViewModel;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    public class MoveItemCommand : IUndoRedoCommand
    {
        private ItemViewModel _item;
        private Point _old;
        private Point _new;

        public MoveItemCommand(ItemViewModel item, Point old, Point __new)
        {
            _item = item;
            _old = old;
            _new = __new;
        }
        public void Execute()
        {
            _item.X = _new.X;
            _item.Y = _new.Y;
        }

        public void Unexecute()
        {
            _item.X = _old.X;
            _item.Y = _old.Y;
        }
    
    }
}
