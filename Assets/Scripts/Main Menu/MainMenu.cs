using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public GameObject optionsScreen;
    
    // Start is called before the first frame update

    

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOption()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
