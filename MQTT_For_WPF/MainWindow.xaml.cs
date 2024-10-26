using MQTT_For_WPF.Cs;
using MQTT_For_WPF.Pages;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MQTT_For_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("List"))
            {
                foreach (string line in File.ReadAllLines("List"))
                {
                    GL.JTList.Add(line);
                }
            }
            if (File.Exists("Title"))
            {
                this.Title = File.ReadAllText("Title");
            }
            Log.WriteLine("程序初始化完毕.");
            GL.Main_Frame = Main_Frame;
            Log.WriteLine("设置全局变量完毕");
            GL.Main_Frame.Navigate(new Main_Page());
            Log.WriteLine("进入主界面");
            //MQTT_Launcher.starting();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.WriteAllLines("List",GL.JTList);

            Environment.Exit(0); // 0通常表示成功退出
        }
    }
}