using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_For_WPF.Cs
{
    class Log
    {
        public static void WriteLine(string message)
        {
            string str = $"\n[{DateTime.Now.ToString("HH:mm:ss")}]:{message}";
            GL.Log_Str += str;
        }
    }
}
