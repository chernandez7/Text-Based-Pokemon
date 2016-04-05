using System;
using System.Collections.Generic;
using System.IO;

namespace TextBasedPokemon
{
   
	/// ASCII Art used from http://www.fiikus.net/?pokedex 
   public class Game 
   {
      public Place CurrentPlace {get; set;} // controls public access
      private readonly Dictionary<string, Place> _places; //places can be found by name
      private readonly CommandMapper _commandMapper;


      public static void Main(string[] args)
      {
			//Initializes basic global variables by either just defining or
			//Reading in the info from files
		
			GlobalVar.NumbWins = 0;

			var reader = Fio.OpenReader("Text Documents/Move_List.txt");
			GlobalVar.MoveDir = Test.GetDictionary (reader);
			reader.Close();

            var pokeReader = Fio.OpenReader("Text Documents/Pokemon_List.txt");
			var pokeDir = Test.GetDictionary (pokeReader);
			pokeReader.Close();

			//2 seperate lists are needed so that if both players have the same pokemon it would be a copy and not
			//the EXACT same pokemon
			GlobalVar.PokeList = Test.MakePokemonList (pokeDir);
			GlobalVar.PokeList2 = Test.MakePokemonList (pokeDir);


			Console.ForegroundColor = ConsoleColor.Black;

			//Once variables are defined then the game begins!
         Game game = new Game();
         game.Play();
			Console.ResetColor();

      }

      public Place GetNamedPlace(string name) {
         return _places[name];
      }
       
      /// Create the game and initialise its internal map.
      public Game()
      {
         _places = Place.CreatePlaces("Text Documents/place_data.txt");
		CurrentPlace = _places["Forest"];
		_commandMapper = new CommandMapper(this);
	
		
      }
   
      ///  Main play routine.  Loops until end of play.
      public void Play()
      {

		printWelcome();
         // Enter the main command loop.  Here we repeatedly read commands and
         // Execute them until the game is over.
         while (! ProcessCommand(Command.GetCommand()))
         {
         }
          Console.WriteLine("Thank you for playing!");
      }
   
      /// Print out the opening message for the player.
      private void printWelcome()
      {
			Console.ForegroundColor = ConsoleColor.White;
			GlobalVar.P1Name = Ui.PromptLine ("What is Player 1's Name: ");
			Console.WriteLine ();

			var tempList1 = new List<Pokemon> ();

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("eevee, pikachu, charmander");
			Console.ResetColor ();
			Console.WriteLine();
			Console.WriteLine ("Pick your first Pokemon!");
			var startResponse = Ui.PromptLine ("Which Pokemon would you like to start with?: ");


			while (startResponse != "eevee" && startResponse != "pikachu" && startResponse != "charmander") {
				Console.WriteLine ("Invalid Name. Try Again!");
				startResponse = Ui.PromptLine ("Pick your Pokemon: ");

				Console.WriteLine();
			}

			if (startResponse == "eevee") {
				tempList1.Add (GlobalVar.PokeList [0]);
			}
			else if (startResponse == "pikachu") {
				tempList1.Add (GlobalVar.PokeList [1]);
			}
			else if (startResponse == "charmander") {
				tempList1.Add (GlobalVar.PokeList [2]);

			}

			GlobalVar.Player1 = GlobalVar.P1Name == "AI" ? new Trainer (GlobalVar.P1Name, tempList1, true) : new Trainer (GlobalVar.P1Name, tempList1, false);


			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine (
				@"
	                                 .::.
	                               .;:**'            
	                               `                  
	   .:XHHHHk.              db.   .;;.     dH  MX   
	 oMMMMMMMMMMM       ~MM  dMMP :MMMMMR   MMM  MR      ~MRMN
	 QMMMMMb  ""MMX       MMMMMMP !MX' :M~   MMM MMM  .oo. XMMM 'MMM
	   `MMMM.  )M> :X!Hk. MMMM   XMM.o""  .  MMMMMMM X?XMMM MMM>!MMP
	    'MMMb.dM! XM M'?M MMMMMX.`MMMMMMMM~ MM MMM XM `"" MX MMXXMM
	     ~MMMMM~ XMM. .XM XM`""MMMb.~*?**~ .MMX M t MMbooMM XMMMMMP
	      ?MMM>  YMMMMMM! MM   `?MMRb.    `""""""   !L""MMMMM XM IMMM
	       MMMX   ""MMMM""  MM       ~%:           !Mh."""""" dMI IMMP
	       'MMM.                                             IMX
	        ~M!M                                             IMP
");
			Console.ForegroundColor = ConsoleColor.White;
			Console.ReadLine ();
			Console.Clear ();
			Console.WriteLine( @"
	                 .""-,.__
	                 `.     `.  ,
	              .--'  .._,'""-' `.
	             .    .'         `'
	             `.   /          ,'
	               `  '--.   ,-""'
	                `""`   |  \
	                   -. \, |
	                    `--Y.'      ___.
	                         \     L._, \
	               _.,        `.   <  <\                _
	             ,' '           `, `.   | \            ( `
	          ../, `.            `  |    .\`.           \ \_
	         ,' ,..  .           _.,'    ||\l            )  '"".
	        , ,'   \           ,'.-.`-._,'  |           .  _._`.
	      ,' /      \ \        `' ' `--/   | \          / /   ..\
	    .'  /        \ .         |\__ - _ ,'` `        / /     `.`.
	    |  '          ..         `-...-""  |  `-'      / /        . `.
	    | /           |L__           |    |          / /          `. `.
	   , /            .   .          |    |         / /             ` `
	  / /          ,. ,`._ `-_       |    |  _   ,-' /               ` \
	 / .           \""`_/. `-_ \_,.  ,'    +-' `-'  _,        ..,-.    \`.
	  '         .-f    ,'   `    '.       \__.---'     _   .'   '     \ \
	' /          `.'    l     .' /          \..      ,_|/   `.  ,'`     L`
	|'      _.-""""` `.    \ _,'  `            \ `.___`.'""`-.  , |   |    | \
	||    ,'      `. `.   '       _,...._        `  |    `/ '  |   '     .|
	||  ,'          `. ;.,.---' ,'       `.   `.. `-'  .-' /_ .'    ;_   ||
	|| '              V      / /           `   | `   ,'   ,' '.    !  `. ||
	||/            _,-------7 '              . |  `-'    l         /    `||
	 |          ,' .-   ,' ||                | .-.        `.      .'     ||
	 `'        ,'    `"".'    |               |    `.        '. -.'       `'
	          /      ,'      |               |,'    \-.._,.'/'
	          .     /        .               .       \    .''
	        .`.    |         `.             /         :_,'.'
	          \ `...\   _     ,'-.        .'         /_.-'
	           `-.__ `,  `'   .  _.>----''.  _  __  /
	                .'        /""'          |  ""'   '_
	               /_|.-'\ ,"".             '.'`__'-( \
	                 / ,""'""\,'               `/  `-.|"" mh

          
                  	Welcome to the world of Pokemon!");

			Console.ReadLine ();
			Console.WriteLine (@"
	                         This is a game by:

			  Morgan Rose    Christopher Hernandez
			  Nathan Blais   Kija Ndekeja

");
			Console.ReadLine ();
			Console.Write ("Pokemon ");
			Console.WriteLine("are animal-like creatures with mysterious powers. Humans and");
			Console.WriteLine ("Pokemon travel alongside each other as friends and partners.");
			Console.WriteLine ("People also battle their Pokemon against other Pokemon to test");
			Console.WriteLine ("their strengths as a team. These people are called Pokemon Trainers.");
			Console.WriteLine ("They battle as teams with thier Pokemon in Arenas.");
			Console.WriteLine ();
			Console.ReadLine ();
			Console.WriteLine ("Today you start your very own Pokemon journey!");
			Console.WriteLine ("Your dream has always been to become the Pokemon Champion by mastering");
			Console.WriteLine ("your Pokemon's strengths by battling your way to the top in the Arena!");
			Console.ReadLine ();
			Console.Clear ();


			Console.WriteLine ();
			Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Your goal is to battle your way to the top of the Pokemon World.");
				Console.WriteLine("The current Pokemon Champion is Red - he is currently south of the Arena");
			Console.WriteLine("and is awaiting new challengers. You should hone your skills and then take him");
			Console.WriteLine("on if you wish to become the new champion and beat the game!");
				Console.WriteLine();
			Console.WriteLine ("You start your journey off in the forest away from home.");

			Console.WriteLine ();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine ("You know the Arena is south of the forest. There is also a Town");
				Console.WriteLine ("to the West and a City to the East. You can buy potions in the City");
				Console.WriteLine ("and new Pokemon in the Town! Make sure you visit each if you want");
			Console.WriteLine ("to become a Pokemon master!");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.ReadLine ();
			Console.Write ("Type '");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write ("help");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("' for help.");
			Console.Write(" Type '");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("map");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("'To see what places you could go.");
			Console.Write(" Type '");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("quit");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("' to quit.");

			Console.ReadLine ();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine ();
			Console.Write("Type '");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("bag");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("' to check your items. You already have 4 health-restoring potions.");

			//Starts with the player off with a couple of free potions
			GlobalVar.Player1.AddItem ("potion");
			GlobalVar.Player1.AddItem ("potion");
			GlobalVar.Player1.AddItem ("super potion");
			GlobalVar.Player1.AddItem ("super potion");

			Console.WriteLine ("You should go south from here to get to the Arena and start battling.");
			Console.WriteLine("After successful each battle, you'll win money. Spend this on new Pokemon");
			Console.WriteLine("and on items to make battling easier! Remeber to visit the City and Town for each.");
			Console.Write ("To travel you need to type the command ");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write ("go ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write ("followed by the ");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("direction ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("you wish to travel.");
			Console.ReadLine ();
			Console.ResetColor ();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ("You are currently in the forest.");
			Console.ResetColor();
			Console.WriteLine ("You should go south of here to start battles. Type 'go south'");
			Console.WriteLine ("to get started!");

      }
   
      /// Given a command: process (that is: Execute) the command.
      /// Return true If the command ends the game, false otherwise.
      private bool ProcessCommand(Command command)
		{
			if (!_commandMapper.IsCommand (command.CommandWord)) {
				Console.WriteLine ("Try something different.");
				return false;
			}
			var response = _commandMapper.GetResponse (command.CommandWord);
			return response.Execute (command);
		
		}
   }
}
