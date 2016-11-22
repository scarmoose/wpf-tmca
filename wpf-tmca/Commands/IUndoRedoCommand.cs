using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Commands
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void Unexecute();
    }
}
