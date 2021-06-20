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

    private bool _initialised;

    public void Initialise()
    {
      _game = new Game();
      _view = new View(_game);

      var generateWordService = new GenerateWordFromFile(WordsFilename);
      var word = generateWordService.Run();

      _game.SetWord(word);

      _initialised = true;
    }

    public void Start()
    {
      if (!_initialised) throw new InvalidOperationException("Hangman controller must be initialised before starting");

      _view.DisplayWelcomeMessage();
      _view.DisplayInstructions();
      _view.DisplayGameState();

      while (_game.IsInPlay())
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

      _view.DisplayGameOutcome();
    }
  }
}