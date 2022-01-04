using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneGame)
    {
        SceneManager.LoadScene(sceneGame);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
