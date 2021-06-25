using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Hangman.Framework.Models;

namespace Hangman.Models
{
  public class Game : PersistenceModelBase
  {
    private const int MaxLives = 7;
    [JsonInclude]
    public string TargetWord;
    [JsonInclude]
    public HashSet<char> Guesses;

    /// <summary>
    /// Required for JSON serialization/deserialization
    /// </summary>
    public Game()
    {
    }

    public Game(string targetWord)
    {
      Guesses = new HashSet<char>();
      TargetWord = targetWord;
    }

    public string GetTargetWord()
    {
      return TargetWord;
    }

    public bool IsWon()
    {
      return TargetWord.All(character => Guesses.Contains(character));
    }

    public bool IsLost()
    {
      return !IsWon() && GetGuessesRemaining() == 0;
    }

    public bool IsInPlay()
    {
      return !IsWon() && !IsLost();
    }

    public GuessResult IsValidGuess(string guess, out char validChar)
    {
      if (char.TryParse(guess, out var guessChar) && char.IsLetter(guessChar))
      {
        validChar = char.ToLower(guessChar);
        return Guesses.Contains(validChar) ? GuessResult.Duplicate : GuessResult.Valid;
      }
      else
      {
        validChar = default;
        return GuessResult.Invalid;
      }
    }

    public char[] GetFilledOutAnswer()
    {
      var result = new char[TargetWord.Length];
      for (var i = 0; i < TargetWord.Length; i++)
      {
        var letter = TargetWord[i];
        
        result[i] = Guesses.Contains(letter) ? letter : default;
      }

      return result;
    }

    public char[] GetIncorrectGuesses()
    {
      return null;
    } 

    public void SubmitGuess(char guess)
    {
      var lowercaseGuess = char.ToLower(guess);
      Guesses.Add(lowercaseGuess);
    }

    public int GetGuessesRemaining()
    {
      var incorrectCount = Guesses.Count(guess => !TargetWord.Contains(guess));

      return MaxLives - incorrectCount;
    }

    public HashSet<char> GetGuesses()
    {
      return Guesses;
    }

    public void HandlePersistence()
    {
      if (!IsInPlay())
      {
        Reset();
      }
      else
      {
        Save(this);
      }
    }

    public static Game LoadExistingGame()
    {
      return Load<Game>();
    }
  }
}