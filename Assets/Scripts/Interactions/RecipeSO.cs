using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Diego/Create New Recipe", order = 1)]
public class RecipeSO : ScriptableObject
{
    public ItemSO[] ingredients = new ItemSO[2];
    public ItemSO Potion;
}
