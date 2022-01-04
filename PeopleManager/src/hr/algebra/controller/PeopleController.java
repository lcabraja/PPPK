/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dao.RepositoryFactory;
import hr.algebra.viewmodel.PersonViewModel;
import java.net.URL;
import java.util.Map;
import java.util.ResourceBundle;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.image.ImageView;

/**
 * FXML Controller class
 *
 * @author daniel.bele
 */
public class PeopleController implements Initializable {
    
    private Map<TextField, Label> validationMap;
    
    private final ObservableList<PersonViewModel> list = FXCollections.observableArrayList();
    
    private PersonViewModel selectedPersonViewModel;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableColumn<PersonViewModel, String> tcFirstName, tcLastName, tcEmail;
   
    @FXML
    private TableColumn<PersonViewModel, Integer> tcAge;
   
    @FXML
    private Tab tabEdit;
    @FXML
    private ImageView ivPerson;
    @FXML
    private TextField tfFirstName;
    @FXML
    private Label lbFirstName;
    @FXML
    private TextField tfLastName;
    @FXML
    private Label lbLastName;
    @FXML
    private TextField tfAge;
    @FXML
    private Label lbAge;
    @FXML
    private TextField tfEmail;
    @FXML
    private Label lbEmail;
    @FXML
    private Label lbIcon;
    @FXML
    private TableView<PersonViewModel> tvPeople;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
       initValidation();
       initPeople();
       initTable();
       addIntegerMask(tfAge);
       setListeners();
    }    

    @FXML
    private void edit(ActionEvent event) {
    }

    @FXML
    private void delete(ActionEvent event) {
    }

    @FXML
    private void upload(ActionEvent event) {
    }

    @FXML
    private void commit(ActionEvent event) {
    }

    private void initValidation() {
    }

    private void initPeople() {
        try {
            RepositoryFactory.getRepository().getPeople().forEach(person -> list.add(new PersonViewModel(person)));
        } catch (Exception ex) {
            Logger.getLogger(PeopleController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private void initTable() {
        tcFirstName.setCellValueFactory(person -> person.getValue().getFirstNameProperty());
        tcLastName.setCellValueFactory(person -> person.getValue().getLastNameProperty());
        tcAge.setCellValueFactory(person -> person.getValue().getAgeProperty().asObject());
        tcEmail.setCellValueFactory(person -> person.getValue().getEmailProperty());
        tvPeople.setItems(list);
    }

    private void addIntegerMask(TextField tfAge) {
    }

    private void setListeners() {
    }
    
}
