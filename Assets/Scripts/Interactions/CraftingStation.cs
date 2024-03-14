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
    private RecipeSO currentRecipe;

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
        if (currentRecipe.ingredients[0].count>= 1 && currentRecipe.ingredients[1].count >= 1)
        {
            currentRecipe.ingredients[0].count--;
            currentRecipe.ingredients[1].count--;
            currentRecipe.Potion.count++;
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
