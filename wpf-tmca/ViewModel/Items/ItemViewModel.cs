using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel
{
    public abstract class ItemViewModel : BaseViewModel, IItem
    {
        private bool _isSelected;
        private Point _initialMousePostion;
        private bool _isMoving;
        private bool _isConnectingItems;
        private Point _initialShapePostion;
        
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
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

        protected Item Item { get; }
        protected ItemViewModel(Item item)
        {
            Item = item;
        }

        #region event

        public ICommand OnMouseLeftBtnDownCommand => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftBtnDown);
        public ICommand OnMouseMoveCommand => new RelayCommand<UIElement>(OnMouseMove);
        public ICommand OnMouseLeftBtnUpCommand => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftUp);

        private void OnMouseLeftBtnDown(MouseButtonEventArgs e)
        {
            var visual = e.Source as UIElement;
            if (visual == null) return;
            if (!IsSelected)
            {
                IsSelected = true;
                visual.Focus();
                e.Handled = true;
                return;
            }
            if (!IsSelected && e.MouseDevice.Target.IsMouseCaptured) return;
            e.MouseDevice.Target.CaptureMouse();
            _initialMousePostion = Mouse.GetPosition(visual);
            _initialShapePostion = new Point(X, Y);
            _isMoving = true;
        }
        private void OnMouseMove(UIElement visual)
        {

            if (!_isMoving) return;

            var pos = Mouse.GetPosition(VisualTreeHelper.GetParent(visual) as IInputElement);
            X = pos.X - _initialMousePostion.X;
            Y = pos.Y - _initialMousePostion.Y;
        }


        private void OnMouseLeftUp(MouseButtonEventArgs e)
        {
            if (!_isMoving) return;
            //Undo add
            _isMoving = false;
            Mouse.Capture(null);
            e.Handled = true;
        }

        #endregion

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
