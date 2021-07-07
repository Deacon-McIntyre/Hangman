using Hangman.Models;
using NUnit.Framework;

namespace Hangman.Tests.Models
{
  [TestFixture]
  public class GuessTests
  {
    [Test]
    public void IsValid_SingleCharacter_ReturnsTrue()
    {
      var result = new Guess('a').IsValid;
      Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_Number_ReturnsFalse()
    {
      var result = new Guess('1').IsValid;
      Assert.That(result, Is.False);
    }

    [Test]
    public void IsValid_Comma_ReturnsFalse()
    {
      var result = new Guess(',').IsValid;
      Assert.That(result, Is.False);
    }

    [Test]
    public void IsValid_Whitespace_ReturnsFalse()
    {
      var result = new Guess(' ').IsValid;
      Assert.That(result, Is.False);
    }
    
    [Test]
    public void Guess_MakesCharactersUppercase()
    {
      var guess = new Guess('a');
      Assert.That(guess.Character, Is.EqualTo('A'));
    }

    [Test]
    public void NewGuess_UppercaseA_ReturnsGuessContainingUppercaseA()
    {
      var guess = new Guess('A');
      Assert.That(guess.Character, Is.EqualTo('A'));
    }
    
    [Test]
    public void Guess_LowercaseA_ReturnsGuessContainingUppercaseA()
    {
      var guess = new Guess('a');
      Assert.That(guess.Character, Is.EqualTo('A'));
    }
  }
}