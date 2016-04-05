using System.Collections.Generic;
using System.IO;

namespace TextBasedPokemon
{
   
   
   public class Place
   {
      public string Description { get; private set; }

      private readonly Dictionary<string, Place> _exits; // stores exits of this place.
   
		//Create the "Place"
      public Place (string description)
      {
         Description = description;
         _exits = new Dictionary<string, Place> ();
      }
       
      /// Create places and their interconnections by taking place names, exit 
      /// data and descriptions from a text file.  
      /// Return a map of place names to places.  File format for each place: 
      ///   First line:  place name (one word)
      ///   Second line: pairs of exit direction and neighbor place name 
      ///   Remaining paragraph: place description, blank line terminated  
      public static Dictionary<string, Place> CreatePlaces (string fileName)
      {
         var reader = Fio.OpenReader(fileName);
         // Map to return
         var places = new Dictionary<string, Place> ();
          
         // temporary Map to delay recording exits until all places exist
         var exitStrings = new Dictionary<string, string> ();
          
         while (!reader.EndOfStream) {
            var name = reader.ReadLine ();
            var exitPairs = reader.ReadLine ();
            // You could also substitute your lab's ReadParagraph for the two
            //   lines below if you want to format each paragraph line yourself.
            var description = TextUtil.LineWrap(reader);
            reader.ReadLine(); // assume empty line after description
             places [name] = new Place (description);
             exitStrings [name] = exitPairs;
         }
         reader.Close ();
         // need places before you can map exits
         // go back and use exitPairs to map exits:
         foreach (var name in places.Keys) {
            var place = places [name];
            var parts = TextUtil.SplitWhite(exitStrings[name]);
            for (var i = 0; i < parts.Length; i += 2) {
               place.SetExit (parts [i], places [parts [i + 1]]);
            }
         }
         return places;
      }
		
		public Dictionary<string, Place> Getplaces(){
			return CreatePlaces ("place_data.txt");
		}



      // Define an exit from this place.
      //  Going to the exit in this direction 
      //  leads to neighbor place.
      public void SetExit (string direction, Place neighbor)
      {
         _exits [direction] = neighbor;
      }
      
      /// Return a description of the place in the form:
      ///     You are in the kitchen.
      ///     Exits: north west
      public string GetLongDescription ()
      {
         return "You are " + Description + "\n" + GetExitstring ();
      }
   
      /// Return a string describing the place's exits, for example
      /// "Exits: north west".
      private string GetExitstring ()
      {
         var s = "Exits: ";
         foreach (var e in _exits.Keys) {
            s += e + ' ';
         }
         return s;
      }
   
      /// Return the place that is reached if we go from this place in direction
      /// "direction". If there is no place in that direction, return null.
      public Place GetExit (string direction)
      {
          return _exits.ContainsKey (direction) ? _exits [direction] : null;
      }
   }
}
