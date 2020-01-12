using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.ViewModel
{
    class Command : ICommand
    {
        private Action exec;
        public event EventHandler CanExecuteChanged;

        public Command(Action exec)
        {
            this.exec = exec;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            exec.Invoke();
        }
    }
}
