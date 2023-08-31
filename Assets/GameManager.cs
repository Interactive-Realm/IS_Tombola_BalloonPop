using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Creates an instance of Game Manager
    public static GameManager Instance { get; private set; }
    

    // GameState Variables
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    public bool hasClicked;

    private List<GameObject> balloons;
    public TMP_Text _prizeText;

    // Prize frame and image
    public GameObject prizeMessage;
    public Button clickWindow;

    // Top text
    private TMP_Text topText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
        _prizeText = GameObject.Find("PrizeText").GetComponent<TMP_Text>();
        topText = GameObject.Find("TopText").GetComponent<TMP_Text>();
        topText.text = "Vælg en ballon og se om du vinder!";
    }
    private void Start()
    {
        UpdateGameState(GameState.Game);
        balloons = new List<GameObject>();

        // Prize showcase set inactive
        prizeMessage.SetActive(false);

        // Prizewindow as button to restart scene
        //clickWindow.onClick.AddListener(RestartScene);


        FindBalloons();
    }
    private void Update()
    {
        if(hasClicked == true)
        {
            UpdateGameState(GameState.Prize);
        }
    }
    void FindBalloons()
    {
        // Add the blue ones
        foreach(GameObject blueballoons in GameObject.FindGameObjectsWithTag("BlueBalloon"))
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

    void RestartScene()
    {
        SceneManager.LoadScene(0);
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Game:
                HandleGame();
                break;
            case GameState.Prize:
                HandlePrize();
                break;



            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
    public void HandleGame()
    {
        hasClicked = false;
    }
    public void HandlePrize()
    {
        prizeMessage.SetActive(true);
        topText.text = "";

        for (int i = 0; i < balloons.Count; i++)
        {
            balloons[i].GetComponent<Button>().interactable = false;
        }
    }
    public enum GameState
    {
        Game,
        Prize
    }
}
