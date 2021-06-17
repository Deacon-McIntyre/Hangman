using System;

namespace Hangman
{
  class Program
  {
    static void Main(string[] args)
    {
      var game = new Game();
      var controller = new HangmanController(game, new View(game));
      controller.Start();
    }
  }
}