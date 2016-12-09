using System.Collections.Generic;
using System.Collections.ObjectModel;
using wpf_tmca.ViewModel.Associations;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    class RemoveAssociationsCommand : IUndoRedoCommand
    {
        private ObservableCollection<AssociationViewModel> associations;
        private List<AssociationViewModel> associationsToRemove;

        public RemoveAssociationsCommand(ObservableCollection<AssociationViewModel> _associations,
            List<AssociationViewModel> _associationsToRemove)
        {
            associations = _associations;
            associationsToRemove = _associationsToRemove;
        }
        public void Execute()
        {
            associationsToRemove.ForEach(x => associations.Remove(x));
        }

        public void Unexecute()
        {
            associationsToRemove.ForEach(x => associations.Add(x));
        }
    }
}
