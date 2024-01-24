using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
  
    public void PlayGame()
    {

        SceneManager.LoadSceneAsync("Overworld1");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}

