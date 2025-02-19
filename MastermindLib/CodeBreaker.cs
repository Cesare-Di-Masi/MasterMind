namespace MastermindLib
{
    public class CodeBreaker
    {
        private Random rnd = new Random();
        private string _name;

        public CodeBreaker(string name,int maxColour)
        {
            _name = name;
        }

        public CodeBreaker()
        {
            _name = "player" + rnd.Next(0,100);
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void ChangeSelectedColour(ref Colours current)
        { 
            current++;
        }
    }
}