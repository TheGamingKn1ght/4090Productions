using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : AbstractInteractable
{
    public override void Interact()
    {
        Collect();
    }
    public void Collect()
    {
        Debug.Log("Collectible Collected");
        this.gameObject.SetActive(false);
    }
}