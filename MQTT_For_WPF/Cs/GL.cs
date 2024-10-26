using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MQTT_For_WPF.Cs
{
    internal class GL
    {
        public static Frame Main_Frame;
        public static ListBox MQTT_UAC_ListBox;
        public static ListBox Message_Box;
        public static List<string> JTList= new List<string> { };
        public static ComboBox zhutifasong_Box;
        public static string MQTT_New_Message = null;
        public static string Log_Str = null;
    }
}
