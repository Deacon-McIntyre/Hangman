using System;
using System.Collections.Generic;
using System.Linq;
using Hangman.Framework.Models;

namespace Hangman.Models
{
  public class Game : BasePersistable
  {
    private const int MaxLives = 7;

    private HashSet<Guess> _guesses;

    public Game(string targetWord)
    {
      TargetWord = targetWord;

      _guesses = new HashSet<Guess>();
    }

    public string TargetWord { get; set; }

    public IEnumerable<Guess> Guesses
    {
      get => new List<Guess>(_guesses);
      set => _guesses = new HashSet<Guess>(value);
    }

    public string GetTargetWord()
    {
      return TargetWord;
    }

    public bool IsWon()
    {
      return TargetWord.All(character => _guesses.Any(g => g.Character == character));
    }

    public bool IsLost()
    {
      return GetGuessesRemaining() == 0;
    }

    public bool IsInPlay()
    {
      return !IsWon() && !IsLost();
    }

    public bool HasAlreadyBeenGuessed(Guess guess)
    {
      return _guesses.Any(g => g.Character == guess.Character);
    }

    public void SubmitGuess(Guess guess)
    {
      _guesses.Add(guess);
    }

    public int GetGuessesRemaining()
    {
      var invalidCount = _guesses.Count(g => !TargetWord.Contains(g.Character));

      return MaxLives - invalidCount;
    }
    
    public char[] GetFilledOutAnswer()
    {
      var result = new char[TargetWord.Length];
      for (var i = 0; i < TargetWord.Length; i++)
      {
        var letter = TargetWord[i];

        result[i] = Guesses.Select(g => g.Character).Contains(letter) ? letter : default;
      }

      return result;
    }

    public IEnumerable<Guess> GetInvalidGuesses()
    {
      return _guesses.Where(g => !TargetWord.Contains(g.Character));
    }

    public void HandlePersistence()
    {
      if (!IsInPlay())
      {
        Reset();
      }
      else
      {
        Save();
      }
    }

    public static Game LoadExisting()
    {
      return Load<Game>();
    }
  }
}