using System;
using UIKit;

namespace TagListView
{
    public class ObjectTapGestureRecognizer : UITapGestureRecognizer
    {
        public ObjectTapGestureRecognizer(object obj, Action<UITapGestureRecognizer> recognizer) : base(recognizer)
        {
            this.Obj = obj;
        }

        public object Obj { get; set; }
    }
}
