using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_tmca.View.Behaviors
{
    public struct PositionTransfer
    {
        public Point OldPostion { get; }
        public Point NewPostion { get; }
        public bool IsMoved { get; }

        public PositionTransfer(Point oldP, Point newP, bool isMoved)
        {
            OldPostion = oldP;
            NewPostion = newP;
            IsMoved = isMoved;
        }


    }
}
