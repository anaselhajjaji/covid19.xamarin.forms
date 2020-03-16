using Covid19.Models;
using Covid19.ViewModels;
using Xamarin.Forms;

namespace Covid19.Views
{
    public partial class CasesView : ContentPage
    {
        public CasesView()
        {
            InitializeComponent();
            BindingContext = new CasesViewModel();

            // Catch the message here and show the alert
            MessagingCenter.Subscribe<CountryCaseDto>(this, "Favorited", (c) =>
            {
                // here just to showcase the data exchange using messaging center
            });
        }
    }
}