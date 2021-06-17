using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
  public class Game
  {
    private const int MaxLives = 7;
    private string _word = "pig";
    private readonly HashSet<char> _guesses;

    public Game()
    {
      _guesses = new HashSet<char>();
    }

    public void GenerateWord()
    {
      string[] lines = System.IO.File.ReadAllLines("words_alpha.txt");
      var rand = new Random();
      _word = lines[rand.Next(0, lines.Length)];
    }

    public string GetWord()
    {
      return _word;
    }

    public bool IsWon()
    {
      return _word.All(character => _guesses.Contains(character));
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
    }

    public int GetGuessesRemaining()
    {
      var invalidCount = 0;
      foreach (var guess in _guesses)
      {
        if (!_word.Contains(guess))
        {
          invalidCount++;
        }
      }
      return Math.Max(0, MaxLives-invalidCount);
    }

    public HashSet<char> GetGuesses()
    {
      return _guesses;
    }
  }
}