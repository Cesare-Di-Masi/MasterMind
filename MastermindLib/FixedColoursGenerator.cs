using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLib
{
    public class FixedColoursGenerator : IGenerator
    {
        public Colours[] generateCode()
        {
            Colours[] result = new Colours[4] { Colours.Blue, Colours.Red, Colours.Blue, Colours.Red };
            return result;
        }
    }
}
