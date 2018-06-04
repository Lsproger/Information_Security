using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZ77
{
    class Dictionary
    {
        private string dictionary;
        private int maxSize;
        /// <summary>
        /// Property for get dictionary value as string. Only get 
        /// </summary>
        public string Value
        {
            get
            {
                return this.dictionary;
            }
            set
            {
                dictionary = value;
            }

        }

        public Dictionary(int size)
        {
            for (int i = 0; i < size; i++)
            {
                dictionary = String.Concat(dictionary, '0');
            }            
            this.maxSize = size;
        }
        /// <summary>
        /// Add input string in the dictionary
        /// </summary>
        /// <param name="input"></param>
        public void Add(string input)
        {
            if ((dictionary.Length + input.Length) <= maxSize)
            {
                dictionary = dictionary + input;
            }
            else
            {
                int difference = (dictionary.Length + input.Length) - maxSize;
                dictionary = dictionary.Remove(0, difference) + input;
            }
        }
        /// <summary>
        /// Add input char in the dictionary as string
        /// </summary>
        /// <param name="input"></param>
        public void Add(char input)
        {
            if ((dictionary.Length + 1) <= maxSize)
            {
                dictionary = dictionary + input.ToString();
            }
            else
            {
                dictionary = dictionary.Remove(0,1) + input.ToString();
            }
        }
    }
}
