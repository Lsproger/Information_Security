using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZ77
{
    class StringInfo
    {
        private static StringBuilder info;
        public static string Info
        {
            get
            {
                return info.ToString();
            }
            set
            {
                info = new StringBuilder(value);
            }
        }
        public StringInfo()
        {
            info = new StringBuilder("Словарь || Буфер данных || Код  \n");
        }

        public void AppendInfo(string dictionary, string buffer, string triad)
        {
            info.Append(dictionary).Append(" || ").Append(buffer).Append(" || ").Append(triad).Append("\n");
        }

        public void AppendMessage(string message)
        {
            info.Append("{message}\n");
        }
    }
}
