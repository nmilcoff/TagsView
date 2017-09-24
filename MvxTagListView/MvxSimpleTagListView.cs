using System;

namespace MvxTagListView
{
    public class MvxSimpleTagListView : MvxTagListView<string>
    {
        public MvxSimpleTagListView(bool enableRemoveButton = true)
            : base(s => s, enableRemoveButton)
        {
        }
    }
}
