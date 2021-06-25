using Newtonsoft.Json;

namespace Hangman.Models
{
  public class Guess
  {
    public Guess(char character)
    {
      Character = char.ToUpper(character);
    }

    public char Character { get; }

    [JsonIgnore]
    public bool IsValid => char.IsLetter(Character);
  }
}
