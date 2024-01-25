using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    void Start()
    {
        float targetAspect = 16.0f / 9.0f;

        float windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleheight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();


        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else 
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
    public void PlayGame()
    {

        SceneManager.LoadSceneAsync("Overworld1");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}

