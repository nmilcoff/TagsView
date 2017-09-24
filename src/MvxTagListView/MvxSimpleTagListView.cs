using System;

namespace MvxTagsView
{
    public class MvxSimpleTagsView : MvxTagsView<string>
    {
        public MvxSimpleTagsView(bool enableRemoveButton = true)
            : base(s => s, enableRemoveButton)
        {
        }
    }
}
