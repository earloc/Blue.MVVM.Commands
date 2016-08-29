#if ICOMMAND
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Input {

    /// <remarks>System.Windows.Input</remarks>
    public interface ICommand {
        // Summary:
        //     Occurs when changes occur that affect whether or not the command should execute.
        event EventHandler CanExecuteChanged;

        // Summary:
        //     Defines the method that determines whether the command can execute in its
        //     current state.
        //
        // Parameters:
        //   parameter:
        //     Data used by the command. If the command does not require data to be passed,
        //     this object can be set to null.
        //
        // Returns:
        //     true if this command can be executed; otherwise, false.
        bool CanExecute(object parameter);
        //
        // Summary:
        //     Defines the method to be called when the command is invoked.
        //
        // Parameters:
        //   parameter:
        //     Data used by the command. If the command does not require data to be passed,
        //     this object can be set to null.
        void Execute(object parameter);
    }
}
#endif
