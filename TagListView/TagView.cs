using System.Linq;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace TagListView
{
    public class TagView : UIView
    {
        protected bool didSetupConstraints;

        public object TagSource { get; set; }

        public UILabel Label { get; set; } = new UILabel();

        private float paddingY;
        public float PaddingY
        {
            get => this.paddingY;
            set
            {
                this.paddingY = value;
                if (this.didSetupConstraints)
                {
                    while (this.Constraints.Any())
                        this.RemoveConstraint(this.Constraints.First());

                    this.didSetupConstraints = false;
                    this.SetNeedsUpdateConstraints();
                }
            }
        }

        private float paddingX;
        public float PaddingX
        {
            get => this.paddingX;
            set
            {
                this.paddingX = value;
                if (this.didSetupConstraints)
                {
                    while (this.Constraints.Any())
                        this.RemoveConstraint(this.Constraints.First());

                    this.didSetupConstraints = false;
                    this.SetNeedsUpdateConstraints();
                }
            }
        }

        public float ControlsDistance { get; set; }

        public TagView()
        {
        }

        public TagView(string title, object tagSource)
        {
            this.TagSource = tagSource;

            this.Label.Text = title;

            this.Initialize();
        }

        protected virtual void Initialize()
        {
            this.UserInteractionEnabled = true;

            this.TranslatesAutoresizingMaskIntoConstraints = false;

            this.AggregateSubviews(this.Label);

            this.SetNeedsUpdateConstraints();
        }

        public override void UpdateConstraints()
        {
            if (!this.didSetupConstraints)
            {
                this.AddConstraints(
                    this.Label.AtLeftOf(this, this.PaddingX),
                    this.Label.AtTopOf(this, this.PaddingY).SetPriority(500),
                    this.Label.AtBottomOf(this, this.PaddingY).SetPriority(500)
                );

                this.AddRightConstraint();

                this.didSetupConstraints = true;
            }
            base.UpdateConstraints();
        }

        protected virtual void AddRightConstraint()
        {
            this.AddConstraints(
                this.Label.AtRightOf(this, this.PaddingX)
            );
        }
    }
}
