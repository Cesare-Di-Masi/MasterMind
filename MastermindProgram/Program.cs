using System.Drawing;
using MastermindLib;

namespace MastermindProgram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CodeBreaker breaker = new CodeBreaker("Gionni", 4);

            Colours actual = Colours.Yellow;
            Colours expected = Colours.Green;

            breaker.NextColour(ref actual);
        }
    }
}