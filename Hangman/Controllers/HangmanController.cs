using Hangman.Models;
using Hangman.Services;
using Hangman.Views;

namespace Hangman.Controllers
{
  public class HangmanController
  {
    private View _view;
    private Game _game;

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
    
    private void Initialise()
    {
      _game = Game.LoadExisting();

      if (_game == null)
      {
        var generateWordService = new GenerateWordFromFile(Constants.WordListPath);
        var targetWord = generateWordService.Run();

        _game = new Game(targetWord);
        _game.HandlePersistence();
      }

      _view = new View(_game);
    }
    
    private void PerformMove()
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
  }
}