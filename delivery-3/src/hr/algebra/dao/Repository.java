/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao;

import hr.algebra.model.Person;
import hr.algebra.model.Pet;
import java.util.List;

/**
 *
 * @author daniel.bele
 */
public interface Repository {
    
    // people
    int addPerson(Person data) throws Exception;
    void updatePerson(Person person) throws Exception;
    void deletePerson(Person person) throws Exception;
    Person getPerson(int idPerson) throws Exception;
    List<Person> getPeople() throws Exception;
    // pets
    int addPet(Pet data) throws Exception;
    void updatePet(Pet pet) throws Exception;
    void deletePet(Pet pet) throws Exception;
    Pet getPet(int idPet) throws Exception;
    List<Pet> getPets() throws Exception;
    
    default void release() throws Exception{};
}

