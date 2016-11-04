// This is free and unencumbered software released into the public domain.
//
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
//
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// For more information, please refer to <http://unlicense.org/>

using System;
using System.Diagnostics;
using System.IO;
using Eto.Forms;
using Ionic.Zip;
using System.Net;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ServoUtility
{
    public static class FreeSO
    {
        static string libName = "ServoUtility";
        static string checkIni = " Edit ServoUtility.ini to change the location.";
        static string fallbackUrl = "www.google.com";
        static string curDir = Environment.CurrentDirectory;
        public static string libVer = libName + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Returns all files by their extensions within the current directory.
        /// You only need to type in the name of the extension.
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static FileInfo[] FileWildCard(string fileExt)
        {
            var dir = new DirectoryInfo(curDir);
            var files = dir.GetFiles("*." + fileExt.ToLower()).Where(p => p.Extension == "." + fileExt.ToLower()).ToArray();
            return files;
        }

        /// <summary>
        /// Returns all files by their extensions in the selected directory.
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static FileInfo[] FileWildCard(string fileExt, string fileDir)
        {
            var dir = new DirectoryInfo(fileDir);
            var files = dir.GetFiles("*." + fileExt.ToLower()).Where(p => p.Extension == "." + fileExt.ToLower()).ToArray();
            return files;
        }

        /// <summary>
        /// Returns a given URL. If it isn't there,
        /// defualt to Google.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>new Uri(url);</returns>
        [Obsolete]
        public static Uri WebPage(string url)
        {
            try
            {
                return new Uri(url);
            }
            catch
            {
                return new Uri(fallbackUrl);
            }
        }

        /// <summary>
        /// Returns a custom URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns>new Uri(url);</returns>
        public static Uri WebURI(string url)
        {
            try
            {
                return new Uri(url);
            }
            catch
            {
                return new Uri(fallbackUrl);
            }
        }

        /// <summary>
        /// Launches the FreeSO IDE executable with the specified
        /// game arguments defined in the global settings.
        /// 
        /// Defualt settings: FSO.IDE.exe located in same
        /// directory and the game arguments are "800x600 w".
        /// 
        /// To allow changes for these settings, use the Configure class.
        /// </summary>
        public static void RunClient()
        {
            var isGUI = GlobalSettings.Default.isGUI;
            var fso = GlobalSettings.Default.Client;
            var notFound = fso + checkIni;
            var fsoProcess = new Process();

            if (File.Exists(fso))
            {
                fsoProcess.StartInfo.FileName = fso;
                fsoProcess.StartInfo.UseShellExecute = true;
                fsoProcess.StartInfo.Arguments = GlobalSettings.Default.Args;
                fsoProcess.Start();
            }
            else
            {

                switch (isGUI)
                {
                    case false:
                        Console.WriteLine(notFound);
                        break;
                    case true:
                    default:
                        MessageBox.Show(notFound);
                        break;
                }

            }
        }

        /// <summary>
        /// Launches the FreeSO IDE executable with the specified
        /// game arguments defined in the global settings.
        /// 
        /// Defualt settings: Volcanic.exe located in same
        /// directory and the game arguments are "800x600 w".
        /// 
        /// To allow changes for these settings, use the Configure class.
        /// </summary>
        public static void RunIDE()
        {
            var isGUI = GlobalSettings.Default.isGUI;
            var ide = GlobalSettings.Default.IDE;
            var notFound = ide + checkIni;
            var fsoProcess = new Process();

            if (File.Exists(ide))
            {
                fsoProcess.StartInfo.FileName = ide;
                fsoProcess.StartInfo.UseShellExecute = true;
                fsoProcess.StartInfo.Arguments = GlobalSettings.Default.Args;
                fsoProcess.Start();
            }
            else
            {
                switch (isGUI)
                {
                    case false:
                        Console.WriteLine(notFound);
                        break;
                    case true:
                    default:
                        MessageBox.Show(notFound);
                        break;
                }
            }
        }

        /// <summary>
        /// Launches a third party client or application.
        /// </summary>
        public static void RunThirdParty(string thirdparty)
        {
            var isGUI = GlobalSettings.Default.isGUI;
            var notFound = thirdparty + checkIni;
            var fsoProcess = new Process();

            if (File.Exists(thirdparty))
            {
                fsoProcess.StartInfo.FileName = thirdparty;
                fsoProcess.StartInfo.UseShellExecute = true;
                fsoProcess.StartInfo.Arguments = GlobalSettings.Default.Args;
                fsoProcess.Start();
            }
            else
            {
                switch (isGUI)
                {
                    case false:
                        Console.WriteLine(notFound);
                        break;
                    case true:
                    default:
                        MessageBox.Show(notFound);
                        break;
                }
            }
        }

        /// <summary>
        /// Return the latest dist number as a string
        /// Thanks to LRB. http://forum.freeso.org/threads/974/
        /// </summary>
        /// <returns>sLine</returns>
        static string DistNumLegacy()
        {
            string url = "http://servo.freeso.org/externalStatus.html?js=1";
            string sDistNumError = "???";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            string sLine = "";
            string fll;
            fll = objReader.ReadLine();
            sLine = fll.Remove(0, 835);
            sLine = sLine.Remove(sLine.IndexOf("</a>"));
            int value;
            //sLine = "FAKE ERROR";
            if (int.TryParse(sLine, out value)) //If we've an integer...
            {
                return sLine; //Return it, ignoring value since sLine's already a string.
            }
            else //Otherwise,
            {
                sLine = sDistNumError; //Set sLine to the error message defined above,
                return sLine; //and send that.
            }
        }

        /// <summary>
        /// New disNum method.
        /// </summary>
        /// <returns></returns>
        // TODO: Put new HTML parser logic here. Make public afterwords. Currently returns DistNum.
        public static string DistNum()
        {
            return DistNumLegacy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ReadBuild(string file)
        {
            string line;

            try
            {
                string buildFile = curDir + @"/" + file;
                var fileRead = new StreamReader(buildFile);
                while ((line = fileRead.ReadLine()) != null)
                {
                    return "#" + line;
                }

                fileRead.Close();
            }
            catch
            {
                return "NONE";
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public static void WriteBuild(string file)
        {
            string buildFile = curDir + @"/" + file;
            string localDist = DistNum();

            try
            {
                File.WriteAllText(buildFile, localDist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region ZipGC

        /// <summary>
        /// Cleans up any ZIP files.
        /// </summary>
        public static void ZipGC()
        {
            var files = FileWildCard("zip");

            foreach (FileInfo file in files)
            {
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Detects for any present zips and unpacks them.
        /// </summary>
        public static void wildUnZip()
        {
            var dir = new DirectoryInfo(Environment.CurrentDirectory);
            var files = dir.GetFiles("*.zip").Where(p => p.Extension == ".zip").ToArray();

            foreach (FileInfo file in files)
            {
                using (ZipFile zip2 = ZipFile.Read(file.FullName))
                {
                    foreach (ZipEntry ex in zip2)
                    {
                        ex.Extract(Environment.CurrentDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
        }

        public static void distUnZip(string dist)
        {
            using (ZipFile zip2 = ZipFile.Read(dist))
            {
                foreach (ZipEntry ex in zip2)
                {
                    ex.Extract(Environment.CurrentDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ConvertStringArrayToString(string[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            var builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ConvertStringArrayToStringJoin(string[] array)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string result = string.Join(".", array);
            return result;
        }

    }
}
