using System;
using System.Collections.Generic;
using System.Text;
using Blue.MVVM.Commands;


namespace System.Windows.Input {

    public interface INotifyExecutionCommand : INotificationCommand {
    }

    public static class INotifyCanExecuteChangedExtensions {
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event
        /// </summary>
        [Obsolete("use 'INotificationCommand.NotifyCanExecuteChanged' instead")]
        public static void OnCanExecuteChanged(this INotifyExecutionCommand source) {
            source.NotifyCanExecuteChanged();
        }

        [Obsolete("Xamarin backwards-compatibility, use 'INotificationCommand.NotifyCanExecuteChanged' instead")]
        public static void ChangeCanExecute(this INotifyExecutionCommand source) {
            source.NotifyCanExecuteChanged();
        }
    }


    [Obsolete("Use Blue.MVVM.Command instead")]
    public class RelayCommand : Command {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(execute, canExecute) {
        }
    }

    [Obsolete("Use Blue.MVVM.Command<T> instead")]
    public class RelayCommand<T> : Command<T> {

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null) 
            : base (execute, canExecute) {
        }

        protected internal RelayCommand(Action execute, Func<bool> canExecute = null) 
            : base (execute, canExecute) {
        }
    }
}
