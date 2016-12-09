using System.Collections.ObjectModel;
using wpf_tmca.ViewModel.Associations;

namespace wpf_tmca.ViewModel
{
    public class DiagramRepresentation
    {
        public ObservableCollection<ItemViewModel> items { get; set; }
        public ObservableCollection<AssociationViewModel> associations { get; set; }
    }
}
