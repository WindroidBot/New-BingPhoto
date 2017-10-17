using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using comlib;

namespace New_BingPhoto
{
    /// <summary>
    /// LockScreenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LockScreenWindow : Window
    {
        public LockScreenWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            configHelper.Initialise_ini();
            Lable_OutputPath.Content = configHelper.GetValue("BINGPHOTO","DIRPATH");
        }
    }
}
