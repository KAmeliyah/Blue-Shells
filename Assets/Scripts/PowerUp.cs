using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Image powerUpImage;
    [SerializeField] private TMP_Text textPowerAmt;
    [SerializeField] private GameObject correctImage;

    private bool isPowerUp = false;
    private bool isDirectionUp = true;
    private float amtPower = 0.0f;
    private float powerSpeed = 100.0f;

    void Start()
    {
        HideCorrectImage();
    }

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

        powerUpImage.fillAmount = (0.492f - 0.25f) * (amtPower / 100.0f) + 0.25f;
    }

    public void StartPowerUp()
    {
        isPowerUp = true;
        amtPower = 0.0f;
        isDirectionUp = true;
        textPowerAmt.text = "";
        HideCorrectImage();
    }

    public void EndPowerUp()
    {
        isPowerUp = false;
        textPowerAmt.text = amtPower.ToString("F0");

        if (powerUpImage.fillAmount >= 0.34f && powerUpImage.fillAmount <= 0.40117f)
        {
            Debug.Log("Correct");
            ShowCorrectImage();
            Invoke("ChangeSceneAfterDelay", 3f);
        }
        else
        {
            Debug.Log("Try again");
        }
    }

    void ChangeSceneAfterDelay()
    {
        SceneManager.LoadScene("Overworld2");
    }

    void ShowCorrectImage()
    {
        if (correctImage != null)
        {
            correctImage.SetActive(true);
        }
    }

    void HideCorrectImage()
    {
        if (correctImage != null)
        {
            correctImage.SetActive(false);
        }
    }
}





