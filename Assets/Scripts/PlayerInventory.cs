using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.OnInventoryInput += ToggleInventory;
    }
    
    public static int fuelCount;
    public GameObject InvPanel;
    public TextMeshProUGUI fuelCounter;

    public void ToggleInventory()
    {
        if (InvPanel != null)
        {
            InvPanel.SetActive(!InvPanel.activeSelf);
        }
    }
}
