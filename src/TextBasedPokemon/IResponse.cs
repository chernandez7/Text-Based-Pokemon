namespace TextBasedPokemon
{
   /// Object that responds to a command
   public interface IResponse
   {
      /// Execute cmd.
      /// Return true if the game is over; false otherwise 
      bool Execute(Command cmd);

      /// Return a Help string for the command 
	  string Help();


      string CommandName {get;}

   }
}
