using System;
using System.Collections.Generic;

namespace TextBasedPokemon
{
	public class Bagger : IResponse
	{
		public string CommandName {get; private set;}
		private readonly CommandMapper _commandMapper;
		private readonly Dictionary<string, IResponse> _responses;

		//Prints what your inventory has and gives your command words
		public bool Execute(Command cmd)
		{
			if (!cmd.HasSecondWord()) {
				Console.WriteLine ("Your bag contains: ");
				GlobalVar.Player1.Display ();

			}
			else if (_responses.ContainsKey(cmd.SecondWord)) {
				Console.WriteLine(_responses[cmd.SecondWord].Help());
			}
			else {
				Console.WriteLine(
					@"Unknown command {0}!  Command words are
    {1}", cmd.SecondWord, _commandMapper.AllCommands);
			}
			return false;
		}


		/// Constructor for objects of class Helper
		public Bagger (Dictionary<string, IResponse> responses,
			CommandMapper commandMapper)
		{
			this._responses = responses;
			this._commandMapper = commandMapper;
			CommandName = "bag";
		}

		public string Help()
		{
			return @"This is your inventory.
Purchasing new items adds stuff to your bag. Using these
items in battle removes them from your bag. To buy more items,
go to the city or town. To use in battle, type 'use' and then
the item you want to use after the prompt screen
Typing 'bag' will show you what items you currently have.";
		}

	}
}