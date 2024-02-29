using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : AbstractInteractable
{
    public TextMeshProUGUI Counter;
    
    public override void Interact()
    {
        Collect();
    } 
    
    public void Collect()
    {
        switch (Interactor.hit.collider.gameObject.tag)
        {
            case ("FuelCan") :
                PlayerInventory.fuelCount++;
                Counter.text = PlayerInventory.fuelCount.ToString();
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
            case ("Coin") :
                PlayerInventory.coinCount++;
                Counter.text = PlayerInventory.coinCount.ToString();    
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
        }
     
        this.gameObject.SetActive(false);
        
    }
}