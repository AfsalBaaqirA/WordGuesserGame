using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIGame uiGame;
    [SerializeField] private UIEndScreen uiEndScreen;
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UILeaderboard uiLeaderboard;


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

    public void ShowMainMenu()
    {
        if (!checkSerializedFields())
        {
            return;
        }
        uiMainMenu.Show();
        uiGame.Hide();
        uiEndScreen.Hide();
        uiLeaderboard.Hide();
    }

    public void ShowGameOverScreen(bool newHighScore, int score)
    {
        if (!checkSerializedFields())
        {
            return;
        }
        uiEndScreen.ShowScore(score);
        uiEndScreen.IsNewHighScore(newHighScore);
        uiEndScreen.Show();
        uiGame.Hide();
        uiMainMenu.Hide();
        uiLeaderboard.Hide();
    }

    public void ShowGameUI()
    {
        if (!checkSerializedFields())
        {
            return;
        }
        uiGame.Show();
        uiEndScreen.Hide();
        uiMainMenu.Hide();
        uiLeaderboard.Hide();
    }

    public void ShowLeaderboard()
    {
        if (!checkSerializedFields())
        {
            return;
        }
        uiLeaderboard.Show();
        uiGame.Hide();
        uiEndScreen.Hide();
        uiMainMenu.Hide();
    }

    public void UpdateTimer(float time)
    {
        uiGame.UpdateTimer(time);
    }

    public void SetLetterSet(string[] letterSet)
    {
        uiGame.UpdateLettersContainer(letterSet);
    }
    public void AddLetterToWordContainer(char character)
    {
        uiGame.AddLetterToWordContainer(character);

    }

    public void RemoveLetterFromWordContainer()
    {
        uiGame.RemoveLetterFromWordContainer();
    }

    public void SubmitWord()
    {
        uiGame.SubmitWord();
    }

    public void UpdateScore(int score)
    {
        uiGame.UpdateScore(score);
    }

    public void ShowWordSucceededEffect(bool isBonusWord)
    {
        uiGame.ShowWordSucceededEffect(isBonusWord);
    }


    public void ShowWordFailedEffect()
    {
        uiGame.ShowWordFailedEffect();
    }

    public bool checkSerializedFields()
    {
        if (uiGame == null)
        {
            Debug.LogWarning("UIGame not set in UIManager");
            return false;
        }
        if (uiEndScreen == null)
        {
            Debug.LogWarning("UIEndScreen not set in UIManager");
            return false;
        }
        if (uiMainMenu == null)
        {
            Debug.LogWarning("UIMainMenu not set in UIManager");
            return false;
        }
        if (uiLeaderboard == null)
        {
            Debug.LogWarning("UILeaderboard not set in UIManager");
            return false;
        }
        return true;
    }
}
