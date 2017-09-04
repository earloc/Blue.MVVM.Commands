using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM.Commands {

    public interface INotificationCommand<T> : INotificationCommand, ICommand<T> {
    }

    public interface INotificationCommand : ICommand {
        event EventHandler Executing;
        event EventHandler Executed;

        void NotifyCanExecuteChanged();
    }

    public interface INotificationCommand<TParam, TResult> 
        : INotificationCommand<TParam> {
        new event EventHandler<CommandResultEventArgs<TResult>> Executed;
        new TResult Execute(TParam param);

        TResult Result { get; }
    }

    public class CommandResultEventArgs<T> : EventArgs {
        public T Result { get; }
    }
}
