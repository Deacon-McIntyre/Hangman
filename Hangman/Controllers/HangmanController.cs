using System;
using Hangman.Models;
using Hangman.Services;
using Hangman.Views;

namespace Hangman.Controllers
{
  public class HangmanController
  {
    private const string WordsFilename = "words_alpha.txt";

    private readonly View _view;
    private readonly Game _game;

    public HangmanController(Game game, View view)
    {
      _game = game;
      _view = view;
    }

    public void Start()
    {
      DisplayGameStart();

      var generateWordService = new GenerateWordFromFile(WordsFilename);
      var word = generateWordService.Run();

      _game.SetWord(word);

      while (_game.IsInPlay())
      {
        PlayTurn();
      }

      _view.DisplayGameOutcome();
    }

    private void DisplayGameStart()
    {
      _view.DisplayWelcomeMessage();
      _view.DisplayInstructions();
      _view.DisplayGameState();
    }

    private void PlayTurn()
    {
      var guess = _view.AskForGuess();
      var result = _game.IsValidGuess(guess, out char guessCharacter);
      switch (result)
      {
        case GuessResult.Valid:
          _game.SubmitGuess(guessCharacter);
          break;
        case GuessResult.Invalid:
          _view.DisplayInvalidGuess();
          break;
        case GuessResult.Duplicate:
          _view.DisplayDuplicateGuess();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      _view.DisplayGameState();
    }
  }
}