
namespace EsHeichSample.Client
{
    using System;
    using System.Windows.Input;

    public class Command : Command<object>
    {
        public Command(Action action, Func<bool> canExecute = null) 
            : base((_)=> action.Invoke(), (_)=> canExecute.Invoke())
        {
        }
    }

    public class Command<TArg> : ICommand
    {
        readonly protected Action<TArg> _action;
        readonly protected Func<TArg, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public Command(Action<TArg> action, Func<TArg, bool> canExecute = default) 
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is TArg arg)
                return _canExecute?.Invoke(arg) ?? false;
            else 
                return false;
        }

        public void Execute(object parameter)
        {
            if (parameter is TArg arg)
            {
                this._action?.Invoke(arg);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
