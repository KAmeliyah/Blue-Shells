using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    FadeInOut fade;

    private bool enterAllowed;
    private string sceneToLoad;

    private void Start()
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
        if (collision.GetComponent<Door>())
        {
            enterAllowed = true;
            sceneToLoad = "Combat1";
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Door>()) 
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
