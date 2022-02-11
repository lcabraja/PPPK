/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.PeopleApplication;
import hr.algebra.dao.RepositoryFactory;
import hr.algebra.model.Person;
import hr.algebra.model.Pet;
import hr.algebra.utilities.FileUtils;
import hr.algebra.utilities.ImageUtils;
import hr.algebra.utilities.ValidationUtils;
import hr.algebra.viewmodel.PetViewModel;
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
import javafx.scene.control.ComboBox;
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
public class PetsController implements Initializable {

    private Map<TextField, Label> validationMap;

    private final ObservableList<PetViewModel> pets = FXCollections.observableArrayList();
    private final ObservableList<Person> owners = FXCollections.observableArrayList();

    private PetViewModel selectedPetViewModel;

    private boolean edit = false;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableView<PetViewModel> tvPets;
    @FXML
    private TableColumn<PetViewModel, String> tcName, tcOwner;
    @FXML
    private TableColumn<PetViewModel, Integer> tcAge;
    @FXML
    private Tab tabEdit;
    @FXML
    private TextField tfName;
    @FXML
    private Label lbName;
    @FXML
    private TextField tfAge;
    @FXML
    private Label lbAge;
    @FXML
    private ComboBox<Person> cbOwners;
    @FXML
    private Label lbOwner;
    @FXML
    private ImageView ivPet;
    @FXML
    private Label lbIcon;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initObservables();
        initTable();
        initComboBox();
        addIntegerMask(tfAge);
        setupListeners();
    }

    @FXML
    private void go(ActionEvent event) {
        PeopleApplication.toggleStage();
    }

    @FXML
    private void edit(ActionEvent event) {
        if (tvPets.getSelectionModel().getSelectedItem() != null) {
            edit = true;
            bindPet(tvPets.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
        }
    }

    private void bindPet(PetViewModel viewModel) {
        resetForm();
        selectedPetViewModel = viewModel != null ? viewModel : new PetViewModel(null);
        tfName.setText(selectedPetViewModel.getPet().getName());
        tfAge.setText(String.valueOf(selectedPetViewModel.getPet().getAge()));
        cbOwners.getSelectionModel().select(selectedPetViewModel.getPet().getOwner());
        ivPet.setImage(
                selectedPetViewModel.getPet().getPicture() != null
                ? new Image(new ByteArrayInputStream(selectedPetViewModel.getPet().getPicture()))
                : new Image(new File("src/assets/no_image.png").toURI().toString())
        );
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvPets.getSelectionModel().getSelectedItem() != null) {
            pets.remove(tvPets.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void upload(ActionEvent event) {
        File file = FileUtils.uploadFileDialog(ivPet.getScene().getWindow(), "jpg", "jpeg", "png");
        if (file != null) {
            Image image = new Image(file.toURI().toString());
            try {
                String ext = file.getName().substring(file.getName().lastIndexOf(".") + 1);
                setImage(selectedPetViewModel.getPet(),
                        image, ext,
                        ivPet);
            } catch (IOException ex) {
                Logger.getLogger(this.getClass().getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    private void setupListeners() {
        tabEdit.setOnSelectionChanged(event -> {
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEdit) && !edit) {
                bindPet(null);
            } else {
                edit = false;
            }
        });
        pets.addListener((ListChangeListener.Change<? extends PetViewModel> change) -> {
            if (change.next()) {
                if (change.wasRemoved()) {
                    change.getRemoved().forEach(pvm -> {
                        try {
                            RepositoryFactory.getRepository().deletePet(pvm.getPet());
                        } catch (Exception ex) {
                            Logger.getLogger(this.getClass().getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                } else if (change.wasAdded()) {
                    change.getAddedSubList().forEach(pvm -> {
                        try {
                            int idPet = RepositoryFactory.getRepository().addPet(pvm.getPet());
                            pvm.getPet().setIDPet(idPet);
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
                setPet(selectedPetViewModel.getPet());

                if (selectedPetViewModel.getPet().getIDPet() == 0) {
                    pets.add(selectedPetViewModel);
                } else {
                    RepositoryFactory.getRepository().updatePet(selectedPetViewModel.getPet());
                    tvPets.refresh();
                }
            } catch (Exception ex) {
                Logger.getLogger(PetsController.class.getName()).log(Level.SEVERE, null, ex);
            }
            selectedPetViewModel = null;
            tpContent.getSelectionModel().select(tabList);
            resetForm();
        }
    }

    private void setImage(Pet player, Image image, String ext, ImageView ivImage) throws IOException {

        player.setPicture(ImageUtils.imageToByteArray(image, ext));
        ivImage.setImage(image);

    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));
        lbIcon.setVisible(false);
    }

    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfName, lbName),
                new AbstractMap.SimpleImmutableEntry<>(tfAge, lbAge)
        ).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
    }

    private void initObservables() {
        try {
            RepositoryFactory.getRepository().getPets().forEach(pet -> pets.add(new PetViewModel(pet)));
            RepositoryFactory.getRepository().getPeople().forEach(person -> owners.add(person));
        } catch (Exception ex) {
            Logger.getLogger(PetsController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private void initTable() {
        tcName.setCellValueFactory(pet -> pet.getValue().getNameProperty());
        tcAge.setCellValueFactory(pet -> pet.getValue().getAgeProperty().asObject());
        tcOwner.setCellValueFactory(pet -> pet.getValue().getOwnerNameProperty());
        tvPets.setItems(pets);
    }

    private void initComboBox() {

       cbOwners.setItems(owners);
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

        if (selectedPetViewModel.getPet().getPicture() == null) {
            lbIcon.setVisible(true);
            ok.set(false);
        } else {
            lbIcon.setVisible(false);
        }
        return ok.get();
    }

    private void setPet(Pet pet) {
        pet.setName(tfName.getText().trim());
        pet.setAge(Integer.valueOf(tfAge.getText().trim()));
        pet.setOwner(cbOwners.getSelectionModel().getSelectedItem());
    }
}
