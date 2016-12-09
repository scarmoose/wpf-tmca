using System;
using System.Collections.Generic;
using System.Windows.Media;
using wpf_tmca.Commands;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel
{
    public abstract class ItemViewModel : BaseViewModel, IItem, IEquatable<ItemViewModel>, IEqualityComparer<ItemViewModel>
    {
        private bool _isSelected;
        private bool _isConnectingItems;
        private CommandController _CommandController => CommandController.Instance;
        
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }
        
        public Brush SelectedColor => IsSelected ? Brushes.DarkRed : Brushes.Black;

        public int ItemNo
        {
            get { return ItemNumber; }
            /*
            set
            {
                //ItemNumber = value;
                NotifyPropertyChanged();
            }
            */
        }

        public bool IsConnectingShapes
        {
            get { return _isConnectingItems; }
            set
            {
                _isConnectingItems = value;
                OnPropertyChanged();
            }
        }

        protected IItem Item { get; }
        protected ItemViewModel(Item item)
        {
            Item = item;
        }

        public override bool Equals(object obj) => Equals(obj as ItemViewModel);
        public bool Equals(ItemViewModel other) => ItemNumber == other?.ItemNumber;
        public bool Equals(ItemViewModel x, ItemViewModel y) => x?.ItemNumber == y?.ItemNumber;

        public override int GetHashCode()
        {
            return ItemNumber;
        }
        public int GetHashCode(ItemViewModel obj)
        {
            if (obj == null) throw new ArgumentNullException($"{nameof(GetHashCode)}() can not be null");
            return obj.GetHashCode();
        }

        #region IItem

        public List<string> Context { get; set; }
        public int ItemNumber => Item.ItemNumber;
        public EItem Type => Item.Type;


        public double Height { get { return Item.Height; } set { Item.Height = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); NotifyPropertyChanged(() => CenterY); } }
        public double Width { get { return Item.Width; } set { Item.Width = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); NotifyPropertyChanged(() => CenterX); } }
       
        public double X { get { return Item.X; } set { Item.X = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); } }
        public double Y { get { return Item.Y; } set { Item.Y = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); } }

        public double CenterX => Width / 2 + X;
        public double CenterY => Height / 2 + Y;

        public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
        public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged(() => Y); } }

        #endregion

        public override string ToString() => Item.ToString();
    }
}
