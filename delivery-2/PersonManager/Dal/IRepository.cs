using System.Collections.Generic;
using Zadatak.Models;

namespace Zadatak.Dal
{
    public interface IRepository
    {
        #region people
        void AddPerson(Person person);
        void DeletePerson(Person person);
        IList<Person> GetPeople();
        Person GetPerson(int idPerson);
        void UpdatePerson(Person person);
        #endregion people
        #region pets
        void AddPet(Pet pet);
        void DeletePet(Pet pet);
        IList<Pet> GetPets();
        Pet GetPet(int idPet);
        void UpdatePet(Pet pet);
        #endregion pets
    }
}