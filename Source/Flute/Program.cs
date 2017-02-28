using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
      public string extension { get; set; }
   }

   class Mp3SourceProperties : SourceProperties
   {
      public string grammerRole { get; set; }
      public string roleTag { get; set; }
      public string hostTag { get; set; }
   }

   class WebPageSourceProperties : SourceProperties
   {
   }

   class Mp3Downloading : ISourceDownloading
   {
      private SourceProperties _sourceObj;

      public Mp3Downloading(SourceProperties objectToDownload)
      {
         this._sourceObj = objectToDownload;

         if (_sourceObj.fullUrl == null)
         {
            Console.Write($"ERROR @Mp3Downloading: No URL found!");
            return;
         }
      }

      public Stream Download()
      {
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_sourceObj.fullUrl);
         HttpWebResponse response = (HttpWebResponse)request.GetResponse();
         Stream responseStream = response.GetResponseStream();

         return responseStream;
      }


   }

   class WebpageDownloading : ISourceDownloading
   {
      private SourceProperties _sourceObj;

      public WebpageDownloading(SourceProperties objectToDownload)
      {
         this._sourceObj = objectToDownload;

         if (_sourceObj.fullUrl == null)
         {
            Console.Write($"ERROR @WebpageDownloading: No URL found!");
            return;
         }
      }

      public Stream Download()
      {
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_sourceObj.fullUrl);
         HttpWebResponse response = (HttpWebResponse)request.GetResponse();
         Stream responseStream = response.GetResponseStream();

         return responseStream;
      }
   }
}
