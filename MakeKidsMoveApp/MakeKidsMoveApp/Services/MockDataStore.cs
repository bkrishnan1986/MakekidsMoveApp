using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MakeKidsMoveApp.Models;
using MakeKidsMoveApp.Resources;
using Newtonsoft.Json;
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

        //public async Task<bool> UpdateItemAsync(Item item)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(string id)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
        //    items.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        //public async Task<Item> GetItemAsync(string id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}

        //public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}

        //public Task<bool> AddItemAsync(Item item)
        //{
        //    throw new NotImplementedException();
        //}
    }
}