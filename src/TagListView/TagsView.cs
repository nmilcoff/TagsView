using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace TagListView
{
    public class TagsView : UIView
    {
        private bool enableTagButton;

        private nfloat currentWidth;

        public TagsView(bool enableRemoveButton = true)
        {
            this.enableTagButton = enableRemoveButton;

            this.TagViews = new List<TagView>();
            this.RowViews = new List<UIStackView>();
        }

        public List<TagView> TagViews { get; set; }

        public List<UIStackView> RowViews { get; set; }

        public event EventHandler<object> TagSelected;
        public event EventHandler<object> RemoveButtonTapped;

        #region bulk styles
        private UIColor tagTextColor = UIColor.White;
        public UIColor TagTextColor
        {
            get
            {
                return this.tagTextColor;
            }
            set
            {
                this.tagTextColor = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.Label.TextColor = this.tagTextColor;
            }
        }

        private UIColor tagBackgroundColor = UIColor.DarkGray;
        public UIColor TagBackgroundColor
        {
            get
            {
                return this.tagBackgroundColor;
            }
            set
            {
                this.tagBackgroundColor = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.BackgroundColor = this.tagBackgroundColor;
            }
        }

        private UIFont textFont = UIFont.SystemFontOfSize(14f);
        public UIFont TextFont
        {
            get
            {
                return this.textFont;
            }
            set
            {
                this.textFont = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.Label.Font = this.textFont;

                this.RearrangeViews();
            }
        }

        private float cornerRadius = 5f;
        public float CornerRadius
        {
            get
            {
                return this.cornerRadius;
            }
            set
            {
                this.cornerRadius = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.Layer.CornerRadius = this.cornerRadius;
            }
        }

        private float borderWidth = 0f;
        public float BorderWidth
        {
            get
            {
                return this.borderWidth;
            }
            set
            {
                this.borderWidth = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.Layer.BorderWidth = this.borderWidth;
            }
        }

        private UIColor borderColor = UIColor.Clear;
        public UIColor BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.Layer.BorderColor = this.borderColor.CGColor;
            }
        }

        private float paddingY = 2f;
        public float PaddingY
        {
            get
            {
                return this.paddingY;
            }
            set
            {
                this.paddingY = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.PaddingY = this.paddingY;

                this.RearrangeViews();
            }
        }

        private float paddingX = 5f;
        public float PaddingX
        {
            get
            {
                return this.paddingX;
            }
            set
            {
                this.paddingX = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.PaddingX = this.paddingX;

                this.RearrangeViews();
            }
        }

        private float controlsDistance = 0f;
        public float ControlsDistance
        {
            get
            {
                return this.controlsDistance;
            }
            set
            {
                this.controlsDistance = value;
                foreach (var tagItem in this.TagViews)
                    tagItem.ControlsDistance = this.controlsDistance;

                this.RearrangeViews();
            }
        }
        #endregion

        #region margin metrics
        private float marginY = 2f;
        public float MarginY
        {
            get
            {
                return this.marginY;
            }
            set
            {
                this.marginY = value;

                this.RearrangeViews();
            }
        }

        private float marginX = 5f;
        public float MarginX
        {
            get
            {
                return this.marginX;
            }
            set
            {
                this.marginX = value;

                this.RearrangeViews();
            }
        }
        #endregion

        #region tag button
        private float tagButtonSize = 25f;
        public float TagButtonSize
        {
            get
            {
                return this.tagButtonSize;
            }
            set
            {
                this.tagButtonSize = value;
                foreach (var tagItem in this.TagViews)
                {
                    if (tagItem is TagButtonView tagButtonView)
                        tagButtonView.ButtonSize = this.tagButtonSize;
                }

                this.RearrangeViews();
            }
        }

        private UIColor tagButtonColor = UIColor.White;
        public UIColor TagButtonColor
        {
            get
            {
                return this.tagButtonColor;
            }
            set
            {
                this.tagButtonColor = value;
                foreach (var tagItem in this.TagViews)
                {
                    if (tagItem is TagButtonView tagButtonView)
                        tagButtonView.Button.TintColor = this.tagButtonColor;
                }
            }
        }

        private UIImage buttonIcon;
        public UIImage ButtonIcon
        {
            get
            {
                return this.buttonIcon;
            }
            set
            {
                this.buttonIcon = value;

                foreach (var tagItem in this.TagViews)
                {
                    if (tagItem is TagButtonView tagButtonView)
                        tagButtonView.Button.SetImage(this.buttonIcon, UIControlState.Normal);
                }
                this.RearrangeViews();
            }
        }

        #endregion

        private TagsAlignment alignment = TagsAlignment.Left;
        public TagsAlignment Alignment
        {
            get
            {
                return this.alignment;
            }
            set
            {
                this.alignment = value;

                this.RearrangeViews();
            }
        }

        public int RowQuantity { get; protected set; }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (this.currentWidth != this.Frame.Width)
                this.RearrangeViews();
        }

        protected void RearrangeViews()
        {
            this.currentWidth = this.Frame.Width;

            // clear current views
            var views = this.TagViews.Select(v => v as UIView).Concat(this.RowViews).ToList();

            foreach (var stackView in this.RowViews)
            {
                for (int i = 0; i < stackView.Subviews.Count(); i++)
                    stackView.RemoveArrangedSubview(stackView.Subviews[i]);
            }

            foreach (var view in views)
                view.RemoveFromSuperview();

            this.RowViews.Clear();

            // initialize values
            var currentRowCount = 0;
            UIStackView currentRowView = null;
            var currentRowTagCount = 0;
            var currentRowWidth = 0f;

            var stackAlignment = UIStackViewAlignment.Leading;
            switch (this.Alignment)
            {
                case TagsAlignment.Left:
                    stackAlignment = UIStackViewAlignment.Leading;
                    break;
                case TagsAlignment.Center:
                    stackAlignment = UIStackViewAlignment.Center;
                    break;
                case TagsAlignment.Right:
                    stackAlignment = UIStackViewAlignment.Trailing;
                    break;
            }

            for (int i = 0; i < this.TagViews.Count; i++)
            {
                var tagView = this.TagViews[i];

                var tagViewWidth = this.GetTagViewWidth(tagView);
                // if needed, instantiate a new row
                if (currentRowTagCount == 0 || currentRowWidth + tagViewWidth + this.MarginX * 2 > this.Frame.Width)
                {
                    currentRowCount += 1;
                    currentRowWidth = 0;
                    currentRowTagCount = 0;
                    currentRowView = new UIStackView { Distribution = UIStackViewDistribution.Fill, Alignment = stackAlignment, UserInteractionEnabled = true, Spacing = this.MarginX };

                    this.RowViews.Add(currentRowView);
                    this.AggregateSubviews(currentRowView);
                }

                // add tagView to row
                currentRowView.AddArrangedSubview(tagView);
                currentRowTagCount += 1;

                // update row's current width
                currentRowWidth += tagViewWidth + this.MarginX * 2;
            }

            this.RowQuantity = currentRowCount;

            //this.AddHackySubviewToStackViews();

            this.SetupRowConstraints();
        }

        //private void AddHackySubviewToStackViews()
        //{
        //    foreach (var stack in this.RowViews)
        //    {
        //        var dummyView = new UIView { BackgroundColor = UIColor.Clear };
        //        dummyView.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

        //        stack.AddArrangedSubview(dummyView);
        //    }
        //}

        private void SetupRowConstraints()
        {
            for (int i = 0; i < this.RowViews.Count; i++)
            {
                switch (this.Alignment)
                {
                    case TagsAlignment.Left:
                        this.AddConstraints(this.RowViews[i].AtLeftOf(this));
                        break;
                    case TagsAlignment.Center:
                        this.AddConstraints(this.RowViews[i].WithSameCenterX(this));
                        break;
                    case TagsAlignment.Right:
                        this.AddConstraints(this.RowViews[i].AtRightOf(this));
                        break;
                }

                if (i == 0)
                {
                    this.AddConstraints(
                        this.RowViews[i].AtTopOf(this)
                    );
                }
                else
                {
                    this.AddConstraints(
                        this.RowViews[i].Below(this.RowViews[i - 1], this.marginY)
                    );
                }

                if (i == this.RowViews.Count - 1)
                {
                    this.AddConstraints(
                        this.RowViews[i].AtBottomOf(this)
                    );
                }
            }

            this.SetNeedsUpdateConstraints();
        }

        private float GetTagViewWidth(TagView tagView)
        {
            var result = (float)tagView.Label.IntrinsicContentSize.Width + this.PaddingX * 2 + (this.enableTagButton ? ((TagButtonView)tagView).ButtonSize : 0);
            return result;
        }

        private TagView CreateNewTagView(string title, object source = null)
        {
            var tagView = this.enableTagButton
                              ? new TagButtonView(title, source ?? title)
                              : new TagView(title, source ?? title);

            tagView.Label.TextColor = this.TagTextColor;
            tagView.BackgroundColor = this.TagBackgroundColor;
            tagView.Layer.CornerRadius = this.CornerRadius;
            tagView.Layer.BorderWidth = this.BorderWidth;
            tagView.Layer.BorderColor = this.BorderColor.CGColor;
            tagView.PaddingX = this.PaddingX;
            tagView.PaddingY = this.PaddingY;
            tagView.ControlsDistance = this.ControlsDistance;

            if (this.TextFont != null)
                tagView.Label.Font = this.TextFont;

            if (this.enableTagButton)
            {
                var tagButtonView = (TagButtonView)tagView;
                tagButtonView.ButtonSize = this.TagButtonSize;
                tagButtonView.Button.TintColor = this.TagButtonColor;
                tagButtonView.Button.TouchUpInside += this.RemoveButtonOnTouchUpInside;

                if (this.ButtonIcon != null)
                    tagButtonView.Button.SetImage(this.ButtonIcon.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            }

            tagView.AddGestureRecognizer(new ObjectTapGestureRecognizer(tagView, this.TagTapGestureHandler));

            return tagView;
        }

        #region event handlers
        private void TagTapGestureHandler(UITapGestureRecognizer gesture)
        {
            var objectGesture = gesture as ObjectTapGestureRecognizer;

            if (objectGesture.Obj is TagView tagView)
            {
                this.TagSelected?.Invoke(this, tagView.TagSource);
            }
        }

        private void RemoveButtonOnTouchUpInside(object sender, EventArgs e)
        {
            var btn = (UIButton)sender;
            if (btn.Superview is TagView tagRemove)
            {
                this.RemoveButtonTapped?.Invoke(this, tagRemove.TagSource);
            }
        }
        #endregion

        #region tag public methods
        public void AddTag(string title, object tagSource = null)
        {
            this.AddTagView(this.CreateNewTagView(title, tagSource));
        }

        public void AddTags(IEnumerable<string> titles)
        {
            this.AddTags(titles.Select(t => new Tuple<string, object>(t, null)).ToList());
        }

        public void AddTags(IEnumerable<Tuple<string, object>> tagsTitleSource)
        {
            var tagViews = new List<TagView>();

            foreach (var tagTitleSource in tagsTitleSource)
                tagViews.Add(this.CreateNewTagView(tagTitleSource.Item1, tagTitleSource.Item2));

            this.AddTagViews(tagViews);
        }

        public void InsertTag(string title, int index, object tagSource = null)
        {
            this.InsertTagView(this.CreateNewTagView(title, tagSource), index);
        }

        public void RemoveTag(object tagSource)
        {
            for (int i = 0; i < this.TagViews.Count; i++)
            {
                if (this.TagViews[i].TagSource == tagSource)
                {
                    this.RemoveTagView(this.TagViews[i]);
                    return;
                }
            }
        }
        #endregion

        #region protected TagView methods
        protected void AddTagViews(IEnumerable<TagView> tagViews)
        {
            foreach (var tagView in tagViews)
            {
                this.TagViews.Add(tagView);
            }
            this.RearrangeViews();
        }

        protected void AddTagView(TagView tagView)
        {
            this.TagViews.Add(tagView);

            this.RearrangeViews();
        }

        protected void InsertTagView(TagView tagView, int index)
        {
            this.TagViews.Insert(index, tagView);

            this.RearrangeViews();
        }

        protected void RemoveTagView(TagView tagView)
        {
            tagView.RemoveFromSuperview();

            var index = this.TagViews.IndexOf(tagView);

            if (index >= 0)
                this.TagViews.RemoveAt(index);

            this.RearrangeViews();
        }
        #endregion

        public void RemoveAllTags()
        {
            var views = this.TagViews.Select(v => v as UIView);

            foreach (var view in views)
                view.RemoveFromSuperview();

            this.TagViews.Clear();

            this.RearrangeViews();
        }
    }
}
