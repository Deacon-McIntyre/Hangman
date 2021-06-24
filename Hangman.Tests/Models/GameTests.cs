using Hangman.Models;
using NUnit.Framework;

namespace Hangman.Tests.Models
{
  public class GameTests
  {
    private Game _game;

    [SetUp]
    public void Setup()
    {
      _game = new Game();
    }
    
    [Test]
    public void GetGuesses_NewGame_ReturnsEmptyHashSet()
    {
      var guesses = _game.GetGuesses();
      Assert.That(guesses, Is.Empty);
    }
    
    [Test]
    public void GetWord_WordIsPig_ReturnsPig()
    {
      _game.SetWord("pig");
      var word = _game.GetWord();
      Assert.That(word, Is.EqualTo("pig"));
    }
    
    [Test]
    public void SubmitGuess_GuessLowercaseA_ReturnsSetContainingLowercaseA()
    {
      _game.SubmitGuess('a');
      var guesses = _game.GetGuesses();
      Assert.That(guesses, Contains.Item('a'));
    }
    
    [Test]
    public void SubmitGuess_GuessCapitalA_ReturnsSetContainingLowercaseA()
    {
      _game.SubmitGuess('A');
      var guesses = _game.GetGuesses();
      Assert.That(guesses, Contains.Item('a'));
    }
    
    [Test]
    public void GetGuessesRemaining_OneWrongGuessSubmitted_ReturnsMaxLivesMinusOne()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('a');
      var guessesRemaining = _game.GetGuessesRemaining();
      Assert.That(guessesRemaining, Is.EqualTo(6));
    }
    
    [Test]
    public void GetGuessesRemaining_OneRightGuessSubmitted_ReturnsMaxLives()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('p');
      var guessesRemaining = _game.GetGuessesRemaining();
      Assert.That(guessesRemaining, Is.EqualTo(7));
    }
    
    [Test]
    public void IsWon_AllCharactersGuessed_ReturnsTrue()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('p');
      _game.SubmitGuess('i');
      _game.SubmitGuess('g');
      var isWon = _game.IsWon();
      Assert.That(isWon, Is.True);
      Assert.That(_game.IsInPlay(), Is.False);
    }
    
    [Test]
    public void IsWon_NotAllCharactersGuessed_ReturnsFalse()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('a');
      var isWon = _game.IsWon();
      Assert.That(isWon, Is.False);
    }
    
    [Test]
    public void IsLost_GuessesLeft_ReturnsFalse()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('a');
      var isLost = _game.IsLost();
      Assert.That(isLost, Is.False);
    }
    
    [Test]
    public void IsLost_NoGuessesLeft_ReturnsTrue()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('a');
      _game.SubmitGuess('b');
      _game.SubmitGuess('c');
      _game.SubmitGuess('d');
      _game.SubmitGuess('e');
      _game.SubmitGuess('f');
      _game.SubmitGuess('h');
      var isLost = _game.IsLost();
      Assert.That(isLost, Is.True);
      Assert.That(_game.IsInPlay(), Is.False);
    }
    
    [Test]
    public void IsInPlay_NotWonGuessesLeft_ReturnsTrue()
    {
      _game.SetWord("pig");
      _game.SubmitGuess('a');
      var inPlay = _game.IsInPlay();
      Assert.That(inPlay, Is.True);
    }

    [Test]
    public void IsValidGuess_SingleCharacter_ReturnsValidGuess()
    {
      var result = _game.IsValidGuess("a", out _);
      Assert.That(result, Is.EqualTo(GuessResult.Valid));
    }

    [Test]
    public void IsValidGuess_TwoCharacters_ReturnsInvalidGuess()
    {
      var result = _game.IsValidGuess("aa", out _);
      Assert.That(result, Is.EqualTo(GuessResult.Invalid));
    }

    [Test]
    public void IsValidGuess_Number_ReturnsInvalidGuess()
    {
      var result = _game.IsValidGuess("1", out _);
      Assert.That(result, Is.EqualTo(GuessResult.Invalid));
    }

    [Test]
    public void IsValidGuess_Comma_ReturnsInvalidGuess()
    {
      var result = _game.IsValidGuess(",", out _);
      Assert.That(result, Is.EqualTo(GuessResult.Invalid));
    }

    [Test]
    public void IsValidGuess_EmptyGuess_ReturnsInvalidGuess()
    {
      var result = _game.IsValidGuess("", out _);
      Assert.That(result, Is.EqualTo(GuessResult.Invalid));
    }

    [Test]
    public void IsValidGuess_Whitespace_ReturnsInvalidGuess()
    {
      var result = _game.IsValidGuess(" ", out _);
      Assert.That(result, Is.EqualTo(GuessResult.Invalid));
    }

    [Test]
    public void IsValidGuess_Null_ReturnsInvalidGuess()
    {
      var result = _game.IsValidGuess(null, out _);
      Assert.That(result, Is.EqualTo(GuessResult.Invalid));
    }
  }
}