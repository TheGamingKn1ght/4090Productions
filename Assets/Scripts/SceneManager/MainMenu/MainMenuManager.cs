using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    [SerializeField] private CinemachineVirtualCamera MainView;
    [SerializeField] private CinemachineVirtualCamera PlayView;
    [SerializeField] private CinemachineVirtualCamera SaveView;
    
    [Header("Canvas'")]
    [SerializeField] private Canvas OpeningScreenCanvas;

    public void ActivateSaveCamera()
    {
        SaveView.Priority = 2;
        PlayView.Priority = 1;
        MainView.Priority = 0;
    }
    public void ActivateMenuCamera()
    {
        OpeningScreenCanvas.gameObject.SetActive(false);

        PlayView.Priority = 2;
        MainView.Priority = 1;
        SaveView.Priority = 0;
    }
    public void ReturnToLastCamera()
    {

    }
}
