using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// Represents a command that can be executed with a specified parameter type.
    /// </summary>
    /// <remarks>This command is typically used in scenarios where the command logic is defined in a delegate,
    /// allowing for flexible command execution and validation logic. The command can be executed if the <see
    /// cref="CanExecute"/> method returns <see langword="true"/>.</remarks>
    /// <typeparam name="T">The type of the parameter passed to the command.</typeparam>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        private EventHandler _canExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _canExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }
        public void RaiseCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
