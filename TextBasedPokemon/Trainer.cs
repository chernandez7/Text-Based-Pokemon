using System;
using System.Collections.Generic;

namespace TextBasedPokemon
{
	//Defines what actuallty makes up a "Trainer"
	public class Trainer
	{
		private readonly string _name;
		private readonly List<Pokemon> _pokemonList;
		private int _cash = 500;
		private readonly bool _aiStatus;
		private readonly List<string> _items;
	    private int _x;


		public Trainer(string name, List<Pokemon> pokemonList, bool aiStatus) { 
			_name = name;
			_pokemonList = pokemonList;
			_aiStatus = aiStatus;
			_items = new List<string>();
		}

		/// Gets the name. Wherever the name would be printed this would be used.
		public string GetName() { return _name; }

		/// Gets the pokemon list. To check current pokemon in party.
		public List<Pokemon> GetPokemonList() { return _pokemonList; }

		//Gets name of Pokemon in a string. Used for showing available moves.
		public List<string> GetPokemonSList() {  
			var tmpL = new List<string>();
			if(_pokemonList.Count == 0) {
				tmpL[0] = "nothing";
			} else {
				foreach (var t in _pokemonList) {
				    tmpL.Add(t.GetName ());
				}
			}
			return tmpL;
		}

		//Gets a list of available pokemon. Used to see which Pokemon a trainer can use.
		public List<Pokemon> GetAvailablePokemon() {  
			var tmpList = new List<Pokemon> ();

			foreach (var t in _pokemonList) {
			    if (t.GetHp() > 0)
			        tmpList.Add (t);
			}
		    return tmpList;
		}

		//Adds Pokemon to the list that the trainer has.
		public void AddPokemon(int pokNum) {
			_pokemonList.Add(GlobalVar.PokeList[pokNum]);
		}

		/// Gets the cash. Check your current amount of Cash.
		public int GetCash() { return _cash; }

		/// Modifies the cash. Used to add or subtract Cash.
		public int ModifyCash(int cost) {   
			_cash += cost;
			return _cash;
		}
			
		//Checks if there will be an AI applied to the Trainer.
		public bool GetAiStatus() { return _aiStatus; }

		//Returns the item list of the Trainer
		public List<string> GetItems() { return _items; }

		//Uses a item in the trainers inventory on a specific pokemon.
		public void Use(string name, Pokemon poke)
		{
		    if (name == "potion") {
		        poke.ModifyHp(30);
		        Console.ForegroundColor = ConsoleColor.Green;
		        Console.WriteLine(poke.GetName() + " restored 30 HP!");
		    }
		    else if (name == "super potion") {
		        poke.ModifyHp(60);
		        Console.ForegroundColor = ConsoleColor.Green;
		        Console.WriteLine(poke.GetName() + " restored 60 HP!");
		    }
            //TODO
            else if (name == "ether") {
                
            }
            else if (name == "max ether") {

            }
            else if (name == "elixir") {

            }
            else if (name == "max elixir") {
                
            }
        }

        //Adds items to the trainers inventory.
        public void AddItem(string name) {
			_items.Add(name);
		}

		//Removes items from the trainers inventory.
		public void RemoveItem(string name) {
			if ((_x = _items.IndexOf(name)) >= 0)
			{
				_items.RemoveAt(_x);
			}
		}

		//Shortcut to see if the inventory contains a certain item.
		public bool ContainsStuff(string name)
		{
		    return _items.Contains (name);
		}

	    //Shortcut to print the inventory out as a string.
		public void Display()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			foreach (var t in _items)
			{
			    Console.Write(t + "  ");
			}
		    Console.WriteLine ();
			Console.ResetColor ();
		}
			
		//Returns the number of items in the inventory of the trainer.
		public int ItemsCount()
		{
			return _items.Count;
		}
	}

}


