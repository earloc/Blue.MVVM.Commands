using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Commands {
    public interface ICommand<T> : ICommand {
        void Execute(T parameter);
        bool CanExecute(T parameter);
    }
}
