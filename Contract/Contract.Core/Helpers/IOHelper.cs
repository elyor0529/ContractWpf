using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Contract.Core.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class IoHelper
    {
         
        /// <summary>
        /// 
        /// </summary>
        public static readonly string CurrentRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetTmpRandomFileName(string tempPath)
        {
            return Path.Combine(tempPath, Path.GetRandomFileName());
        }
         
    }

}
