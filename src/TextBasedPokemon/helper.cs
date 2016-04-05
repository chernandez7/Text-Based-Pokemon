using System;
using System.Collections.Generic;

namespace TextBasedPokemon
{
   
   /// Help Response
   public class Helper : IResponse
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
            Console.WriteLine(
@"Are you lost?
                             
Your command words are:
   {0}

{1}", _commandMapper.AllCommands, Help());
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

      public string Help()
      {
         return @"Enter
    help command
for help on the command.";
      }
           
      /// Constructor for objects of class Helper
      public Helper(Dictionary<string, IResponse> responses,
                    CommandMapper commandMapper)
      {
         _responses = responses;
         _commandMapper = commandMapper;
			CommandName = "help";

      }
   }
}
