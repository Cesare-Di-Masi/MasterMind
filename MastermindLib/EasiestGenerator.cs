using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLib
{
    public class EasiestGenerator : IGenerator
    {
        public Colours[] generateCode()
        {
            return new Colours[4] {Colours.Red,Colours.Red,Colours.Red,Colours.Red };
        }
    }
}
