using System.Collections.Generic;
using wpf_tmca.ViewModel;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    class MoveItemsCommand : IUndoRedoCommand
    {
        private List<ItemViewModel> _items;
        private double offset_x;
        private double offset_y;

        public MoveItemsCommand(List<ItemViewModel> _items, double offset_x, double offset_y)
        {
            this._items = _items;
            this.offset_x = offset_x;
            this.offset_y = offset_y;
        }

        public void Execute()
        {
            foreach(var i in _items) {
                i.X += offset_x;
                i.Y += offset_y;
            }
        }

        public void Unexecute()
        {
            foreach(var i in _items) {
                i.X -= offset_x;
                i.Y -= offset_y;
            }
        }
    }
}
