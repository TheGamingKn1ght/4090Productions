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
    List<CinemachineVirtualCamera> priorityQueue;

    [Header("Canvas'")]
    [SerializeField] private Canvas OpeningScreenCanvas;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        priorityQueue = new List<CinemachineVirtualCamera>();
        priorityQueue.Add(MainView);
        priorityQueue.Add(PlayView);
        priorityQueue.Add(SaveView);
    }

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
        int priority = 0;
        foreach(var View in priorityQueue)
        {
            if(priority < View.Priority)
            {
                priority = View.Priority;
            }
            if(priority == 2)
            {
                View.Priority -= 2;
            }
        }

    }
}
