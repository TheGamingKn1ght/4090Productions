using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private PlayerController player;
    private void OnEnable()
    {
        InputManager.OnPauseInput += Pause;
    }

    private void OnDisable()
    {
        InputManager.OnPauseInput -= Pause;
    }

    private void Start()
    {
        pauseCanvas.SetActive(false);
    }
    void Update()
    {
        /*
        if (pauseCanvas.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            player.StopMoving();

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }*/
    }

    public void Pause()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);

        if (pauseCanvas.activeSelf)
        {
            UIManager.Singleton.PauseGame();
        }
        else
        {
            UIManager.Singleton.ResumeGame();
        }
    }

    public void GoToMainMenu(int sceneIndex)
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
