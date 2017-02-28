using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flute
{
   class Program
   {
      static void Main(string[] args)
      {

         Console.ReadLine();
      }
   }

   /// <summary>
   /// Downloading sources on internet.
   /// </summary>
   interface ISourceDownloading
   {
      //TODO: Implement
   }

   /// <summary>
   /// Saving downloaded source as a file.
   /// </summary>
   interface ISourceSaving
   {
      //TODO: Implement
   }

   /// <summary>
   /// Get link copied into clipboard.
   /// </summary>
   interface ILinkMagnetor
   {
      //TODO: Implement 
   }

   /// <summary>
   /// Handling *.cfg file.
   /// </summary>
   interface IUserConfiguration
   {
      //TODO: Implement
   }

   /// <summary>
   /// Detecting properties related to object need to be download.
   /// </summary>
   interface IPropertiesDetector
   {
      //TODO: Implement
   }

   abstract class SourceProperties
   {
      public string name { get; set; }
      public string saveTo { get; set; }
      public string host { get; set; }
      public string fullUrl { get; set; }
      public string relativeUrl { get; set; }
   }

   class Mp3SourceProperties : SourceProperties
   {
      public string extension { get; set; }
      public string grammerRole { get; set; }
      public string roleTag { get; set; }
      public string hostTag { get; set; }
   }
}
