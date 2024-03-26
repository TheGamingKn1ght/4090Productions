using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void PlayGame(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
