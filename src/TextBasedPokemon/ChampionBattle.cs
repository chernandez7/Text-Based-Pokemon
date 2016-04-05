using System;
using System.Collections.Generic;

namespace TextBasedPokemon
{
	public class Champ : IResponse
	{
		public string CommandName {get; private set;}
		private readonly Game _game;


		public bool Execute(Command command)
		{

			if (_game.CurrentPlace == _game.GetNamedPlace ("Champion")) {

				//Prompts player for secret password to fight the champion.
				var p1Response = Ui.PromptLine ("What is the secret Password: ");

				if (p1Response == "Onix") {

				    var tempList2 = new List<Pokemon>
				    {
				        GlobalVar.PokeList2[3],
				        GlobalVar.PokeList2[4],
				        GlobalVar.PokeList2[5],
				        GlobalVar.PokeList2[6],
				        GlobalVar.PokeList2[1],
				        GlobalVar.PokeList2[7]
				    };

				    //Champion is difficult so he is assigned with 6 Predefined Pokemon
				    //Assigned Charizard
				    //Assigned Zapdos
				    //Assigned Gengar
				    //Assigned Lapras
				    //Assigned Pikachu
				    //Assigned Blastoise


				    const string p2Name = "Pokemon Trainer Red";
					Console.WriteLine ();
					var trainer2 = new Trainer (p2Name, tempList2, true);


					var bs = new BattleSystem (GlobalVar.Player1, trainer2, GlobalVar.MoveDir);
					bs.BattleSystemRun ();

					foreach (var poke in GlobalVar.Player1.GetPokemonList())
					{
						poke.MaxHp();
					}
					foreach (var poke in trainer2.GetPokemonList())
					{
						poke.MaxHp();
					}
					//If the player beats the champion they win the game and get a nice reward!
					if (bs.GetWinStatus()) {
						GlobalVar.Player1.ModifyCash (2000);
						Console.WriteLine ("You gained 2000 Cash");
						Console.WriteLine ("Your total Cash is "+ GlobalVar.Player1.GetCash()+"!");

						Console.WriteLine ();
						Console.WriteLine ("Congratulations! You Won!");
						Console.WriteLine ("You are the new Pokemon Master!");
						Console.WriteLine ("Thank you for playing Pokemon!");

						//If player loses they have unlimited tries but need to continue to battle to make money.
					} else {
						Console.WriteLine ();
						Console.WriteLine ("You Lost. To start a new battle type 'champ' then password again.");
					}
				} else {
					Console.WriteLine ("Type the password to fight the Champion");
				}
			
			}			
		
		else {
			Console.WriteLine ("Go south of the arena to fight the champion!");
		}
		return false;
	}



		public string Help()
		{
			return @"Enter
'champ' and then the password to fight the champion
The password is recieved after winning 10 battles in the
arena. The champion is south of the arena!";
		}

		/// Constructor for objects of class Goer
		public Champ(Game game)
		{
			this._game = game;
			CommandName = "champ";

		}
	}
}
