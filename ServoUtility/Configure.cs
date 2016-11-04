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
            GlobalSettings.Default.Client = client;
            GlobalSettings.Default.IDE = ide;
            GlobalSettings.Default.Args = args;
        }

        /// <summary>
        /// Resets Client back to FreeSO, IDE back to Volcanic, and Args to 800x600 w.
        /// </summary>
        public static void resetAll()
        {
            GlobalSettings.Default.Client = "FreeSO.exe";
            GlobalSettings.Default.IDE = "Volcanic.exe";
            GlobalSettings.Default.Args = "800x600 w";
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
            GlobalSettings.Default.Client = client;
            GlobalSettings.Default.IDE = ide;
        }


        public static void changeClient(string client)
        {
            GlobalSettings.Default.Client = client;
        }

        public static void changeIDE(string ide)
        {
            GlobalSettings.Default.IDE = ide;
        }

        public static void changeArgs(string args)
        {
            GlobalSettings.Default.Args = args;
        }

        /// <summary>
        /// Is this client a GUI? By defualt this is true.
        /// If the answer is false, StartFSO and StartIDE will provide
        /// error messages from the terminal.
        /// </summary>
        /// <param name="isgui"></param>
        public static void isGUI(bool isgui)
        {
            GlobalSettings.Default.isGUI = isgui;
        }
    }
}
