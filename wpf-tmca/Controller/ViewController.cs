using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_tmca.Controller
{
    class ViewController
    {
        private bool _statusBar, _toolBox;

        private static readonly ViewController _self = new ViewController();
        public static ViewController Instance => _self;

        public RelayCommand HideStatusBarCommand => new RelayCommand(HideStatusBar);

        public RelayCommand HideToolBoxCommand => new RelayCommand(HideToolBox);

        private ViewController()
        {
            _statusBar = true;
            _toolBox = true;
        }

        public bool StatusBar
        {
            get
            {
                return _statusBar;
            }
            set
            {
                _statusBar = value;
            }
        }

        public bool ToolBox
        {
            get
            {
                return _toolBox;
            }
            set
            {
                _toolBox = value;
            }
        }

        public void HideStatusBar()
        {
            if(StatusBar)
            {
                StatusBar = false;
            }
            else
            {
                StatusBar = true;
            }
            Console.WriteLine(StatusBar);
        }

        public void HideToolBox()
        {
            if (ToolBox)
            {
                ToolBox = false;
            }
            else
            {
                ToolBox = true;
            }
            Console.WriteLine(ToolBox);

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
    }
}
