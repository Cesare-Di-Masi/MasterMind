namespace MastermindLib
{
    public class CodeBreaker
    {
        private Random rnd = new Random();
        private string _name;
        private int _maxColour;

        public CodeBreaker(string name, int maxColour)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("illegal player name");

            if (maxColour < 1 || maxColour > 20)
                throw new ArgumentOutOfRangeException("illegal maxColour");

            _name = name;
            _maxColour = maxColour;
        }

        public CodeBreaker(int maxColour)
        {
            _name = "player" + rnd.Next(0, 100);
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void NextColour(ref Colours current)
        {
            int cos = (int)current;

            if (cos >= _maxColour - 1)
                current = 0;
            else
                current++;
        }
    }
}