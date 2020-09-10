using MakeKidsMoveApp.Models;
using MakeKidsMoveApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MakeKidsMoveApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildListPage : ContentPage
    {
        RegistrationViewModel _viewModel;
        public Registration RegistrationModel { get; set; }
        public ChildListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RegistrationViewModel();
        }
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Registration)layout.BindingContext;
            await Navigation.PushAsync(new ProfilePage(new RegistrationViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProfilePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (_viewModel.item.Count == 0)
                _viewModel.IsBusy = true;
        }
    }
}