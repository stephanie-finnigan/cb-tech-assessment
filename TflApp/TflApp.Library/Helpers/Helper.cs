using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Helpers
{
    public static class Helper
    {
        public static string DisplayName<T>(string propName)
        {
            MemberInfo property = typeof(T).GetProperty(propName);
            
            return property.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        }
    }
}
