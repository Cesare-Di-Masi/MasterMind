using System.Drawing;
using MastermindLib;

namespace MastermindProgram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FixedColoursGenerator generator = new FixedColoursGenerator();
            GameManager game = new GameManager(false, 4, 4, 3, 1, generator);
            Colours[] attempt = new Colours[4] { Colours.Red, Colours.Blue, Colours.Red, Colours.Blue };
            game.EndOfTheTurn(attempt);
            int actual = game.WrongPosition;
            int expected = 4;
        }
    }
}