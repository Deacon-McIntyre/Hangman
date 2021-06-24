using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Hangman.Models
{
  public abstract class BasePersistableGame
  {
    private const string FilePath = "save.txt";

    public void Save(SaveModel state)
    {
      string json = JsonConvert.SerializeObject(state);

      File.WriteAllText(FilePath, json);
    }

    public void Load()
    {
      string json = File.ReadAllText(FilePath);

      var state = JsonConvert.DeserializeObject<SaveModel>(json);

      Load(state);
    }

    protected abstract void Load(SaveModel state);
  }
}
