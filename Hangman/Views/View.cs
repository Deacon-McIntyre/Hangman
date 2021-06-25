﻿using System;
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
      var guessesRemaining = _game.GetGuessesRemaining();
      var answer = _game.GetWord();
      var guessesSoFar = _game.GetGuesses();

      var stringBuilder = new StringBuilder();

      foreach (var character in answer)
      {
        stringBuilder.Append(guessesSoFar.Any(g => g.Character == character) ? character : '_');
      }

      stringBuilder.Append($" | You have {guessesRemaining} lives left");

      var incorrectGuesses = string.Join(", ",  _game.GetInvalidGuesses().Select(g => g.Character));

      if (!string.IsNullOrEmpty(incorrectGuesses))
      {
        stringBuilder.Append($" | Incorrect guesses so far: {incorrectGuesses}");
      }

      Console.WriteLine(stringBuilder.ToString());
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
        Console.WriteLine($"The word was {_game.GetWord()}");
      }
    }

    public void DisplayDuplicateGuess()
    {
      Console.WriteLine("You've guessed that already!");
    }
  }
}