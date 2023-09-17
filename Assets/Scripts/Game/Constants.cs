public enum EnemyStates
{
    Parachuting,
    Falling,
    Idle,
    Attacking,
    TouchingBase,
    Dying
}

public enum GameState
{
    MainMenu,
    Intro,
    InGame,
    Paused,
    GameOver,
    Quit
}

public class Constants
{
    public static bool IsFirstEnemyDead = false;
}