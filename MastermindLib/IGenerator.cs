using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLib
{
    public interface IGenerator
    {
        public Colours[] generateCode();
    }
}
