using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangman.Models;

namespace Hangman.Views
{
  public class View
  {
    private readonly Game _game;

    public View(Game game)
    {
      _game = game;
    }

    public void DisplayWelcomeMessage()
    {
      Console.WriteLine("Welcome to Hangman!");
    }

    public void DisplayInstructions()
    {
      Console.WriteLine("Try to guess the word! You start with 7 lives. Guess a character each turn, incorrect guesses lose you a life!");
    }

    public void DisplayGameState()
    {
      var stringBuilder = new StringBuilder();
      
      AppendAnswerProgress(stringBuilder);
      AppendLivesLeft(stringBuilder);
      AppendIncorrectGuesses(stringBuilder);

      Console.WriteLine(stringBuilder.ToString());
    }

    private void AppendAnswerProgress(StringBuilder sb)
    {
      var filledOutAnswer = _game.GetFilledOutAnswer();

      foreach (var character in filledOutAnswer)
      {
        sb.Append(character != default ? character : '_');
      }
    }

    private void AppendLivesLeft(StringBuilder sb)
    {
      int guessesRemaining = _game.GetGuessesRemaining();
      sb.Append($" | You have {guessesRemaining} lives left");
    }

    private void AppendIncorrectGuesses(StringBuilder sb)
    {
      var incorrectGuesses = string.Join(", ",  _game.GetInvalidGuesses().Select(g => g.Character));

      if (!string.IsNullOrEmpty(incorrectGuesses))
      {
        sb.Append($" | Incorrect guesses so far: {incorrectGuesses}");
      }
    }

    public Guess AskForGuess()
    {
      Console.WriteLine("Guess a character: ");
      char character = Console.ReadKey(true).KeyChar;

      return new Guess(character);
    }

    public void DisplayInvalidGuess()
    {
      Console.WriteLine("That guess is invalid. Please enter a single letter");
    }

    public void DisplayGameOutcome()
    {
      if (_game.IsWon())
      {
        Console.WriteLine("You won!");
      }

      if (_game.IsLost())
      {
        Console.WriteLine("You lost :(");
        Console.WriteLine($"The word was {_game.GetTargetWord()}");
      }
    }

    public void DisplayDuplicateGuess()
    {
      Console.WriteLine("You've guessed that already!");
    }
  }
}