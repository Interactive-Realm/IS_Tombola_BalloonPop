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
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    // GameState Variables
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    public bool hasClicked;

    public bool introFinished = false;

    public bool poppedBalloon = false;






    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        // Initial Game State
        UpdateGameState(GameState.Intro);


        

        // Prizewindow as button to restart scene
        //clickWindow.onClick.AddListener(RestartScene);


        
    }
    private void Update()
    {
        if(hasClicked == true)
        {
            UpdateGameState(GameState.Prize);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

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

        OnGameStateChanged?.Invoke(newState);
    }

    void HandleIntro()
    {
        introFinished = false;
    }
    public void HandleGame()
    {
        introFinished = true;
        hasClicked = false;
    }
    public void HandlePrize()
    {
        
    }
    

    void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
public enum GameState
{
    Intro,
    Game,
    Prize
}
