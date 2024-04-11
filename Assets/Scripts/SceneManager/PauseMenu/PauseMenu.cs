using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
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
        if (pauseCanvas.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Pause()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
    }

    public void GoToMainMenu(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
