using Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Dao
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Item>> GetItemsAsync(string queryString);
        Task<Item> GetItemAsync(string id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Item item);

    }
}
