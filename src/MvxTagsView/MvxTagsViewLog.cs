using System;
using MvvmCross;
using MvvmCross.Logging;

namespace MvxTagListView
{
    internal static class MvxTagsViewLog
    {
        internal static IMvxLog Instance { get; } = Mvx.CanResolve<IMvxLogProvider>() ? Mvx.Resolve<IMvxLogProvider>().GetLogFor("MvxTagsView") : null;
    }
}
