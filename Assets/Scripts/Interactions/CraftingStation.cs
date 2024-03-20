using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class CraftingStation : AbstractInteractable
{
    public GameObject CraftingPanel;
    public GameObject Ingredient1;
    public GameObject Ingredient2;
    public GameObject CraftButton;
    public CinemachineVirtualCamera cinemachineCamera;
    public static RecipeSO currentRecipe;
    [SerializeField] public List<ItemSO> allItems = new List<ItemSO>();
    [SerializeField] public List<ItemSO> allPotions = new List<ItemSO>();

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

    public void LoadRecipeIngredients(RecipeSO recipe)
    {
        Ingredient1.GetComponent<Image>().sprite = recipe.ingredients[0].icon;
        Ingredient2.GetComponent<Image>().sprite = recipe.ingredients[1].icon;
        currentRecipe = recipe;
    }
    public void BrewPotion()
    {
        if (Collectible.computer.allItems[0].count >= 1 && Collectible.computer.allItems[2].count >= 1)
        {
            Debug.Log("Brewing");
            Collectible.computer.allItems[0].count--;
            Collectible.computer.allItems[2].count--;
            Collectible.computer.allPotions[0].count++;
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
