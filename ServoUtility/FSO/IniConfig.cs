﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.IO;

namespace ServoUtility.FSO
{
    public abstract class IniConfig
    {
        private string ActivePath;

        public abstract Dictionary<string, string> DefaultValues
        {
            get; set;
        }

        private void SetValue(string key, string value)
        {
            var prop = this.GetType().GetProperty(key);
            if (prop != null)
            {
                try
                {
                    if (prop.PropertyType != typeof(string))
                        prop.SetValue(this, Convert.ChangeType(value, prop.PropertyType));
                    else prop.SetValue(this, value);
                }
                catch (Exception) { }
            }
        }

        public IniConfig(string path)
        {
            ActivePath = path;
            Load();
        }

        public void Load()
        {
            //assume default values for all unset properties
            foreach (var pair in DefaultValues)
            {
                SetValue(pair.Key, pair.Value);
            }

            if (!File.Exists(ActivePath))
            {
                Save();
            }
            else
            {
                var lines = File.ReadAllLines(ActivePath);
                foreach (var line in lines)
                {
                    var clean = line.Trim();
                    if (clean[0] == '#' || clean[0] == '[') continue;
                    var split = clean.IndexOf('=');
                    if (split == -1) continue; //?
                    var prop = clean.Substring(0, split).Trim();
                    var value = clean.Substring(split + 1).Trim();

                    SetValue(prop, value);
                }
            }
        }

        public void Save()
        {
            try
            {
                using (var stream = new StreamWriter(File.Open(ActivePath, FileMode.Create, FileAccess.Write)))
                {
                    stream.WriteLine("# Servo Utility Settings File. Properties are self explanatory.");
                    var props = this.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        if (prop.Name == "Default" || prop.Name == "DefaultValues") continue;
                        stream.WriteLine(prop.Name + "=" + prop.GetValue(this).ToString());
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
