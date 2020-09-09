using MakeKidsMoveApp.Models;
using MakeKidsMoveApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MakeKidsMoveApp.Views
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _viewModel;
        public Login LoginModel { get; set; }
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LoginViewModel();

        }
    }
}