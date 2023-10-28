using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFEvents.Infrastructure
{
    internal class RelayCommand : ICommand
    {
        Predicate<object> canExecute;
        Action<object> execute;
        public RelayCommand( Action<object> execute, Predicate<object> canExecute=null)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
