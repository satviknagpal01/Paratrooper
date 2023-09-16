using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject IntroUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject menu;

    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text highScore;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
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
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(false);
    }

    private void Intro()
    {
        menu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(true);
    }

    private void PlayGame()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(false);
        menu.SetActive(false);
    }

    private void PauseGame()
    {
        menu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        IntroUI.SetActive(false);
    }

    private void GameOver()
    {
        menu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        IntroUI.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.InGame);
    }

    public void OnPauseButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.Paused);
    }

    public void OnResumeButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.InGame);
    }

    public void OnRestartButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.InGame);
    }

    public void OnQuitButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.MainMenu);
    }

    public void OnQuitToMenuButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.MainMenu);
    }

    public void OnQuitToDesktopButtonClicked()
    {
        Application.Quit();
    }

    public void OnIntroButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.Intro);
    }

    public void OnIntroSkipButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.MainMenu);
    }

    public void OnGameOverButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameState.GameOver);
    }


}
