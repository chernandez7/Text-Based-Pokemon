using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextBasedPokemon
{
	public static class Test
	{
		/// Reads the dictionary contents and returns them.
		public static Dictionary<string,List<string>> GetDictionary(StreamReader reader)
		{
			var d = new Dictionary<string, List<string>> ();

			while (!reader.EndOfStream)
			{
				var key = reader.ReadLine();
			    if (key != null)
			    {
			        key = key.ToLower ();
			        var bodyS = reader.ReadLine();
			        if (bodyS != null)
			        {
			            bodyS = bodyS.ToLower ();
			            var body = bodyS.Split (' ').ToList ();
			            d [key] = body;
			        }
			    }
			    reader.ReadLine ();
			}
			return d;
		}

		//Takes the keys of the dictionary and returns them as a string.
		public static string DirToStr(Dictionary<string,List<string>> d)
		{
			var tmpS = "";
			foreach (var i in d.Keys)
				tmpS = tmpS + i;
				
			return tmpS;
		}

		//Used to read the text files and get the information correctly.
		public static string ReadParagraph(StreamReader reader)
		{                                       
			var strAdd = "";
			var tempR = reader.ReadLine();

			while (tempR != null && tempR.Trim() != "") {
				strAdd = strAdd + tempR + "\n";
				tempR = reader.ReadLine();
			}
			return strAdd;
		}

		//Creates and returns the list of all the pokemon in the text file.
		public static List<Pokemon> MakePokemonList (Dictionary<string,List<string>> pokeDir)
		{
			var pokeList = new List<Pokemon> ();
			foreach (var i in pokeDir.Keys){
				var pokemon = new Pokemon(i, pokeDir[i]);
					pokeList.Add(pokemon);
					}
			return pokeList;
		}

		//Shortcut to turn a list to a string.
		public static string ListToStr(List<string> list)
		{
			var tmpS = "";
			foreach (var t in list)
			    tmpS = tmpS +t + " ";

		    return tmpS;
		}

		//Shortcut to return all the names of the Pokemon in the list as a string.
		public static string PokeListToStr(List<Pokemon> list)
		{
			var tmpS = "";
			foreach (var t in list)
			    tmpS = tmpS +t.GetName() + " ";

		    return tmpS;
		}


	}
}

