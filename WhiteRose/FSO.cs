// This software is under the public domain. See the UNLICENSE file for more details.

using System;
using System.Diagnostics;
using System.IO;
using Eto.Forms;
using System.Text.RegularExpressions;
using Ionic.Zip;
using System.Net;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WhiteRose
{
    public static class FSO
    {
        [Obsolete]
        public static string[] fsoParmas { get; set; }

        public static string libVer = "WhiteRose " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Returns all files by their extensions within the current directory.
        /// You only need to type in the name of the extension.
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static FileInfo[] FileWildCard(string fileExt)
        {
            var dir = new DirectoryInfo(Environment.CurrentDirectory);
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
            try {
                return new Uri(url);
            }
            catch {
                return new Uri("https://google.com");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>new Uri(url);</returns>
        public static Uri WebURI(string url, string fallback = "https://google.com")
        {
            try {
                return new Uri(url);
            }
            catch {
                return new Uri(fallback);
            }
        }

        /// <summary>
        /// Launches the FreeSO IDE executable with the specified
        /// game arguments defined in the application settings.
        /// 
        /// Defualt settings: FSO.IDE.exe must be located in same
        /// directory and the game arguments are "800x600 w".
        /// 
        /// To allow changes for these settings, use the Configure class.
        /// </summary>
        public static void StartFSO()
        {
            var fso = Properties.Settings.Default.Client;
            var fsoProcess = new Process();

			if (File.Exists(fso))
			{
                fsoProcess.StartInfo.FileName = fso;
                fsoProcess.StartInfo.UseShellExecute = true;
                fsoProcess.StartInfo.Arguments = Properties.Settings.Default.Args;
                fsoProcess.Start();
            }
			else {
				MessageBox.Show(fso + " not found.");
			}
        }

        /// <summary>
        /// Launches the FreeSO IDE executable with the specified
        /// game arguments defined in the application settings.
        /// 
        /// Defualt settings: FSO.IDE.exe must be located in same
        /// directory and the game arguments are "800x600 w".
        /// 
        /// To allow changes for these settings, use the Configure class.
        /// </summary>
        public static void StartIDE()
        {
            var ide = Properties.Settings.Default.IDE;
            var fsoProcess = new Process();

            if (File.Exists(ide))
            {
                fsoProcess.StartInfo.FileName = ide;
                fsoProcess.StartInfo.UseShellExecute = true;
                fsoProcess.StartInfo.Arguments = Properties.Settings.Default.Args;
                fsoProcess.Start();
            }
            else
            {
                MessageBox.Show(ide + " not found.");
            }
        }

        /// <summary>
        /// Return the latest dist number as a string
        /// Thanks to LRB. http://forum.freeso.org/threads/974/
        /// </summary>
        /// <returns>sLine</returns>
        // TODO: We need a new way to parse HTML files!
        public static string DistNumLegacy()
        {
            string url = "http://servo.freeso.org/externalStatus.html?js=1";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            var objReader = new StreamReader(objStream);
            string sLine = "";
            string fll;
            fll = objReader.ReadLine();
            sLine = fll.Remove(0, 855);
            sLine = sLine.Remove(sLine.IndexOf("</a>", StringComparison.Ordinal));
            return sLine;
        }

        /// <summary>
        /// New disNum method.
        /// </summary>
        /// <returns></returns>
        // TODO: Put new HTML parser logic here. Make public afterwords.
        static string DistNum()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ReadBuild(string file)
        {
            string line;

            try {
                string buildFile = Environment.CurrentDirectory + @"/" + file;
                var fileRead = new StreamReader(buildFile);
                while ((line = fileRead.ReadLine()) != null) {
                    return "#" + line;
                }

                fileRead.Close();
            }
            catch {
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
            string buildFile = Environment.CurrentDirectory + @"/" + file;
            string localDist = DistNumLegacy();

            try {
                File.WriteAllText(buildFile, localDist);
            }
            catch (Exception ex) {
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

            foreach (FileInfo file in files) {
                try {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch (Exception ex) {
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

            foreach (FileInfo file in files) {
                using (ZipFile zip2 = ZipFile.Read(file.FullName)) {
                    foreach (ZipEntry ex in zip2) {
                        ex.Extract(Environment.CurrentDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
        }

        public static void distUnZip(string dist)
        {
            using (ZipFile zip2 = ZipFile.Read(dist)) {
                foreach (ZipEntry ex in zip2) {
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
            foreach (string value in array) {
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
