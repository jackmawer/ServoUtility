// This software is under the public domain. See the UNLICENSE file for more details.

namespace ServoUtility
{
    /// <summary>
    /// Runtime configuration to access different clients, IDEs, or simply
    /// change varies properties of the game
    /// </summary>
    public class Configure
    {
        
        /// <summary>
        /// Changes client and ide locations as well their settings.
        /// Recommended for migration purposes only.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="ide"></param>
        /// <param name="args"></param>
        public static void changeAll(string client, string ide, string args)
        {
            Settings.Default.Client = client;
            Settings.Default.IDE = ide;
            Settings.Default.Args = args;
        }

        /// <summary>
        /// Resets Client back to FreeSO, IDE back to Volcanic, and Args to 800x600 w.
        /// </summary>
        public static void resetAll()
        {
            Settings.Default.Client = "FreeSO.exe";
            Settings.Default.IDE = "Volcanic.exe";
            Settings.Default.Args = "800x600 w";
        }

        /// <summary>
        /// Changes client and ide locations.
        /// Recommended for migration purposes only.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="ide"></param>
        /// <param name="args"></param>
        public static void changeClientIDE(string client, string ide)
        {
            Settings.Default.Client = client;
            Settings.Default.IDE = ide;
        }


        public static void changeClient(string client)
        {
            Settings.Default.Client = client;
        }

        public static void changeIDE(string ide)
        {
            Settings.Default.IDE = ide;
        }

        public static void changeArgs(string args)
        {
            Settings.Default.Args = args;
        }

        /// <summary>
        /// Is this client a GUI? By defualt this is true.
        /// If the answer is false, StartFSO and StartIDE will provide
        /// error messages from the terminal.
        /// </summary>
        /// <param name="isgui"></param>
        public static void isGUI(bool isgui)
        {
            Settings.Default.isGUI = isgui;
        }
    }
}
