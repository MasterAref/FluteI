using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
   interface IStreamSaving
   {
      //TODO: Implement
   }

   /// <summary>
   /// Get link copied into clipboard.
   /// </summary>
   interface ILinkMagnet
   {
      //TODO: Implement 
   }

   /// <summary>
   /// Handling *.cfg file.
   /// </summary>
   abstract class UserConfiguration
   {
      private string configPath { get; set; }
      private ConfigObject _configObject;
      private Dictionary<string, string> cfgSettingsDictionary = new Dictionary<string, string>()
      {
         ["sources"] = "",
         ["sourcesTag"] = "",
         ["rolesTag"] = "",
         ["saveTo"] = ""
      };

      /// <summary>
      /// Instantiate 'configPath' property.
      /// </summary>
      public UserConfiguration()
      {
         if (IsDefConfigExist())
         {
            Console.WriteLine("Configuration file found!");
         }
         else
         {
            string tempUserCmd = "";
            do
            {
               if (tempUserCmd == "")
               {
                  Console.WriteLine("Can not find config.cfg file!You have 2 option:\n" +
                                    "Type path of existing *.cfg file.\n" +
                                    "type 'C' to create new one.");
                  //TODO: Can be send to utility method just for showing messages!
               }
               else
               {
                  Console.WriteLine(new string('-', 10));
                  Console.WriteLine("We can not find anything, yet! Try again or type 'C' for create new cfg file.");
               }
               tempUserCmd = Console.ReadLine().ToLower();
            } while (tempUserCmd != "c" && !IsConfigExist(tempUserCmd));


            //   NOTE: Going out of loop means Either
            //1.user entered 'c' to create new config file.
            //2.We found new config file.
            if (tempUserCmd == "c")
            {
               //TODO: Call method to create new config.
            }
            else
            {
               // JUST A NOTIFICATION.
               Console.WriteLine("We found your config file.");
            }
         }

         // 'configPath' CHECKING TO BE SET.
         if (string.IsNullOrEmpty(configPath))
         {
            //TODO: throw an ERROR
         }
      }

      private bool IsDefConfigExist()
      {
         if (File.Exists($"{Environment.CurrentDirectory}\\config.cfg") == true)
         {
            configPath = Environment.CurrentDirectory + "\\config.cfg";
            return true;
         }
         return false;
      }

      public bool IsConfigExist(string path)
      {
         if (File.Exists(path) == true)
         {
            configPath = path;
            return true;
         }
         return false;
      }

      public void ReadConfig()
      {
         string[] configlines = File.ReadAllLines(configPath);

         foreach (var line in configlines)
         {
            string currentLine = line.Replace(" ", String.Empty);
            string lineKey = currentLine.Remove(currentLine.IndexOf(':'));
            string lineValue = currentLine.Remove(0, currentLine.IndexOf(':') + 1);

            if (cfgSettingsDictionary.ContainsKey(lineKey))
            {
               cfgSettingsDictionary[lineKey] = lineValue;
            }
            else
            {
               //TODO: Maybe add other lines (which are unofficial) to another dictionary  & use it someHow!!!
            }
         }
      }
   }

   abstract class ConfigObject
   {
      public Dictionary<string, string> SourcesDictionary { get; set; }
      public Dictionary<string, string> SourcesTagDictionary { get; set; }
      public Dictionary<string, string> RolesTagDictionary { get; set; }
      public string saveTo { get; set; }

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

   class StreamSaving : IStreamSaving
   {
      private SourceProperties _sourceObj;
      private Stream _dataStream;

      public StreamSaving(Stream downloadedStream, SourceProperties objectToSave)
      {
         if (objectToSave == null || downloadedStream == null) return;

         if (String.IsNullOrEmpty(_sourceObj.name))
         {
            Console.WriteLine("ERROR @MP3FileSaving: Something not right. File NAME not set.");
            return;
         }
         if (String.IsNullOrEmpty(_sourceObj.saveTo))
         {
            Console.WriteLine("ERROR @MP3FileSaving: Something not right. file PATH not set.");
            return;
         }
         if (String.IsNullOrEmpty(_sourceObj.extension))
         {
            Console.WriteLine("ERROR @MP3FileSaving: Something not right. file EXTENSION not set.");
            return;
         }
         this._sourceObj = objectToSave;
         this._dataStream = downloadedStream;
      }

      private bool SaveAsFile()
      {
         string fullPath = _sourceObj.saveTo + _sourceObj.name + _sourceObj.extension;
         using (FileStream fs = File.Create(fullPath))
         {
            _dataStream.CopyTo(fs);
         }

         if (!File.Exists(fullPath)) return false;
         return true;
      }
   }

   class ClipboardMagnet : ILinkMagnet
   {
      [DllImport("User32.dll")]
      private static extern bool OpenClipboard(IntPtr hWndNewOwner);

      [DllImport("User32.dll")]
      private static extern IntPtr GetClipboardData(uint uFormat);

      [DllImport("User32.dll")]
      private static extern bool EmptyClipboard();

      [DllImport("User32.dll")]
      private static extern bool CloseClipboard();

      public string CopyLink()
      {
         OpenClipboard(IntPtr.Zero);
         string data = Marshal.PtrToStringAuto(GetClipboardData(13));

         CloseClipboard();
         //CAUTION: if not closing clipboard while program is running, other program can not copy anything into clipboard.

         //TODO: check if string is null/empty 
         return data;
      }
   }

   class CfgConfiguration : UserConfiguration
   {
      //TODO: Implement
   }
}
