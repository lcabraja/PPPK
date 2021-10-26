using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.DAL
{
    public interface IRepository
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        IList<Person> GetPeople();
        Person GetPerson(int idPerson);
        void UpdatePerson(Person person);
    }
}
