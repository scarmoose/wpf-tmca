using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using wpf_tmca.ViewModel.Items;
using wpf_tmca.Commands;
using wpf_tmca.Commands.UndoRedoCommands;
using System.Collections.ObjectModel;
using wpf_tmca.ViewModel.Associations;
using GalaSoft.MvvmLight;
using wpf_tmca.Model;
using System.Windows.Media;
using System.Windows.Controls;
using wpf_tmca.SaveAndLoad;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

namespace wpf_tmca.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private bool _isAddingClassPressed, _isAddingTextBoxPressed, _isAddingAssociation, _itemMoving, _statusBar, _toolBox;
        private string _statusBarVisability, _toolBoxVisability, path;
        private ItemViewModel _selectedItem;
        private Point initialMousePosition, initialItemPosition;
        public ObservableCollection<AssociationViewModel> Associations { get; private set; }
        public DiagramRepresentation diagram { get; private set; }
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        private CommandController commandController => CommandController.Instance;
        private SaveLoadController saveLoadController => SaveLoadController.Instance;

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
        public ICommand DeleteItemCommand => new RelayCommand(deleteItem, canDeleteItem);
        public ICommand DeleteAssociationsCommand => new RelayCommand<IList>(deleteAssociations);

        public ICommand SaveAsCommand => new RelayCommand(SaveAsToFile);
        public ICommand SaveCommand => new RelayCommand(SaveToFile);
        public ICommand LoadCommand => new RelayCommand(LoadFromFile);
        public ICommand newProjectCommand => new RelayCommand(newProject);
        #endregion

        public MainViewModel() : base()
        {
            _statusBar = true;
            _toolBox = true;

            Items = new ObservableCollection<ItemViewModel>();
            Associations = new ObservableCollection<AssociationViewModel>();
            diagram = new DiagramRepresentation();
            diagram.items = Items;
            diagram.associations = Associations;
        }

        #region Adding
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

        public bool IsAddingAssociationPressed
        {
            get { return _isAddingAssociation; }
            set
            {
                _isAddingAssociation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Save and Load
        //save as with chosen path
        public void SaveAsToFile()
        {
            FileDialog fd = new SaveFileDialog();
            bool? result = fd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                path = fd.FileName;
                Thread t = new Thread(() => saveLoadController.SaveToFile(diagram, fd.FileName));
                t.Start();
                //saveLoadController.SaveToFile(this.Items, fd.FileName);
            }
        }
        // save with current path
        public void SaveToFile()
        {
            if (path == null)
            {
                SaveAsToFile();
            }
            else
            {
                Thread t = new Thread(() => saveLoadController.SaveToFile(diagram, path));
                t.Start();
            }
        }
        
        public void PerformLoadFromFile()
        {            
                Application.Current.Dispatcher.Invoke(() =>
                {
                    diagram = saveLoadController.LoadFromFile(path);
                    this.Items.Clear();
                    this.Associations.Clear();
                    this.Items = diagram.items;
                    this.Associations = diagram.associations;
                    OnPropertyChanged();
                    //Items = new ObservableCollection<ItemViewModel>();
                    //Associations = new ObservableCollection<AssociationViewModel>();
                    /*
                    foreach (ItemViewModel item in diagram.items)
                    {
                        this.Items.Add(item);
                    }
                    foreach (AssociationViewModel ass in diagram.associations)
                    {
                        this.Associations.Add(ass);
                    }*/
                    //Items = diagram.items;
                    //Associations = diagram.associations;
                });   
        }
        
        public void LoadFromFile()
        {
            FileDialog fd = new OpenFileDialog();
            bool? result = fd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                path = fd.FileName;
                Thread t = new Thread(new ThreadStart(PerformLoadFromFile));
                t.Start();
            }
        }

        public void newProject()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure, you want to create a new project?" + 
                Environment.NewLine +
                Environment.NewLine +
                "Remember to save your project before you create a new!", "New Project", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                Items.Clear();
                Associations.Clear();
                Item._itemNumber = 0;
                commandController.reset();
                path = null;
            }
        }

        #endregion

        #region View

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
                item = new ClassViewModel() { X = position.X, Y = position.Y };
                IsAddingClassPressed = false;
            }
            else if (IsAddingTextBoxPressed)
            {
                Console.WriteLine("TextBox");
                item = new TextBoxViewModel() { X = position.X, Y = position.Y };
                IsAddingTextBoxPressed = false;
            }
            if (item != null)
            {
                commandController.AddAndExecute(new AddItemCommand(Items, item));
            }
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
            var itemVisualElement = (FrameworkElement)e.MouseDevice.Target;
            var canvas = FindParentOfType<Canvas>(itemVisualElement);
            return Mouse.GetPosition(canvas);
        }

        private void MouseDownItem(MouseButtonEventArgs e)
        {
            if (!IsAddingAssociationPressed)
            {
                var item = TargetItem(e);
                var mousePosition = RelativeMousePosition(e);

                initialMousePosition = mousePosition;
                initialItemPosition = new Point(item.X, item.Y);

                e.MouseDevice.Target.CaptureMouse();
            }
        }

        private void MouseMoveItem(MouseEventArgs e)
        {
            if (Mouse.Captured != null && !IsAddingAssociationPressed)
            {
                var item = TargetItem(e);
                var mousePosition = RelativeMousePosition(e);

                item.X = initialItemPosition.X + (mousePosition.X - initialMousePosition.X);
                item.Y = initialItemPosition.Y + (mousePosition.Y - initialMousePosition.Y);
                _itemMoving = true;
            }
            else
            {
                _itemMoving = false;
            }
        }

        private void MouseUpItem(MouseButtonEventArgs e)
        {
            var item = TargetItem(e);

            if (_selectedItem == null)
            {
                _selectedItem = item;
                _selectedItem.IsSelected = true;
            }
            else if (_selectedItem.ItemNumber == item.ItemNumber)
            {
                _selectedItem.IsSelected = false;
                _selectedItem = null;
            }
            else if ((!IsAddingAssociationPressed && _selectedItem.ItemNumber != item.ItemNumber) || (IsAddingAssociationPressed && _selectedItem.Type == EItem.TextBox && _selectedItem.ItemNumber != item.ItemNumber))
            {
                _selectedItem.IsSelected = false;
                _selectedItem = item;
                _selectedItem.IsSelected = true;
            }

            if (item != null && IsAddingAssociationPressed && item.Type == EItem.Class && _selectedItem.Type == EItem.Class && _selectedItem.ItemNumber != item.ItemNumber)
            {
                commandController.AddAndExecute(new AddAssociationCommand(Associations, new DependencyViewModel() { From = _selectedItem, To = item }));
                _selectedItem.IsSelected = false;

                IsAddingAssociationPressed = false;
                _selectedItem = null;
            }
            else if (_itemMoving)
            {
                var mousePosition = RelativeMousePosition(e);

                item.X = initialItemPosition.X;
                item.Y = initialItemPosition.Y;

                commandController.AddAndExecute(new MoveItemCommand(item, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));
                e.MouseDevice.Target.ReleaseMouseCapture();

                if (_selectedItem != null)
                {
                    _selectedItem.IsSelected = false;
                    _selectedItem = null;
                }
            }
        }

        #endregion

        #region delete

        private bool canDeleteItem() => _selectedItem != null;

        private void deleteItem()
        {
            commandController.AddAndExecute(new RemoveItemsCommand(Items, Associations, new List<ItemViewModel>() { _selectedItem }));
        }

        private void deleteAssociations(IList _associations)
        {
            commandController.AddAndExecute(new RemoveAssociationsCommand(Associations, _associations.Cast<AssociationViewModel>().ToList()));
        }

        #endregion
    }
}
