using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    /// <summary>
    /// generic command implementation for passing the execution logic as <see cref="Action"/> / <see cref="Func{T}"/>s
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    public class Command<TParam, TResult> : CommandBase<TParam, TResult> {

        /// <summary>
        /// Initializes a new instance of the <see cref="Command{T}"/> class.
        /// </summary>
        /// <param name="execute">the execution-logic of the command</param>
        /// <param name="canExecute">The <see cref="Func{T}"/> determining if the command can be executed</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public Command(Action<TParam> execute, Func<TParam, bool> canExecute = null) {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = canExecute;
        }

        private readonly Func<TParam, TResult> _Execute;
        private readonly Func<TParam, bool> _CanExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command{T}"/> class.
        /// </summary>
        /// <param name="execute">the execution-logic of the command</param>
        /// <param name="canExecute">The <see cref="Func{T}"/> determining if the command can be executed</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        protected internal Command(Action execute, Func<bool> canExecute = null) {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _Execute = new Func<TParam, TResult> ( p => {
                execute();
                return default(TResult);
            });

           if (canExecute != null)
                _CanExecute = new Func<TParam, bool>(p => canExecute());
        }

        /// <summary>
        /// Executes the <see cref="Command{T}"/>s execution logic
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override object Execute(TParam parameter) => _Execute(parameter);

        /// <summary>
        /// returns the result of the <see cref="Command{T}"/>s CanExecute logic
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public override bool CanExecute(TParam parameter) {
            if (_CanExecute == null)
                return base.CanExecute(parameter);
            return _CanExecute(parameter);
        }
    }
}
