using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Commands {
    public abstract class CommandBase<T> : CommandBase<T, object> {

    }
    /// <summary>
    /// generic command implementation. Logic for Execute and CanExecute has to be implemented in derived types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommandBase<TParam, TResult> : INotificationCommand<TParam, TResult> {
        
        public CommandBase() {
            IsReentranceEnabled = CommandBase.IsReentranceEnabledByDefault;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        bool System.Windows.Input.ICommand.CanExecute(object parameter) {

            if (!IsReentranceEnabled && _IsExecuting)
                return false;

            var p = Getparameter(parameter);
            return CanExecute(p);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        void System.Windows.Input.ICommand.Execute(object parameter) {
            var p = Getparameter(parameter);

            if (!CanExecute(p))
                return;

            WhileBlockingReentrance(() => {
                OnExecuting(p);
                Execute(p);
                OnExecuted(p);
            });
        }

        private TParam Getparameter(object parameter) {
            if (parameter == null)
                return default(TParam);
            try {
                return (TParam)parameter;
            }
            catch (InvalidCastException ex) {
                var expected    = typeof(TParam);
                var actual      = parameter.GetType();

                throw new InvalidCastException($"expected parameter of type '{expected.FullName}', but was '{actual.FullName}'", ex);
            }
        }

        /// <summary>
        /// In a derived class, determines if the command can be executed. The default implentation returns always true.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public virtual bool CanExecute(TParam parameter) => true;

        /// <summary>
        /// In a derived class, executes the command´s logic
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public abstract TResult Execute(TParam parameter);

        public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        protected void OnExecuting(TParam parameter) => Executing?.Invoke(this, EventArgs.Empty);
        public event EventHandler Executing;

        protected void OnExecuted(TParam parameter) => Executed?.Invoke(this, EventArgs.Empty);
        public event EventHandler Executed;

        private void WhileBlockingReentrance(Action action) {
            if (_IsExecuting)
                return;

            _IsExecuting = true;
            NotifyCanExecuteChanged();
            try {
                action();
            }
            finally {
                _IsExecuting = false;
                NotifyCanExecuteChanged();
            }
        }
        private bool _IsExecuting = false;

        /// <summary>
        /// gets or sets a value indicating if the command can be executed again if it is still executing. The default behavior is to block reentrance, which means the command cannot be executed while it is still executing
        /// </summary>
        public bool IsReentranceEnabled { get; set; }
    }
}
