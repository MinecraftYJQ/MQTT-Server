using MQTT_For_WPF.Cs;
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

namespace MQTT_For_WPF.Windows
{
    /// <summary>
    /// Add_Mqtt.xaml 的交互逻辑
    /// </summary>
    public partial class Add_Mqtt : Window
    {
        public Add_Mqtt()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid
            {
                Height = 50,
                Width = 584,
                VerticalAlignment = VerticalAlignment.Top
            };
            Label vers = new Label
            {
                Content = zhutitext.Text,
                Margin = new Thickness(87, 8, 84, 8),
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            grid.Children.Add(vers);

            grid.Children.Add(new Button
            {
                Content = "删除",
                Margin = new Thickness(0, 0, 10, 0),
                Height = 30,
                Name = zhutitext.Text.Split('/')[1],
                HorizontalAlignment = HorizontalAlignment.Right
            });

            GL.MQTT_UAC_ListBox.Items.Add(grid);
            string item = zhutitext.Text;
            GL.JTList.Add(item);



            Grid grid1 = new Grid
            {
                Height = 50,
                Width = 584,
                VerticalAlignment = VerticalAlignment.Top
            };
            Label topic_ts = new Label
            {
                Content = "主题 " + zhutitext.Text + "：",
                Margin = new Thickness(5, 8, 0, 0),
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            grid1.Children.Add(topic_ts);
            Label vers1 = new Label
            {
                Content = zhutitext.Text,
                Margin = new Thickness(87, 8, 84, 8),
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            New_Message_List_Up.Make_Control(vers1, zhutitext.Text);
            grid1.Children.Add(vers1);
            GL.Message_Box.Items.Add(grid1);
            Close();
        }
    }
}
