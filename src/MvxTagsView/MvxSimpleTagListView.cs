using System;

namespace MvxTagsView
{
    public class MvxSimpleTagListView : MvxTagListView<string>
    {
        public MvxSimpleTagListView(bool enableTagButton = true)
            : base(s => s, enableTagButton)
        {
        }
    }
}
