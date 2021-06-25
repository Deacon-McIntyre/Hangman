using System;
using System.Collections.Generic;
using System.Linq;

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

    public string GetWord()
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

      Save();
    }

    public int GetGuessesRemaining()
    {
      var invalidCount = _guesses.Count(g => !TargetWord.Contains(g.Character));

      return Math.Max(0, MaxLives - invalidCount);
    }

    public IEnumerable<Guess> GetInvalidGuesses()
    {
      return _guesses.Where(g => !TargetWord.Contains(g.Character));
    }

    public HashSet<Guess> GetGuesses()
    {
      return _guesses;
    }
  }
}