using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWT
{
    class BWT
    {
        public string Message { get; set; }

        public string GetTransformString()
        {
            int originalMessagePosition = -1;
            List<string> matrix = new List<string>();

            matrix.Add(Message);

            for (int i = 0; i < Message.Length - 1; i++)
            {
                matrix.Add(matrix[i].Substring(1, matrix[i].Length - 1) + matrix[i][0]);
            }

            matrix.Sort();

            for (int i = 0; i < Message.Length; i++)
            {
                if(matrix[i] == Message)
                {
                    originalMessagePosition = i;
                }
            }

            StringBuilder lastMatrixCollumn = new StringBuilder();

            foreach (var item in matrix)
            {
                lastMatrixCollumn.Append(item[item.Length - 1]);
            }

            lastMatrixCollumn.Append(",");
            lastMatrixCollumn.Append(originalMessagePosition);

            Console.WriteLine("first matrix");
            PrintMatrix(matrix);

            return lastMatrixCollumn.ToString();
        }

        public string restoreOriginalString(string transformString)
        {
            int commaPosition = transformString.LastIndexOf(',');
            string transformedString = transformString.Substring(0, commaPosition);
            int originalMessagePosition = Convert.ToInt32(transformString.Substring(commaPosition+1, transformString.Length - 1 - transformedString.Length));

            List<string> restoringMatrix = new List<string>();

            for (int i = 0; i < transformedString.Length; i++)
            {
                restoringMatrix.Add("");
            }

            for (int i = 0; i < transformedString.Length; i++)
            {
                for (int j = 0; j < transformedString.Length; j++) //добавляем по символу в начало каждой строки
                {
                    restoringMatrix[j] = transformedString[j] + restoringMatrix[j];
                }
                restoringMatrix.Sort();
            }

            Console.WriteLine("Second Matrix");
            PrintMatrix(restoringMatrix);
            return restoringMatrix[originalMessagePosition];           
        }

        private void PrintMatrix(List<string> matrix)
        {
            foreach (var item in matrix)
            {
                foreach (var stringItem in item)
                {
                    Console.Write(stringItem + " ");
                }
                Console.WriteLine();
            }
        }


    }
}
