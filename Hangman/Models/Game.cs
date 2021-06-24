using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman.Models
{
  public class Game : BasePersistable
  {
    private const int MaxLives = 7;
    private string _targetWord;
    private HashSet<char> _guesses;

    public Game(string targetWord)
    {
      _targetWord = targetWord;

      _guesses = new HashSet<char>();
    }

    public string TargetWord
    {
      get { return _targetWord; }
      set { _targetWord = value; }
    }

    public IEnumerable<char> Guesses
    {
      get { return new List<char>(_guesses);}
      set { _guesses = new HashSet<char>(value);}
    }

    public string GetWord()
    {
      return _targetWord;
    }

    public bool IsWon()
    {
      return _targetWord.All(character => _guesses.Contains(character));
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
        validChar = guessChar;
        return _guesses.Contains(guessChar) ? GuessResult.Duplicate : GuessResult.Valid;
      }
      else
      {
        validChar = default;
        return GuessResult.Invalid;
      }
    }

    public void SubmitGuess(char guess)
    {
      var lowercaseGuess = char.ToLower(guess);
      _guesses.Add(lowercaseGuess);

      Save();
    }

    public int GetGuessesRemaining()
    {
      var invalidCount = 0;
      foreach (var guess in _guesses)
      {
        if (!_targetWord.Contains(guess))
        {
          invalidCount++;
        }
      }

      return Math.Max(0, MaxLives - invalidCount);
    }

    public HashSet<char> GetGuesses()
    {
      return _guesses;
    }
  }
}