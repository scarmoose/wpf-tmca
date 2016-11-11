using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace wpf_tmca.View.Adorners
{
    public class MoveAdorner : Adorner, IDisposable
    {
        private readonly Action<double, double> _setPostion;
        private readonly Func<bool> _canMove;
        private double _xBefore;
        private double _yBefore;
        private Point _initialPostion;
        private Canvas _closestCanvasParent;
        private readonly AdornerLayer _layer;
        private ContentPresenter _ContentPresenter;
        private VisualCollection _Visuals;

        #region popupElemenet

        protected override Size MeasureOverride(Size constraint)
        {
            _ContentPresenter.Measure(constraint);
            return _ContentPresenter.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //change values for position
            _ContentPresenter.Arrange(new Rect(40, 40,
                 finalSize.Width, finalSize.Height));
            return _ContentPresenter.RenderSize;
        }

        protected override Visual GetVisualChild(int index) => _Visuals[index];

        protected override int VisualChildrenCount => _Visuals.Count;

        public object Content
        {
            get { return (FrameworkElement)_ContentPresenter.Content; }
            set { _ContentPresenter.Content = value; }
        }

        #endregion


        public MoveAdorner(FrameworkElement adornedElement) : base(adornedElement)
        {
            //if(setPostion==null|| canMove==null) throw new ArgumentNullException();


            //visuals
            _Visuals = new VisualCollection(this);
            _ContentPresenter = new ContentPresenter();
            _Visuals.Add(_ContentPresenter);

            //_setPostion = setPostion;
            //_canMove = canMove;
            _layer = AdornerLayer.GetAdornerLayer(adornedElement);
            _layer.Add(this);




        }

        public void Hide() => Visibility = Visibility.Collapsed;
        public void Show() => Visibility = Visibility.Visible;


        //Outcommented part is not yet ready for use. will come later

        //private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!_canMove() && !sender.Equals(this)) return;

        //    _xBefore = Canvas.GetLeft(AdornedElement);
        //    _yBefore = Canvas.GetTop(AdornedElement);
        //    _initialPostion = e.GetPosition(AdornedElement);
        //    _closestCanvasParent = FindParentOf<Canvas>(AdornedElement);
        //    e.Handled = true;
        //}
        //private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (!e.Device.Target.IsMouseCaptured) return;
        //    var posistion = e.GetPosition(_closestCanvasParent);
        //    _setPostion(posistion.X - _initialPostion.X, posistion.Y - _initialPostion.Y);
        //    e.Handled = true;
        //}

        //private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (_canMove()&& e.Device.Target.IsMouseCaptured)
        //    {
        //        e.Device.Target.ReleaseMouseCapture();
        //    }
        //    else
        //    {
        //        _setPostion(_xBefore, _yBefore);
        //    }
        //}

        public T FindParentOf<T>(DependencyObject o) where T : DependencyObject => o == null || o.GetType().IsAssignableFrom(typeof(Canvas)) ? (T)o : (T)VisualTreeHelper.GetParent(o);

        public void Dispose()
        {
            _layer.Remove(this);
            //_ContentPresenter.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            //_ContentPresenter.MouseMove -= AssociatedObject_MouseMove;
            //_ContentPresenter.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }
    }
}
