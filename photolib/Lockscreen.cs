using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using comlib;
using static System.Net.Mime.MediaTypeNames;

namespace photolib
{
    /// <summary>
    /// 
    /// </summary>
    public struct Alockscreen
    {
        private string imagePath;//锁屏图片的完整路径
        private float length;//锁屏图片的大小
        private string imageName;//锁屏图片的文件名
        private string extensioin;//锁屏图片扩展名

        public string ImagePath { get => imagePath; set => imagePath = value; }
        public float Length { get => length; set => length = value; }
        public string Extensioin { get => extensioin; set => extensioin = value; }
        public string ImageName { get => imageName; set => imageName = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Lockscreen
    {
        private string assetsDir;
        private string lockScreenDir;
        private List<Alockscreen> screenList = new List<Alockscreen>();

        public string AssetsDir { get => assetsDir; set => assetsDir = value; }
        public string LockScreenDir { get => lockScreenDir; set => lockScreenDir = value; }
        public List<Alockscreen> ScreenList { get => screenList; set => screenList = value; }

        /// <summary>
        /// Lockscreen的构造函数
        /// </summary>
        /// <param name="assetsDir">assets目录的路径</param>
        /// <param name="lockScreenDir">锁屏图片输出的路径</param>
        public Lockscreen(string assetsDir,string lockScreenDir)
        {
            this.assetsDir = AssetsDir;
            this.lockScreenDir = lockScreenDir;
            ErgodicDirector(assetsDir);
        }

        /// <summary>
        /// 遍历目录，并将文件信息存入screenList中，只允许被Lockscreen()调用
        /// </summary>
        /// <param name="path">assets目录的路径</param>
        private void ErgodicDirector(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            foreach(FileInfo file in directoryInfo.GetFiles())
            {
                Alockscreen alockscreen = new Alockscreen
                {
                    ImagePath = file.FullName,
                    Length = file.Length,
                    ImageName=file.Name,
                    Extensioin = file.Extension,
                };
                //剔除国小的无关图片
                if(alockscreen.Length < 150000)
                {
                    continue;
                }
                screenList.Add(alockscreen);
            }
        }

        /// <summary>
        /// 访问ScreenList结构体的访问器
        /// </summary>
        /// <param name="index">结果集的索引</param>
        /// <returns>Alockscreen结构体</returns>
        public Alockscreen GetAlockScreenValue(int index)
        {
            return ScreenList[index];
        }        

        /// <summary>
        /// 向指定目录输出文件，筛选后转换为图片
        /// </summary>
        public void OutputScreen()
        {
            if (!Directory.Exists(lockScreenDir))
            {
                Directory.CreateDirectory(lockScreenDir);
                MessageBox.Show("目标目录不存在，并已创建！\n" + lockScreenDir,
                "必应美图小助手", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Console.WriteLine("【system】图片目录不存在，并已创建，在" + lockScreenDir);
            }
            foreach (Alockscreen alockscreen in screenList)
            {               
                System.IO.File.Copy(alockscreen.ImagePath, lockScreenDir + "\\" + alockscreen.ImageName + ".jpg", true);

                
                /*
                if (!moblie)
                {
                    BitmapImage image = new BitmapImage(new Uri(lockScreenDir + "\\" + alockscreen.ImageName + ".jpg"));
                    if (image.Height > image.Width)
                    {
                        System.IO.File.Delete(lockScreenDir + "\\" + alockscreen.ImageName + ".jpg");
                    }
                }
                */
            }
        }

        /// <summary>
        /// 向指定目录输出文件，筛选后转换为图片，使用FileStream复制
        /// </summary>
        public void OutputScreen2()
        {
            if (!Directory.Exists(lockScreenDir))
            {
                Directory.CreateDirectory(lockScreenDir);
                MessageBox.Show("目标目录不存在，并已创建！\n" + lockScreenDir,
                "必应美图小助手", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Console.WriteLine("【system】图片目录不存在，并已创建，在" + lockScreenDir);
            }
            foreach(Alockscreen alockscreen in screenList)
            {
                FileStream sourceStream = new FileStream(alockscreen.ImagePath, FileMode.Open);
                FileStream destStream = new FileStream(lockScreenDir + "\\" + alockscreen.ImageName + ".jpg", FileMode.Create);
                int d;
                byte b;
                while ((d = sourceStream.ReadByte()) != -1)
                {
                    b = (byte)d;
                    destStream.WriteByte(b);
                }
                sourceStream.Close();
                destStream.Close();
            }

        }

        /// <summary>
        /// 删除手机端图片
        /// </summary>
        public void DeleteMoblieLock()
        {
            foreach(Alockscreen alockscreen in screenList)
            {
                BitmapImage image = new BitmapImage();
                try
                {
                    image = (new ImageHelper()).BitImageHelper(lockScreenDir + "\\" + alockscreen.ImageName + ".jpg");
                    image.EndInit();
                }
                catch(FileNotFoundException)
                {
                    continue;
                }                
                if (image.Height > image.Width)
                {
                    image.Freeze();//释放BitmapImage，文件可以被其他进程操作
                    System.IO.File.Delete(lockScreenDir + "\\" + alockscreen.ImageName + ".jpg");
                }
            }
        }
    }
}
