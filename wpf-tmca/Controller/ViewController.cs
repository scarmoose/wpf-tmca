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
        private static readonly ViewController _self = new ViewController();
        public static ViewController Instance => _self;

        public RelayCommand HideStatusBarCommand => new RelayCommand(HideStatusBar);

        public void HideStatusBar()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow != null)
            {
                HideStatusBar();
            }
        }

        public void HideToolBox()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow != null)
            {
                //Hide ToolBox
            }
        }
    }
}
