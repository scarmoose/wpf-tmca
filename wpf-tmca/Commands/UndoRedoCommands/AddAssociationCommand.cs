using System;
using System.Collections.ObjectModel;
using wpf_tmca.ViewModel.Associations;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    class AddAssociationCommand : IUndoRedoCommand
    {
        private ObservableCollection<AssociationViewModel> associations;
        private AssociationViewModel association;

        public AddAssociationCommand(ObservableCollection<AssociationViewModel> _associations,
            AssociationViewModel _association)
        {
            associations = _associations;
            association = _association;
        }

        public void Execute()
        {
            associations.Add(association);
        }

        public void Unexecute()
        {
            associations.Remove(association);
        }
    }
}
