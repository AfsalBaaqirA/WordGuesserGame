using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndScreen : MonoBehaviour
{
    [SerializeField] private GameObject endScreenWindow;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        if (continueButton == null)
        {
            Debug.LogWarning("Continue button not set in UIEndScreen");
            return;
        }
        continueButton.onClick.AddListener(ContinueButtonClick);
    }

    public void Show()
    {
        if (endScreenWindow == null)
        {
            Debug.LogWarning("End screen window not set in UIEndScreen");
            return;
        }
        endScreenWindow.SetActive(true);
    }

    public void Hide()
    {
        if (endScreenWindow == null)
        {
            Debug.LogWarning("End screen window not set in UIEndScreen");
            return;
        }
        endScreenWindow.SetActive(false);
    }

    public void ShowScore(int score)
    {
        if (scoreText == null)
        {
            Debug.LogWarning("Score text not set in UIEndScreen");
            return;
        }
        scoreText.text = score.ToString();
    }

    public void IsNewHighScore(bool newHighScore)
    {
        if (highScoreText == null)
        {
            Debug.LogWarning("High score text not set in UIEndScreen");
            return;
        }
        if (newHighScore)
        {
            highScoreText.gameObject.SetActive(true);
        }
    }

    public void ContinueButtonClick()
    {
        GameManager.Instance.ShowMainMenu();
    }
}
