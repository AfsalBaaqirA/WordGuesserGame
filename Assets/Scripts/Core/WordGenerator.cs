using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    public static WordGenerator Instance { get; private set; }

    private readonly char[] Alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private HashSet<string> validWords;
    private HashSet<string> foundWords;
    private List<string> bonusWords = new List<string>();
    private string[] letterSet;

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

        LoadDictionary();
    }

    private void LoadDictionary()
    {
        Debug.Log("Loading dictionary...");
        string dictionaryPath = Path.Combine(Application.streamingAssetsPath, "dictionary.txt");
        if (File.Exists(dictionaryPath))
        {
            validWords = new HashSet<string>(File.ReadAllLines(dictionaryPath));
            Debug.Log("Dictionary loaded with " + validWords.Count + " words.");
        }
        else
        {
            Debug.LogError("Dictionary file not found at " + dictionaryPath);
        }
    }

    public void GenerateNewLetterSet()
    {
        System.Random random = new System.Random();
        char[] letters = (char[])Alphabets.Clone();
        int length = random.Next(4, 10); // Length of the array of random letters, between 4 and 9

        // Fisher-Yates shuffle algorithm to shuffle the letters array
        for (int i = letters.Length - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            char temp = letters[i];
            letters[i] = letters[swapIndex];
            letters[swapIndex] = temp;
        }

        // Take the first 'length' elements from the shuffled letters array
        letterSet = letters.Take(length).Select(c => c.ToString()).ToArray();
        Debug.Log("Generated new letter set: " + string.Join(", ", letterSet));

        UIManager.Instance.SetLetterSet(letterSet);
        foundWords = new HashSet<string>();
    }


    public bool IsValidWord(string word)
    {
        if (validWords.Contains(word.ToLower()) && !foundWords.Contains(word.ToLower()))
        {
            foundWords.Add(word.ToLower());
            return true;
        }
        return false;
    }

    public bool IsBonusWord(string word)
    {
        return bonusWords.Contains(word.ToLower());
    }

}
