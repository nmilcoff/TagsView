using System;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvxTagsView;
using MvxTagsView_Sample.Core.ViewModels;
using UIKit;

namespace MvxTagsView_Sample.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController
    {
        private UITextField txtNewTag;
        private UIButton btnAddTag;
        private MvxSimpleTagListView simpleTagsView;

        public FirstView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.EdgesForExtendedLayout = UIRectEdge.None;

            this.txtNewTag = new UITextField
            {
                BorderStyle = UITextBorderStyle.RoundedRect,
            };
            this.btnAddTag = new UIButton();
            this.btnAddTag.SetTitle("Add", UIControlState.Normal);
            this.btnAddTag.SetTitleColor(UIColor.Blue, UIControlState.Normal);

            this.simpleTagsView = new MvxSimpleTagListView();

            this.View.AddSubviews(this.txtNewTag, this.btnAddTag, this.simpleTagsView);
            this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.View.AddConstraints(
                this.txtNewTag.WithSameCenterY(this.btnAddTag),
                this.txtNewTag.WithRelativeWidth(this.View, .7f),
                this.txtNewTag.AtLeftOf(this.View, 8f),

                this.btnAddTag.AtTopOf(this.View, 40f),
                this.btnAddTag.AtRightOf(this.View, 8f),
                this.btnAddTag.ToRightOf(this.txtNewTag, 8f),

                this.simpleTagsView.Below(this.btnAddTag, 8f),
                this.simpleTagsView.AtLeftOf(this.View, 8f),
                this.simpleTagsView.AtRightOf(this.View, 8f)
            );

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(this.txtNewTag).To(vm => vm.NewTag).Mode(MvvmCross.Binding.MvxBindingMode.TwoWay);
            set.Bind(this.btnAddTag).To(vm => vm.AddNewTagCommand);
            set.Bind(this.simpleTagsView).For(v => v.ItemsSource).To(vm => vm.SimpleSource);
            set.Bind(this.simpleTagsView).For(v => v.TagSelectedCommand).To(vm => vm.SimpleTagSelectedCommand);
            set.Bind(this.simpleTagsView).For(v => v.TagButtonTappedCommand).To(vm => vm.SimpleTagButtonTappedCommand);
            set.Apply();
        }
    }
}
