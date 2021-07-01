using System.IO;
using Newtonsoft.Json;

namespace Hangman.Framework.Models
{
  public abstract class PersistenceModelBase
  {
    protected void Save()
    {
      var serializedModel = JsonConvert.SerializeObject(this);
      
      File.WriteAllText(Constants.PersistenceFilePath, serializedModel);
    }

    protected static T Load<T>() where T : PersistenceModelBase
    {
      T result = default;

      if (!File.Exists(Constants.PersistenceFilePath))
      {
        return null;
      }

      var str = File.ReadAllText(Constants.PersistenceFilePath);

      try
      {
        result = JsonConvert.DeserializeObject<T>(str);
      }
      catch
      {
        // Ignore
      }

      return result;
    }

    protected static void Reset()
    {
      File.Delete(Constants.PersistenceFilePath);
    }
  }
}