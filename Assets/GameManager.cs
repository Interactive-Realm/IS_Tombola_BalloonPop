using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private List<GameObject> balloons;
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        UpdateGameState(GameState.Game);
        balloons = new List<GameObject>();
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
        for (int i = 0; i < balloons.Count; i++)
        {
            balloons[i].SetActive(false);
        }
    }
    public enum GameState
    {
        Game,
        Prize
    }
}
