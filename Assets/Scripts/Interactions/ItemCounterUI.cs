using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCounterUI : MonoBehaviour
{
    [SerializeField] ItemSO item;
    [SerializeField] TMP_Text text;

    private void Update()
    {
        text.text = item.count.ToString();
    }
}
