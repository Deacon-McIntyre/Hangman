using System;
using System.IO;
using System.Text.Json;

namespace Hangman.Framework.Models
{
  public class PersistenceModelBase
  {
    protected void Save<T>(T obj)
    {
      var serializedModel = JsonSerializer.Serialize(obj);
      
      File.WriteAllText(Constants.PersistenceFilePath, serializedModel);
    }

    protected static T Load<T>()
    {
      T result = default;

      if (!File.Exists(Constants.PersistenceFilePath))
      {
        return default;
      }

      var str = File.ReadAllText(Constants.PersistenceFilePath);

      try
      {
        result = JsonSerializer.Deserialize<T>(str);
      }
      catch
      {
        // Ignore
      }

      return result;
    }

    protected void Reset()
    {
      File.Delete(Constants.PersistenceFilePath);
    }
  }
}