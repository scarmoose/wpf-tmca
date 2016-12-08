using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel.Associations
{
    public abstract class AssociationViewModel : BaseViewModel, IAssociation
    {
        private ItemViewModel _to;
        private ItemViewModel _from;
        private bool _isSelected;
        private readonly Association _association;
        public int fromPoint => _from.ItemNumber;
        public string Label { get; set; }
        public int toPoint => _to.ItemNumber;
        public EAssociation Type { get; set; }

        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }
        public Brush SelectedColor => IsSelected ? Brushes.DarkRed : Brushes.Black;

        protected AssociationViewModel(Association association)
        {
            _association = association;
        }
        protected AssociationViewModel(Association association, ItemViewModel from, ItemViewModel to) : this(association)
        {
            To = to;
            From = from;
        }
        public ItemViewModel To
        {
            get { return _to; }
            set
            {
                _to = value;
                _association.toPoint = value.ItemNumber;
                OnPropertyChanged();
                OnPropertyChanged(nameof(toPoint));
            }
        }

        public ItemViewModel From
        {
            get { return _from; }
            set
            {
                _from = value;
                _association.fromPoint = value.ItemNumber;
                OnPropertyChanged();
                OnPropertyChanged(nameof(fromPoint));
            }
        }

    }
}
    
