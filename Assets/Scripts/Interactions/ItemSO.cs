using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Item Asset", menuName = "Diego/Create new Item", order = 0)]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public bool isStackable = false;
    public int count;
    //public TMP_Text textMesh;
}
