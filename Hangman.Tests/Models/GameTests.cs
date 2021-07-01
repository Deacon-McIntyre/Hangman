using System.Linq;
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
      _game = new Game("PIG");
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
      var word = _game.GetTargetWord();
      Assert.That(word, Is.EqualTo("PIG"));
    }
    
    [Test]
    public void SubmitGuess_GuessUppercaseA_ReturnsSetContainingUppercaseA()
    {
      _game.SubmitGuess(new Guess('A'));
      var guess = _game.GetGuesses().First();
      Assert.That(guess.Character, Is.EqualTo('A'));
    }
    
    [Test]
    public void SubmitGuess_GuessLowercaseA_ReturnsSetContainingUppercaseA()
    {
      _game.SubmitGuess(new Guess('a'));
      var guess = _game.GetGuesses().First();
      Assert.That(guess.Character, Is.EqualTo('A'));
    }
    
    [Test]
    public void GetGuessesRemaining_OneWrongGuessSubmitted_ReturnsMaxLivesMinusOne()
    {
      _game.SubmitGuess(new Guess('a'));
      var guessesRemaining = _game.GetGuessesRemaining();
      Assert.That(guessesRemaining, Is.EqualTo(6));
    }
    
    [Test]
    public void GetGuessesRemaining_OneRightGuessSubmitted_ReturnsMaxLives()
    {
      _game.SubmitGuess(new Guess('p'));
      var guessesRemaining = _game.GetGuessesRemaining();
      Assert.That(guessesRemaining, Is.EqualTo(7));
    }
    
    [Test]
    public void IsWon_AllCharactersGuessed_ReturnsTrue()
    {
      _game.SubmitGuess(new Guess('p'));
      _game.SubmitGuess(new Guess('i'));
      _game.SubmitGuess(new Guess('g'));
      var isWon = _game.IsWon();
      Assert.That(isWon, Is.True);
      Assert.That(_game.IsInPlay(), Is.False);
    }
    
    [Test]
    public void IsWon_NotAllCharactersGuessed_ReturnsFalse()
    {
      _game.SubmitGuess(new Guess('a'));
      var isWon = _game.IsWon();
      Assert.That(isWon, Is.False);
    }
    
    [Test]
    public void IsLost_GuessesLeft_ReturnsFalse()
    {
      _game.SubmitGuess(new Guess('a'));
      var isLost = _game.IsLost();
      Assert.That(isLost, Is.False);
    }
    
    [Test]
    public void IsLost_NoGuessesLeft_ReturnsTrue()
    {
      _game.SubmitGuess(new Guess('a'));
      _game.SubmitGuess(new Guess('b'));
      _game.SubmitGuess(new Guess('c'));
      _game.SubmitGuess(new Guess('d'));
      _game.SubmitGuess(new Guess('e'));
      _game.SubmitGuess(new Guess('f'));
      _game.SubmitGuess(new Guess('h'));
      var isLost = _game.IsLost();
      Assert.That(isLost, Is.True);
      Assert.That(_game.IsInPlay(), Is.False);
    }
    
    [Test]
    public void IsInPlay_NotWonGuessesLeft_ReturnsTrue()
    {
      _game.SubmitGuess(new Guess('a'));
      var inPlay = _game.IsInPlay();
      Assert.That(inPlay, Is.True);
    }
  }
}