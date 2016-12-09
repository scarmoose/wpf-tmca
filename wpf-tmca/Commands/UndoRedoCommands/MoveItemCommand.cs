using System;
using wpf_tmca.ViewModel;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    public class MoveItemCommand : IUndoRedoCommand
    {
        private ItemViewModel _item;
        private double _x;
        private double _y;

        public MoveItemCommand(ItemViewModel item, double x, double y)
        {
            _item = item;
            _x = x;
            _y = y;
        }
        public void Execute()
        {
            _item.CanvasCenterX += _x;
            _item.CanvasCenterY += _y;
        }

        public void Unexecute()
        {
            _item.CanvasCenterX -= _x;
            _item.CanvasCenterY -= _y;
        }
    
    }
}
