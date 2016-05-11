using System;
using System.Collections.Generic;

namespace TextBasedPokemon.Commands
{
	/// Response to try to go to a new place.
	public class Battler : IResponse
	{
		public string CommandName {get; private set;}
		private readonly Game _game;

		public bool Execute(Command command)
		{
			//Doesn't allow battle outside of Arena area
			if (_game.CurrentPlace == _game.GetNamedPlace ("Arena")) {

				var tempList2 = new List<Pokemon>();
				var c = new Random ();

				//Adds more Pokemon to enemies depending on how many victories you have had so far.
				if (GlobalVar.NumbWins <5){
					var poke = c.Next (0,GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2 [poke]);
				}
				else if (GlobalVar.NumbWins >5 && GlobalVar.NumbWins <10){
					/*for (int i = 0;i<1;i++){
					int Poke = c.Next (0,GlobalVar.PokeList2.Count);
					TempList2.Add (GlobalVar.PokeList2[Poke]);
					}*/
					var poke = c.Next (0,GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2[poke]);
					var poke2 = c.Next (0,GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2[poke2]);
				}
				else if (GlobalVar.NumbWins >10 && GlobalVar.NumbWins <15){
					var poke = c.Next (0,GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2[poke]);
					var poke2 = c.Next (0,GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2[poke2]);
					var poke3 = c.Next (0,GlobalVar.PokeList2.Count);
					tempList2.Add (GlobalVar.PokeList2[poke3]);
					}
				else if (GlobalVar.NumbWins >15 && GlobalVar.NumbWins <20){
				var poke = c.Next (0,GlobalVar.PokeList2.Count);
				tempList2.Add (GlobalVar.PokeList2[poke]);
				var poke2 = c.Next (0,GlobalVar.PokeList2.Count);
				tempList2.Add (GlobalVar.PokeList2[poke2]);
				var poke3 = c.Next (0,GlobalVar.PokeList2.Count);
				tempList2.Add (GlobalVar.PokeList2[poke3]);
				var poke4 = c.Next (0,GlobalVar.PokeList2.Count);
				tempList2.Add (GlobalVar.PokeList2[poke4]);
					}
				else if (GlobalVar.NumbWins >20 && GlobalVar.NumbWins <25){
			var poke = c.Next (0,GlobalVar.PokeList2.Count);
			tempList2.Add (GlobalVar.PokeList2[poke]);
			int Poke2 = c.Next (0,GlobalVar.PokeList2.Count);
			tempList2.Add (GlobalVar.PokeList2[Poke2]);
			int Poke3 = c.Next (0,GlobalVar.PokeList2.Count);
			tempList2.Add (GlobalVar.PokeList2[Poke3]);
			int Poke4 = c.Next (0,GlobalVar.PokeList2.Count);
			tempList2.Add (GlobalVar.PokeList2[Poke4]);
			int Poke5 = c.Next (0,GlobalVar.PokeList2.Count);
			tempList2.Add (GlobalVar.PokeList2[Poke5]);
					}
				else{
		int Poke = c.Next (0,GlobalVar.PokeList2.Count);
		tempList2.Add (GlobalVar.PokeList2[Poke]);
		int Poke2 = c.Next (0,GlobalVar.PokeList2.Count);
		tempList2.Add (GlobalVar.PokeList2[Poke2]);
		int Poke3 = c.Next (0,GlobalVar.PokeList2.Count);
		tempList2.Add (GlobalVar.PokeList2[Poke3]);
		int Poke4 = c.Next (0,GlobalVar.PokeList2.Count);
		tempList2.Add (GlobalVar.PokeList2[Poke4]);
		int Poke5 = c.Next (0,GlobalVar.PokeList2.Count);
		tempList2.Add (GlobalVar.PokeList2[Poke5]);
		int Poke6 = c.Next (0,GlobalVar.PokeList2.Count);
		tempList2.Add (GlobalVar.PokeList2[Poke6]);
					}

				//Creates enemy Trainer and Gives some Potions to enemy before battle
				var p2Name = "Arena Challenger";
				Console.WriteLine ();
				var trainer2 = new Trainer(p2Name, tempList2, true);
				trainer2.AddItem ("potion");
				trainer2.AddItem ("potion");
				trainer2.AddItem ("super potion");
				trainer2.AddItem ("super potion");


				var bs = new BattleSystem (GlobalVar.Player1, trainer2, GlobalVar.MoveDir);
				bs.BattleSystemRun ();

				//After battle restores HP of both players.
				foreach (var poke in GlobalVar.Player1.GetPokemonList())
				{
					poke.MaxHp();
				}
				foreach (var poke in trainer2.GetPokemonList())
				{
					poke.MaxHp();
				}

				//Rewards the player for winning and gives the next instructions
				if (bs.GetWinStatus() == true) {
					GlobalVar.NumbWins +=1;
					Console.WriteLine ("Your current Win count is {0}!",GlobalVar.NumbWins);
					Console.WriteLine ();
					Console.ForegroundColor = ConsoleColor.Blue;
					GlobalVar.Player1.ModifyCash (500);
					Console.WriteLine ("You gained 500 Cash from your victory!");
					Console.WriteLine ("Your total Cash is now "+ GlobalVar.Player1.GetCash()+"!");
					Console.ResetColor ();
					Console.WriteLine ("You are almost good enough to fight the Champion!");
					Console.WriteLine ("Type 'battle' again to fight another trainer!");


					//if the player has won enought they get a secret password
					if (GlobalVar.NumbWins >= 10){
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine ();
						Console.WriteLine ("Go south again to fight the Champion!");
						Console.WriteLine ("The special password is 'Onix'!");
						Console.ResetColor();
					}

					//If player loses then they don't get anything out of the battles.
				} else {
					Console.WriteLine ("You Lost. To start a new battle type 'battle'.");
					Console.WriteLine ("Maybe you aren't set out to be the Champion...");

				}
			} else {
				Console.WriteLine ("Go to the arena to battle. The arena is south of the forest.");
			}
			return false;
		}





		public string Help()
		{
			return @"Enter
    go direction
to exit the current place in the specified direction.
The direction should be in the list of exits for the current place.";
		}

		/// Constructor for objects of class Goer
		public Battler(Game game)
		{
			this._game = game;
			CommandName = "battle";
		}
	}
}
