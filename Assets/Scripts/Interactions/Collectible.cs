using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : AbstractInteractable
{
    public TextMeshProUGUI Counter;
    public PlayerInventory activeInventory;

    public override void Interact()
    {
        Collect();
    } 
    
    public void Collect()
    {
       foreach(var item in PlayerInventory.singleton.allItems)
        {
            if (Interactor.hit.collider.gameObject.tag == item.name)
            {
                item.count++;
                Counter.text = item.count.ToString();
                Debug.Log(Interactor.hit.collider.gameObject.tag.ToString());
            }
        }
       
        this.gameObject.SetActive(false);
        
    }
}