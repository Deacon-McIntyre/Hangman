using System.IO;
using Newtonsoft.Json;

namespace Hangman.Framework.Models
{
  public abstract class BasePersistable
  {
    protected void Save()
    {
      string json = JsonConvert.SerializeObject(this);

      File.WriteAllText(Constants.PersistenceFilePath, json);
    }

    protected static T Load<T>() where T : BasePersistable
    {
      if (!File.Exists(Constants.PersistenceFilePath)) return null;
      
      string json = File.ReadAllText(Constants.PersistenceFilePath);

      T state = JsonConvert.DeserializeObject<T>(json);

      return state;
    }
    
    protected void Reset()
    {
      File.Delete(Constants.PersistenceFilePath);
    }
  }
}
