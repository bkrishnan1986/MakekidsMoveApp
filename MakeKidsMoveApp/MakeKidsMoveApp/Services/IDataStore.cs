using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeKidsMoveApp.Services
{
    public interface IDataStore<T>
    {
        Task<TOutput> AddItemAsync<TInput, TOutput>(TInput item);
        Task<TOutput> UpdateItemAsync<TInput, TOutput>(TInput item);
        Task<TOutput> GetItemAsync<TOutput>();
        //Task<bool> UpdateItemAsync(T item);
        //Task<bool> DeleteItemAsync(string id);
        //Task<T> GetItemAsync(string id);
        //Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
