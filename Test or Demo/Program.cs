using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using photolib;
using comlib;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Test_or_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConfigHelper configHelper = new ConfigHelper();
            //Lockscreen lockscreen = new Lockscreen(configHelper.GetValue("LOCKSCREEN", "ASSETS"), configHelper.GetValue("LOCKSCREEN", "OUTPATH"));
            //lockscreen.OutputScreen();
            //lockscreen.DeleteMoblieLock();
            //Console.WriteLine(lockscreen.GetAlockScreenValue(40).ImageName);
            //System.IO.File.Copy("C:\\Users\\xiexy\\Pictures\\QQ截图20170410201857.png", "C:\\Users\\xiexy\\Pictures\\test\\QQ截图20170410201857.png", true);
            //lockscreen.ErgodicDirector(configHelper.GetValue("LOCKSCREEN", "ASSETS"));
            //Version currentVersion = Environment.OSVersion.Version;
            //Console.WriteLine(currentVersion.Major);
            //Console.WriteLine("Your Windows Version Major is : " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            //string strJson = httpHelper.GetHttpData("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=3");
            //Console.WriteLine(strJson);
            //string strJson2 = @"{ ""student"": { ""Name1"": ""小明"" , ""Name2"": ""小花"" , ""Name3"": ""小红""} }";
            //JObject jsonObj = JObject.Parse(strJson);
            //Console.WriteLine(jsonObj["images"][2]["copyright"].ToString());
            //Console.WriteLine(jsonObj["student"]["Name2"].ToString());
            //Console.ReadLine();


            /*
            Console.ReadLine();
            Photos photos = new Photos(0, 8);
            int[] aa = new int[8];
            for(int i = 0; i < 8; i++)
            {
                Console.WriteLine(photos.GetAphotoValue(i).Copyright);
            }
            */
            /*
            try
            {

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\xiexy\Source\Repos\New BingPhoto\New BingPhoto\bin\Debug\New BingPhoto.exe", //启动的应用程序名称  
                    Arguments = "aaaa",
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(startInfo);

            }
            catch (Exception ex)
            {
                throw;
            }
            */

            /*
            HttpHelper httpHelper = new HttpHelper();
            ConfigHelper configHelper = new ConfigHelper();
            Setting setting = new Setting();
            Photo photo = new Photo(0);
            string imagedir = configHelper.GetValue("BINGPHOTO", "DIRPATH");
            httpHelper.DownLoadPhoto(photo.HDUrl);
            setting.SetWallpaper(imagedir + "/" + System.IO.Path.GetFileName(photo.HDUrl));
            */
            //Process.Start(@"C:\Users\windr\Source\Repos\New-BingPhoto\New BingPhoto\bin\Debug\New BingPhoto.exe", "aaaaaaaa");
            /*
            AutoSettingHelper autoSettingHelper = new AutoSettingHelper();
            autoSettingHelper.SetSetupWindowOpenRun("aaaaa", "测试快捷方式");
            Console.ReadLine();
            autoSettingHelper.UnSetSetupWindowOpenRun();
            */

            //AutoSettingHelper autoSettingHelper = new AutoSettingHelper();
            /*
            while (!Setting.IsConnectInternet())
            {
                Console.WriteLine("网络中断，等待网络连接！");
                Thread.Sleep(500);
            }
            */
            //autoSettingHelper.AutoActive();

            /*
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(dateTime.ToShortDateString().ToString());
            */

            SchtasksHelper schtasksHelper = new SchtasksHelper("testtask", @"C:\Users\windr\source\repos\New-BingPhoto\New BingPhoto\bin\Debug\New BingPhoto.exe", "-autoActive", "DAILY", "1", "00:01:00");
            schtasksHelper.CreateSchtask();

            //TaskSchedulerHelper.DeleteTaskScheduler("testtask");

            Console.WriteLine("执行结束！");
            Console.ReadLine();
        }
    }
}