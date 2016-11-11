using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_tmca.Controls.Exit;

namespace wpf_tmca.ViewModel
{
    class MainViewModel
    {
        private ExitController exit => ExitController.Instance;

        public ICommand ExitCommand => exit.ExitCommand;
    }

    
}
