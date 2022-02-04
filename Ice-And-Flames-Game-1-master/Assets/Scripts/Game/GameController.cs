using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public GameObject diffuseScreen = null;
    public GameObject pausePopup    = null;

    private SwipeControl controller = null;

    public bool gameStarted = false;
    public bool gamePaused  = false;

    public int lives = 3;
    private int currentLives = 3;

    public List<GameObject> livesPanel = new List<GameObject>();

    public Sprite soundOnImage = null;
    public Sprite soundOffImage = null;
    public Button soundButton = null;

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

    private void Start()
    {
        controller = GetComponent<SwipeControl>();

        diffuseScreen.SetActive(false);
        pausePopup.SetActive(false);

        gameStarted = true;
        gamePaused  = false;

        lives = 3;
        currentLives = lives;

        foreach (GameObject life in livesPanel)
        {
            life.SetActive(true);
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

    private void Update()
    {
        if (lives == 0 && gameStarted)
        {
            Debug.Log("GAME OVER ============================================================");
            StartCoroutine(EndGame());
        }

        if (lives == currentLives)
        {
            return;
        }

        if (lives != currentLives)
        {
            livesPanel[0].SetActive(false);
            livesPanel.RemoveAt(0);
            currentLives--;
        }
    }

    internal IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.5f);

        gameStarted = false;

        ButtonPausePush();
    }

    public void ButtonPausePush()
    {
        gamePaused = true;

        Debug.Log("Pause Button pushed");
        diffuseScreen.SetActive(true);
        pausePopup.SetActive(true);

        controller.enabled = false;
    }

    public void ButtonResumePush()
    {

        Debug.Log("Resume Button pushed");
        diffuseScreen.SetActive(false);
        pausePopup.SetActive(false);

        controller.enabled = true;
        gamePaused = false;
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

    public void ButtonHomePush()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
