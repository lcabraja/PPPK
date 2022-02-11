/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.viewmodel;

import hr.algebra.model.Pet;
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
public class PetViewModel {
    
    private final Pet pet;

    public PetViewModel(Pet pet) {
        if (pet == null) {
            pet = new Pet(0, "", 0, null);
        }
        this.pet = pet;
    }

    public Pet getPet() {
        return pet;
    }
    
    public StringProperty getNameProperty() {
        return new SimpleStringProperty(pet.getName());
    }

    public StringProperty getOwnerNameProperty() {
        return new SimpleStringProperty(pet.getOwner().getFirstName() + " " + pet.getOwner().getLastName());
    }

    public IntegerProperty getIdProperty() {
        return new SimpleIntegerProperty(pet.getIDPet());
    }

    public IntegerProperty getAgeProperty() {
        return new SimpleIntegerProperty(pet.getAge());
    }
   
    public ObjectProperty<byte[]> getPictureProperty() {
        return new SimpleObjectProperty<>(pet.getPicture());
    }
}
