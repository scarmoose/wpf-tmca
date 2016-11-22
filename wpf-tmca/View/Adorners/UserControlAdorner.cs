using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AdvancedWPFDemo.View.Adorners
{
    public class UserControlAdorner : Adorner, IDisposable
    {
        private readonly AdornerLayer _layer;
        private FrameworkElement _child;

        public UserControlAdorner(FrameworkElement adornedElement) : base(adornedElement)
        {
            //visuals
            _layer = AdornerLayer.GetAdornerLayer(AdornedElement);
            _layer?.Add(this);
            Hide();
        }
        public void Hide() => Visibility = Visibility.Collapsed;
        public void Show() => Visibility = Visibility.Visible;
        #region UserControlInjection

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return _child;
        }

        public FrameworkElement Child
        {
            get { return _child; }
            set
            {
                if (_child != null)
                {
                    RemoveVisualChild(_child);
                }
                _child = value;
                if (_child != null)
                {
                    AddVisualChild(_child);
                }
            }
        }

        public bool IsSizeFitToAdorned { get; set; }

        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var ad = AdornedElement as FrameworkElement;
            _child.Arrange(new Rect(new Point(0, 0), new Size(ad.ActualWidth, ad.ActualHeight)));
            return IsSizeFitToAdorned
                ? new Size((AdornedElement as FrameworkElement).ActualWidth, (AdornedElement as FrameworkElement).ActualHeight)
                : new Size(_child.ActualWidth, _child.ActualHeight);
        }

        #endregion
        public void Dispose()
        {
            _layer?.Remove(this);

        }
    }
}
