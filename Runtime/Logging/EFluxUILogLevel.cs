using System;

namespace FluxUI.Logging
{
    [Flags]
    public enum EFluxUILogLevel
    {
        None    = 0,
        Error   = 1 << 0,
        Warning = 1 << 1,
        Info    = 1 << 2,
        Verbose = 1 << 3,
        All = Error | Warning | Info | Verbose
    }
}