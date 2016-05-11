using System.Collections.Generic;
using TextBasedPokemon.Commands;

namespace TextBasedPokemon
{
   
   /// Map commands names to commands.
   public class CommandMapper
   {
      public string AllCommands {get; private set;}
      private readonly Dictionary<string, IResponse> _responses; //responses to commands

       
      /// Initialize the command response mapping
      ///  game The game being played.
		public CommandMapper(Game game)
      {
         _responses = new Dictionary<string, IResponse>();
         IResponse[] resp = {

            new Quitter(),
			new Mp(_responses, this),
            new Goer(game),
            new Helper(_responses, this),
			new Bagger (_responses, this),
			new Battler (game),
			new Champ (game),
			new Versus (game),
			new Shopper(game)

         };
         AllCommands = "";
         foreach (var r in resp) {
            _responses[r.CommandName] = r;
            AllCommands += r.CommandName + " ";
         }
      }
   
      /// Check whether aString is a valid command word. 
      /// Return true if it is, false if it isn't.
      public bool IsCommand(string aString)
      {
         return _responses.ContainsKey(aString);
      }
   
      /// Return the command associated with a command word.
      ///  cmdWord The command word.
      /// Return the Response for the command.
      public IResponse GetResponse(string cmdWord)
      {
         return _responses[cmdWord];
      }
   }
}
