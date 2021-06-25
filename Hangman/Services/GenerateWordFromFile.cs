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
      string[] lines;

      try
      {
        lines = File.ReadAllLines(_fileName);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Unable to read the words from the {_fileName} file", ex);
      }

      var rand = new Random();

      return lines[rand.Next(0, lines.Length)];
    }
  }
}