using System.Linq;
using System.Reflection.PortableExecutable;
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
    public void GetWord_WordIsPig_ReturnsPig()
    {
      var word = _game.GetTargetWord();
      Assert.That(word, Is.EqualTo("PIG"));
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
    
    [Test]
    public void HasAlreadyBeenGuessed_NoGuessesYet_ReturnsFalse()
    {
      var result = _game.HasAlreadyBeenGuessed(new Guess('a'));
      Assert.That(result, Is.False);
    }
    
    [Test]
    public void HasAlreadyBeenGuessed_GuessHasNotBeenGuessedYet_ReturnsFalse()
    {
      _game.SubmitGuess(new Guess('a'));
      var result = _game.HasAlreadyBeenGuessed(new Guess('b'));
      Assert.That(result, Is.False);
    }
    
    [Test]
    public void HasAlreadyBeenGuessed_GuessHasBeenGuessed_ReturnsTrue()
    {
      _game.SubmitGuess(new Guess('a'));
      var result = _game.HasAlreadyBeenGuessed(new Guess('a'));
      Assert.That(result, Is.True);
    }

    [Test]
    public void GetFilledOutAnswer_NoGuessesYet_ReturnsArrayOfDefaults()
    {
      var result = _game.GetFilledOutAnswer();
      Assert.That(result.All(character => Equals(character, default(char))), Is.True);
    }

    [Test]
    public void GetFilledOutAnswer_GuessMiddleLetter_ReturnsArrayWithMiddleLetterCorrectAndOthersDefault()
    {
      _game.SubmitGuess(new Guess('i'));
      var result = _game.GetFilledOutAnswer();
      Assert.That(result[0], Is.EqualTo(default(char)));
      Assert.That(result[1], Is.EqualTo('I'));
      Assert.That(result[2], Is.EqualTo(default(char)));
    }

    [Test]
    public void GetFilledOutAnswer_GuessAllLetters_ReturnsArrayWithAllLetters()
    {
      _game.SubmitGuess(new Guess('p'));
      _game.SubmitGuess(new Guess('i'));
      _game.SubmitGuess(new Guess('g'));
      var result = _game.GetFilledOutAnswer();
      Assert.That(result[0], Is.EqualTo('P'));
      Assert.That(result[1], Is.EqualTo('I'));
      Assert.That(result[2], Is.EqualTo('G'));
    }

    [Test]
    public void GetInvalidGuesses_NoInvalidGuesses_ReturnsEmptyList()
    {
      var result = _game.GetInvalidGuesses();
      Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetInvalidGuesses_OneInvalidGuess_ListWithThatGuess()
    {
      _game.SubmitGuess(new Guess('q'));
      var result = _game.GetInvalidGuesses();
      Assert.That(result, Is.All.Matches<Guess>(guess => guess.Character == 'Q'));
    }

    [Test]
    public void GetInvalidGuesses_TwoInvalidGuess_ListWithThoseGuesses()
    {
      _game.SubmitGuess(new Guess('q'));
      _game.SubmitGuess(new Guess('x'));
      var result = _game.GetInvalidGuesses().ToList();
      Assert.That(result.First().Character, Is.EqualTo('Q'));
      Assert.That(result.Last().Character, Is.EqualTo('X'));
    }
  }
}