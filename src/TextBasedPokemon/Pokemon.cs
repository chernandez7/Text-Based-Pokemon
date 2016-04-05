using System.Collections.Generic;

namespace TextBasedPokemon
{
    public class Pokemon

    {
        private readonly string _name;
        private int _hp;
        private readonly List<string> _moves;

        /// Initializes a new instance of the Pokemon class.
        public Pokemon(string name, List<string> moves)
        {
            _name = name;
            _hp = 100;
            _moves = moves;
        }

        /// Gets the name. Used when the Pokemon's name will be printed.
        public string GetName()
        {
            return _name;
        }

        /// Gets the moves. Returns the moves of the specified Pokemon.
        public List<string> GetMoves()
        {
            return _moves;
        }

        /// Gets the current HP. Used when you want to check or print HP.
        public int GetHp()
        {
            return _hp;
        }

        /// Shortcut to reset Pokemon's HP to full.
        public void MaxHp()
        {
            _hp = 100;
        }

        /// Modifies the HP (Either take damage or heal)
        public int ModifyHp(int heal)
        {
            _hp += heal;
            return _hp;
        }

        //Sets the HP to a certain number.
        public int SetHp(int number)
        {
            _hp = number;
            return _hp;
        }
    }
}