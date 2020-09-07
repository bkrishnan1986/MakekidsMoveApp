﻿using MakeKidsMoveApp.Models;
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
   public class RegistrationViewModel:BaseViewModel<Registration>
    {
        private INavigation _navigation;
        internal INavigation Navigation;

        public IAsyncCommand Register { get; private set; }
        public Registration RegistrationModel { get; set; }
        public RegistrationViewModel(INavigation navigation)
        {
            try
            {
                this._navigation = navigation;
                Register = new AsyncCommand(ExecuteSubmitAsync);
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
                RegistrationModel.UserType = 1;
                RegistrationModel.UserTypeDesc = "parent";
                RegistrationModel.ParentId = 0;
                RegistrationModel.CreatedAt = DateTime.Now;
                RegistrationModel.CreatedBy = "Admin";
                if (string.IsNullOrEmpty(RegistrationModel.FirstName))
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Name is required", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(RegistrationModel.UserName))
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Username/Email is required", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(RegistrationModel.Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Password is required", "OK");
                    return;
                }
                if (!string.IsNullOrEmpty(RegistrationModel.ConfirmPassword))
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Confirm Password is required", "OK");
                    return;

                }
                if (!string.IsNullOrEmpty(RegistrationModel.FirstName))
                {
                    Regex rgx = new Regex(@"^([A-Za-z]{0,19})+$");
                    if (!rgx.IsMatch(RegistrationModel.FirstName))
                    {
                        await Application.Current.MainPage.DisplayAlert("Failed", "Enter a Valid Name", "OK");
                        return;
                    }
                }  
                if (!string.IsNullOrEmpty(RegistrationModel.UserName))
                {
                    Regex rgx = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                         + "@"
                                         + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
                    if (!rgx.IsMatch(RegistrationModel.UserName))
                    {
                        await Application.Current.MainPage.DisplayAlert("Failed", "Please enter valid email id", "OK");
                        return;
                    }
                }
                //return;
                IsBusy = true;
                Preferences.Set("executeurl", DataServiceUrls.SaveParentRegistrationDetail);
                var result = await DataStore.AddItemAsync<Registration, JObject>(RegistrationModel);
                if(result.Count>0)
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Successfully Registered", "ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Registration Failed", "ok");
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
