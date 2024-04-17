using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteInventory : MonoBehaviour
{
    public int NumberOfNotes { get; private set; }
    public UnityEvent<NoteInventory> OnNoteCollected;

    public void NotesCollected(GameObject cureSmoke)
    {
        NumberOfNotes++;
        if(NumberOfNotes == 11)
        {
            AllNotesCollected(cureSmoke);
        }
        OnNoteCollected.Invoke(this);
    }

    public void AllNotesCollected(GameObject cureSmoke)
    {
        cureSmoke.SetActive(true);
    }
}
