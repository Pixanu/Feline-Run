using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance = null;

    public AudioSource clickSound = null;

    public bool isSoundEnabled = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("soundEnabled"))
        {
            PlayerPrefs.SetInt("soundEnabled", 1);
        }

        AudioListener.pause = false;

        SetAudioState();
    }

    private void Update()
    {
        if (!PlayerPrefs.HasKey("soundEnabled"))
        {
            PlayerPrefs.SetInt("soundEnabled", 1);
        }
    }

    public void SwitchAudioControl()
    {
        PlayerPrefs.SetInt("soundEnabled", PlayerPrefs.GetInt("soundEnabled") == 1 ? 0 : 1);
        if (Debug.isDebugBuild)
        {
            Debug.Log("Audio state is " + (PlayerPrefs.GetInt("soundEnabled") == 1 ? "ON" : "OFF"));
        }

        SetAudioState();
    }

    private void SetAudioState()
    {
        AudioListener.pause = false;

        isSoundEnabled = PlayerPrefs.GetInt("soundEnabled") == 1;
        AudioListener.volume = PlayerPrefs.GetInt("soundEnabled") == 1 ? 1f : 0f;

        if (Debug.isDebugBuild)
        {
            Debug.Log("AudioListener.volume is " + (AudioListener.volume * 100f).ToString() + "%");
        }

        //AudioListener.pause = !isSoundEnabled;
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }
}
