using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MakeKidsMoveApp.ErrorHandler;
using MakeKidsMoveApp.Models;
using MakeKidsMoveApp.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MakeKidsMoveApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        string _baseUrl = "";
        public MockDataStore()
        {
            _baseUrl = DataServiceUrls.BaseUrl;
        }

        public async Task<TOutput> AddItemAsync<TInput, TOutput>(TInput item)
        {
            try
            { 
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {


                    string recordObject = JsonConvert.SerializeObject(item);
                    HttpContent content = new StringContent(recordObject, Encoding.UTF8, "application/json");
                    using (HttpClient client = new HttpClient())
                    {
                        var url = Preferences.Get("executeurl", null);
                        HttpResponseMessage response = await client.PostAsync(_baseUrl + url, content);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<TOutput>(apiResponse);
                        }
                        else
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            throw new Exception(apiResponse);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Please check the internet connectivity", "OK");
                    throw new Exception("network error has occured.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TOutput> GetItemAsync<TOutput>()
        {
            try
            {
                //  IsDefaultValuesOk();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var url = Preferences.Get("executeurl", null);

                        var response = await client.GetAsync(this._baseUrl + url);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<TOutput>(json);
                        }
                        else
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();

                            //JObject
                            var jobjet = JObject.Parse(apiResponse);
                            var errorMsg = Convert.ToString(jobjet.SelectToken("$.ExceptionMessage"));
                            //sample:  "Error Type: DatabaseError, Message:Sequence contains no elements."
                            var index = errorMsg.IndexOf("Message:");
                            string userFriendlyMessage = "";
                            if (index > 0)
                            {
                                userFriendlyMessage = errorMsg.Substring(index + 8); ;
                            }

                            throw new Exception(userFriendlyMessage);
                        }
                    }
                }
                else
                {
                    //  await Application.Current.MainPage.DisplayAlert("Message", "Please check the internet connectivity", "OK");
                    throw new Exception("network error has occured.");
                }
            }
            catch (Exception ex)
            {
                // await Application.Current.MainPage.DisplayAlert("Message", "Please check the internet connectivity", "OK");
                //await Application.Current.MainPage.DisplayAlert("Error", "Error Message" + ex.Message, "OK");
                throw ex;
            }
        }
        public async Task<TOutput> UpdateItemAsync<TInput, TOutput>(TInput item)
        {
            {
                TOutput result = default(TOutput);

                try
                {
                    string recordObject = "";

                    if (item != null)
                    {
                        recordObject = JsonConvert.SerializeObject(item);
                    }

                    HttpContent content = new StringContent(recordObject, Encoding.UTF8, "application/json");

                    using (HttpClient client = new HttpClient())
                    {
                        var url = Preferences.Get("executeurl", null);
                        HttpResponseMessage response = await client.PutAsync(_baseUrl + url, content);


                        var apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var tmp = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<TOutput>(tmp);
                        }
                        else
                        {
                            ApiException apiException = JsonConvert.DeserializeObject<ApiException>(apiResponse);
                            StringBuilder strMsg = new StringBuilder();
                            if (apiException.ErrorType == ErrorMessageType.FieldValidation)
                            {

                                foreach (var value in apiException.DetailErrorDetails)
                                {
                                    foreach (var error in value.Errors)
                                    {
                                        strMsg.Append(error);
                                    }
                                }
                                throw new ApiException(strMsg.ToString());
                            }
                            if (apiException.ErrorType == ErrorMessageType.RecordExists)
                            {
                                throw new ApiException(apiException.ErrorMessage);
                            }
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    Trace.TraceError("Failed to post record.", "API Failed");
                    throw;
                }
                catch (ApiException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }
    }
}