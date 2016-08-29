using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Commands {
    public abstract class AsyncCommandBase<T> : CommandBase<T>, IAsyncNotificationCommand<T> {

        public async override void Execute(T parameter) {
            await ExecuteAsync(parameter);
        }
        public abstract Task ExecuteAsync(T parameter);
    }
}
