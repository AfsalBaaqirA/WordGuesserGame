using UnityEngine;
public class LinearScoringStrategy : IScoringStrategy
{
    [SerializeField] private int pointsPerLetter = 1;
    [SerializeField] private int bonusMultiplier = 3;

    public int CalculateScore(string word, bool isBonus)
    {
        int score = word.Length * pointsPerLetter;
        return isBonus ? score * bonusMultiplier : score;
    }
}