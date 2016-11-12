using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_tmca.Controller.Exit;

namespace wpf_tmca.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private ExitController exit => ExitController.Instance;

        #region Commands
        public ICommand ExitCommand => exit.ExitCommand;

        #endregion
    }
}
