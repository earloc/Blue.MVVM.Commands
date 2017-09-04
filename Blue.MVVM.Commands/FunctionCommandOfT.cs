using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Commands {
    public class FunctionCommand<TParam, TResult> : Command<TParam> {
        public FunctionCommand(Action<TParam> execute, Func<TParam, bool> canExecute = null) 
            : base(execute, canExecute) {
        }

        protected internal FunctionCommand(Action execute, Func<bool> canExecute = null) 
            : base(execute, canExecute) {
        }
    }
}
