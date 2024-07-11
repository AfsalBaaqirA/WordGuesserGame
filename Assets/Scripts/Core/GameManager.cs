using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { MainMenu, Playing, GameOver, Leaderboard }
    public static GameManager Instance { get; private set; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private WordGenerator wordGenerator;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private LeaderboardManager leaderboardManager;

    private GameState currentState;
    private float gameTimer;
    private const float GameDuration = 60f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TransitionToState(GameState.MainMenu);
    }

    // Handles the timer and game over trigger
    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            gameTimer -= Time.deltaTime;
            if (uiManager != null)
            {
                uiManager.UpdateTimer(gameTimer);
            }
            else
            {
                Debug.LogWarning("UIManager not set in GameManager");
            }
            if (gameTimer <= 0)
            {
                GameOver();
            }
        }
    }

    // Called for the Play button in the main menu
    public void PlayGame()
    {
        TransitionToState(GameState.Playing);
    }

    // Called when the state changes to Playing 
    public void StartNewGame()
    {
        if (uiManager == null)
        {
            Debug.LogWarning("UIManager not set in GameManager");
            return;
        }
        if (wordGenerator == null)
        {
            Debug.LogWarning("WordGenerator not set in GameManager");
            return;
        }
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not set in GameManager");
            return;
        }
        scoreManager.ResetScore();
        wordGenerator.GenerateNewLetterSet();
        uiManager.ShowGameUI();
        gameTimer = GameDuration;
    }
    // Handles the word submission, score calculation, and UI updates
    public void SubmitWord(string word)
    {
        if (wordGenerator == null)
        {
            Debug.LogWarning("WordGenerator not set in GameManager");
            return;
        }
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not set in GameManager");
            return;
        }
        if (uiManager == null)
        {
            Debug.LogWarning("UIManager not set in GameManager");
            return;
        }
        if (!wordGenerator.IsValidWord(word))
        {
            uiManager.ShowWordFailedEffect();
            return;
        }
        bool isBonusWord = wordGenerator.IsBonusWord(word);
        int score = scoreManager.CalculateWordScore(word, isBonusWord);
        scoreManager.AddScore(score);
        uiManager.UpdateScore(scoreManager.CurrentScore);
        uiManager.ShowWordSucceededEffect(isBonusWord);
    }
    // Called by Quit button in the main menu
    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    // Called when the game timer reaches 0
    public void GameOver()
    {
        TransitionToState(GameState.GameOver);
    }
    // Called by Leaderboard button in the main menu
    public void ShowLeaderboard()
    {
        if (uiManager == null)
        {
            Debug.LogWarning("UIManager not set in GameManager");
            return;
        }
        TransitionToState(GameState.Leaderboard);
        uiManager.ShowLeaderboard();
    }
    // Called by the Continue button in the leaderboard screen, end screen and at start of the game
    public void ShowMainMenu()
    {
        TransitionToState(GameState.MainMenu);
    }
    // Called when the GameState is GameOver
    public void EndGame()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not set in GameManager");
            return;
        }
        if (leaderboardManager == null)
        {
            Debug.LogWarning("LeaderboardManager not set in GameManager");
            return;
        }
        if (uiManager == null)
        {
            Debug.LogWarning("UIManager not set in GameManager");
            return;
        }
        int finalScore = scoreManager.CurrentScore;
        bool newHighScore = leaderboardManager.AddScore(finalScore);
        uiManager.ShowGameOverScreen(newHighScore, finalScore);
    }
    // Changes the current state
    private void TransitionToState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                uiManager.ShowMainMenu();
                break;
            case GameState.Playing:
                StartNewGame();
                break;
            case GameState.GameOver:
                EndGame();
                break;
            case GameState.Leaderboard:
                break;
        }
    }
    // Returns the current GameState
    public GameState GetCurrentState()
    {
        return currentState;
    }
}