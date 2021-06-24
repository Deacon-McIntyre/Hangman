using System.Collections.Generic;

namespace Hangman.Models
{
  public class SaveModel
  {
    public string TargetWord { get; set; }
    public IEnumerable<char> Guesses { get; set; }
  }
}
