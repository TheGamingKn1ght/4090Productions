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
            case ("Berry") :
                PlayerInventory.berryCount++;
                Counter.text = PlayerInventory.berryCount.ToString();
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
            case ("Honey") :
                PlayerInventory.honeyCount++;
                Counter.text = PlayerInventory.honeyCount.ToString();    
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
            case ("Fuel") :
                PlayerInventory.fuelCount++;
                Counter.text = PlayerInventory.fuelCount.ToString();    
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
                break;
        }
     
        this.gameObject.SetActive(false);
        
    }
}