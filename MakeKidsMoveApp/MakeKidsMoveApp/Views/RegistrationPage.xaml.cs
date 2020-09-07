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
    public partial class RegistrationPage : ContentPage
    {
        RegistrationViewModel _viewModel;
        public Registration RegistrationModel { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();
            Task.Run(() =>
            {
                _viewModel = new RegistrationViewModel(this.Navigation);
                _viewModel.Navigation = Navigation;

                BindingContext = _viewModel;
            }).Wait();
        }
    }
}