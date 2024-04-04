using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiaTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform NPCTransform;

    private bool hadSpooken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hadSpooken)
        {
            other.gameObject.GetComponent<DiaManager>().DialogueStart(dialogueStrings, NPCTransform);
            hadSpooken = true;

        }
    }
}
[System.Serializable]
public class dialogueString
{
    public string text; //Npc Text
    public bool isEnd; //Last line

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public int option1IndexJump;

    [Header("Trigger Events")]
    public UnityEvent startDialogueEvents;
    public UnityEvent endDialogueEvents;
}