/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao.sql;

import hr.algebra.dao.Repository;
import hr.algebra.model.Person;
import hr.algebra.model.Pet;
import java.util.List;
import javax.persistence.EntityManager;

public class HibernateRepository implements Repository {

    @Override
    public int addPerson(Person data) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            Person person = new Person(data);
            em.persist(person);
            em.getTransaction().commit();
            return person.getIDPerson();
        }
    }

    @Override
    public void updatePerson(Person person) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            em.find(Person.class, person.getIDPerson()).updateDetails(person);
            em.getTransaction().commit();
        }
    }

    @Override
    public void deletePerson(Person person) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            em.remove(em.contains(person) ? person : em.merge(person));
            em.getTransaction().commit();
        }
    }

    @Override
    public Person getPerson(int idPerson) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            return em.find(Person.class, idPerson);
        }
    }

    @Override
    public List<Person> getPeople() throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            return em.createNamedQuery(HibernateFactory.SELECT_ALL_PEOPLE).getResultList();
        }
    }

    @Override
    public void release() {
        HibernateFactory.release();
    }

    @Override
    public int addPet(Pet data) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            Pet pet = new Pet(data);
            em.persist(pet);
            em.getTransaction().commit();
            return pet.getIDPet();
        }
    }

    @Override
    public void updatePet(Pet pet) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            em.find(Pet.class, pet.getIDPet()).updateDetails(pet);
            em.getTransaction().commit();
        }
    }

    @Override
    public void deletePet(Pet pet) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            em.remove(em.contains(pet) ? pet : em.merge(pet));
            em.getTransaction().commit();
        }
    }

    @Override
    public Pet getPet(int idPet) throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            return em.find(Pet.class, idPet);
        }
    }

    @Override
    public List<Pet> getPets() throws Exception {
        try (EntityManagerWrapper wrapper = HibernateFactory.getEntityManger()) {
            EntityManager em = wrapper.get();
            return em.createNamedQuery(HibernateFactory.SELECT_ALL_PETS).getResultList();
        }
    }
}
