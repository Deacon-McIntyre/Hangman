using System;

namespace Hangman
{
  public class HangmanController
  {
    private readonly View _view;
    private readonly Game _game;

    public HangmanController(Game game, View view)
    {
      _game = game;
      _view = view;
    }

    public void Start()
    {
      _game.GenerateWord();
      _view.Welcome();
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