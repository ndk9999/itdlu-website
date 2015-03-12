using DLUProject.Domain;
using DLUProject.Services;
using DLUProjectFramework.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class GetSinglePage
{
    private static object syncLock = new object();
    private static IServices<Pages> _instance;
    public static IServices<Pages> Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        var container = new Container(c => c.AddRegistry<DefaultRegistry>());
                        _instance = container.GetInstance<PagesService>();
                    }
                }
            }
            return _instance;
        }
    }
    public static Pages Get(int id)
    {
        var p = Instance.All().FirstOrDefault(c => c.PageID == id);
        if (p == null) return new Pages();
        return p;
    }
}
 
