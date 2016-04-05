using System.IO;

namespace TextBasedPokemon
{
   /// File I/O functions to help find files and folders
   /// when they may be in more than one path.
   public class Fio
   {
      private static readonly string[] Paths = { ".", "..", Path.Combine("..", "..") };

                                         // GetLocation chunk
      /// Return a directory conaining the filename, if it exists. 
      /// Otherwise return null.
      public static string GetLocation(string filename)
      {                                  // body chunk
         foreach (var dir in Paths) {
            var filePath = Path.Combine(dir, filename);
            if (File.Exists(filePath))
               return dir;
         }
         return null;
      }
                                       // GetPath chunk
      /// Find a directory containing the filename
      /// and return the full file path, if it exists. 
      /// Otherwise return null.
      public static string GetPath(string filename)
      {
         foreach (var dir in Paths) {
            var filePath = Path.Combine(dir, filename);
            if (File.Exists(filePath))
               return filePath;
         }
         return null;
      }
                                      //OpenReader chunk
      /// Find a directory containing filename;
      /// return a new StreamReader to the file
      /// or null if the file does not exist.
      public static StreamReader OpenReader(string filename)
      {
          var filePath = GetPath(filename);
          return filePath == null ? null : new StreamReader(filePath);
      }

       //OpenReader2 chunk
      /// Join the location directory and filename;
      /// return a new StreamReader to the file
      /// or null if the file does not exist.
      public static StreamReader OpenReader(string location, string filename)
      {
          var filePath = Path.Combine(location, filename);
          return File.Exists(filePath) ? new StreamReader(filePath) : null;
      }

       // OpenWriter chunk     
      /// Join the location directory and filename;
      /// open and return a StreamWriter to the file. 
      public static StreamWriter OpenWriter(string location, string filename) 
      {           
         var filePath = Path.Combine(location, filename);
         return new StreamWriter(filePath);
      }
   }                                        //end OpenWriter chunk                                      
}    

