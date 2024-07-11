using UnityEngine;

public class OddScoringStrategy : IScoringStrategy
{
    [SerializeField] private int bonusMultiplier = 2;

    public int CalculateScore(string word, bool isBonus)
    {
        int score = word.Length * word.Length;
        return isBonus ? score * bonusMultiplier : score;
    }
}