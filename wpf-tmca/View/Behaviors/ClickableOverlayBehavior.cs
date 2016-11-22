using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using wpf_tmca.View.Adorners;

namespace wpf_tmca.View.Behaviors
{
    [DefaultProperty("AdornerChildElement")]
    [ContentProperty("AdornerChildElement")]

    public class ClickableOverlayBehavior : BasicCommandBehavior<FrameworkElement>
    {
        #region Fields

        private UserControlAdorner _createLineAdorner;
        private bool _isLoaded;

        #endregion

        #region AddornerChild DependencyProperty with hookup

        public FrameworkElement AdornerChildElement
        {
            get { return (FrameworkElement)GetValue(AdornerChildElementProperty); }
            set { SetValue(AdornerChildElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdornerChildElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdornerChildElementProperty =
            DependencyProperty.Register("AdornerChildElement", typeof(FrameworkElement), typeof(ClickableOverlayBehavior), new PropertyMetadata(null, OnAdornerElementChanged));

        private static void OnAdornerElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ClickableOverlayBehavior)
                (d as ClickableOverlayBehavior).HookUpAdorner((FrameworkElement)e.OldValue, (FrameworkElement)e.NewValue);
        }

        private void HookUpAdorner(FrameworkElement oldValue, FrameworkElement newValue)
        {
            if (oldValue != null)
                RemoveAdornerElement(oldValue);
            if (newValue != null)
                AddAdornerElement(newValue);
        }

        private void AddAdornerElement(FrameworkElement newValue)
        {
            if (!_isLoaded) return;
            newValue.MouseLeftButtonUp += AdornedElement_MouseLeftButton;
            _createLineAdorner = new UserControlAdorner(AssociatedObject) { Child = newValue, IsSizeFitToAdorned = true };


        }

        private void AdornedElement_MouseLeftButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!sender.Equals(e.Source)) return;
            if (!PassEventArgsOnClick)
                TriggerCommand();
            else if (e.ButtonState == MouseButtonState.Released)
            {
                CommandParameter = e;
                TriggerCommand();
            }
            e.Handled = true;

        }

        private void RemoveAdornerElement(FrameworkElement oldValue)
        {
            oldValue.MouseLeftButtonUp -= AdornedElement_MouseLeftButton;
        }
        #endregion

        #region IsAdornerVisible DependencyProperty

        public bool IsAdornerChildRendered
        {
            get { return (bool)GetValue(IsAdornerChildRenderedProperty); }
            set { SetValue(IsAdornerChildRenderedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAdornerChildRendered.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAdornerChildRenderedProperty =
            DependencyProperty.Register("IsAdornerChildRendered", typeof(bool), typeof(ClickableOverlayBehavior),
                new FrameworkPropertyMetadata(false, OnIsAdornerRenderedChanged) { BindsTwoWayByDefault = true });

        private static void OnIsAdornerRenderedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as ClickableOverlayBehavior)?.OnIsAdornerRenderedChanged(e);

        private void OnIsAdornerRenderedChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue) _createLineAdorner?.Show();
            else _createLineAdorner?.Hide();
        }
        #endregion

        #region PassEventArgsOnClick



        public bool PassEventArgsOnClick
        {
            get { return (bool)GetValue(PassEventArgsOnClickProperty); }
            set { SetValue(PassEventArgsOnClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassEventArgsOnClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassEventArgsOnClickProperty =
            DependencyProperty.Register("PassEventArgsOnClick", typeof(bool), typeof(ClickableOverlayBehavior), new PropertyMetadata(false));



        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += Loaded;
        }


        private void Loaded(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.Equals(e.OriginalSource)) return;
            _isLoaded = true;
            AddAdornerElement(AdornerChildElement);
            //_createLineAdorne
        }
        #region abstract member implementation

        protected override void CanExecuteChanged(object sender, EventArgs e)
        {
            if (Command is RoutedCommand)
                IsAdornerChildRendered = (Command as RoutedCommand).CanExecute(CommandParameter, CommandTarget);
            else if (Command != null) IsAdornerChildRendered = Command.CanExecute(CommandParameter);
            else IsAdornerChildRendered = false;
        }

        #endregion

    }
}
