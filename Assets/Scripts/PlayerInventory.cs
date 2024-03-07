using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerInventory : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.OnInventoryInput += ToggleInventory;
    }
    
    public GameObject InvPanel;

    #region Inventory Counters

    // ---- Ingredients ------

    public static int fuelCount;
    public static int berryCount;
    public static int honeyCount;
    public TextMeshProUGUI fuelCounter;
    public TextMeshProUGUI berryCounter;
    public TextMeshProUGUI honeyCounter;

    public CinemachineVirtualCamera cinemachineCamera;


    // ---- Potions ------

    public static int HPCount;
    public static int SPCount;
    public TextMeshProUGUI HPCounter;
    public TextMeshProUGUI SPCounter;

    public TextMeshProUGUI HPCounterhotkey;
    public TextMeshProUGUI SPCounterhotkey;

    #endregion

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
        berryCounter.text = berryCount.ToString();
        honeyCounter.text = honeyCount.ToString();

        HPCounter.text = HPCount.ToString();
        SPCounter.text = SPCount.ToString();

        HPCounterhotkey.text = HPCount.ToString();
        SPCounterhotkey.text = SPCount.ToString();
    }
}
