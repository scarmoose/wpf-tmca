

namespace wpf_tmca.Commands
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void Unexecute();
    }
}
