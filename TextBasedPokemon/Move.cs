using System;
using System.CodeDom;

namespace TextBasedPokemon
{

/*
 *  Physical attacks were any moves of the types Normal, Fighting, Poison, Ground, Flying, Bug, Rock, Ghost or Steel
*/

/*
 * Special attacks were those having the Fire, Water, Electric, Grass, Ice, Psychic, Dragon or Dark type.
*/

    class Move
    {
        private readonly string _name;
        private readonly string _element;
        private readonly int _maxPp;
        private int _currentPp;
        private int _power;
        private int _accuracy;
        private  bool _isSpecial;
        private  bool _isStatus;

        public Move(string name, int maxPp, int power, int accuracy,
            string element, bool isSpecial, bool isStatus) {
            _name = name;
            _maxPp = maxPp;
            _power = power;
            _accuracy = accuracy;
            _element = element;
            _isSpecial = isSpecial;
            _isStatus = isStatus;
            _currentPp = _maxPp;
        }

        // Get Move name
        public string GetName() { return _name; }

        // Get Element Name
        public string GetElement() { return _element; }

        // Get Max Power Points
        public int GetMaxPP() { return _maxPp; }

        // Get Current PP
        public int GetCurrentPP() { return _currentPp; }

        // Set Current PP
        public void SetCurrentPP(int newPP) {
            _currentPp = newPP;
        }

        // Add to current PP
        public void AddPp(int value) {
            _currentPp += value;
        }

        // Subtract to Current PP
        public void SubPp(int value) {
            _currentPp -= value;
        }

        // Get Current Power
        public int GetPower() { return _power; }

        //Add to Current Power
        public void AddPower(int value) {
            _power += value;
        }

        // Subtract to Current Power
        public void SubPower(int value) {
            _power -= value;
        }

        // Set Power
        public void SetPower(int value) {
            _power = value;
        }

        // Get Current Power
        public int GetAccuracy() { return _accuracy; }

        //Add to Current Power
        public void AddAccuracy(int value) {
            _accuracy += value;
        }

        // Subtract to Current Power
        public void SubAccuracy(int value) {
            _accuracy -= value;
        }

        // Set Power
        public void SetAccuracy(int value) {
            _accuracy = value;
        }

        public bool GetIsSpecial() { return _isSpecial; }

        public void SetIsSpecial(bool value) {
            _isSpecial = value;
        }

        public bool GetIsStatus() { return _isStatus; }

        public void SetIsStatus(bool value)
        {
            _isStatus = value;
        }
    }
}