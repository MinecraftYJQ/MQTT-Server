using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MQTT_For_WPF.Cs
{
    class New_Message_List_Up
    {
        public static void Make_Control(Label control,string topic)
        {
            control.Content = "";
            Task.Run(() => {
                while (true) {
                    if (GL.MQTT_New_Message != null) {
                        string temp = GL.MQTT_New_Message.Split('|')[0];
                        if (temp == topic)
                        {
                            control.Dispatcher.Invoke(new Action(() =>
                            {
                                control.Content = GL.MQTT_New_Message.Split('|')[1];
                            }));
                            GL.MQTT_New_Message = null;
                        }

                        Thread.Sleep(200);
                    }
                }
            });
        }
    }
}
