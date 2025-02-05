namespace MastermindLib
{
    public class GameManager
    {
        private int _codeComplexity;
        private int _codeLength;
        private Colours[] _codeSolution;
        private int _nAttempts;
        private int _nColours;
        private bool _isBotOn;
        private bool _isColorBlind;
        private bool _isAllWrong;
        private int _rightPosition;
        private int _wrongPosition;

        private CodeGenerator _bot;

        public GameManager(bool isBotOn ,bool isColorBlind,int codeLength,int nColours, int nAttempts, int codeComplexity)
        {
            _bot = new CodeGenerator(codeLength,nColours,codeComplexity);
            //_codeSolution = _bot.GenerateCode();
            _nAttempts = nAttempts;
            _codeLength = codeLength;
            _nColours = nColours;
            _isColorBlind = isColorBlind;
            
        }

        public GameManager(Colours[] codeSolution,bool isBotOn ,bool isColorBlind,int codeLength, int nColours, int nAttempts, int codeComplexity):this(isBotOn,isColorBlind, codeLength, nColours,nAttempts,codeComplexity)
        {
            _codeSolution = codeSolution;
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
                return _nColours;
            }
        }

        public int CodeComplexity
        {
            get
            {
                return _codeComplexity;
            }
        }

        public bool IsColorBlindness
        {
            get
            {
                return _isColorBlind;
            }
        }

        public int RightPosition
        {
            get
            {
                return _rightPosition;
            }
        }

        public int WrongPosition
        {
            get 
            {
                return _wrongPosition;
            }
        }

        public bool IsAllWrong
        {
            get
            {
                return _isAllWrong;
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
            _rightPosition = 0;
            for(int i = 0; i<codeToCheck.Length; i++)
            {
                if(codeToCheck[i] == _codeSolution[i])
                {
                    correct = true;
                    _rightPosition++;
                }
                i++;
            }
            _isAllWrong = _rightPosition == 0;


            return correct;

        }
    }
}