using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<int> OnScoreChanged;

    public int score;
    public int highScore;

    [SerializeField] private GameObject mainGame;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (Constants.IsFirstEnemyDead)
        {
            score += scoreToAdd;
            OnScoreChanged?.Invoke(score);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Intro:
                Intro();
                break;
            case GameState.InGame:
                PlayGame();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.Quit:
                Application.Quit();
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void Intro()
    {
        mainGame.SetActive(false);
    }

    private void GameOver()
    {
        mainGame.SetActive(false);
        Reset();
    }

    private void PauseGame()
    {
        mainGame.SetActive(false);
    }

    private void PlayGame()
    {
        mainGame.SetActive(true);
    }

    private void MainMenu()
    {
        mainGame.SetActive(false);
    }

    private void Reset()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
        BulletPoolController.instance.Reset();
        EnemyController.instance.Reset();
        HelicopterController.instance.Reset();
    }
}
