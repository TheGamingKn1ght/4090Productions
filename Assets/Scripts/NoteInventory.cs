using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteInventory : MonoBehaviour
{
    public int NumberOfNotes { get; private set; }
    public UnityEvent<NoteInventory> OnNoteCollected;

    public void NotesCollected()
    {
        NumberOfNotes++;
        OnNoteCollected.Invoke(this);
    }
}
