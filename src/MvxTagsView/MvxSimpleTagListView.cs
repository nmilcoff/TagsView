using System;

namespace MvxTagsView
{
    public class MvxSimpleTagsView : MvxTagsView<string>
    {
        public MvxSimpleTagsView(bool enableTagButton = true)
            : base(s => s, enableTagButton)
        {
        }
    }
}
