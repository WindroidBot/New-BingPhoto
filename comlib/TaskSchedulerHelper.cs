using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler;

namespace comlib
{
    public class TaskSchedulerHelper
    {
        /// <summary>
        /// 获取计划任务的列表
        /// </summary>
        /// <returns>计划任务列表</returns>
        public static IRegisteredTaskCollection GetAllTaskScheduler()
        {
            TaskSchedulerClass taskSchedulerClass = new TaskSchedulerClass();
            taskSchedulerClass.Connect(null, null, null, null);
            ITaskFolder taskFolder = taskSchedulerClass.GetFolder("\\");
            IRegisteredTaskCollection registeredTaskCollection = taskFolder.GetTasks(1);
            return registeredTaskCollection;
        }

        /// <summary>
        /// 以任务名为标准判断计划任务是否存在
        /// </summary>
        /// <param name="taskName">待判断的计划任务名</param>
        /// <returns>计划任务存在性的布尔值</returns>
        public static bool IsTaskExists(string taskName)
        {
            bool isExists = false;
            IRegisteredTaskCollection registeredTaskCollection = GetAllTaskScheduler();
            for(int i = 1; i <= registeredTaskCollection.Count; i++)
            {
                IRegisteredTask registeredTask = registeredTaskCollection[i];
                if (registeredTask.Name.Equals(taskName))
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }

        /// <summary>
        /// 删除指定任务名的计划任务
        /// </summary>
        /// <param name="taskName">计划任务名</param>
        /// <returns>删除成功返回true，否则返回false</returns>
        public static bool DeleteTaskScheduler(string taskName)
        {
            TaskSchedulerClass taskSchedulerClass = new TaskSchedulerClass();
            taskSchedulerClass.Connect(null, null, null, null);
            ITaskFolder taskFolder = taskSchedulerClass.GetFolder("\\");
            try
            {
                taskFolder.DeleteFolder(taskName, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[system]" + ex);
                return false;
            }
            Console.WriteLine("[system]Delete TaskScheduler Succeful!");
            return true;
        }
    }
}