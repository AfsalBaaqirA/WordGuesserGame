using UnityEngine;

public class WordInputManager : MonoBehaviour
{
    public static WordInputManager Instance { get; private set; }
    private GameManager gameManager;
    private UIManager uiManager;

    void Awake()
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

    void Start()
    {
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
    }
    // Handles player inputs
    void Update()
    {
        if (gameManager.GetCurrentState() != GameManager.GameState.Playing)
        {
            return;
        }
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                uiManager.AddLetterToWordContainer(c);
            }
            else if (c == '\b') // Backspace
            {
                uiManager.RemoveLetterFromWordContainer();
            }
            else if (c == '\r' || c == '\n') // Enter
            {
                uiManager.SubmitWord();
            }
        }
    }
}
