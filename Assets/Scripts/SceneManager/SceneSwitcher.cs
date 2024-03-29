using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    #region Singleton
    public static SceneSwitcher Singleton;
    public void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
    public void PlayGame(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
