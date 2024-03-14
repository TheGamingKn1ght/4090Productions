using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class CraftingStation : AbstractInteractable
{
    public GameObject CraftingPanel;
    public GameObject HealthPotionRecipe;
    public GameObject SpeedPotionRecipe;
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

    public void LoadHealthIngredients()
    {
        HealthPotionRecipe.SetActive(true);
        SpeedPotionRecipe.SetActive(false);
    }

    public void LoadSpeedIngredients()
    {
        SpeedPotionRecipe.SetActive(true);
        HealthPotionRecipe.SetActive(false);
    }

    public void BrewPotion()
    {
        if (HealthPotionRecipe.activeInHierarchy)
        {
            if (PlayerInventory.fuelCount >= 1 && PlayerInventory.berryCount >= 1)
            {
                PlayerInventory.berryCount--;
                PlayerInventory.fuelCount--;
                PlayerInventory.HPCount++;
            }
        }
        else if (SpeedPotionRecipe.activeInHierarchy)
        {
            if (PlayerInventory.fuelCount >= 1 && PlayerInventory.honeyCount >= 1)
            {
                PlayerInventory.honeyCount--;
                PlayerInventory.fuelCount--;
                PlayerInventory.SPCount++;
            }
        }
    }

    void Update()
    {
        //CheckInventory();

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
