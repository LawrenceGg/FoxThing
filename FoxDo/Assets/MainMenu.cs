using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame ()
    {
        SceneManager.LoadScene("FoxHole");
    }
    public void QuitGame ()
    {
        Debug.Log("I QUIT");
        Application.Quit();        
    }
}
