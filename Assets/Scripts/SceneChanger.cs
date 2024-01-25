using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    FadeInOut fade;

    private bool enterAllowed;
    private string sceneToLoad;

    private void Awake()
    {
        fade = FindObjectOfType<FadeInOut>();
    }


    public IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scene scene = SceneManager.GetActiveScene();
        
        if (collision.GetComponent<SceneChanger>() && scene.name == "Overworld1")
        {
            enterAllowed = true;
            sceneToLoad = "Overworld1";
        }
        else
        {
            enterAllowed = true;
            sceneToLoad = "End Credits";
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SceneChanger>()) 
        { 
            enterAllowed = false;
        
        }
    }



    private void Update()
    {
        if (enterAllowed) 
        {
           StartCoroutine(ChangeScene());
        }
    }
}
