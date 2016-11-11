using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tmca.Controller.Exit
{
    class ExitController
    {
        private static readonly ExitController _self = new ExitController();
        public static ExitController Instance => _self;

        public RelayCommand ExitCommand => new RelayCommand(ExitProgram);

        public void ExitProgram()
        {
            
        }
    }
}
