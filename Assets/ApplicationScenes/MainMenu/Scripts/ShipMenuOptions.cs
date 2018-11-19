using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMenuOptions : MonoBehaviour {
    //default red fighter ship on game start
    public string selectedShipColor = "red";
    MainMenu mm = new MainMenu();

    public void ChangeSelectedShipColor(string shipColor){
        selectedShipColor = shipColor;
        mm.UpdateSelectedShipColor(selectedShipColor);
    }

    public string GetSelectedShipColor(){
        return selectedShipColor;
    }


}
