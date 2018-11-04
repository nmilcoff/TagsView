using Foundation;
using MvvmCross.Platforms.Ios.Core;
using MvxTagsView_Sample.Core;
using UIKit;

namespace MvxTagsView_Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            return base.FinishedLaunching(app, options);
        }
    }
}
