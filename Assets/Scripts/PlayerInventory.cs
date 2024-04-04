using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    public static PlayerInventory singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(var item in allItems)
        {
            item.count = 0;
        }
        foreach (var potion in allPotions)
        {
            potion.count = 0;
        }

    }


    #endregion
    
    private void OnEnable()
    {
        InputManager.OnInventoryInput += ToggleInventory;
    }
    public GameObject InvPanel;

    [SerializeField] public List<ItemSO> allItems = new List<ItemSO>();
    [SerializeField] public List<ItemSO> allPotions = new List<ItemSO>();
    #region Inventory Counters

    // ---- Ingredients ------

    public TextMeshProUGUI fuelCounter;
    public TextMeshProUGUI berryCounter;
    public TextMeshProUGUI honeyCounter;

    public CinemachineVirtualCamera cinemachineCamera;


    // ---- Potions ------

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
        /*
        foreach (var item in allItems)
        {
            item.textMesh.text = item.count.ToString();
        }
        foreach (var potion in allPotions)
        {
            potion.textMesh.text = potion.count.ToString();
            SPCounterhotkey.text = potion.count.ToString();
        }
        
        fuelCounter.text = allItems[2].count.ToString();
        berryCounter.text = allItems[0].count.ToString();
        honeyCounter.text = allItems[1].count.ToString();

        HPCounter.text = allPotions[0].count.ToString();
        SPCounter.text = allPotions[1].count.ToString();

        HPCounterhotkey.text = allPotions[0].count.ToString();
        SPCounterhotkey.text = allPotions[1].count.ToString();

        */
    }
}
