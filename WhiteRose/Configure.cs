// This software is under the public domain. See the UNLICENSE file for more details.

namespace WhiteRose
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
            Properties.Settings.Default.Client = client;
            Properties.Settings.Default.IDE = ide;
            Properties.Settings.Default.Args = args;
        }

        public static void resetAll()
        {
            Properties.Settings.Default.Client = "FreeSO.exe";
            Properties.Settings.Default.IDE = "FSO.IDE.exe";
            Properties.Settings.Default.Args = "800x600 w";
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
            Properties.Settings.Default.Client = client;
            Properties.Settings.Default.IDE = ide;
        }


        public static void changeClient(string client)
        {
            Properties.Settings.Default.Client = client;
        }

        public static void changeIDE(string ide)
        {
            Properties.Settings.Default.IDE = ide;
        }

        public static void changeArgs(string args)
        {
            Properties.Settings.Default.Args = args;
        }

        /// <summary>
        /// Is this client a GUI? By defualt this is true.
        /// If the answer is false, StartFSO and StartIDE will provide
        /// error messages from the terminal.
        /// </summary>
        /// <param name="isgui"></param>
        public static void isGUI(bool isgui)
        {
            Properties.Settings.Default.isGUI = isgui;
        }
    }
}
