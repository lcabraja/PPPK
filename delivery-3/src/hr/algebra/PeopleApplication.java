/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra;

import hr.algebra.utilities.ViewUtils;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

/**
 *
 * @author daniel.bele
 */
public class PeopleApplication extends Application {
    
    private static Stage mainStage = null;
    private static boolean showingPeople = false;
    
    @Override
    public void start(Stage primaryStage) throws IOException {
        mainStage = primaryStage;
        Parent root = FXMLLoader.load(getClass().getResource("view/People.fxml"));
        Scene scene = new Scene(root);
        primaryStage.setTitle("People manager");
        primaryStage.setScene(scene);
        primaryStage.centerOnScreen();
        primaryStage.show();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
    
    public static Stage getMainStage() {
        return mainStage;
    }
    
    public static void toggleStage() {
        if (mainStage != null) {
            try {
                ViewUtils.loadView(PeopleApplication.class.getResource(showingPeople ? "view/People.fxml" : "view/Pets.fxml"));
                showingPeople = !showingPeople;
            } catch (IOException ex) {
                Logger.getLogger(PeopleApplication.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
}
