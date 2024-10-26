using iNKORE.UI.WPF.Modern;
using Microsoft.Win32;
using MQTT_For_WPF.Cs;
using MQTT_For_WPF.Cs.Base64_Get_Code;
using MQTT_For_WPF.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MQTT_For_WPF.Pages
{
    /// <summary>
    /// Main_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        { 
            InitializeComponent();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            Log.WriteLine("加载主题");
            GL.MQTT_UAC_ListBox = Main_UAC_ListBox;
            GL.Message_Box = Message_List;
            GL.zhutifasong_Box = zhutiComboBox;
            Log.WriteLine("加载全局变量");
            Task.Run(() =>
            {
                Log.WriteLine("日志系统启动完毕");
                while (true)
                {
                    if (GL.Log_Str != null)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            LogBox.Text = GL.Log_Str.Substring(1, GL.Log_Str.Length - 1);
                        }));
                    }
                }
            });

            Log.WriteLine("初始化MQTT配置");
            if (File.Exists("Title"))
            {
                Thread.Sleep(10);
                title_TextBox.Text = File.ReadAllText("Title");
            }
            if (GL.JTList != null)
            {
                foreach (string line in GL.JTList)
                {
                    Grid grid = new Grid
                    {
                        Height = 50,
                        Width = 584,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    Label vers = new Label
                    {
                        Content = line,
                        Margin = new Thickness(87, 8, 84, 8),
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    grid.Children.Add(vers);

                    Button button = new Button
                    {
                        Content = "删除",
                        Margin = new Thickness(0, 0, 10, 0),
                        Height = 30,
                        Name = line.Split('/')[1],
                        HorizontalAlignment = HorizontalAlignment.Right,
                    };
                    button.Click += (s, e) => {
                        but(s,e,line);
                    };

                    grid.Children.Add(button);
                    zhutiComboBox.Items.Add(line);
                    GL.MQTT_UAC_ListBox.Items.Add(grid);





                    Grid grid1 = new Grid
                    {
                        Height = 50,
                        Width = 584,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    Label topic_ts = new Label
                    {
                        Content = "主题 "+line+"：",
                        Margin = new Thickness(5, 8, 0, 0),
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    grid1.Children.Add(topic_ts);
                    Label vers1 = new Label
                    {
                        Content = line,
                        Margin = new Thickness(87, 8, 84, 8),
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    New_Message_List_Up.Make_Control(vers1, line);
                    grid1.Children.Add(vers1);
                    GL.Message_Box.Items.Add(grid1);
                }
            }
            Log.WriteLine("配置完毕");
            start_MQTTBut.Content = "启动MQTT";
        }
        private void but(object sender, RoutedEventArgs e,string lin)
        {
            GL.JTList.Remove(lin);
            Log.WriteLine($"删除{lin}主题");
            GL.MQTT_UAC_ListBox.Items.Clear();
            GL.Message_Box.Items.Clear();
            zhutiComboBox.Items.Clear();
            foreach (string line in GL.JTList)
            {
                Grid grid = new Grid
                {
                    Height = 50,
                    Width = 584,
                    VerticalAlignment = VerticalAlignment.Top
                };
                Label vers = new Label
                {
                    Content = line,
                    Margin = new Thickness(87, 8, 84, 8),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                grid.Children.Add(vers);

                Button button = new Button
                {
                    Content = "删除",
                    Margin = new Thickness(0, 0, 10, 0),
                    Height = 30,
                    Name = line.Split('/')[1],
                    HorizontalAlignment = HorizontalAlignment.Right,
                };
                button.Click += (s, e) => {
                    but(s, e, line);
                };
                grid.Children.Add(button);
                zhutiComboBox.Items.Add(line);
                GL.MQTT_UAC_ListBox.Items.Add(grid);



                Grid grid1 = new Grid
                {
                    Height = 50,
                    Width = 584,
                    VerticalAlignment = VerticalAlignment.Top
                };
                Label topic_ts = new Label
                {
                    Content = "主题 " + line + "：",
                    Margin = new Thickness(5, 8, 0, 0),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                };
                grid1.Children.Add(topic_ts);
                Label vers1 = new Label
                {
                    Content = line,
                    Margin = new Thickness(87, 8, 84, 8),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                New_Message_List_Up.Make_Control(vers1, line);
                grid1.Children.Add(vers1);
                GL.Message_Box.Items.Add(grid1);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("添加监听...");
            Add_Mqtt add = new Add_Mqtt();
            add.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ApplicationTheme == ApplicationTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            }
        }
        private Thread JTThread = null;
        private bool JTEnabled = false;
        private void start_MQTTBut_Click(object sender, RoutedEventArgs e)
        {
            if (start_MQTTBut.Content == "启动MQTT")
            {
                start_MQTTBut.Content = "关闭MQTT";
                JTThread = new Thread(() =>
                {
                    MQTT_Launcher.Start();
                });
                JTThread.Start();
            }
            else
            {
                start_MQTTBut.Content = "启动MQTT";
                MQTT_Launcher.stop();
                //JTEnabled = false;
            }
        }

        private void Save_Settings(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("保存设置，重启软件生效");
            File.WriteAllText("Title", title_TextBox.Text);
        }

        private void fs_(object sender, RoutedEventArgs e)
        {
            MQTT_Launcher.Sendmesage(zhutiComboBox.Text, mess.Text);
        }

        private void fs_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.png|视频文件|*.mp4|音频文件|*.mp3";
            openFileDialog.ShowDialog();

            mess.Text = Base64_Get_Code_For_File.Base64_Get_Code_For_File_Main(openFileDialog.FileName);
        }

        int iss = 0;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.png";
            openFileDialog.ShowDialog();

            byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
            using (var memoryStream = new System.IO.MemoryStream(imageBytes))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                var imageBrush = new ImageBrush
                {
                    ImageSource = bitmapImage,
                    Stretch = Stretch.Uniform
                };
                tianyanzuizong.Items.Add(new Image
                {
                    Source = bitmapImage,   
                    Height = 100
                });
                string name = System.IO.Path.GetFileName(openFileDialog.FileName);
                MQTT_Launcher.Sendmesage("topic/name", name);
            }
        }
    }
}
