using UnityEngine;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    private List<int> highScores = new List<int>();

    private string highScorePath = Application.streamingAssetsPath + "/highscores.txt";

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
        FetchHighScores();
    }
    // Logic to add new score to the list
    public bool AddScore(int score)
    {
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }
        if (highScores.Contains(score) && highScores.IndexOf(score) == 0)
        {
            return true;
        }
        return false;
    }

    public List<int> GetHighScores()
    {
        return new List<int>(highScores);
    }
    // Read high scores from file
    private void FetchHighScores()
    {
        if (System.IO.File.Exists(highScorePath))
        {
            string[] scores = System.IO.File.ReadAllLines(highScorePath);
            foreach (string score in scores)
            {
                highScores.Add(int.Parse(score));
            }
        }
    }
    // Save high scores to file
    private void SaveHighScores()
    {
        System.IO.File.WriteAllLines(highScorePath, highScores.ConvertAll(x => x.ToString()).ToArray());
    }
    // Called when the application is closed to save high scores
    private void OnApplicationQuit()
    {
        SaveHighScores();
    }
}
