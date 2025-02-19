namespace MastermindLib
{
    public class CodeGenerator:IGenerator
    {
        private int _codeLength;

        private int _codeComplexity;

        private int _nColours;

        private int[] _chosenColours;

        private int _extracedColours;

        public CodeGenerator(int codeLength, int nColours, int codeComplexity)
        {
            _codeLength = codeLength;
            _codeComplexity = codeComplexity;
            _nColours = nColours;
            _chosenColours = new int[nColours];
        }

        public Colours[] generateCode()
        {
            Colours[] code = new Colours[_codeLength];
            Colours cl = 0;
            Random rnd = new Random();
            bool redo = false;

            for (int i = 0; i < _codeLength; i++)
            {
                while (redo == false)
                {
                    _extracedColours = rnd.Next(0, _nColours - 1);

                    if (_chosenColours[_extracedColours] > _codeComplexity)
                    {
                        redo = true;
                    }
                    else
                    {
                        redo = false;
                        _chosenColours[_extracedColours]++;
                        code[i] = cl + _extracedColours;
                    }
                }

            }

            return code;
        }
    }
}