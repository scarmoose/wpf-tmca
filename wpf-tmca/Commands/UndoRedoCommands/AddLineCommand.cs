using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
