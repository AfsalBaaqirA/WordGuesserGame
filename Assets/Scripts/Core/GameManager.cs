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

    public void PlayGame()
    {
        TransitionToState(GameState.Playing);
    }

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

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void GameOver()
    {
        TransitionToState(GameState.GameOver);
    }

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

    public void ShowMainMenu()
    {
        TransitionToState(GameState.MainMenu);
    }

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

    public GameState GetCurrentState()
    {
        return currentState;
    }
}