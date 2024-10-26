using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_For_WPF.Cs.Base64_Get_Code
{
    class Base64_Get_Code_For_File
    {
        public static string Base64_Get_Code_For_File_Main(string path)
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
    }
}
