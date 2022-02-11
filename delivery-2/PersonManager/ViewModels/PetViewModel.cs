using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Dal;
using Zadatak.Models;

namespace Zadatak.ViewModels
{
    public class PetViewModel
    {
        public ObservableCollection<Pet> Pets { get; }
        public PetViewModel()
        {
            Pets = new ObservableCollection<Pet>(RepositoryFactory.GetRepository().GetPets());
            Pets.CollectionChanged += People_CollectionChanged;
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddPet(Pets[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeletePet(e.OldItems.OfType<Pet>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdatePet(e.NewItems.OfType<Pet>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Pet pet) => Pets[Pets.IndexOf(pet)] = pet;
    }
}
