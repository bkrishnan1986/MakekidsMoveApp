using MakeKidsMoveApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MakeKidsMoveApp.ViewModels;
using MakeKidsMoveApp.Views;
using MakeKidsMoveApp.Commands;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using MakeKidsMoveApp.Resources;

namespace MakeKidsMoveApp.ViewModels
{
   public class LoginViewModel : BaseViewModel<Login>
    {
        private INavigation _navigation;
        internal INavigation Navigation;
        public IAsyncCommand Login { get; private set; }
        public Login LoginModel { get; set; }
        public LoginViewModel()
        {
            try
            {
                LoginModel = new Login();
                Login = new AsyncCommand(ExecuteSubmitAsync);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task ExecuteSubmitAsync()
        {
            try
            {
                IsBusy = true;
                if (string.IsNullOrEmpty(LoginModel.UserName))
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Username/Email is required", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(LoginModel.Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Password is required", "OK");
                    return;
                }

                if (!string.IsNullOrEmpty(LoginModel.UserName))
                {
                    Regex rgx = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                         + "@"
                                         + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
                    if (!rgx.IsMatch(LoginModel.UserName))
                    {
                        await Application.Current.MainPage.DisplayAlert("Failed", "Please enter valid email id", "OK");
                        return;
                    }
                }
                //return;
                IsBusy = true;
                Preferences.Set("executeurl", DataServiceUrls.Login);
                var result = await DataStore.AddItemAsync<Login, JObject>(LoginModel);
                if (result.Count > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Successfully Login", "ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Login Failed", "ok");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", ex.Message, cancel: "Cancel");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
