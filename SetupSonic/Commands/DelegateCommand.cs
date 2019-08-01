using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SetupSonic.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecute;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _executeAction(parameter);

        public event EventHandler CanExecuteChanged;

        public void InvokeCanExecuteChanged() => Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty)));
    }
}