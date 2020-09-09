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
    public partial class RegistrationPage : ContentPage
    {
        RegistrationViewModel _viewModel;
        public Registration RegistrationModel { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RegistrationViewModel();

        }
    }
}