using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardWindow;
    [SerializeField] private GameObject highScoreContainer;
    [SerializeField] private GameObject highScoreText;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        if (continueButton == null)
        {
            Debug.LogWarning("Continue button not set in UILeaderboard");
            return;
        }
        GetHighScore();
        continueButton.onClick.AddListener(ContinueButtonClick);
    }

    public void Show()
    {
        if (leaderboardWindow == null)
        {
            Debug.LogWarning("Leaderboard window not set in UILeaderboard");
            return;
        }
        GetHighScore();
        leaderboardWindow.SetActive(true);
    }

    public void Hide()
    {
        if (leaderboardWindow == null)
        {
            Debug.LogWarning("Leaderboard window not set in UILeaderboard");
            return;
        }
        leaderboardWindow.SetActive(false);
    }

    public void ContinueButtonClick()
    {
        GameManager.Instance.ShowMainMenu();
    }

    private void GetHighScore()
    {
        if (highScoreContainer == null)
        {
            Debug.LogWarning("High score container not set in UILeaderboard");
            return;
        }
        ClearHighScoreContainer();
        List<int> highScores = LeaderboardManager.Instance.GetHighScores();
        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject score = Instantiate(highScoreText, highScoreContainer.transform);
            score.transform.SetParent(highScoreContainer.transform);
            score.GetComponent<TextMeshProUGUI>().text = highScores[i].ToString();

        }
    }
    private void ClearHighScoreContainer()
    {

        foreach (Transform child in highScoreContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
