using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvxTagsView;
using MvxTagsView_Sample.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;

namespace MvxTagsView_Sample.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController
    {
        private MvxSimpleTagsView simpleTagsView;

        public FirstView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.simpleTagsView = new MvxSimpleTagsView();

            this.View.AddSubviews(this.simpleTagsView);
            this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.View.AddConstraints(
                this.simpleTagsView.AtLeftOf(this.View, 8f),
                this.simpleTagsView.AtRightOf(this.View, 8f),
                this.simpleTagsView.AtTopOf(this.View, 40f)
            );

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(Label).To(vm => vm.Hello);
            set.Bind(TextField).To(vm => vm.Hello);
            set.Bind(this.simpleTagsView).For(v => v.ItemsSource).To(vm => vm.SimpleSource);
            set.Bind(this.simpleTagsView).For(v => v.TagSelectedCommand).To(vm => vm.SimpleTagSelectedCommand);
            set.Bind(this.simpleTagsView).For(v => v.TagButtonCommand).To(vm => vm.SimpleTagButtonTappedCommand);
            set.Apply();
        }
    }
}
