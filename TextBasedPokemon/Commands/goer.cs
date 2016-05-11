using System;

namespace TextBasedPokemon
{
   
   /// Response to try to go to a new place.
   public class Goer : IResponse
   {
      public string CommandName {get; private set;}
      private readonly Game _game;
      
      /// Try to go to one direction. If there is an exit, enter the new
      /// place, otherwise print an error message.
      /// Return false(does not end game)
      public bool Execute(Command command)
      {
         if(!command.HasSecondWord()) {
            // if there is no second word, we don't know where to go...
            Console.WriteLine("Go where?");
            return false;
         }
         var direction = command.SecondWord;
         // Try to leave current place.
         var nextPlace = _game.CurrentPlace.GetExit(direction);
         if (nextPlace == null) {
				Console.WriteLine("You can't go that way.");
         }
         else {
            _game.CurrentPlace = nextPlace;
            Console.WriteLine(nextPlace.GetLongDescription());
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
      public Goer(Game game)
      {
         _game = game;
         CommandName = "go";
      }
   }
}
