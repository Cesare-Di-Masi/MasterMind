using System.Drawing;
using MastermindLib;

namespace MastermindProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator(6, 4, 1);

            Colours[] col = generator.generateCode();
            int[] check = new int[col.Length];
            bool cor = false;

            for (int i = 0; i < col.Length; i++)
            {
                check[(int)col[i]]++;
                if (check[(int)col[i]] > 1)
                    cor = true;
            }
        }
    }
}
