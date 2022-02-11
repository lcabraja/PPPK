/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.PeopleApplication;
import hr.algebra.dao.RepositoryFactory;
import hr.algebra.model.Person;
import hr.algebra.utilities.FileUtils;
import hr.algebra.utilities.ImageUtils;
import hr.algebra.utilities.ValidationUtils;
import hr.algebra.viewmodel.PersonViewModel;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.util.AbstractMap;
import java.util.Map;
import java.util.ResourceBundle;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.function.UnaryOperator;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
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
import javafx.scene.control.TextFormatter;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.util.converter.IntegerStringConverter;

/**
 * FXML Controller class
 *
 * @author daniel.bele
 */
public class PeopleController implements Initializable {

    private Map<TextField, Label> validationMap;

    private final ObservableList<PersonViewModel> list = FXCollections.observableArrayList();

    private PersonViewModel selectedPersonViewModel;

    private boolean edit = false;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableView<PersonViewModel> tvPeople;
    @FXML
    private TableColumn<PersonViewModel, String> tcFirstName, tcLastName, tcEmail;
    @FXML
    private TableColumn<PersonViewModel, Integer> tcAge;
    @FXML
    private Tab tabEdit;
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
    private ImageView ivPerson;
    @FXML
    private Label lbIcon;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initPeople();
        initTable();
        addIntegerMask(tfAge);
        setupListeners();
    }

    @FXML
    private void go(ActionEvent event) {
        PeopleApplication.toggleStage();
    }

    @FXML
    private void edit(ActionEvent event) {
        if (tvPeople.getSelectionModel().getSelectedItem() != null) {
            edit = true;
            bindPerson(tvPeople.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
        }
    }

    private void bindPerson(PersonViewModel viewModel) {
        resetForm();
        selectedPersonViewModel = viewModel != null ? viewModel : new PersonViewModel(null);
        tfFirstName.setText(selectedPersonViewModel.getPerson().getFirstName());
        tfLastName.setText(selectedPersonViewModel.getPerson().getLastName());
        tfAge.setText(String.valueOf(selectedPersonViewModel.getPerson().getAge()));
        tfEmail.setText(selectedPersonViewModel.getPerson().getEmail());
        ivPerson.setImage(
                selectedPersonViewModel.getPerson().getPicture() != null
                ? new Image(new ByteArrayInputStream(selectedPersonViewModel.getPerson().getPicture()))
                : new Image(new File("src/assets/no_image.png").toURI().toString())
        );
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvPeople.getSelectionModel().getSelectedItem() != null) {
            list.remove(tvPeople.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void upload(ActionEvent event) {
        File file = FileUtils.uploadFileDialog(ivPerson.getScene().getWindow(), "jpg", "jpeg", "png");
        if (file != null) {
            Image image = new Image(file.toURI().toString());
            try {
                String ext = file.getName().substring(file.getName().lastIndexOf(".") + 1);
                setImage(selectedPersonViewModel.getPerson(),
                        image, ext,
                        ivPerson);
            } catch (IOException ex) {
                Logger.getLogger(this.getClass().getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    private void setupListeners() {
        tabEdit.setOnSelectionChanged(event -> {
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEdit) && !edit) {
                bindPerson(null);
            } else {
                edit = false;
            }
        });
        list.addListener((ListChangeListener.Change<? extends PersonViewModel> change) -> {
            if (change.next()) {
                if (change.wasRemoved()) {
                    change.getRemoved().forEach(pvm -> {
                        try {
                            RepositoryFactory.getRepository().deletePerson(pvm.getPerson());
                        } catch (Exception ex) {
                            Logger.getLogger(this.getClass().getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                } else if (change.wasAdded()) {
                    change.getAddedSubList().forEach(pvm -> {
                        try {
                            int idPerson = RepositoryFactory.getRepository().addPerson(pvm.getPerson());
                            pvm.getPerson().setIDPerson(idPerson);
                        } catch (Exception ex) {
                            Logger.getLogger(this.getClass().getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                }
            }
        });
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            try {
                setPerson(selectedPersonViewModel.getPerson());

                if (selectedPersonViewModel.getPerson().getIDPerson() == 0) {
                    list.add(selectedPersonViewModel);
                } else {
                    RepositoryFactory.getRepository().updatePerson(selectedPersonViewModel.getPerson());
                    tvPeople.refresh();
                }
            } catch (Exception ex) {
                Logger.getLogger(PeopleController.class.getName()).log(Level.SEVERE, null, ex);
            }
            selectedPersonViewModel = null;
            tpContent.getSelectionModel().select(tabList);
            resetForm();
        }
    }

    private void setImage(Person player, Image image, String ext, ImageView ivImage) throws IOException {

        player.setPicture(ImageUtils.imageToByteArray(image, ext));
        ivImage.setImage(image);

    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));
        lbIcon.setVisible(false);
    }

    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstName, lbFirstName),
                new AbstractMap.SimpleImmutableEntry<>(tfLastName, lbLastName),
                new AbstractMap.SimpleImmutableEntry<>(tfAge, lbAge),
                new AbstractMap.SimpleImmutableEntry<>(tfEmail, lbEmail)
        ).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
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

    private void addIntegerMask(TextField tf) {
        UnaryOperator<TextFormatter.Change> integerFilter = change -> {
            String input = change.getText();
            if (input.matches("\\d*")) {
                return change;
            }
            return null;
        };

        tf.setTextFormatter(new TextFormatter<>(new IntegerStringConverter(), 0, integerFilter));
    }
    
    private boolean formValid() {
        final AtomicBoolean ok = new AtomicBoolean(true);
        validationMap.keySet().forEach(tf -> {
            if (tf.getText().trim().isEmpty() || tf.getId().contains("Email") && !ValidationUtils.isValidEmail(tf.getText().trim())) {
                validationMap.get(tf).setVisible(true);
                ok.set(false);
            } else {
                validationMap.get(tf).setVisible(false);
            }
        });

        if (selectedPersonViewModel.getPerson().getPicture() == null) {
            lbIcon.setVisible(true);
            ok.set(false);
        } else {
            lbIcon.setVisible(false);
        }
        return ok.get();
    }

    private void setPerson(Person person) {
        person.setFirstName(tfFirstName.getText().trim());
        person.setLastName(tfLastName.getText().trim());
        person.setAge(Integer.valueOf(tfAge.getText().trim()));
        person.setEmail(tfEmail.getText().trim());
    }
}
