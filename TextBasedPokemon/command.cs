using System;

namespace TextBasedPokemon
{
   public class Command
   {
      public string CommandWord {get; private set;}
      public string SecondWord {get; private set;}
   
      public static Command GetCommand()
      {
         string cmd;
         string parameter = null;
         var line = Ui.PromptLine("> ").Trim();

         var i = line.IndexOf(" ", StringComparison.Ordinal);
         if (i == -1) {
            cmd = line;     
         }
         else {
            cmd = line.Substring(0, i);
            parameter = line.Substring(i).Trim();
         }
         return new Command(cmd, parameter);
      }

      /// Create a command object. First and second word must be supplied, but
      /// either one (or both) can be null.
      public Command(string firstWord, string secondWord)
      {
         CommandWord = firstWord;
         SecondWord = secondWord;
      }
      
      /// Return true if the command has a second word.
      public bool HasSecondWord()
      {
         return (SecondWord != null);
      }
   }
}
