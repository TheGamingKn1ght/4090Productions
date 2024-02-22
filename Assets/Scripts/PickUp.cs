using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : Interactor, IInteractable
{
    public TextMeshProUGUI fuelCounter;

    public void Interact()
    {
            Debug.Log("Fuel picked up"); 

            switch (Interactor.tag)
            {
                case ("FuelCan") :
                    PlayerInventory.fuelCount++;
                    fuelCounter.text = PlayerInventory.fuelCount.ToString();
                    break;
            }
    }
}
