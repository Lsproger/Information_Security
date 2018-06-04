using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace notvirus
{
    class Autorun
    {
        public static bool SetAutorunValue(bool autorun, string npath)
        {
            const string name = "notvirus";
            string ExePath = npath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);
                reg.Flush();
                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
