﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Commands
{
    public class CommandController
    {
        private static readonly CommandController _self = new CommandController();
        private readonly Stack<IUndoRedoCommand> _undoStack = new Stack<IUndoRedoCommand>();
        private readonly Stack<IUndoRedoCommand> _redoStack = new Stack<IUndoRedoCommand>();

        private CommandController() : base()
        {
                
        }

        public static CommandController Instance => _self;

        public RelayCommand UndoCommand => new RelayCommand(Undo, CanUndo);
        public RelayCommand RedoCommand => new RelayCommand(Redo, CanRedo);

        public void AddAndExecute(IUndoRedoCommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
            command.Execute();
            UpdateCommandStatus();
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
        }

    }
}