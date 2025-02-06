namespace MastermindLib
{
    public class CodeGenerator
    {
        private int _codeLength;

        private int _codeComplexity;

        private int _nColours;

        public CodeGenerator(int codeLength, int nColours, int codeComplexity)
        {
            _codeLength = codeLength;
            _codeComplexity = codeComplexity;
            _nColours = nColours;
        }

        public Colours[] GenerateCode()
        {
            Colours[] code = new Colours[_codeLength];
            Colours cl = 0;
            Random rnd = new Random();

            for (int i = 0; i < _codeLength; i++)
            {
                code[i] = cl + rnd.Next(0, _nColours - 1);
            }

            return code;
        }
    }
}