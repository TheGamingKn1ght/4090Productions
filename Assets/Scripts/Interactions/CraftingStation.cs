using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Item
{
    private int ID { get; set; }
    private string Name { get; set; }
    private int Count { get; set; }
    private Item[] Recipe { get; set; }

    private void AddItem()
    {
        Count++;
    }

    private void RemoveItem()
    {
        Count--;
    }
}

public class CraftingStation : AbstractInteractable, Iinteractable
{
    public GameObject CraftingPanel;
    public GameObject ResourcesPanel;
    public TextMeshProUGUI Resource1;
    public TextMeshProUGUI Resource2;
    public TextMeshProUGUI CraftText;
    public TextMeshProUGUI GoldFuel;
    public GameObject CraftButton;
    public CinemachineVirtualCamera cinemachineCamera;

    public void ToggleCrafting()
    {
        if (CraftingPanel != null)
        {
            CraftingPanel.SetActive(!CraftingPanel.activeSelf);
        }
    }

    public override void Interact()
    {
        ToggleCrafting();
    }

    public void ToggleResources()
    {
        if (ResourcesPanel != null)
        {
            ResourcesPanel.SetActive(!ResourcesPanel.activeSelf);
        }
    }

    public void CraftGoldFuel()
    {
        if (PlayerInventory.fuelCount >= 1 && PlayerInventory.coinCount >= 1)
        {
            PlayerInventory.GFCount++;
            PlayerInventory.fuelCount--;
            PlayerInventory.coinCount--;
        }
    }

    private void CheckInventory()
    {
        if (PlayerInventory.fuelCount < 1)
        {
           Resource1.color = new Color(255,0,0,255);
        }
        else
        {
            Resource1.color = new Color(255,255,255,255);
        }

        if (PlayerInventory.coinCount < 1)
        {
           Resource2.color = new Color(255,0,0,255);
        }
        else
        {
            Resource2.color = new Color(255,255,255,255);
        }
    }

    void Update()
    {
        CheckInventory();

        if(CraftingPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            cinemachineCamera.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
            cinemachineCamera.enabled = true;
        }
    }
}
