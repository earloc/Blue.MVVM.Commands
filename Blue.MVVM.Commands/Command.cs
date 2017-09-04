using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    /// <summary>
    /// non generic command implementation for passing the execution logic as <see cref="Action"/> / <see cref="System.Func{T}"/>s
    /// </summary>
    public class Command : Command<object> {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="execute">the execution-logic of the command</param>
        /// <param name="canExecute">The <see cref="System.Func{T}"/> determining if the command can be executed</param>
        public Command(Action execute, Func<bool> canExecute = null)
            : base(execute, canExecute) {
        }
    }
}
