using System;
using System.Collections.Generic;
using System.Text;

namespace System {
    public delegate void Action();
    public delegate T Func<T>();
    public delegate TResult Func<T1, TResult>(T1 p);
}

