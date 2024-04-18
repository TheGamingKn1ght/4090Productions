using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NoteUI : MonoBehaviour
{
    private TextMeshProUGUI NoteTextUI;

    private void Start()
    {
        NoteTextUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateNoteText()
    {
        NoteTextUI.text = NoteInventory.NumberOfNotes.ToString();
    }
}
