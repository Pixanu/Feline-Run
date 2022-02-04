using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundTrigger : MonoBehaviour
{
    public void PlayButtonSound()
    {
        if (SoundController.instance != null)
        {
            SoundController.instance.PlayClickSound();
        }
    }
}
