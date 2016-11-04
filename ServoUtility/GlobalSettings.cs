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
using System.Collections.Generic;
using System.IO;
using ServoUtility.FSO;

namespace ServoUtility
{
    public class GlobalSettings : IniConfig
    {
        private static GlobalSettings defaultInstance;

        public static GlobalSettings Default
        {
            get
            {
#if DEBUG
                if (defaultInstance == null)
                    defaultInstance = new GlobalSettings(Path.Combine(Environment.CurrentDirectory, "ServoUtility.ini"));
                return defaultInstance;
#else
                if (defaultInstance == null)
                    defaultInstance = new GlobalSettings(Path.Combine(FSOEnvironment.UserDir, "ServoUtility.ini"));
                return defaultInstance;
#endif
            }
        }

        public GlobalSettings(string path) : base(path) { }

        private Dictionary<string, string> _DefaultValues = new Dictionary<string, string>()
        {
            {"Client", "FreeSO.exe" },
            {"IDE", "Volcanic.exe" },
            {"Args", "800x600 w" },
            {"isGUI", "True" },
            {"TeamCity", "servo.freeso.org" },
            {"BuildType", "FreeSO_TsoClient" },
            {"Updater", "BlueRoseLauncher.exe" },
        };
        public override Dictionary<string, string> DefaultValues
        {
            get { return _DefaultValues; }
            set { _DefaultValues = value; }
        }

        public string Client { get; set; }
        public string IDE { get; set; }
        public string Args { get; set; }
        public bool isGUI { get; set; }
        public string TeamCity { get; set; }
        public string BuildType { get; set; }
        public string Updater { get; set; }

    }
}
