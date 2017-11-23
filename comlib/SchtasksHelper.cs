using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comlib
{
    public class SchtasksHelper
    {
        /// <summary>
        /// 计划任务名，对应 /TN
        /// </summary>
        private string schtaskName;
        /// <summary>
        /// 程序执行路径，对应 /TR
        /// </summary>
        private string exePath;
        /// <summary>
        /// 程序执行参数
        /// </summary>
        private string paraMeter;
        
        /// <summary>
        /// 周期单位，对应 /SC
        /// </summary>
        private string periodicUnit;
        /// <summary>
        /// 周期具体值，对应 /MO
        /// </summary>
        private string periodic;
        /// <summary>
        /// 开始执行时间，对应 /ST 时间格式为 HH:mm (24 小时时间)，例如 14:30 表示2:30 PM
        /// </summary>
        private string startTime;

        /// <summary>
        /// 构造一个SchtasksHelper实例
        /// </summary>
        /// <param name="schtaskName">计划任务名，对应 /TN</param>
        /// <param name="exePath">程序执行路径，对应 /TR</param>
        /// <param name="paraMeter">程序执行参数</param>
        /// <param name="periodicUnit">周期单位，对应 /SC</param>
        /// <param name="periodic">周期具体值，对应 /MO</param>
        /// <param name="startTime">开始执行时间，对应 /ST</param>
        public SchtasksHelper(string schtaskName, string exePath, string paraMeter, string periodicUnit, string periodic, string startTime)
        {
            this.schtaskName = schtaskName;
            this.exePath = exePath;
            this.paraMeter = paraMeter;
            this.periodicUnit = periodicUnit;
            this.periodic = periodic;
            this.startTime = startTime;
        }

        public string SchtaskName { get => schtaskName; set => schtaskName = value; }
        public string ExePath { get => exePath; set => exePath = value; }
        public string ParaMeter { get => paraMeter; set => paraMeter = value; }
        public string PeriodicUnit { get => periodicUnit; set => periodicUnit = value; }
        public string Periodic { get => periodic; set => periodic = value; }
        public string StartTime { get => startTime; set => startTime = value; }

        /// <summary>
        /// 创建计划任务
        /// </summary>
        public void CreateSchtask()
        {
            string schtaskString = @"schtasks /create /SC " + periodicUnit + @" /MO " + periodic + @" /TN " + "\""+schtaskName+"\"" + @" /TR " + "\"\"\"\"" + exePath + "\"\"\" " + paraMeter + "\"" + @" /ST " + startTime;
            Console.WriteLine("[system]Your Cmdlet String is:" + schtaskString);
            CmdHelper.RunCmd(schtaskString);
        }
    }
}
