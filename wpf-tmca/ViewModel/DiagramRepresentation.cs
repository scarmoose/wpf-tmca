using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_tmca.ViewModel.Associations;

namespace wpf_tmca.ViewModel
{
    public class DiagramRepresentation
    {
        public ObservableCollection<ItemViewModel> items { get; set; }
        public ObservableCollection<AssociationViewModel> associations { get; set; }
    }
}
