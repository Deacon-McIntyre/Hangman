using System;
using System.Linq;

namespace Hangman
{
  public class View
  {
    private readonly Game _game;
    public View(Game game)
    {
      _game = game;
    }

    public void Welcome()
    {
      Console.WriteLine("Welcome to Hangman!");
    }

    public void DisplayInstructions()
    {
      Console.WriteLine("Try to guess the word! You start with 7 lives. Guess a character each turn, incorrect guesses lose you a life!");
    }

    public void DisplayGameState()
    {
      var guessesRemaining = _game.GetGuessesRemaining();
      var target = _game.GetWord();
      var guessesSoFar = _game.GetGuesses();
      foreach (var character in target)
      {
        if (guessesSoFar.Contains(character))
        {
          Console.Write(character);
        }
        else
        {
          Console.Write("_");
        }
      }

      var guessesSoFarString = string.Join(", ", guessesSoFar.Except(target));
      Console.Write($"   You have {guessesRemaining} lives left");
      if (!string.IsNullOrEmpty(guessesSoFarString))
      {
        Console.Write($". Incorrect guesses so far: {guessesSoFarString}");
      }
      
      
      Console.WriteLine();
    }

    public string AskForGuess()
    {
      Console.WriteLine("Guess a character: ");
      return Console.ReadLine();
    }

    public void DisplayInvalidGuess()
    {
      Console.WriteLine("That guess is invalid. Please enter a single letter");
    }

    public void DisplayGameOutcome()
    {
      if(_game.IsWon())
      {
        Console.WriteLine("You won!");
      }
      if(_game.IsLost())
      {
        Console.WriteLine("You lost :(");
        Console.WriteLine($"The word was {_game.GetWord()}");
      }
    }

    public void DisplayDuplicateGuess()
    {
      Console.WriteLine("You've guessed that already!");
    }
  }
}