using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class NoteInteraction : MonoBehaviour
{
    public GameObject NotePanel;
    public CinemachineVirtualCamera cinemachineCamera;

    public void Interact()
    {
        ToggleNote();
    }

    public void ToggleNote()
    {
        if (NotePanel != null)
        {
            NotePanel.SetActive(!NotePanel.activeSelf);
        }
    }
}
