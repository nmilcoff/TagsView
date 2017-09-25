using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace MvxTagsView_Sample.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        public FirstViewModel()
        {
            this.SimpleTagSelectedCommand = new MvxCommand<string>(item => this.SimpleSource.Add($"{item} copied!"));
            this.SimpleTagButtonTappedCommand = new MvxCommand<string>(item => this.SimpleSource.Remove(item));
        }

        string hello = "Hello MvvmCross";
        public string Hello
        {
            get { return hello; }
            set { SetProperty(ref hello, value); }
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            for (int i = 0; i < 10; i++)
            {
                this.SimpleSource.Add($"Item number {i}");
            }

        }

        public MvxObservableCollection<string> SimpleSource { get; set; } = new MvxObservableCollection<string>();

        public IMvxCommand<string> SimpleTagSelectedCommand { get; private set; }

        public IMvxCommand<string> SimpleTagButtonTappedCommand { get; private set; }
    }
}
