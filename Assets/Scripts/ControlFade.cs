using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFade : MonoBehaviour
{

    FadeInOut fade;

    // Start is called before the first frame update
    private void Start()
    {
        fade = FindObjectOfType<FadeInOut>();

        fade.FadeOut();
    }

   
}
