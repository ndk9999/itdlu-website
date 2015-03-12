using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLUProject.Domain;
using DLUProject.Services;
using ColorLife.Core.Helper;
using StructureMap;
using DLUProjectFramework.DependencyResolution;
 
    public  class SettingOption
    {
        private static object syncLock = new object();
        private static IServices<Setting> _instance;
        public static IServices<Setting> Instance
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
                            _instance = container.GetInstance<SettingService>();
                        }
                    }
                }
                return _instance;
            }
        }
        public static string Get(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                var setting = Instance.All().FirstOrDefault(c => c.Name.Equals(key));
                return setting == null ? "" : setting.Value;
            }
            return String.Empty;
        }
        public static int Int(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                var setting = Instance.Table.FirstOrDefault(c => c.Name.Equals(key));
                return setting == null ? 1 : setting.Value.ToInt();
            }
            return -1;
        }
        public static bool Bool(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                var setting = Instance.Table.FirstOrDefault(c => c.Name.Equals(key));
                return setting == null ? false : setting.Value.ToBool();
            }
            return false;
        }
    }
 
