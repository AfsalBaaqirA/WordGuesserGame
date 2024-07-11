using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject gameWindow;
    [SerializeField] private GameObject timerSlider;
    [SerializeField] private GameObject lettersContainer;
    [SerializeField] private GameObject wordContainer;
    [SerializeField] private GameObject letter;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject wordFailedEffect;
    [SerializeField] private GameObject wordSucceededEffect;

    public void Show()
    {
        if (gameWindow == null)
        {
            Debug.LogWarning("Game window not set in UIGame");
            return;
        }
        gameWindow.SetActive(true);
    }

    public void Hide()
    {
        if (gameWindow == null)
        {
            Debug.LogWarning("Game window not set in UIGame");
            return;
        }
        gameWindow.SetActive(false);
    }

    public void UpdateTimer(float time)
    {
        if (timerSlider == null)
        {
            Debug.LogWarning("Timer slider not set in UIGame");
            return;
        }
        timerSlider.GetComponent<Slider>().value = time;
    }

    public void UpdateLettersContainer(string[] letterSet)
    {
        if (lettersContainer == null)
        {
            Debug.LogWarning("Letters container not set in UIGame");
            return;
        }
        for (int i = 0; i < letterSet.Length; i++)
        {
            GameObject letterObject = lettersContainer.transform.GetChild(i).gameObject;
            TMP_Text letterText = letterObject.GetComponentInChildren<TMP_Text>();
            letterText.text = letterSet[i];
            letterObject.SetActive(true);
        }
    }

    public void AddLetterToWordContainer(char character)
    {
        if (wordContainer == null)
        {
            Debug.LogWarning("Word container not set in UIGame");
            return;
        }
        if (letter == null)
        {
            Debug.LogWarning("Letter prefab not set in UIGame");
            return;
        }
        GameObject wordObject = Instantiate(letter, wordContainer.transform);
        wordObject.transform.SetParent(wordContainer.transform);
        TMP_Text wordText = wordObject.GetComponentInChildren<TMP_Text>();
        wordText.text = character.ToString().ToUpper();
    }

    public void RemoveLetterFromWordContainer()
    {
        if (wordContainer == null)
        {
            Debug.LogWarning("Word container not set in UIGame");
            return;
        }
        if (wordContainer.transform.childCount > 0)
        {
            Destroy(wordContainer.transform.GetChild(wordContainer.transform.childCount - 1).gameObject);
        }
    }

    public void ClearWordContainer()
    {
        if (wordContainer == null)
        {
            Debug.LogWarning("Word container not set in UIGame");
            return;
        }
        for (int i = 0; i < wordContainer.transform.childCount; i++)
        {
            GameObject letterObject = wordContainer.transform.GetChild(i).gameObject;
            TMP_Text letterText = letterObject.GetComponentInChildren<TMP_Text>();
            letterText.text = "";
            letterObject.SetActive(false);
        }
    }

    public void SubmitWord()
    {
        if (wordContainer == null)
        {
            Debug.LogWarning("Word container not set in UIGame");
            return;
        }
        string word = "";
        for (int i = 0; i < wordContainer.transform.childCount; i++)
        {
            GameObject wordObject = wordContainer.transform.GetChild(i).gameObject;
            TMP_Text wordText = wordObject.GetComponentInChildren<TMP_Text>();
            word += wordText.text;
        }
        GameManager.Instance.SubmitWord(word);
    }

    public void UpdateScore(int score)
    {
        if (scoreText == null)
        {
            Debug.LogWarning("Score text not set in UIGame");
            return;
        }
        scoreText.text = score.ToString();
    }

    public void ShowWordSucceededEffect()
    {
        if (wordSucceededEffect == null)
        {
            Debug.LogWarning("Word succeeded effect not set in UIGame");
            return;
        }
        wordSucceededEffect.SetActive(true);
        Invoke("HideWordSucceededEffect", 0.1f);
        ClearWordContainer();
    }

    public void HideWordSucceededEffect()
    {
        wordSucceededEffect.SetActive(false);
    }

    public void ShowWordFailedEffect()
    {
        if (wordFailedEffect == null)
        {
            Debug.LogWarning("Word failed effect not set in UIGame");
            return;
        }
        wordFailedEffect.SetActive(true);
        Invoke("HideWordFailedEffect", 0.1f);
        ClearWordContainer();
    }

    public void HideWordFailedEffect()
    {
        wordFailedEffect.SetActive(false);
    }
}
