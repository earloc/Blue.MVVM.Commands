using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    /// <summary>
    /// generic command implementation for passing the execution logic as <see cref="Action"/> / <see cref="Func{T}"/>s
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Command<T> : CommandBase<T> {

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">the execution-logic of the command</param>
        /// <param name="canExecute">The <see cref="Func{T}"/> determining if the command can be executed</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public Command(Action<T> execute, Func<T, bool> canExecute = null) {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _Execute    = execute;
            _CanExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">the execution-logic of the command</param>
        /// <param name="canExecute">The <see cref="Func{T}"/> determining if the command can be executed</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        protected internal Command(Action execute, Func<bool> canExecute = null) {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _Execute = new Action<T> ( p => execute());

           if (canExecute != null)
                _CanExecute = new Func<T, bool>(p => canExecute());

        }

        /// <summary>
        /// Executes the <see cref="RelayCommand{T}"/>s execution logic
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void OnExecute(T parameter) {
            _Execute(parameter);
        }

        /// <summary>
        /// returns the result of the <see cref="RelayCommand{T}"/>s CanExecute logic
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected override bool OnCanExecute(T parameter) {
            if (_CanExecute == null)
                return base.OnCanExecute(parameter);
            return _CanExecute(parameter);
        }

        private readonly Action<T>     _Execute;
        private readonly Func<T, bool> _CanExecute;
    }
}
