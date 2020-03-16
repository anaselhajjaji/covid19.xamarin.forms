using Covid19.Models;
using Covid19.Services;
using Covid19.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Covid19.ViewModels
{
    public class CasesViewModel : BindableObject
    {
        ObservableCollection<CountryCaseDto> _cases;
        
        // Commands
        public ICommand RefreshCommand { get; }
        public Command<CountryCaseDto> FavoriteCommand { get; set; }

        public CasesViewModel()
        {
            LoadCases();
            RefreshCommand = new Command(LoadCases);

            FavoriteCommand = new Command<CountryCaseDto>(async (c) =>
            {
                MessagingCenter.Send(c, "Favorited");

                if (c.Favorite)
                {
                    await CoronaService.Instance.RemoveFromFavorites(c.Country);
                }
                else
                {
                    await CoronaService.Instance.AddToFavorites(c.Country);
                }

                c.Favorite = !c.Favorite;

                // This is an example for adding specific code
                DependencyService.Get<IMessage>().ShortAlert((c.Favorite ? "Added to" : "Removed from") + " favorites.");
            });
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        async void LoadCases()
        {
            IsRefreshing = true;
            Cases = new ObservableCollection<CountryCaseDto>(await CoronaService.Instance.GetCountryCases());

            // Stop refreshing
            IsRefreshing = false;
        }

        public ObservableCollection<CountryCaseDto> Cases
        {
            get { return _cases; }
            set
            {
                _cases = value;
                OnPropertyChanged();
            }
        }
    }
}