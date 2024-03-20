using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : AbstractInteractable
{
    public TextMeshProUGUI Counter;
    public static CraftingStation computer;

    public override void Interact()
    {
        Collect();
    } 
    
    public void Collect()
    {
        
        switch (Interactor.hit.collider.gameObject.tag)
        {
            case ("Berry") :
                computer.allItems[0].count++;
                //PlayerInventory.berryCount++;
                Counter.text = PlayerInventory.berryCount.ToString();
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
            case ("Honey") :
                computer.allItems[1].count++;
                //PlayerInventory.honeyCount++;
                Counter.text = PlayerInventory.honeyCount.ToString();    
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
            case ("Fuel") :
                computer.allItems[2].count++;
                //PlayerInventory.fuelCount++;
                Counter.text = PlayerInventory.fuelCount.ToString();    
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
        }
     
        this.gameObject.SetActive(false);
        
    }
}