using System;

using UIKit;
using Cirrious.FluentLayouts.Touch;

namespace TagListView.Sample
{
    public partial class ViewController : UIViewController
    {
        private UITextField input;
        private UIButton btnAdd;
        private TagsView tagsView;

        private UIButton btnAlignLeft, btnAlignCenter, btnAlignRight;
        private UIButton btnChangeBackgroundColor, btnChangeTextColor, btnChangeButtonSize, btnChangeRemoveButtonIcon, btnChangeRemoveButtonIconColor;
        private UIButton btnMarginX, btnMarginY, btnPaddingX, btnPaddingY;
        private UIStackView stackAlignment, stackBulkStyles, stackBulkStyles2;

        private TagsView borderedTagsView;

        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.input = new UITextField { BorderStyle = UITextBorderStyle.RoundedRect };
            this.btnAdd = new UIButton();
            this.btnAdd.SetTitle("Add", UIControlState.Normal);
            this.btnAdd.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnAdd.TouchUpInside += this.BtnAdd_TouchUpInside;
            this.tagsView = new TagsView(true)
            {
                PaddingY = 4f,
                TextFont = UIFont.SystemFontOfSize(20f)
            };
            this.tagsView.RemoveButtonTapped += (sender, e) =>
            {
                this.tagsView.RemoveTag(e);
            };
            this.tagsView.TagSelected += (sender, e) =>
            {
                var alert = UIAlertController.Create("TagListView Sample", $"Selected item source: {e.ToString()}", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (obj) => this.DismissViewController(true, null)));
                this.PresentViewController(alert, true, null);
            };


            this.stackAlignment = new UIStackView { Axis = UILayoutConstraintAxis.Horizontal };
            this.btnAlignLeft = new UIButton();
            this.btnAlignLeft.SetTitle("Left", UIControlState.Normal);
            this.btnAlignLeft.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnAlignLeft.TouchUpInside += (sender, e) => this.tagsView.Alignment = TagsAlignment.Left;
            this.stackAlignment.AddArrangedSubview(this.btnAlignLeft);

            this.btnAlignCenter = new UIButton();
            this.btnAlignCenter.SetTitle("Center", UIControlState.Normal);
            this.btnAlignCenter.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnAlignCenter.TouchUpInside += (sender, e) => this.tagsView.Alignment = TagsAlignment.Center;
            this.stackAlignment.AddArrangedSubview(this.btnAlignCenter);

            this.btnAlignRight = new UIButton();
            this.btnAlignRight.SetTitle("Right", UIControlState.Normal);
            this.btnAlignRight.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnAlignRight.TouchUpInside += (sender, e) => this.tagsView.Alignment = TagsAlignment.Right;
            this.stackAlignment.AddArrangedSubview(this.btnAlignRight);



            this.stackBulkStyles = new UIStackView { Axis = UILayoutConstraintAxis.Horizontal, Spacing = 5f };
            this.btnChangeBackgroundColor = new UIButton();
            this.btnChangeBackgroundColor.SetTitle("Background", UIControlState.Normal);
            this.btnChangeBackgroundColor.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.btnChangeBackgroundColor.TouchUpInside += (sender, e) => this.tagsView.TagBackgroundColor = UIColor.Brown;
            this.stackBulkStyles.AddArrangedSubview(this.btnChangeBackgroundColor);

            this.btnChangeTextColor = new UIButton();
            this.btnChangeTextColor.SetTitle("|TextColor", UIControlState.Normal);
            this.btnChangeTextColor.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnChangeTextColor.TouchUpInside += (sender, e) => this.tagsView.TagTextColor = UIColor.Black;
            this.stackBulkStyles.AddArrangedSubview(this.btnChangeTextColor);

            this.btnChangeRemoveButtonIconColor = new UIButton();
            this.btnChangeRemoveButtonIconColor.SetTitle("|IconColor", UIControlState.Normal);
            this.btnChangeRemoveButtonIconColor.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.btnChangeRemoveButtonIconColor.TouchUpInside += (sender, e) => this.tagsView.TagButtonColor = UIColor.Green;
            this.stackBulkStyles.AddArrangedSubview(this.btnChangeRemoveButtonIconColor);

            this.btnChangeRemoveButtonIcon = new UIButton();
            this.btnChangeRemoveButtonIcon.SetTitle("Icon", UIControlState.Normal);
            this.btnChangeRemoveButtonIcon.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnChangeRemoveButtonIcon.TouchUpInside += (sender, e) => this.tagsView.ButtonIcon = UIImage.FromBundle("ic_add");
            this.stackBulkStyles.AddArrangedSubview(this.btnChangeRemoveButtonIcon);

            this.btnChangeButtonSize = new UIButton();
            this.btnChangeButtonSize.SetTitle("Size", UIControlState.Normal);
            this.btnChangeButtonSize.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.btnChangeButtonSize.TouchUpInside += (sender, e) => this.tagsView.TagButtonSize = 40f;
            this.stackBulkStyles.AddArrangedSubview(this.btnChangeButtonSize);

            this.stackBulkStyles2 = new UIStackView { Axis = UILayoutConstraintAxis.Horizontal, Spacing = 5f };
            this.btnMarginX = new UIButton();
            this.btnMarginX.SetTitle("MarginX", UIControlState.Normal);
            this.btnMarginX.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.btnMarginX.TouchUpInside += (sender, e) => this.tagsView.MarginX = 20f;
            this.stackBulkStyles2.AddArrangedSubview(this.btnMarginX);

            this.btnMarginY = new UIButton();
            this.btnMarginY.SetTitle("MarginY", UIControlState.Normal);
            this.btnMarginY.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnMarginY.TouchUpInside += (sender, e) => this.tagsView.MarginY = 20f;
            this.stackBulkStyles2.AddArrangedSubview(this.btnMarginY);

            this.btnPaddingX = new UIButton();
            this.btnPaddingX.SetTitle("PaddingX", UIControlState.Normal);
            this.btnPaddingX.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.btnPaddingX.TouchUpInside += (sender, e) => this.tagsView.PaddingX = 20f;
            this.stackBulkStyles2.AddArrangedSubview(this.btnPaddingX);

            this.btnPaddingY = new UIButton();
            this.btnPaddingY.SetTitle("PaddingY", UIControlState.Normal);
            this.btnPaddingY.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            this.btnPaddingY.TouchUpInside += (sender, e) => this.tagsView.PaddingY = 20f;
            this.stackBulkStyles2.AddArrangedSubview(this.btnPaddingY);


            this.borderedTagsView = new TagsView(false)
            {
                BorderColor = UIColor.Orange,
                TagBackgroundColor = UIColor.Red,
                TextFont = UIFont.SystemFontOfSize(20f),
                BorderWidth = 2f,
                Alignment = TagsAlignment.Right
            };
            for (int i = 0; i < 5; i++)
            {
                this.borderedTagsView.AddTag($"Tag item {i}");
            }

            this.View.AddSubviews(this.stackAlignment, this.stackBulkStyles, this.stackBulkStyles2, this.input, this.btnAdd, this.tagsView, this.borderedTagsView);
            this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.View.AddConstraints(
                this.stackAlignment.AtLeftOf(this.View),
                this.stackAlignment.AtTopOf(this.View, 40f),
                this.stackAlignment.AtRightOf(this.View),

                this.stackBulkStyles.Below(this.stackAlignment),
                this.stackBulkStyles.AtLeftOf(this.View),
                this.stackBulkStyles.AtRightOf(this.View),

                this.stackBulkStyles2.Below(this.stackBulkStyles),
                this.stackBulkStyles2.AtLeftOf(this.View),
                this.stackBulkStyles2.AtRightOf(this.View),

                this.input.Below(this.stackBulkStyles2, 20f),
                this.input.AtLeftOf(this.View),

                this.btnAdd.AtRightOf(this.View),
                this.btnAdd.ToRightOf(this.input),
                this.btnAdd.WithSameCenterY(this.input),

                this.tagsView.Below(this.input, 20f),
                this.tagsView.AtLeftOf(this.View),
                this.tagsView.AtRightOf(this.View),

                this.borderedTagsView.Below(this.tagsView, 60f),
                this.borderedTagsView.AtLeftOf(this.View),
                this.borderedTagsView.AtRightOf(this.View)
            );
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        private void BtnAdd_TouchUpInside(object sender, EventArgs e)
        {
            tagsView.AddTag(this.input.Text);
            this.input.Text = string.Empty;
        }
    }
}
