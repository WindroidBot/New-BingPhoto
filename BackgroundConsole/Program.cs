using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;
using photolib;

namespace BackgroundConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.HideConsole();
            AutoSettingHelper autoSettingHelper = new AutoSettingHelper();
            autoSettingHelper.AutoActive();
            Console.WriteLine("执行结束！");
            Console.ReadLine();
        }
    }
}
