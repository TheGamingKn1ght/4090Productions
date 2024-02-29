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
    
    public GameObject InvPanel;
    public static int fuelCount;
    public static int coinCount;
    public static int GFCount;
    public TextMeshProUGUI fuelCounter;
    public TextMeshProUGUI coinCounter;
    public TextMeshProUGUI GFCounter;

    public void ToggleInventory()
    {
        if (InvPanel != null)
        {
            InvPanel.SetActive(!InvPanel.activeSelf);
        }
    }

    void Update()
    {
        fuelCounter.text = fuelCount.ToString();
        coinCounter.text = coinCount.ToString();
        GFCounter.text = GFCount.ToString();
    }
}
