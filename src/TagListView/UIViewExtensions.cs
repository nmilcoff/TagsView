using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace TagsView
{
    internal static class UIViewExtensions
    {
        public static void AggregateSubviews(this UIView view, params UIView[] subviews)
        {
            view.AddSubviews(subviews);
            view.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        }
    }
}

