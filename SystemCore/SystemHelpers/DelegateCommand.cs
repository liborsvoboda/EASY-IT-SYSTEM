using System;
using System.Windows.Input;

namespace EasyITSystemCenter.SystemHelper {

    public class DelegateCommand<T> : ICommand {
        private readonly Func<T, bool> canExecuteFunc;

        private readonly Action<T> executeAction;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, (val) => true) {
        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteFunc) {
            this.executeAction = executeAction;
            this.canExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter) {
            return canExecuteFunc((T)parameter);
        }

        public void Execute(object parameter) {
            executeAction((T)parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public class SimpleCommand : ICommand {
            public Predicate<object> CanExecuteDelegate { get; set; }
            public Action<object> ExecuteDelegate { get; set; }

            public bool CanExecute(object parameter) {
                if (CanExecuteDelegate != null)
                    return CanExecuteDelegate(parameter);
                return true; // if there is no can execute default to true
            }

            public event EventHandler CanExecuteChanged {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter) {
                if (ExecuteDelegate != null)
                    ExecuteDelegate(parameter);
            }
        }
    }
}