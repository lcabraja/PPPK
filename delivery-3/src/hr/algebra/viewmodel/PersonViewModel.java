/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.viewmodel;

import hr.algebra.model.Person;
import javafx.beans.property.IntegerProperty;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleObjectProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;

/**
 *
 * @author daniel.bele
 */
public class PersonViewModel {
    
    private final Person person;

    public PersonViewModel(Person person) {
        if (person == null) {
            person = new Person(0, "", "", 0, "");
        }
        this.person = person;
    }

    public Person getPerson() {
        return person;
    }
    
    public StringProperty getFirstNameProperty() {
        return new SimpleStringProperty(person.getFirstName());
    }

    public StringProperty getLastNameProperty() {
        return new SimpleStringProperty(person.getLastName());
    }

    public StringProperty getEmailProperty() {
        return new SimpleStringProperty(person.getEmail());
    }

    public IntegerProperty getIdProperty() {
        return new SimpleIntegerProperty(person.getIDPerson());
    }

    public IntegerProperty getAgeProperty() {
        return new SimpleIntegerProperty(person.getAge());
    }
   
    public ObjectProperty<byte[]> getPictureProperty() {
        return new SimpleObjectProperty<>(person.getPicture());
    }
}
