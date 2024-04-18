using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;
using Cinemachine;

public class DiaManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float TurnSpeed = 2.0f;

    private List<dialogueString> dialogueList;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    public CinemachineVirtualCamera cinemachineCamera;
    private Transform playerCamera;

    private int currentDialogueIndex = 0;

    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
    }

    public void DialogueStart(List<dialogueString> textToPrint, Transform NPC)
    {
        dialogueParent.SetActive(true);
        playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cinemachineCamera.enabled = false;

        StartCoroutine(TurnCameraTowardsNPC(NPC));

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButton();

        StartCoroutine(PrintDialogue());
    }
    private void DisableButton()
    {
        option1Button.interactable = false;

        option1Button.GetComponentInChildren<TMP_Text>().text = "No Option";
    }

    private IEnumerator TurnCameraTowardsNPC(Transform NPC)
    {
        Quaternion startRotation = playerCamera.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            playerCamera.rotation =Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * TurnSpeed;
            yield return null;
        }
        playerCamera.rotation = targetRotation;
    }

    private bool optionalSelected = false;
    private IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count) 
        { 
            dialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvents?.Invoke();
            if(line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.interactable = true;

                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));

                yield return new WaitUntil(() => optionalSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }
            line.endDialogueEvents?.Invoke();
            optionalSelected = false;
        }
        DialogueStop();
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionalSelected = true;
        DisableButton();

        currentDialogueIndex = indexJump;
    }
    private IEnumerator TypeText(string text)
    {
        Debug.Log(text);
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (!dialogueList[currentDialogueIndex].isQuestion) 
        { 
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogueList[currentDialogueIndex].isEnd) 
        {
            DialogueStop();
        }
        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachineCamera.enabled = true;
    }

    void Update()
    {
        /*
        if (dialogueParent.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerController.StopMoving();
            cinemachineCamera.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cinemachineCamera.enabled = true;
        }*/
    }
}
