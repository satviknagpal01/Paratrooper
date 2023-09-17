using UnityEngine;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject IntroUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject menu;

    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private TMP_Text gameOverScore;
    [SerializeField] private TMP_Text gameOverHighScore;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
        GameManager.OnScoreChanged += ScoreChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnScoreChanged -= ScoreChanged;
        GameManager.OnGameStateChanged -= GameStateChanged;
    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                OpenMainMenu();
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
            default:
                throw new System.ArgumentOutOfRangeException(nameof(state), state, null);
        }

    }

    private void OpenMainMenu()
    {
        menu.SetActive(true);
        mainMenu.SetActive(true);
        //pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(false);
    }

    private void Intro()
    {
        menu.SetActive(true);
        mainMenu.SetActive(false);
        //pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(true);
    }

    private void PlayGame()
    {
        mainMenu.SetActive(false);
        //pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(false);
        menu.SetActive(false);
    }

    private void PauseGame()
    {
        menu.SetActive(true);
        mainMenu.SetActive(false);
        //pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(false);
    }

    private void GameOver()
    {
        menu.SetActive(true);
        mainMenu.SetActive(false);
        //pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        IntroUI.SetActive(false);
        gameOverScore.text = "Your Score : " + GameManager.instance.score;
        gameOverHighScore.text = "High Score : " + GameManager.instance.highScore;
    }
    private void ScoreChanged(int obj)
    {
        score.text = "SCORE : " + obj;
    }
}
