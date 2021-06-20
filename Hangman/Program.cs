using Hangman.Controllers;

namespace Hangman
{
  class Program
  {
    static void Main(string[] args)
    {
      var controller = new HangmanController();
      controller.Initialise();
      controller.Start();
    }
  }
}