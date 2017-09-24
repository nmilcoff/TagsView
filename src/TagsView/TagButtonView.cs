using System;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace TagsView
{
    public class TagButtonView : TagView
    {
        public UIButton Button { get; set; } = new UIButton();

        private float buttonSize;
        public float ButtonSize
        {
            get => this.buttonSize;
            set
            {
                this.buttonSize = value;
                if (this.didSetupConstraints)
                {
                    while (this.Constraints.Any())
                        this.RemoveConstraint(this.Constraints.First());

                    this.didSetupConstraints = false;
                    this.SetNeedsUpdateConstraints();
                }
            }
        }

        public TagButtonView()
        {
        }

        public TagButtonView(string title, object tagSource)
                : base(title, tagSource)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.Button.SetImage(UIImage.FromBundle("ic_removetag").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            this.Button.VerticalAlignment = UIControlContentVerticalAlignment.Fill;
            this.Button.HorizontalAlignment = UIControlContentHorizontalAlignment.Fill;

            this.AggregateSubviews(this.Button);
        }

        protected override void AddRightConstraint()
        {
            this.AddConstraints(
                    this.Button.Width().EqualTo(this.ButtonSize),
                    this.Button.Height().EqualTo(this.ButtonSize),

                    this.Button.AtTopOf(this, PaddingY).SetPriority(750),
                    this.Button.AtBottomOf(this, PaddingY).SetPriority(750),

                    this.Button.ToRightOf(this.Label, this.ControlsDistance),
                    this.Button.WithSameCenterY(this.Label).SetPriority(750),
                    this.Button.AtRightOf(this, this.PaddingX)
                );
        }
    }
}
