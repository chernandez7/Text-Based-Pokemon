using System;
using System.Collections.Generic;

namespace TextBasedPokemon
{

	/// Help Response
	public class Mp : IResponse
	{
		public string CommandName {get; private set;}
		private readonly CommandMapper _commandMapper;
		private readonly Dictionary<string, IResponse> _responses;

		/// Print out some Help information.
		/// Here we print some stupid, cryptic message and a list of the 
		/// command words.
		public bool Execute(Command cmd)
		{
			if (!cmd.HasSecondWord()) {
				Console.WriteLine (@"
       The Forest is in the center of the map.
	  The City is East of the Forest.
	  The Town is West of the Forest.
	  The Arena is South of the Forest.
	  The Multiplayer Area is North of the Forest.

      Type 'go' and then a direction to
      enter a new area.
");





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
		public Mp(Dictionary<string, IResponse> responses,
			CommandMapper commandMapper)
		{
			_responses = responses;
			_commandMapper = commandMapper;
			CommandName = "map";


		}

		public string Help()
		{
			return @"
The map shows where you can go.";
		}

	}
}
