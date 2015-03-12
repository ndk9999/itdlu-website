using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorLife.Core.FileManager
{
  public  class FileManager
    {      
      static IFileRepository _instance;
      static object syncLock = new object();
      public static IFileRepository Instance
      {
          get
          {
              if (_instance == null)
              {
                  lock (syncLock)
                  {
                      if (_instance == null)
                          _instance = new FileRepository();
                  }
              }
              return _instance;
          }
      }
    }
}
