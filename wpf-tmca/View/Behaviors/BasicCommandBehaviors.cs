using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace wpf_tmca.View.Behaviors
{
    public abstract class BasicCommandBehavior<T> : Behavior<T>, ICommandSource where T : FrameworkElement
    {
        private EventHandler _canExecuteChangedHandler;

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(BasicCommandBehavior<T>), new PropertyMetadata(null));



        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(BasicCommandBehavior<T>), new PropertyMetadata(null));



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(BasicCommandBehavior<T>), new PropertyMetadata(null, OnCommandChanged));



        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BasicCommandBehavior<T>)?.OnCommandChanged((ICommand)e.NewValue, (ICommand)e.OldValue);
        }

        private void OnCommandChanged(ICommand newValue, ICommand oldValue)
        {
            if (oldValue != null)
                RemoveHookUpFromCommand(oldValue);
            if (newValue != null)
                HookUpCommand(newValue);
        }

        private void HookUpCommand(ICommand newValue)
        {
            _canExecuteChangedHandler = CanExecuteChanged;
            newValue.CanExecuteChanged += _canExecuteChangedHandler;
        }

        private void RemoveHookUpFromCommand(ICommand oldValue)
        {
            oldValue.CanExecuteChanged -= _canExecuteChangedHandler;
        }

        protected abstract void CanExecuteChanged(object sender, EventArgs e);

        protected virtual void TriggerCommand()
        {
            if (Command == null) return;
            if (Command is RoutedCommand)
                (Command as RoutedCommand).Execute(CommandParameter, CommandTarget);
            else Command.Execute(CommandParameter);
        }
    }
}
