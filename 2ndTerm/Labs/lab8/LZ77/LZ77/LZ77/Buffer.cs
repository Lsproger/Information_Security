using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZ77
{
    class Buffer
    {
        private string buffer;
        private int maxSize;

        public string GetValue
        {
            get
            {
                return this.buffer;
            }

        }

        public Buffer(int size)
        {
            buffer = string.Empty;
            this.maxSize = size;
        }
        /// <summary>
        /// Add input string in the buffer
        /// </summary>
        /// <param name="input"></param>
        public void Add(string input)
        {
            if ((buffer.Length + input.Length) <= maxSize)
            {
                buffer = buffer + input;
            }
            else
            {
                int difference = (buffer.Length + input.Length) - maxSize;
                buffer = buffer.Remove(0, difference) + input;
            }
        }
        /// <summary>
        /// Add input char in the buffer as string
        /// </summary>
        /// <param name="input"></param>
        public void Add(char input)
        {
            if ((buffer.Length + 1) <= maxSize)
            {
                buffer = buffer + input.ToString();
            }
            else
            {
                buffer = buffer.Remove(0, 1) + input.ToString();
            }
        }
        /// <summary>
        /// Remove iput string from buffer. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void Remove(int index, int count)
        {
            this.buffer = buffer.Remove(index,count); 
        }
        /// <summary>
        /// Return boolean. True if buffer is full or false if no
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            if (buffer.Length < maxSize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
