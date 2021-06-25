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
      _game = Game.Load<Game>();

      if (_game == null)
      {
        var generateWordService = new GenerateWordFromFile(WordsFilename);
        var targetWord = generateWordService.Run();

        _game = new Game(targetWord);
      }

      _view = new View(_game);

      _initialised = true;
    }

    public void Start()
    {
      if (!_initialised) throw new InvalidOperationException("Hangman controller must be initialized before starting");

      _view.DisplayWelcomeMessage();
      _view.DisplayInstructions();
      _view.DisplayGameState();

      if (_game.IsInPlay())
      {
        Guess guess = _view.AskForGuess();

        if (!guess.IsValid)
        {
          _view.DisplayInvalidGuess();
        }
        else if (_game.HasAlreadyBeenGuessed(guess))
        {
          _view.DisplayDuplicateGuess();
        }
        else
        {
          _game.SubmitGuess(guess);
        }

        _view.DisplayGameState();
      }

      _view.DisplayGameOutcome();
    }
  }
}