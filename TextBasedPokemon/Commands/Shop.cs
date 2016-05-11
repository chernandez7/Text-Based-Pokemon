using System;

namespace TextBasedPokemon
{

	/// Response to try to go to a new place.
	public class Shopper : IResponse
	{
		public string CommandName {get; private set;}
		private Game game;
	
		public bool Execute(Command command)
		{
			if (game.CurrentPlace == game.GetNamedPlace ("City")) {
				Console.WriteLine (@"
Welcome to the shop! Type 'buy' to purchase an item!
Type 'exit' to leave.

             What would you like to buy?

potion:         $50  - Restore your Pokemon's health by 30HP 

super potion:   $100 - Restore your Pokemon's health by 60HP

	Type 'exit' to leave the shop.

Your current balance is {0}
", GlobalVar.Player1.GetCash ());

				string userresponse = "";

				while (userresponse != "exit") {
					Console.Write ("Buy: ");
					userresponse = Console.ReadLine ().ToLower ();

					if (userresponse == "potion" || userresponse == "super potion") {

						//Checks if funds are sufficient to buy selected items and if it is then it adds to inventory.
						if (userresponse == "potion") {
							if (GlobalVar.Player1.GetCash () >= 50) {
								GlobalVar.Player1.ModifyCash (-50);
								Console.WriteLine ("Your current cash value is: {0}", GlobalVar.Player1.GetCash ());
								Console.WriteLine ("You bought {0} ", userresponse);
								GlobalVar.Player1.AddItem (userresponse);
							} else {
								Console.WriteLine ("You don't have sufficient funds to purchase {0}", userresponse);
							}
						}

							if (userresponse == "super potion") {
								if (GlobalVar.Player1.GetCash () >= 100) {
									GlobalVar.Player1.ModifyCash (-100);
									Console.WriteLine ("Your current cash value is: {0}", GlobalVar.Player1.GetCash ());
									Console.WriteLine ("You bought {0} ", userresponse);
									GlobalVar.Player1.AddItem (userresponse);
								} else {
									Console.WriteLine ("You don't have sufficient funds to purchase {0}", userresponse);
								}

							}
						 else if (userresponse == "exit") {
								Console.WriteLine ("Thank you for shopping.");
							} else {
								Console.WriteLine ("Buy what?");
							}
						}
					}
				}
	
				
		
			//*************Second Shop********************//

				if (game.CurrentPlace == game.GetNamedPlace ("Town")) {

					Console.WriteLine (@"
Welcome to the Pokemon Center! You can obtain new Pokemon here.
Type 'exit' to leave.

             What Pokemon would you like to add to your team?
rayquaza:	$2000 - Rayquaza is a Dragon/Flying type Pokémon.

venusaur:	$2000 - Venusaur is a Grass/Poison type Pokémon.

pidgeot:	$2000 - Pidgeot is a Normal/Flying type Pokémon.

charizard:	$2000 - Charizard is a Fire/Flying type Pokémon.

espeon:	$2000 - Espeon is a Psychic type Pokémon.

		Type 'exit' to leave the shop.

Your current balance is {0}."
, GlobalVar.Player1.GetCash ());

					string userresponse2 = "";

					while (userresponse2 != "exit") {
						Console.Write ("Buy: ");
						userresponse2 = Console.ReadLine ().ToLower ();


					//Allows player to buy pokemon and add them to their team to battle with.
					if (userresponse2 == "rayquaza" || userresponse2 == "venusaur" || userresponse2 == "pidgeot" || userresponse2 == "charizard"
						|| userresponse2 == "espeon") {

						if (userresponse2 == "rayquaza" && GlobalVar.Player1.GetPokemonSList().Contains("rayquaza") == false) {
							if (GlobalVar.Player1.GetCash () >= 2000) {
								GlobalVar.Player1.ModifyCash (-2000);
								Console.WriteLine ("You bought a {0}! {1} is now in your team! ", userresponse2,userresponse2);
								Console.WriteLine ("Your current cash amount is: {0}", GlobalVar.Player1.GetCash ());
								GlobalVar.Player1.AddPokemon (8);
							} else {
								Console.WriteLine ("You don't have sufficient funds to purchase {0} or you already purchased this pokemon.", userresponse2);
							}
						}
						else if (userresponse2 == "venusaur" && GlobalVar.Player1.GetPokemonSList().Contains("venusaur") == false) {
							if (GlobalVar.Player1.GetCash () >= 2000) {
								GlobalVar.Player1.ModifyCash (-2000);
								Console.WriteLine ("You bought a {0}! {1} is now in your team! ", userresponse2,userresponse2);
								Console.WriteLine ("Your current cash amount is: {0}", GlobalVar.Player1.GetCash ());
								GlobalVar.Player1.AddPokemon (9);
							} else {
								Console.WriteLine ("You don't have sufficient funds to purchase {0} or you already purchased this pokemon.", userresponse2);
							}
						}
						else if (userresponse2 == "pidgeot"&& GlobalVar.Player1.GetPokemonSList().Contains("pidgeot") == false) {
							if (GlobalVar.Player1.GetCash () >= 2000) {
								GlobalVar.Player1.ModifyCash (-2000);
								Console.WriteLine ("You bought a {0}! {1} is now in your team! ", userresponse2,userresponse2);
								Console.WriteLine ("Your current cash amount is: {0}", GlobalVar.Player1.GetCash ());
								GlobalVar.Player1.AddPokemon (10);
							} else {
								Console.WriteLine ("You don't have sufficient funds to purchase {0} or you already purchased this pokemon.", userresponse2);
							}
						}
						else if (userresponse2 == "charizard" && GlobalVar.Player1.GetPokemonSList().Contains("charizard") == false) {
							if (GlobalVar.Player1.GetCash () >= 2000) {
								GlobalVar.Player1.ModifyCash (-2000);
								Console.WriteLine ("You bought a {0}! {1} is now in your team! ", userresponse2,userresponse2);
								Console.WriteLine ("Your current cash amount is: {0}", GlobalVar.Player1.GetCash ());
								GlobalVar.Player1.AddPokemon (3);
							} else {
								Console.WriteLine ("You don't have sufficient funds to purchase {0} or you already purchased this pokemon.", userresponse2);
							}
						}
						else if (userresponse2 == "espeon"&& GlobalVar.Player1.GetPokemonSList().Contains("espeon") == false) {
							if (GlobalVar.Player1.GetCash () >= 2000) {
								GlobalVar.Player1.ModifyCash (-2000);
								Console.WriteLine ("You bought a {0}! {1} is now in your team! ", userresponse2,userresponse2);
								Console.WriteLine ("Your current cash amount is: {0}", GlobalVar.Player1.GetCash ());
								GlobalVar.Player1.AddPokemon (11);
							} else {
								Console.WriteLine ("You don't have sufficient funds to purchase {0} or you already purchased this pokemon.", userresponse2);
							}
						}

						else if (userresponse2 == "exit") {
									Console.WriteLine ("Thank you for shopping.");
								} 

						else {
							Console.WriteLine ("You don't have sufficient funds to purchase {0} or you already purchased this pokemon.", userresponse2);
								}
							}
						}
				}

			return false;
		
				if (game.CurrentPlace != game.GetNamedPlace ("City")) {
					Console.WriteLine ("Go to a city or town to buy something.");

				}
		
			 else {
				return false;
			}
		}
			

		public string Help()
		{
			return @"Type 'buy' when in a city or town. You will
be prompted 'buy what?' and then will be able to type in the name
of the item you wish to buy. Potions restore a Pokemon's Health Points,
while Ethers restore Power Points (a number of moves a Pokemon can do).";
		}

		/// Constructor for objects of class Goer
			public Shopper(Game game)
		{
			this.game = game;
			CommandName = "buy";
		}
	}
}
