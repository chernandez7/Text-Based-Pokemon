using System;

namespace TextBasedPokemon
{
   /// Quit Response 
   public class Quitter : IResponse
   {
      public string CommandName {get; private set;}

      public Quitter()
      {
         CommandName = "quit";

      }
   
      /// "Quit" was entered. Check the rest of the command to see
      /// whether we really quit the game.
      /// Return true, if this command quits the game, false otherwise.
      public bool Execute(Command command)
      {
         if(command.HasSecondWord()) {
            Console.WriteLine("Quit what?");
            return false;
         }
         else {
            return Ui.Agree("Do you really want to quit?");
         }
      }

      public string Help()
      {
         return @"Enter
    quit
to quit the game.";
      }
   }
}
