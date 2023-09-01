using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Creates an instance of UI Manager
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("UI Manager is NULL");

            return _instance;
        }
    }



    private void Awake()
    {
        //UI Manager Reference
        _instance = this;

        GameManager.OnGameStateChanged += UIManagerStateChange;
    }

    public void UIManagerStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Intro:
                HandleIntro();
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.Prize:
                HandlePrize();
                break;

            default:
                break;
        }
    }


    // Variables
    public GameObject safeArea;
    public Image interSportLogo;
    public TMP_Text _prizeText;
    private List<GameObject> balloons;
    // Prize frame and image
    public GameObject prizeMessage;
    public Button prizeWindow;
    public float fadeSpeed;
    public float fadeOutDelay;

    // Top text
    public TMP_Text topText;
    public GameObject balloonHolder;

    private void Start()
    {
        topText.text = "Vælg en ballon og se om du vinder!";
        balloons = new List<GameObject>();

        
        // Prize showcase set inactive
        prizeMessage.SetActive(false);
        FindBalloons();
    }

    IEnumerator FadeImage(bool fadeAway, Image image)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    void HandleIntro()
    {
        DisableGameUI();


        interSportLogo.color = new Color(1, 1, 1, 0); // set image to 0 opacity
         
        StartCoroutine(IntroSequence(fadeOutDelay));

    }
    IEnumerator IntroSequence(float seconds)
    {
        StartCoroutine(FadeImage(false, interSportLogo)); // true = fade out, false = fade in
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeImage(true, interSportLogo));
        yield return new WaitForSeconds(seconds);
        // Fade in balloons
        foreach(GameObject balloon in balloons) {
            StartCoroutine(FadeImage(false, balloon.GetComponent<Image>()));
        }
        yield return new WaitForSeconds(seconds);
        interSportLogo.gameObject.SetActive(false);
        GameManager.Instance.introFinished = true; // Can now click and expand balloons
        GameManager.Instance.UpdateGameState(GameState.Game);
    }

    void DisableGameUI()
    {
        prizeMessage.SetActive(false);
        prizeWindow.transform.gameObject.SetActive(false);

        // Set opacity to 0 for balloons
        foreach (GameObject balloon in balloons)
        {
            balloon.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            //balloon.gameObject.GetComponent<Button>().enabled = false;
        }
        topText.transform.gameObject.SetActive(false);
    }

    void HandleGame()
    {

    }
    
    void HandlePrize()
    {
        prizeWindow.transform.gameObject.SetActive(true);
        prizeMessage.SetActive(true);
        topText.text = "";

        for (int i = 0; i < balloons.Count; i++)
        {
            balloons[i].GetComponent<Button>().interactable = false;
        }
    }

    void FindBalloons()
    {
        // Add the blue ones
        foreach (GameObject blueballoons in GameObject.FindGameObjectsWithTag("BlueBalloon"))
        {
            Debug.Log(blueballoons.name);
            balloons.Add(blueballoons);
        }
        // Add the red balloons
        foreach (GameObject redballoons in GameObject.FindGameObjectsWithTag("RedBalloon"))
        {
            balloons.Add(redballoons);
        }

    }

}
