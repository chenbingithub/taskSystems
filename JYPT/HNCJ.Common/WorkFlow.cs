using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HNCJ.Common
{
    public class WorkFlow
    {
        public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["wf"];
        public static System.Activities.Activity GetActivities(string name)
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + "."+name) as System.Activities.Activity;
        }
    }
}
