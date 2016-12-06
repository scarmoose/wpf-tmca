using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows;
using wpf_tmca.ViewModel.Items;
using wpf_tmca.Commands;
using wpf_tmca.Commands.UndoRedoCommands;
using System.Collections.ObjectModel;
using wpf_tmca.ViewModel.Associations;
using GalaSoft.MvvmLight;
using wpf_tmca.Model;
using System.Windows.Media;
using System.Windows.Controls;

namespace wpf_tmca.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private bool _isAddingClassPressed;
        private bool _isAddingTextBoxPressed;
        public ItemsCollection Items { get;  }
        public ObservableCollection<AssociationViewModel> Associations { get; }
        private CommandController commandController => CommandController.Instance;

        #region Commands
        public ICommand ExitCommand => commandController.ExitCommand;
        public ICommand HideStatusBarCommand => new RelayCommand(HideStatusBar);
        public ICommand HideToolBoxCommand => new RelayCommand(HideToolBox);
        public RelayCommand<MouseButtonEventArgs> CreateItemCommand => new RelayCommand<MouseButtonEventArgs>(OnClickCreateItem, CanCreateItem);
        public ICommand UndoCommand => commandController.UndoCommand;
        public ICommand RedoCommand => commandController.RedoCommand;
        public ICommand MouseDownItemCommand => new RelayCommand<MouseButtonEventArgs>(MouseDownItem);
        public ICommand MouseMoveItemCommand => new RelayCommand<MouseEventArgs>(MouseMoveItem);
        public ICommand MouseUpItemCommand => new RelayCommand<MouseButtonEventArgs>(MouseUpItem);
        public ICommand AddAssociationCommand => new RelayCommand(AddAssociation);

        #endregion

        #region View

        private bool _statusBar, _toolBox;
        private string _statusBarVisability, _toolBoxVisability;

        public void HideStatusBar()
        {
            if (_statusBar)
            {
                _statusBar = false;
            }
            else
            {
                _statusBar = true;
            }

            StatusBarVisability = getVisibility(_statusBar);
        }

        public void HideToolBox()
        {
            if (_toolBox)
            {
                _toolBox = false;
            }
            else
            {
                _toolBox = true;
            }

            ToolBoxVisability = getVisibility(_toolBox);
        }

        public string getVisibility(bool visible)
        {
            if (visible)
            {
                return "Visible";
            }
            else
            {
                return "Collapsed";
            }
        }

        public string StatusBarVisability
        {
            get { return _statusBarVisability; }
            set
            {
                _statusBarVisability = value;
                this.OnPropertyChanged("StatusBarVisability");
            }
        }

        public string ToolBoxVisability
        {
            get { return _toolBoxVisability; }
            set
            {
                _toolBoxVisability = value;
                this.OnPropertyChanged("ToolBoxVisability");
            }
        }

        #endregion

        public MainViewModel() : base()
        {
            _statusBar = true;
            _toolBox = true;

            Items = new ItemsCollection();
            Associations = new ObservableCollection<AssociationViewModel>();
        }

        #region MouseEvents

        private bool CanCreateItem(MouseButtonEventArgs e)
        {
            return IsAddingClassPressed || IsAddingTextBoxPressed;
        }

        private void OnClickCreateItem(MouseButtonEventArgs e)
        {
            ItemViewModel item = null;
            var position = e.MouseDevice.GetPosition(e.Source as IInputElement);
            if (IsAddingClassPressed)
            {
                Console.WriteLine("Class");
                item = new ClassViewModel() { Width = 60, Height = 80, X = position.X, Y = position.Y };
                IsAddingClassPressed = false;
            }
            else if (IsAddingTextBoxPressed)
            {
                Console.WriteLine("TextBox");
                item = new TextBoxViewModel() { Width = 60, Height = 60, X = position.X, Y = position.Y };
                IsAddingTextBoxPressed = false; 
            }
            if (item != null)
            {
                commandController.AddAndExecute(new AddItemCommand(Items, item)); 
            }
        }

        #endregion

        public bool IsAddingClassPressed
        {
            get { return _isAddingClassPressed; }
            set
            {
                _isAddingClassPressed = value;
                OnPropertyChanged();
                CreateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsAddingTextBoxPressed
        {
            get { return _isAddingTextBoxPressed; }
            set
            {
                _isAddingTextBoxPressed = value;
                OnPropertyChanged();
                CreateItemCommand.RaiseCanExecuteChanged();
            }
        }

        #region Association

        private bool isAddingAssociation;
        public double ModeOpacity => isAddingAssociation ? 0.4 : 1.0;
        private ItemViewModel addingAssociationFrom;

        private Point initialMousePosition;

        private Point initialShapePosition;

        private void AddAssociation()
        {
            isAddingAssociation = true;
            RaisePropertyChanged(() => ModeOpacity);
        }

        private ItemViewModel TargetItem(MouseEventArgs e)
        {
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            return (ItemViewModel)shapeVisualElement.DataContext;
        }

        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }

        private Point RelativeMousePosition(MouseEventArgs e)
        {
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            var canvas = FindParentOfType<Canvas>(shapeVisualElement);
            return Mouse.GetPosition(canvas);
        }

        private void MouseDownItem(MouseButtonEventArgs e)
        {
            if (!isAddingAssociation)
            {
                var shape = TargetItem(e);
                var mousePosition = RelativeMousePosition(e);

                initialMousePosition = mousePosition;
                initialShapePosition = new Point(shape.X, shape.Y);

                e.MouseDevice.Target.CaptureMouse();
            }
        }

        private void MouseMoveItem(MouseEventArgs e)
        {
            if (Mouse.Captured != null && !isAddingAssociation)
            {
                var shape = TargetItem(e);
                var mousePosition = RelativeMousePosition(e);

                shape.X = initialShapePosition.X + (mousePosition.X - initialMousePosition.X);
                shape.Y = initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y);
            }
        }

        private void MouseUpItem(MouseButtonEventArgs e)
        {
            if (isAddingAssociation)
            {
                var item = TargetItem(e);

                if (addingAssociationFrom == null) { addingAssociationFrom = item; addingAssociationFrom.IsSelected = true; }
                
                else if (addingAssociationFrom.ItemNumber != item.ItemNumber)
                {
                    commandController.AddAndExecute(new AddAssociationCommand(Associations, new DependencyViewModel() { From = addingAssociationFrom, To = item }));
                    addingAssociationFrom.IsSelected = false;

                    isAddingAssociation = false;
                    addingAssociationFrom = null;

                    RaisePropertyChanged(() => ModeOpacity);
                }
            }
            else
            {
                var item = TargetItem(e);
                var mousePosition = RelativeMousePosition(e);

                item.X = initialShapePosition.X;
                item.Y = initialShapePosition.Y;

                commandController.AddAndExecute(new MoveItemCommand(item, mousePosition, initialMousePosition));

                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }

        #endregion

    }
}
