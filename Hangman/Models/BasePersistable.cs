using System.IO;
using Newtonsoft.Json;

namespace Hangman.Models
{
  public abstract class BasePersistable
  {
    private const string FilePath = "save.txt";

    public void Save()
    {
      string json = JsonConvert.SerializeObject(this);

      File.WriteAllText(FilePath, json);
    }

    public static T Load<T>() where T : BasePersistable
    {
      if (File.Exists(FilePath))
      {
        string json = File.ReadAllText(FilePath);

        T state = JsonConvert.DeserializeObject<T>(json);

        return state;
      }

      return null;
    }
  }
}
