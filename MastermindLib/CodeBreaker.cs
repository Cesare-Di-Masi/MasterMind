using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MastermindLib
{
    public class CodeBreaker
    {
        private Random rnd = new Random();
        private string _name;
        public CodeBreaker(string name)
        {
            _name = name;
        }

        public CodeBreaker()
        {
            _name = "player" + rnd.Next(1);
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void ChangeSelectedColour()
        {
            throw new System.NotImplementedException();
        }
    }
}