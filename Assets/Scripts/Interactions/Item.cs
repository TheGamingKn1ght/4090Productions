using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class Item : MonoBehaviour
{
    private int ID { get; set; }
    private string Name { get; set; }
    private int Count { get; set; }
    private Dictionary<Item, Item> Recipe = new Dictionary<Item, Item>();

    private void Awake()
    {
        Name = this.gameObject.name;
    }
    private void AddItem()
    {
        this.Count++;
    }

    private void RemoveItem()
    {
        this.Count--;
    }

    private void LoadRecipe()
    {

    }
}
