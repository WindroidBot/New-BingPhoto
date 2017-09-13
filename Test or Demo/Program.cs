using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;

namespace Test_or_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Setting setting = new Setting();
            ConfigHelper configHelper = new ConfigHelper();
            //string oldPath = setting.GetOldWallpaperPath();
            configHelper.Initialise_ini(setting.GetOldWallpaperPath(), setting.GetMyDocumentsPath(), setting.GetRunPath(), false, false);
            Console.ReadLine();
            configHelper.Flush_ini();
            Console.ReadLine();
        }
    }
}
