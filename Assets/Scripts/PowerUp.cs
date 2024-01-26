using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private Image imagePowerUp;
    [SerializeField]
    private TMP_Text textPowerAmt;
    [SerializeField]
    private GameObject correctImage;  // New serialized field for the correct image

    private bool isPowerUp = false;
    private bool isDirectionUp = true;
    private float amtPower = 0.0f;
    private float powerSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the correct image at the beginning
        HideCorrectImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPowerUp)
        {
            PowerActive();
        }
    }

    void PowerActive()
    {
        if (isDirectionUp)
        {
            amtPower += Time.deltaTime * powerSpeed;
            if (amtPower > 100)
            {
                isDirectionUp = false;
                amtPower = 100.0f;
            }
        }
        else
        {
            amtPower -= Time.deltaTime * powerSpeed;
            if (amtPower < 0)
            {
                isDirectionUp = true;
                amtPower = 0.0f;
            }
        }

        imagePowerUp.fillAmount = (0.492f - 0.25f) * amtPower / 100.0f + 0.25f;
    }

    public void StartPowerUp()
    {
        isPowerUp = true;
        amtPower = 0.0f;
        isDirectionUp = true;
        textPowerAmt.text = "";
        HideCorrectImage();  // Call a method to hide the correct image when the power-up starts
    }

    public void EndPowerUp()
    {
        isPowerUp = false;
        textPowerAmt.text = amtPower.ToString("F0");

        // Check if the fillAmount is in the specified range (0.3 to 0.4) after the button is released
        if (imagePowerUp.fillAmount >= 0.34f && imagePowerUp.fillAmount <= 0.40117f)
        {
            Debug.Log("Correct");
            ShowCorrectImage();  // Call a method to display the correct image
        }
        else
        {
            Debug.Log("Try again");
        }
    }

    void ShowCorrectImage()
    {
        // Activate the correct image GameObject
        if (correctImage != null)
        {
            correctImage.SetActive(true);
        }
    }

    void HideCorrectImage()
    {
        // Deactivate the correct image GameObject
        if (correctImage != null)
        {
            correctImage.SetActive(false);
        }
    }
}





