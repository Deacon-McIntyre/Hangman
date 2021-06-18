using System;
using System.IO;

namespace Hangman.Services
{
  public class GenerateWordFromFile
  {
    private readonly string _fileName;

    public GenerateWordFromFile(string fileName)
    {
      _fileName = fileName;
    }

    public string Run()
    {
      string[] lines = File.ReadAllLines(_fileName);
      var rand = new Random();

      return lines[rand.Next(0, lines.Length)];
    }
  }
}