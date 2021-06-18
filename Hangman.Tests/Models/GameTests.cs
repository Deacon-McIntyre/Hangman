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