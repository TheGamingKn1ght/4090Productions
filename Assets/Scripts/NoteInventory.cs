using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteInventory : MonoBehaviour
{
    public static int NumberOfNotes { get; private set; }
    public UnityEvent<NoteInventory> OnNoteCollected;

    public void NotesCollected(GameObject cureButton)
    {
        NumberOfNotes++;
        if(NumberOfNotes == 11)
        {
            AllNotesCollected(cureButton);
        }
        OnNoteCollected.Invoke(this);
    }

    public void AllNotesCollected(GameObject cureButton)
    {
        cureButton.SetActive(true);
    }
}
