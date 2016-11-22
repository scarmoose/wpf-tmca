using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel.Associations
{
    class DependencyViewModel : AssociationViewModel
    {
        public DependencyViewModel() : this(new Association() {Type = EAssociation.Dependency }) { }
        public DependencyViewModel(Association line) : base(line)
        {
        }

        public DependencyViewModel(ItemViewModel from, ItemViewModel to) : base(new Association() {Type=EAssociation.Dependency}, from, to)
        {
        }
    }
}
