using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_tmca.Controller;
using wpf_tmca.Controller.Exit;
using System.Windows;
using wpf_tmca.ViewModel.Items;

namespace wpf_tmca.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private ExitController exit => ExitController.Instance;
        private bool _isAddingClassPressed;
        private ItemViewModel _itemViewModel;


        #region Commands
        public ICommand ExitCommand => exit.ExitCommand;
        public ICommand HideStatusBarCommand => new RelayCommand(HideStatusBar);
        public ICommand HideToolBoxCommand => new RelayCommand(HideToolBox);
        public RelayCommand<MouseButtonEventArgs> CreateItemCommand => new RelayCommand<MouseButtonEventArgs>(OnClickCreateItem, CanCreateItem);

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
                return "Hidden";
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

        public MainViewModel()
        {
            _statusBar = true;
            _toolBox = true;
        }

        private bool CanCreateItem(MouseButtonEventArgs e)
        {
            return IsAddingClassPressed;
        }

        private void OnClickCreateItem(MouseButtonEventArgs e)
        {
            Console.WriteLine(IsAddingClassPressed);
            ItemViewModel item = null;
            var position = e.MouseDevice.GetPosition(e.Source as IInputElement);
            if (IsAddingClassPressed)
            {
                item = new ClassViewModel() { Width = 60, Height = 80, X = position.X, Y = position.Y };
                IsAddingClassPressed = false;
            }             
        }

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
    }
}
