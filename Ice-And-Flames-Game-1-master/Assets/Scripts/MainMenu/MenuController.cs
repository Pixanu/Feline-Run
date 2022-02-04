using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    public GameObject creditsPopup  = null;
    public GameObject diffuseScreen = null;

    public Button soundButton = null;

    public Sprite soundOnImage  = null;
    public Sprite soundOffImage = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Start()
    {
        creditsPopup.SetActive(false);
        diffuseScreen.SetActive(false);

        if (!PlayerPrefs.HasKey("soundEnabled"))
        {
            PlayerPrefs.SetInt("soundEnabled", 1);
        }

        if (PlayerPrefs.GetInt("soundEnabled") == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundOffImage;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOnImage;
        }
    }
    public void ButtonPlayPush()
    {
        SceneManager.LoadScene("GameScene - Mishu");
    }

    public void ButtonQuitPush()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    public void ButtonCreditsPush()
    {
        creditsPopup.SetActive(true);
        diffuseScreen.SetActive(true);
    }

    public void ButtonCreditsClose()
    {
        creditsPopup.SetActive(false);
        diffuseScreen.SetActive(false);
    }

    public void ButtonSoundToggle()
    {
        Debug.Log("Sound Button pushed");
        ButtonSwitchSoundState();
    }

    private void ButtonSwitchSoundState()
    {
        if (SoundController.instance != null)
        {
            SoundController.instance.SwitchAudioControl();
            //soundButtonText.text = PlayerPrefs.GetInt("soundEnabled") == 1 ? "SOUND ON" : "SOUND OFF";

            if (PlayerPrefs.GetInt("soundEnabled") == 0)
            {
                soundButton.GetComponent<Image>().sprite = soundOffImage;
            }
            else
            {
                soundButton.GetComponent<Image>().sprite = soundOnImage;
            }
        }
        else
        {
            Debug.Log("SoundController is inactive. Go to RegisterMenu or restart the application!");
        }
    }
}
