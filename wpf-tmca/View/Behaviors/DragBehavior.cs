using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using wpf_tmca.View.Adorners;

namespace wpf_tmca.View.Behaviors
{
    class DragBehavior : Behavior<FrameworkElement>
    {


        #region cordinate links
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(double), typeof(DragBehavior), new FrameworkPropertyMetadata(0d, OnXChanged) { BindsTwoWayByDefault = true });

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var db = d as DragBehavior;
            if (db?._closestListBoxItem == null) return;
            Canvas.SetLeft(db._closestListBoxItem, (double)e.NewValue);
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(double), typeof(DragBehavior), new FrameworkPropertyMetadata(0d, OnYChanged) { BindsTwoWayByDefault = true });

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var db = d as DragBehavior;
            if (db?._closestListBoxItem == null) return;
            Canvas.SetTop(db._closestListBoxItem, (double)e.NewValue);
        }

        #endregion Cordiante links


        private bool _isPressed;
        private MoveAdorner _moveAdorner;
        private double _xBefore;
        private double _yBefore;
        private Point _initialPostion;
        private Canvas _closestCanvasParent;
        private ListBoxItem _closestListBoxItem;

        #region On initserilation, desposing
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            this.AssociatedObject.KeyUp += AssociatedObject_KeyUp;
            //_moveAdorner = new MoveAdorner(AssociatedObject) { Content = new Ellipse() { Width = 50, Height = 50, Fill = new SolidColorBrush(Colors.Gray)}};
            //_moveAdorner.Hide();

            AssociatedObject.PreviewMouseRightButtonDown += AssociatedObject_MouseRightButtonDown;
            AssociatedObject.PreviewMouseMove += AssociatedObject_MouseMove;
            AssociatedObject.PreviewMouseRightButtonUp += AssociatedObject_MouseRightButtonUp;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var a = FindParentOf<ListBoxItem>(AssociatedObject);
            _closestListBoxItem = a as ListBoxItem;
            Canvas.SetLeft(_closestListBoxItem, X);
            Canvas.SetTop(_closestListBoxItem, Y);
        }

        private void AssociatedObject_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_closestListBoxItem.IsSelected && e.Device.Target.IsMouseCaptured)
            {
                e.Device.Target.ReleaseMouseCapture();
            }
            else
            {
                X = _xBefore;
                Y = _yBefore;
            }
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (!e.Device.Target.IsMouseCaptured) return;
            var posistion = e.GetPosition(_closestCanvasParent);
            X = posistion.X - _initialPostion.X;
            Y = posistion.Y - _initialPostion.Y;
            e.Handled = true;
        }

        private void AssociatedObject_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _xBefore = X;
            _yBefore = Y;
            _initialPostion = e.GetPosition(AssociatedObject);
            _closestCanvasParent = FindParentOf<Canvas>(AssociatedObject) as Canvas;
            e.Handled = true;
        }

        private void AssociatedObject_KeyUp(object sender, KeyEventArgs e)
        {
            if (!_isPressed) return;
            _isPressed = false;
            _moveAdorner.Hide();
        }

        private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isPressed || e.Key != Key.LeftCtrl) return;
            _isPressed = true;
            _moveAdorner.Show();
        }

        protected override void OnDetaching()
        {
            _moveAdorner = null;
            this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
            this.AssociatedObject.KeyUp -= AssociatedObject_KeyUp;
            base.OnDetaching();
        }


        public DependencyObject FindParentOf<T>(DependencyObject o) where T : DependencyObject
        => (o == null || o.GetType().IsAssignableFrom(typeof(T))) ? o : FindParentOf<T>(VisualTreeHelper.GetParent(o));


        #endregion

    }
}
