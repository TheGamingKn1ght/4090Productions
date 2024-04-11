using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteTrigger : MonoBehaviour
{
    [SerializeField] private List<NoteString> NoteStrings = new List<NoteString>();
    [SerializeField] private Transform NPCTransform;

    private bool hadSpooken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hadSpooken)
        {
            other.gameObject.GetComponent<NoteManager>().NoteStart(NoteStrings, NPCTransform);
            hadSpooken = true;

        }
    }
}
[System.Serializable]
public class NoteString
{
    public string text; //Npc Text
    public bool isEnd; //Last line

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public int option1IndexJump;

    [Header("Trigger Events")]
    public UnityEvent startNoteEvents;
    public UnityEvent endNoteEvents;
}