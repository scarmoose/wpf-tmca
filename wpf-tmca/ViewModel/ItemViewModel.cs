using System;
using System.Collections.Generic;
using System.Windows;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel
{
    class ItemViewModel : BaseViewModel, IItem
    {
        private bool _isSelected;
        private Point _initialMousePostion;
        private bool _isMoving;
        private bool _isConnectingShapes;
        private Point _initialShapePostion;


        protected Item Item { get; }
        protected ItemViewModel(Item item)
        {
            Item = item;
        }

        #region IItem

        public List<string> Context { get; set; }
        public int ItemNumber => Item.ItemNumber;
        public EItem Type => Item.Type;

        public double Height
        {
            get { return Item.Height; }
            set
            {
                Item.Height = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }

        public double Width
        {
            get { return Item.Width; }
            set
            {
                Item.Width = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }

        public double X
        {
            get { return Item.X; }
            set
            {
                Item.X = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }

        public double Y
        {
            get { return Item.Y; }
            set
            {
                Item.Y = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }

        public double CenterX => Width / 2 + X;
        public double CenterY => Height / 2 + Y;

        #endregion
    }
}
