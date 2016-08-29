using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Commands {
    interface IAsyncNotificationCommand<T> : IAsyncCommand<T>, INotificationCommand<T> {
    }
}
