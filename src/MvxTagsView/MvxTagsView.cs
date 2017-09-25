using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.WeakSubscription;
using TagsView;

namespace MvxTagsView
{
    public class MvxTagsView<TSourceItem> : TagsView.TagListView, IMvxBindable
    {
        private Func<TSourceItem, string> sourceItemToStringFunc;

        private IEnumerable<TSourceItem> _itemsSource;
        private IDisposable _subscription;

        public MvxTagsView(Func<TSourceItem, string> sourceItemToStringFunc, bool enableRemoveButton = true)
            : base(enableRemoveButton)
        {
            this.CreateBindingContext();

            this.sourceItemToStringFunc = sourceItemToStringFunc;

            this.TagSelected += this.Handle_TagSelected;
            this.TagButtonTapped += this.Handle_RemoveButtonTapped;

            if (this.BindingContext is MvxTaskBasedBindingContext context)
            {
                context.RunSynchronously = true;
            }
        }

        public IMvxBindingContext BindingContext { get; set; }

        [MvxSetToNullAfterBinding]
        public object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BindingContext.ClearAllBindings();

                _subscription?.Dispose();
                _subscription = null;

                this.TagSelected -= this.Handle_TagSelected;
                this.TagButtonTapped -= this.Handle_RemoveButtonTapped;
            }
            base.Dispose(disposing);
        }

        [MvxSetToNullAfterBinding]
        public virtual IEnumerable<TSourceItem> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                if (Object.ReferenceEquals(_itemsSource, value))
                    return;

                _subscription?.Dispose();
                _subscription = null;

                _itemsSource = value;

                if (_itemsSource is INotifyCollectionChanged collectionChanged)
                {
                    _subscription = collectionChanged.WeakSubscribe(this.CollectionChangedOnCollectionChanged);
                }

                this.RemoveAllTags();

                this.AddTagsFromItemsSource();
            }
        }

        public Action<object> AfterRemoveCommandExecutedAction { get; set; }

        public IMvxCommand<TSourceItem> TagSelectedCommand { get; set; }

        public IMvxCommand<TSourceItem> RemoveButtonCommand { get; set; }

        protected virtual void CollectionChangedOnCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in args.NewItems)
                        this.AddTag(this.sourceItemToStringFunc.Invoke((TSourceItem)newItem), newItem);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (int i = 0; i < args.OldItems.Count; i++)
                        this.RemoveTag((TSourceItem)args.OldItems[i]);
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (args.NewItems.Count != 1 && args.OldItems.Count != 1)
                    {
                        MvxTrace.Warning("BindableTagsView: Move action called with more than one movement!");
                        break;
                    }

                    this.RemoveTag((TSourceItem)args.OldItems[0]);
                    this.InsertTag(this.sourceItemToStringFunc.Invoke((TSourceItem)args.NewItems[0]), args.NewStartingIndex, (TSourceItem)args.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (args.NewItems.Count != args.OldItems.Count)
                        break;

                    for (int i = 0; i < args.OldItems.Count; i++)
                    {
                        this.RemoveTag((TSourceItem)args.OldItems[i]);
                        this.InsertTag(this.sourceItemToStringFunc.Invoke((TSourceItem)args.NewItems[i]), args.NewStartingIndex, (TSourceItem)args.NewItems[i]);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.RearrangeViews();
                    break;
            }
        }

        private void Handle_TagSelected(object sender, object e)
        {
            if (this.TagSelectedCommand == null)
                return;

            var param = (TSourceItem)e;
            if (this.TagSelectedCommand.CanExecute(param))
                this.TagSelectedCommand.Execute(param);
        }

        private void Handle_RemoveButtonTapped(object sender, object e)
        {
            if (this.RemoveButtonCommand == null)
                return;

            var param = (TSourceItem)e;
            if (this.RemoveButtonCommand.CanExecute(param))
            {
                this.RemoveButtonCommand.Execute(param);
            }
        }

        private void AddTagsFromItemsSource()
        {
            foreach (var item in this.ItemsSource)
                this.AddTag(this.sourceItemToStringFunc.Invoke(item), item);
        }
    }
}