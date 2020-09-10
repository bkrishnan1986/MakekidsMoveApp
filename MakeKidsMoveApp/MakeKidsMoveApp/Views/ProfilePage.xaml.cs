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
    public partial class ProfilePage : ContentPage
    {
        RegistrationViewModel viewModel;
        public ProfilePage(RegistrationViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ProfilePage()
        {
            InitializeComponent();

            var item = new Registration
            {
                FirstName  = viewModel.RegistrationModel.FirstName,
                Age=viewModel.RegistrationModel.Age,
                NickName=viewModel.RegistrationModel.NickName,
                UserName=viewModel.RegistrationModel.UserName
            };
            viewModel = new RegistrationViewModel(item);
            BindingContext = viewModel;
        }
    }
}