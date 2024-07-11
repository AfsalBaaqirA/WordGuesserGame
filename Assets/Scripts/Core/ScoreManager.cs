using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int CurrentScore { get; private set; }

    [SerializeField] private IScoringStrategy scoringStrategy;


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
        ResetScore();
#if LINEAR_SCORING_STRATEGY
        scoringStrategy = new LinearScoringStrategy();
        Debug.Log("Using Linear Scoring Strategy");
#else
        scoringStrategy = new OddScoringStrategy();
        Debug.Log("Using Odd Scoring Strategy");
#endif
    }

    public int CalculateWordScore(string word, bool isBonus)
    {
        return scoringStrategy.CalculateScore(word, isBonus);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }

    public void AddScore(int score)
    {
        if (score > 0)
        {
            CurrentScore += score;
        }
    }
}
