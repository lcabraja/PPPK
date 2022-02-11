/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.model;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Lob;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author daniel.bele
 */
@Entity
@Table(name = "Pet")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Pet.findAll", query = "SELECT p FROM Pet p"), 
    @NamedQuery(name = "Pet.findByIDPet", query = "SELECT p FROM Pet p WHERE p.idPet = :idPet"), 
    @NamedQuery(name = "Pet.findByName", query = "SELECT p FROM Pet p WHERE p.name = :name"), 
    @NamedQuery(name = "Pet.findByAge", query = "SELECT p FROM Pet p WHERE p.age = :age")
})
public class Pet implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDPet")
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer idPet;
    
    @Basic(optional = false)
    @Column(name = "Name")
    private String name;
    
    @Basic(optional = false)
    @Column(name = "Age")
    private int age;
    
    @JoinColumn(name = "OwnerID", referencedColumnName = "IDPerson")
    @ManyToOne(optional = false, cascade = CascadeType.MERGE)
    private Person owner;
    
    @Lob
    @Column(name = "Picture")
    private byte[] picture;

    public Pet() {
    }
    
    public Pet(Pet data) {
        updateDetails(data);
    }
    
    

    public Pet(Integer iDPet) {
        this.idPet = iDPet;
    }

    public Pet(Integer idPet, String name, int age, Person owner) {
        this.idPet = idPet;
        this.name = name;
        this.age = age;
        this.owner = owner;
    }

    public Integer getIDPet() {
        return idPet;
    }

    public void setIDPet(Integer iDPet) {
        this.idPet = iDPet;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public Person getOwner() {
        return owner;
    }

    public void setOwner(Person owner) {
        this.owner = owner;
    }

    public byte[] getPicture() {
        return picture;
    }

    public void setPicture(byte[] picture) {
        this.picture = picture;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (idPet != null ? idPet.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Pet)) {
            return false;
        }
        Pet other = (Pet) object;
        if ((this.idPet == null && other.idPet != null) || (this.idPet != null && !this.idPet.equals(other.idPet))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.Pet[ iDPet=" + idPet + " ]";
    }

    public void updateDetails(Pet data) {
        this.name = data.getName();
        this.age = data.getAge();
        this.owner = data.getOwner();
        this.picture = data.getPicture();
    }
    
}
