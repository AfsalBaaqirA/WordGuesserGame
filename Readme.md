# Word Guesser Game
## Overview
Word Guesser Game is a Unity-based game where players generate words from a set of randomly generated letters. The game checks word validity against a predefined dictionary and calculates scores based on word length and specific bonus word criteria.

## Project Structure
```
Assets/
├── Prefabs/
│   ├── EndScreenWindow.prefab
│   ├── GameWindow.prefab
│   ├── HighscoreText.prefab
│   ├── LeaderboardWindow.prefab
│   ├── Letter.prefab
│   └── MainMenuWindow.prefab
├── Scenes/
│   └── MainScene.unity
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── LeaderboardManager.cs
│   │   ├── ScoreManager.cs
│   │   ├── WordGenerator.cs
│   │   └── WordInputManager.cs
│   ├── Scoring/
│   │   ├── IScoringStrategy.cs
│   │   ├── LinearScoringStrategy.cs
│   │   └── OddScoringStrategy.cs
│   └── UI/
│       ├── UIEndScreen.cs
│       ├── UIGame.cs
│       ├── UILeaderboard.cs
│       ├── UIMainMenu.cs
│       └── UIManager.cs
├── StreamingAssets/
│   ├── dictionary.txt
│   └── highscores.txt

```

## Features
### Menu
The main menu provides the following options:
- Play: Starts a new game round.
- Leaderboard: Displays the top 5 high scores.
- Quit: Exits the application (in builds) or stops Play Mode (in the Unity editor).
### Game Round
Each game round consists of the following features:
- Letter Set Generation: A random set of 4 to 9 letters from the English alphabet is generated at the start of each round.
- Timer: A 60-second countdown timer starts once the letters are generated.
- Word Entry: Players can input words using the generated letters.
- Word Validation: Entered words are checked against a provided dictionary.
    - Dictionary is implemented as HashSet for fast word lookup `O(1)`.
    - Valid words are scored based on their length.
    - Invalid words are rejected, and no score is added.
    - Already entered words are rejected, and no score is added.
- Bonus Words: Three random valid bonus words are selected for each round. Bonus words yield higher scores.
- User is notified when their word is valid, invalid, and bonus using different screen flashes.
- End of Round: The round ends when the timer reaches zero. The player is shown their score and option to return to Main Menu
## Scoring Systems:
The game supports two scoring systems:
- Odd Number Scoring System: Default scoring system, awarding sum of N odd numbers, where N is the length of the word and bonus of 2 points for bonus words.
    - Example: "word" = 1 + 3 + 5 + 7 = 16 points
    - Double the points for bonus words.
- Linear Scoring System: Optional scoring system giving one point per letter, with bonus words yielding triple points.
    - Example: "word" = 4 points
    - Triple the points for bonus words.

The scoring system can be changed by adding/removing `LINEAR_SCORING_SYSTEM` define in the Unity Symbol Defines.

## Known Issues
- The game does not end when the player enters all possible words that can be generated from the LetterSet.

## Bonus Words
Bonus words are special words that yield higher scores. Each round has three bonus words. Bonus words are calculated based on the following criteria:
- Each word the user enters has a 50/50 chance of being a bonus word, when the word is valid.
## Leaderboard
- Shows the top 5 high scores.
- Scores persist between game sessions.
- High scores are saved to a file on quit and loaded at the start of the game.
- Notifies the player if they achieve a high score and adds it to the leaderboard.