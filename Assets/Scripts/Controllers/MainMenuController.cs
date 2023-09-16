using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private Controls controls;

    void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.MainMenu.Start.performed += ctx => StartGame();
        controls.MainMenu.Instructions.performed += ctx => ShowInstructions();
        controls.MainMenu.Quit.performed += ctx => QuitGame();
    }

    private void ShowInstructions()
    {
        GameManager.instance.UpdateGameState(GameState.Intro);
    }

    private void QuitGame()
    {
        GameManager.instance.UpdateGameState(GameState.Quit);
    }

    private void StartGame()
    {
        GameManager.instance.UpdateGameState(GameState.InGame);
    }
}
