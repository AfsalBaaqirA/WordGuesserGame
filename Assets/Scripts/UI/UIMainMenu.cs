using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuWindow;
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        if (playButton == null)
        {
            Debug.LogWarning("Play button not set in UIMainMenu");
            return;
        }
        if (leaderboardButton == null)
        {
            Debug.LogWarning("Leaderboard button not set in UIMainMenu");
            return;
        }
        if (quitButton == null)
        {
            Debug.LogWarning("Quit button not set in UIMainMenu");
            return;
        }

        playButton.onClick.AddListener(PlayButtonClick);
        leaderboardButton.onClick.AddListener(LeaderboardButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);
    }

    public void Show()
    {
        if (mainMenuWindow == null)
        {
            Debug.LogWarning("Main menu window not set in UIMainMenu");
            return;
        }

        mainMenuWindow.SetActive(true);
    }

    public void Hide()
    {
        if (mainMenuWindow == null)
        {
            Debug.LogWarning("Main menu window not set in UIMainMenu");
            return;
        }
        mainMenuWindow.SetActive(false);
    }

    public void PlayButtonClick()
    {
        GameManager.Instance.PlayGame();
    }

    public void LeaderboardButtonClick()
    {
        GameManager.Instance.ShowLeaderboard();
    }

    public void QuitButtonClick()
    {
        GameManager.Instance.QuitGame();
    }
}
