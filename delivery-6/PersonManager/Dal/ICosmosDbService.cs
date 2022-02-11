using PersonManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonManager.Dal
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Person>> GetPeopleAsync(string queryString);
        Task<Person> GetPersonAsync(string id);
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(string id);
    }
}