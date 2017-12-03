using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Test_Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick+=new EventHandler()
            */
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            for (ProgressBar_test.Value = 0; ProgressBar_test.Value < ProgressBar_test.Maximum; ProgressBar_test.Value++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("[system]" + ProgressBar_test.Value);
            }
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar_test.IsEnabled = false;
            ProgressBar_test.Value = 50;
            ProgressBar_test.IsEnabled = true;
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar_test.Value++;
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar_test.Value--;
        }
    }
}
