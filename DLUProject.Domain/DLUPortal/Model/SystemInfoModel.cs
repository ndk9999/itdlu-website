using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLUProject.Domain
{
    public class SystemInfoModel
    {
        public SystemInfoModel()
        {
            this.LoadedAssemblies = new List<LoadedAssembly>();
        }
        public string AspNetInfo { get; set; }
        public string IsFullTrust { get; set; }
        public string AppVersion { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime ServerLocalTime { get; set; }
        public string ServerTimeZone { get; set; }
        public DateTime UtcTime { get; set; }
        public string HttpHost { get; set; }
        public IList<LoadedAssembly> LoadedAssemblies { get; set; }
        public partial class LoadedAssembly
        {
            public string FullName { get; set; }
            public string Location { get; set; }
        }
    }
}
