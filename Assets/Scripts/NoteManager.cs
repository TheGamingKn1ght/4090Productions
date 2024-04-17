using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;
using Cinemachine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject NoteParent;
    [SerializeField] private TMP_Text NoteText;
    [SerializeField] private Button NoteButton1;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float TurnSpeed = 2.0f;

    private List<NoteString> NoteList;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    public CinemachineVirtualCamera cinemachineCamera;
    private Transform playerCamera;

    private int currentNoteIndex = 0;

    private void Start()
    {
        NoteParent.SetActive(false);
        playerCamera = Camera.main.transform;
    }

    public void NoteStart(List<NoteString> textToPrint, Transform NPC)
    {
        NoteParent.SetActive(true);
        playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cinemachineCamera.enabled = false;

        StartCoroutine(TurnCameraTowardsNPC(NPC));

        NoteList = textToPrint;
        currentNoteIndex = 0;

        DisableButton();

        StartCoroutine(PrintNote());
    }
    private void DisableButton()
    {
        NoteButton1.interactable = false;

        NoteButton1.GetComponentInChildren<TMP_Text>().text = "No Option";
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
    private IEnumerator PrintNote()
    {
        while(currentNoteIndex < NoteList.Count) 
        {
            NoteString line = NoteList[currentNoteIndex];

            line.startNoteEvents?.Invoke();
            if(line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                NoteButton1.interactable = true;

                NoteButton1.GetComponentInChildren<TMP_Text>().text = line.answerOption1;

                NoteButton1.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));

                yield return new WaitUntil(() => optionalSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }
            line.endNoteEvents?.Invoke();
            optionalSelected = false;
        }
        NoteStop();
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionalSelected = true;
        DisableButton();

        currentNoteIndex = indexJump;
    }
    private IEnumerator TypeText(string text)
    {
        Debug.Log(text);
        NoteText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            NoteText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (!NoteList[currentNoteIndex].isQuestion) 
        { 
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (NoteList[currentNoteIndex].isEnd) 
        {
            NoteStop();
        }
        currentNoteIndex++;
    }

    private void NoteStop()
    {
        StopAllCoroutines();
        NoteText.text = "";
        NoteParent.SetActive(false);

        playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachineCamera.enabled = true;
    }
    /*
    void Update()
    {
        if (NoteParent.activeInHierarchy)
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
        }
    }
    */
}
