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

    [SerializeField] PlayerController playerController;
    [SerializeField] public GameObject CureIcon;

    public void ToggleCrafting()
    {
        if (CraftingPanel != null)
        {
            CraftingPanel.SetActive(!CraftingPanel.activeSelf);
        }

        if (CraftingPanel.activeSelf)
        {
            UIManager.Singleton.PauseGame();
        }
        else
        {
            UIManager.Singleton.ResumeGame();
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
        if (currentRecipe.ingredients[0].count >= 1 && currentRecipe.ingredients[1].count >= 1)
        {
            Debug.Log("Brewing");
            currentRecipe.ingredients[0].count--;
            currentRecipe.ingredients[1].count--;
            currentRecipe.Potion.count++;
        }
    }

    public void BrewCure()
    {
        CureIcon.SetActive(true);
        
    }
    /*
    public void Resume()
    {
        CraftingPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachineCamera.enabled = true;
        Debug.Log("Game is not paused!!!");
    }
    void Pause()
    {

        CraftingPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cinemachineCamera.enabled = false;
        Debug.Log("the game is paused!!!");
    }
    */
    void Update()
    {
        /*
        if(CraftingPanel.activeInHierarchy)
        {
            Pause();
        }
        else
        {
            Resume();
        }*/
    }
}
