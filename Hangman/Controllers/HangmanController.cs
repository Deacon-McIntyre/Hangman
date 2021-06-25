using System;
using Hangman.Models;
using Hangman.Services;
using Hangman.Views;

namespace Hangman.Controllers
{
  public class HangmanController
  {
    private const string WordsFilename = "words_alpha.txt";

    private View _view;
    private Game _game;

    private void Initialise()
    {
      _game = Game.LoadExistingGame();
      
      if (_game == null)
      {
        var generateWordService = new GenerateWordFromFile(WordsFilename);
        var targetWord = generateWordService.Run();

        _game = new Game(targetWord);
        _game.HandlePersistence();
      }

      _view = new View(_game);
    }

    public void Run()
    {
      Initialise();

      _view.DisplayWelcomeMessage();
      _view.DisplayInstructions();
      _view.DisplayGameState();

      if (_game.IsInPlay())
      {
        PerformMove();
      }

      _view.DisplayGameOutcome();
      _game.HandlePersistence();
    }

    private void PerformMove()
    {
      var hasValidGuess = false;
      while (!hasValidGuess)
      {
        var guess = _view.AskForGuess();
        var result = _game.IsValidGuess(guess, out char guessCharacter);
        switch (result)
        {
          case GuessResult.Valid:
            _game.SubmitGuess(guessCharacter);
            _view.DisplayGameState();

            hasValidGuess = true;
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
      }
    }
  }
}