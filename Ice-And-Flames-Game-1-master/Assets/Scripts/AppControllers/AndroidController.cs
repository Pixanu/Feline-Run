using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AndroidController : MonoBehaviour
{
    public static AndroidController instance = null;

    private bool clickedOnce = false;
    private bool clickedTwice = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!clickedOnce && !clickedTwice)
            {
                clickedOnce = true;
                StartCoroutine(Countdown());
            }
            else if (clickedOnce && !clickedTwice)
            {
                clickedTwice = true;

#if UNITY_EDITOR
                Debug.Log("Editor exited ============================================");
                EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
    }

    internal IEnumerator Countdown()
    {
        GetMessage("Press again to quit. 3 seconds left.");

        yield return new WaitForSeconds(3);

        clickedOnce  = false;
        clickedTwice = false;
    }

    public void GetMessage(string message)
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("DEBUG BUILD. " + message);
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            GetComponent<ToastMessageScript>().showToastOnUiThread(message);
            return;
        }

    }
}
