using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace wpf_tmca.Commands
{
    public class CommandController
    {
        private static readonly CommandController _self = new CommandController();
        private readonly Stack<IUndoRedoCommand> _undoStack = new Stack<IUndoRedoCommand>();
        private readonly Stack<IUndoRedoCommand> _redoStack = new Stack<IUndoRedoCommand>();

        private CommandController() : base()
        {  
            this.UndoCommand = new RelayCommand(Undo, CanUndo); 
            this.RedoCommand = new RelayCommand(Redo, CanRedo);
        }

        public static CommandController Instance => _self;

        public RelayCommand UndoCommand { get; private set; } 
        public RelayCommand RedoCommand { get; private set; } 
        public RelayCommand ExitCommand => new RelayCommand(ExitProgram);

        public void ExitProgram()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow != null)
            {
                mainWindow.Close();
            }
        }

        public void AddAndExecute(IUndoRedoCommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
            command.Execute();
            UpdateCommandStatus();
            foreach(var v in _undoStack)
            {
                Console.WriteLine(v.ToString());
            }
        }

        public bool CanUndo() => _undoStack.Any();
        public bool CanRedo() => _redoStack.Any();

        public void Undo()
        {
            IUndoRedoCommand command = _undoStack.Pop();
            _redoStack.Push(command);
            command.Unexecute();
            UpdateCommandStatus();
        }

        public void Redo()
        {
            IUndoRedoCommand command = _redoStack.Pop();
            _undoStack.Push(command);
            command.Execute();
            UpdateCommandStatus();
        }

        private void UpdateCommandStatus()
        {
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
            Console.WriteLine("UPDATE UNDO/REDO");
        }

    }
}
