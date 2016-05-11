using System;
using System.Collections.Generic;
using TextBasedPokemon.Commands;

namespace TextBasedPokemon
{

	/// Response to try to go to a new place.
	public class Versus : IResponse
	{
		public string CommandName {get; private set;}
		private readonly Game _game;

		//A bonus feature of the game which was our original battle system before an AI was created.
		//You can control both players so you and a friend can play!
		public bool Execute(Command cmd)
		{


			if (_game.CurrentPlace == _game.GetNamedPlace ("Multiplayer")) {

				var tempList2 = new List<Pokemon> ();
				var c = new Random ();

				//Gives Player 2 the same amount of Pokemon as Player 1 (Random Pokemon)
				foreach (var poke in GlobalVar.Player1.GetPokemonList()) {
				
					var Poke = c.Next (0, GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2 [Poke]);
				}
					

				const string p2Name = "Player 2";

					Console.WriteLine ();

				var trainer2 = new Trainer (p2Name, tempList2, false);

				trainer2.AddItem ("potion");
				trainer2.AddItem ("potion");
				trainer2.AddItem ("super potion");
				trainer2.AddItem ("super potion");

					var bs = new BattleSystem (GlobalVar.Player1, trainer2, GlobalVar.MoveDir);
					bs.BattleSystemRun ();

				//No rewards for multiplayer battles due to it being exploitable and only for fun.
					if (bs.GetWinStatus()) {
						
					Console.WriteLine ("Congratulations " + GlobalVar.Player1.GetName () + "!" + " You defeated " + trainer2.GetName () + ".");

					} else {
						Console.WriteLine ();
					Console.WriteLine ("Congratulations " + trainer2.GetName () + "!" + " You defeated " + GlobalVar.Player1.GetName () + ".");

					}
				
			
			}
			else {
				Console.WriteLine ("Go north of the forest to fight Multiplayer battle!");
			}
			return false;
		}


		public string Help()
		{
			return @"
This is for multiplayer battles. Go north of the arena and type 'versus'
to start a 2-player battle between you and a friend.";
		}

		/// Constructor for objects of class Goer
		public Versus(Game game)
		{
			_game = game;
			CommandName = "versus";

		}
	}
}
