namespace MastermindLib
{
    public class GameManager
    {
        private int _codeComplexity;
        private int _codeLength;
        private Colours[] _codeSolution;
        private int _nAttempts;
        private int _nColours;
        private int _isBotOn;

        private CodeGenerator _bot;

        public GameManager(bool isBotOn,int codeLength,int nColours, int nAttempts, int codeComplexity)
        {
            _bot = new CodeGenerator(codeLength,nColours,codeComplexity);
            //_codeSolution = _bot.GenerateCode();
            _nAttempts = nAttempts;
        }

        public GameManager(Colours[] codeSolution,int codeLength, int nColours, int nAttempts, int codeComplexity)
        {
            
        }

        public int NAttempts
        {
            get
            {
                return _nAttempts; 
            }

        }

        public int CodeLength
        {
            get
            {

            return _codeLength; 
            }
        }

        public int NColours
        {
            get
            {
                return NCo
            }
        }

        public int CodeComplexity
        {
            get => default;
        }

        public bool IsColorBlindness
        {
            get => default;
        }

        public int RightPosition
        {
            get => default;
            set
            {
            }
        }

        public int WrongPosition
        {
            get => default;
            set
            {
            }
        }

        public int IsAllWrong
        {
            get => default;
            set
            {
            }
        }

        public CodeGenerator CodeGenerator
        {
            get => default;
            set
            {
            }
        }

        public CodeBreaker CodeBreaker
        {
            get => default;
            set
            {
            }
        }

        //stampa la soluzione del codice nel caso si sia indovinato o siano finite le vite
        public void GiveColourCode()
        {
            throw new System.NotImplementedException();
        }

        public bool EndOfTheTurn(Colours[] codeToCheck)
        {
            if(CheckGuess(codeToCheck) || _nAttempts == 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckGuess(Colours[] codeToCheck)
        {
            bool correct = true;
            int i = 0;
            while (correct || i < codeToCheck.Length)
            {
                correct = codeToCheck[i] == _codeSolution[i];
                i++;
            }

            return correct;

        }
    }
}