using System;
using System.Collections.Generic;
using System.Linq;

namespace TextBasedPokemon.Commands
{
	public class BattleSystem
	{
		private readonly Trainer _player1;
		private readonly Trainer _player2;
		private readonly Dictionary<string, List<Move>> _moveDict;
	    private bool _winStatus;
	    private bool _p1TurnStatus;

		public BattleSystem(Trainer player1, Trainer player2, Dictionary<string, List<Move>> moveDict) {
			_player1 = player1;
			_player2 = player2;
			_moveDict = moveDict;
		}

		public void BattleSystemRun()
		{
			_p1TurnStatus = true;

			//General Battle Introduction and Defines active pokemon for P1 and P2 (Or AI).
			Console.WriteLine(_player1.GetName() +" you are challenged by "+ _player2.GetName() +"!");
			var pokRefrence2 = ChooseP(_player2); 
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(_player2.GetName() + " sent out " +  pokRefrence2.GetName() + "!");
			Console.ResetColor ();
			var pokRefrence1 = ChooseP(_player1); 
			Console.WriteLine ("Go! " + pokRefrence1.GetName () + "!");

			//Initializes the beginning of the Battle.
			while (_player1.GetAvailablePokemon ().Count != 0 && _player2.GetAvailablePokemon ().Count != 0)
			{
				//Player 1's Turn
				if (_p1TurnStatus) {
					if (!_player1.GetAiStatus())
						P1Turn (pokRefrence1,pokRefrence2);
					else
						AiTurn(pokRefrence1,pokRefrence2); //Has check if P1 is also AI.

					_p1TurnStatus = false;
				}

				if (pokRefrence2.GetHp () <= 0)
					pokRefrence2 = ChooseP (_player2); 

				//Player 2's Turn
				else {
					if (!_player2.GetAiStatus ())
						P2Turn (pokRefrence1, pokRefrence2); //Has check if P2 is not an AI.
					else 
						AiTurn (pokRefrence2, pokRefrence1);

					_p1TurnStatus =true;
				}

				//Redefines AI's active Pokemon if one faints.
				if (pokRefrence1.GetHp () <= 0 && _player1.GetAvailablePokemon().Count > 0) {
					pokRefrence1 = ChooseP (_player1); 
				}
			}
			//Win Function after Battler concludes.
		    if (_player1.GetAvailablePokemon().Any() && _player2.GetAvailablePokemon().Any()) return;
		    //Player 2 Wins
		    if(!_player1.GetAvailablePokemon().Any()) 
		        P2Win ();

		    //Player 1 Wins
		    else 
		        P1Win ();
		}

		//Chooses the trainer's active Pokemon.
		public Pokemon ChooseP(Trainer trainer)
		{
			if (trainer.GetAiStatus())
				return trainer.GetAvailablePokemon ().Count != 0 ? trainer.GetAvailablePokemon () [0] : null;

			//P2 is not an AI. Manually chosen from available Pokemon.
			else {
				SpaceBefore(trainer.GetName() + ", your available Pokemon are: ");
                FormatStrGreen (Reader.PokeListToStr(trainer.GetAvailablePokemon()));

				var pokeStr = Ui.PromptLine ("Type your chosen Pokemon: ");
				pokeStr = pokeStr.ToLower();
				Space();

				while (trainer.GetPokemonSList().Contains(pokeStr) != true) {
					Console.WriteLine ("Invalid Pokemon. Try Again!");
					pokeStr = Ui.PromptLine ("Type your chosen Pokemon: ");
					pokeStr = pokeStr.ToLower();
					Space();
				}

				for (var p = 0; p < trainer.GetPokemonList().Count; p++) {
					var j = trainer.GetPokemonSList()[p].ToLower();

					if (j == pokeStr) 
						return trainer.GetPokemonList () [p];
				}
				return null;
			}
		}

		//P1's Turn, choosing between moves or items and completes turn.
		public void P1Turn(Pokemon pokRefrence1, Pokemon pokRefrence2) {
			SpaceBefore (_player1.GetName () + ", what will " + pokRefrence1.GetName () + " do?");
			SpaceBefore ("Choose between 'moves' and 'items'.");

			var response = Console.ReadLine ();
		    if (response == null) return;
		    response = response.ToLower();

		    while (response != "moves" && response != "items") {
		        Console.WriteLine ("Invalid Command. Try Again!");
		        response = Ui.PromptLine ("Choose between 'moves' and 'items': ");
		        response = response.ToLower();
		        Space();
		    }
				
		    //if moves are chosen
		    if (response == "moves") {

		        FormatStrGreen (Reader.ListToStr(pokRefrence1.GetMoves()));

		        string move = Ui.PromptLine ("Pick your move: ");
		        move = move.ToLower ();
		        Space ();

		        while (pokRefrence1.GetMoves ().Contains (move) != true) {
		            Console.WriteLine ("Invalid Move. Try Again!");
		            move = Ui.PromptLine ("Pick your move: ");
		            move = move.ToLower ();
		            Space ();
		        }
					
		        int moveVal = int.Parse (_moveDict [move] [0]);
		        pokRefrence2.ModifyHp (-moveVal);
		        if (pokRefrence2.GetHp () > 0) {
		            FormatStrRed (_player2.GetName () + "'s " + pokRefrence2.GetName () + " lost " + moveVal + " HP!",
		                _player2.GetName () + "'s " + pokRefrence2.GetName () + " now has " + pokRefrence2.GetHp () + " HP left!");


		        } else { //Doesn't Allow Negative Numbers in HP
		            FormatStrRed (_player2.GetName () + "'s " + pokRefrence2.GetName () + " lost " + moveVal + " HP!",
		                _player2.GetName () + "'s " + pokRefrence2.GetName () + " now has 0 HP left!");
		        }

		        //if items is chosen
		    } else if (response == "items" && _player1.ItemsCount () != 0) {
		        Console.ForegroundColor = ConsoleColor.Yellow;
		        _player1.Display ();
		        Space ();
		        Console.ResetColor ();
		        string response2 = Ui.PromptLine ("Which item will you choose?: "); 
		        Space ();

		        while (_player1.ContainsStuff (response2) != true) {
		            Console.WriteLine ("Invalid Name. Try Again!");
		            Space ();
		            response2 = Ui.PromptLine ("Which item will you choose?: ");
		            response2 = response2.ToLower ();
		            Space ();
		        }
		        Console.WriteLine (_player1.GetName () + " used " + response2 + "!");
		        Space ();
		        _player1.Use (response2, pokRefrence1);
		        Console.ForegroundColor = ConsoleColor.Green;
		        if (pokRefrence1.GetHp () > 100) {
		            Console.WriteLine (pokRefrence1.GetName () + " now has 100 HP!");
		            pokRefrence1.SetHp (100);
		        } else {
		            Console.WriteLine (pokRefrence1.GetName () + " now has " + pokRefrence1.GetHp () + " HP!");
		        }
		        Space ();
		        _player1.RemoveItem (response2);
		        Console.ResetColor ();
		    } else {
		        Console.WriteLine ("You have no items to use.");
		        Space ();
		        Console.WriteLine ("You lost your turn!");
		        _p1TurnStatus = false;
		    }
		}

		//Turn made for an AI to automatically follow a certain behavior
		public void AiTurn(Pokemon attacker,Pokemon victim)
		{
			var c = new Random ();

			if (_p1TurnStatus == true) {


				if (_player1.ItemsCount () == 0) {
					//while (Player1.GetItems ().Contains (RandItem) != true) {
					//Console.WriteLine ("This item is not in your inventory.");
					//goto moves;
					//}
					moves:
					//AI will randomly attack if HP is above 25%
					if (attacker.GetHp () >= 25) {

						//moves:
						var moveInt = c.Next (0, attacker.GetMoves ().Count);

						var move = attacker.GetMoves () [moveInt];

						var moveVal = int.Parse (_moveDict [move] [0]);
						Console.WriteLine ();
						if (_p1TurnStatus == true) {

							Console.WriteLine (_player1.GetName () + "'s " + attacker.GetName () + " used " + move + "!");
							victim.ModifyHp (-moveVal);
						} else {
							Console.WriteLine (_player2.GetName () + "'s " + attacker.GetName () + " used " + move + "!");
							victim.ModifyHp (-moveVal);
						}
							
						if (victim.GetHp () > 0) {
							if (_p1TurnStatus == true) {
								FormatStrRed (_player2.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player2.GetName () + "'s " + victim.GetName () + " now has " + victim.GetHp () + " HP left!");
							} else {
								FormatStrRed (_player1.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player1.GetName () + "'s " + victim.GetName () + " now has " + victim.GetHp () + " HP left!");
							}

						} else { //Doesn't Allow Negative Numbers in HP
							if (_p1TurnStatus == false) {
								FormatStrRed (_player1.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player1.GetName () + "'s " + victim.GetName () + " now has 0 HP left!");
							} else {
								FormatStrRed (_player2.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player2.GetName () + "'s " + victim.GetName () + " now has 0 HP left!");
							}
						}
					}

				} else {
					var randItem = _player1.GetItems () [0];

					_player1.Use (randItem, victim);
					Console.WriteLine (_player1.GetName () + " used " + randItem + "!");
					Space ();
					_player1.RemoveItem (randItem);

					Console.ForegroundColor = ConsoleColor.Green;
					if (victim.GetHp () > 100) {
						Console.WriteLine (attacker.GetName () + " now has 100 HP!");
						victim.SetHp (100);
					} else {
						Console.WriteLine (attacker.GetName () + " now has " + attacker.GetHp () + " HP!");
					}
					Console.ResetColor ();
				}

			} else {

				if (_player2.ItemsCount () == 0) {

					//AI will randomly attack if HP is above 25%
					if (attacker.GetHp () >= 25) {

						//moves:
						var moveInt = c.Next (0, attacker.GetMoves ().Count);

						var move = attacker.GetMoves () [moveInt];

						var moveVal = int.Parse (_moveDict [move] [0]);
						Console.WriteLine ();
						if (_p1TurnStatus == true) {

							Console.WriteLine (_player1.GetName () + "'s " + attacker.GetName () + " used " + move + "!");
							victim.ModifyHp (-moveVal);
						} else {
							Console.WriteLine (_player2.GetName () + "'s " + attacker.GetName () + " used " + move + "!");
							victim.ModifyHp (-moveVal);
						}
							
						if (victim.GetHp () > 0) {
							if (_p1TurnStatus == true) {
								FormatStrRed (_player1.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player1.GetName () + "'s " + victim.GetName () + " now has " + victim.GetHp () + " HP left!");
							} else {
								FormatStrRed (_player2.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player2.GetName () + "'s " + victim.GetName () + " now has " + victim.GetHp () + " HP left!");
							}

						} else { //Doesn't Allow Negative Numbers in HP
							if (_p1TurnStatus == false) {
								FormatStrRed (_player1.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player1.GetName () + "'s " + victim.GetName () + " now has 0 HP left!");
							} else {
								FormatStrRed (_player2.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player2.GetName () + "'s " + victim.GetName () + " now has 0 HP left!");
							}

						}
					}

				} else {

					if (attacker.GetHp () <= 25) {
						var randItem = _player2.GetItems () [0];

						_player2.Use (randItem, attacker);
						Console.WriteLine (_player2.GetName () + " used " + randItem + "!");
						Space ();
						_player2.RemoveItem (randItem);

						Console.ForegroundColor = ConsoleColor.Green;
						if (victim.GetHp () > 100) {
							Console.WriteLine (attacker.GetName () + " now has 100 HP!");
							victim.SetHp (100);
						} else {
							Console.WriteLine (attacker.GetName () + " now has " + attacker.GetHp () + " HP!");
						}
						Console.ResetColor ();

					} else {

						var moveInt = c.Next (0, attacker.GetMoves ().Count);

						var move = attacker.GetMoves () [moveInt];

						var moveVal = int.Parse (_moveDict [move] [0]);
						Console.WriteLine ();
						if (_p1TurnStatus == true) {

							Console.WriteLine (_player2.GetName () + "'s " + attacker.GetName () + " used " + move + "!");
							victim.ModifyHp (-moveVal);
						} else {
							Console.WriteLine (_player1.GetName () + "'s " + attacker.GetName () + " used " + move + "!");
							victim.ModifyHp (-moveVal);
						}

						if (victim.GetHp () > 0) {
							if (_p1TurnStatus == true) {
								FormatStrRed (_player2.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player2.GetName () + "'s " + victim.GetName () + " now has " + victim.GetHp () + " HP left!");
							} else {
								FormatStrRed (_player1.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player1.GetName () + "'s " + victim.GetName () + " now has " + victim.GetHp () + " HP left!");
							}

						} else { //Doesn't Allow Negative Numbers in HP
							if (_p1TurnStatus == false) {
								FormatStrRed (_player1.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player1.GetName () + "'s " + victim.GetName () + " now has 0 HP left!");
							} else {
								FormatStrRed (_player2.GetName () + "'s " + victim.GetName () + " lost " + moveVal + " HP!",
									_player2.GetName () + "'s " + victim.GetName () + " now has 0 HP left!");
							}

						}
					}
				}

			}
		}

		//P2's Turn, choosing between moves or items and completes turn.
		public void P2Turn(Pokemon pokRefrence1,Pokemon pokRefrence2)
		{
			SpaceBefore (_player2.GetName () + ", what will " + pokRefrence2.GetName () + " do?");
			Space ();
			SpaceBefore ("Choose between 'moves' and 'items'.");
			Space ();

			var response = Console.ReadLine ();
			response.ToLower ();
			while (response != "moves" && response != "items") {
				Console.WriteLine ("Invalid Command. Try Again!");
				response = Ui.PromptLine ("Choose between 'moves' and 'items': ");
				response = response.ToLower ();
				Space ();
			}

				//If moves are chosen
				if (response == "moves") {

					FormatStrGreen (Reader.ListToStr (pokRefrence2.GetMoves ()));

					var move = Ui.PromptLine ("Pick your move: ");
					move = move.ToLower ();
					Space ();

					while (pokRefrence2.GetMoves ().Contains (move) != true) {
						Console.WriteLine ("Invalid Move. Try Again!");
						move = Ui.PromptLine ("Pick your move: ");
						move = move.ToLower ();
						Space ();
					}


					var moveVal = int.Parse (_moveDict [move] [0]);
					pokRefrence1.ModifyHp (-moveVal);

					if (pokRefrence1.GetHp () > 0) {
						FormatStrRed (_player1.GetName () + "'s " + pokRefrence1.GetName () + " lost " + moveVal + " HP!",
							_player1.GetName () + "'s " + pokRefrence1.GetName () + " now has " + pokRefrence1.GetHp () + " HP left!");


					} else { //Doesn't Allow Negative Numbers in HP
						FormatStrRed (_player1.GetName () + "'s " + pokRefrence1.GetName () + " lost " + moveVal + " HP!",
							_player1.GetName () + "'s " + pokRefrence1.GetName () + " now has 0 HP left!");
						//Add Else If for over 100 (Problem being that need to assign actual HP aswell)?
					}

					//Items are chosen instead
				} else if (response == "items" && _player2.ItemsCount () != 0) {
					Console.ForegroundColor = ConsoleColor.Yellow;
					_player2.Display ();
					Space ();
					Console.ResetColor ();
					var response2 = Ui.PromptLine ("Which item will you choose: "); //Add check for not being stuck in loop
					Space ();

					while (_player2.ContainsStuff (response2) != true) {
						Console.WriteLine ("Invalid Name. Try Again!");
						Space ();
						response2 = Ui.PromptLine ("Which item will you choose: ");
						response2 = response2.ToLower ();
						Space ();
					}

					Console.WriteLine (_player2.GetName () + " used " + response2 + "!");
					Space ();
					_player2.Use (response2, pokRefrence2);
					Console.ForegroundColor = ConsoleColor.Green;
					if (pokRefrence2.GetHp () > 100) {
						Console.WriteLine (pokRefrence2.GetName () + " now has 100 HP!");
						pokRefrence2.SetHp (100);
					} else {
						Console.WriteLine (pokRefrence2.GetName () + " now has " + pokRefrence2.GetHp () + " HP!");
					}
					Space ();
					_player2.RemoveItem (response2);
					Console.ResetColor ();
				}
					
			else {
					Console.WriteLine ("Invalid Command or Out of Items. Try Again!");
					response = Ui.PromptLine ("Choose between 'moves' and 'items': ");
					response = response.ToLower ();
					Space ();
				}
			
		}
	

		//P1 wins, changes winstatus to true.
		public void P1Win()
		{
			FormatStrBlue (_player2.GetName () + " is out of usable Pokemon!",
				"Congratulations " + _player1.GetName () + "! You Defeated Pokemon Trainer " + _player2.GetName () + "!");
			Space ();
			_winStatus = true;

			return;
		}

		//P2 Wins and keeps winstatus as false.
		public void P2Win()
		{
			FormatStrBlue ("You are out of usable Pokemon!",
				"You were defeated by " + _player2.GetName () + "!");
			Space ();
			return;
		}

		//Format to code to add color and spaces.
		public void FormatStrGreen(string s)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine (s);
			Console.ResetColor();
			Space ();
		}

		//Format to code to add color and spaces.
		public void FormatStrBlue(string s,string s2)
		{
			Space ();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine (s);
			Console.WriteLine (s2);
			Console.ResetColor ();
		}

		//Format to code to add color and spaces.
		public void FormatStrRed(string s,string s2)
		{
			Space ();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (s);
			Console.WriteLine (s2);
			Console.ResetColor ();
		}

		//Simple formatting shortcut.
		public void SpaceBefore(string s)
		{
			Space ();
			Console.WriteLine (s);
		}

		//Empty Line Shortcut.
		public void Space()
		{
			Console.WriteLine ();
		}
			
		//Tells if P1 has won the battle or not.	
		public bool GetWinStatus()
		{
		    return _winStatus;
		}
	}
}

