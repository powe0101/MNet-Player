﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Security;

namespace nmpApplication
{
    internal class BrowserEmulator
    {
        public enum BrowserEmulationVersion
        {

            Default = 0,

            Version7 = 7000,

            Version8 = 8000,

            Version8Standards = 8888,

            Version9 = 9000,

            Version9Standards = 9999,

            Version10 = 10000,

            Version10Standards = 10001,

            Version11 = 11000,

            Version11Edge = 11001

        }

        public BrowserEmulator(BrowserEmulationVersion version11)
        {
            SetBrowserEmulationVersion(BrowserEmulationVersion.Version11);
        }

        private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";
        private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

        protected static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
        {
            bool result;



            result = false;



            try
            {

                RegistryKey key;



                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);



                if (key != null)
                {

                    string programName;



                    programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);



                    if (browserEmulationVersion != BrowserEmulationVersion.Default)
                    {

                        // if it's a valid value, update or create the value

                        key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);

                    }

                    else
                    {

                        // otherwise, remove the existing value

                        key.DeleteValue(programName, false);

                    }



                    result = true;

                }

            }

            catch (SecurityException)
            {

                // The user does not have the permissions required to read from the registry key.

            }

            catch (UnauthorizedAccessException)
            {

                // The user does not have the necessary registry rights.

            }



            return result;

        }
        protected static bool SetBrowserEmulationVersion()
        {

            int ieVersion;

            BrowserEmulationVersion emulationCode;



            ieVersion = GetInternetExplorerMajorVersion();



            if (ieVersion >= 11)
            {

                emulationCode = BrowserEmulationVersion.Version11;

            }

            else
            {

                switch (ieVersion)
                {

                    case 10:

                        emulationCode = BrowserEmulationVersion.Version10;

                        break;

                    case 9:

                        emulationCode = BrowserEmulationVersion.Version9;

                        break;

                    case 8:

                        emulationCode = BrowserEmulationVersion.Version8;

                        break;

                    default:

                        emulationCode = BrowserEmulationVersion.Version7;

                        break;

                }

            }

            return SetBrowserEmulationVersion(emulationCode);

        }
        
        protected static int GetInternetExplorerMajorVersion()
        {

            int result;
            result = 0;
            try
            {

                RegistryKey key;



                key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);



                if (key != null)
                {

                    object value;



                    value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);



                    if (value != null)
                    {

                        string version;

                        int separator;



                        version = value.ToString();

                        separator = version.IndexOf('.');

                        if (separator != -1)
                        {

                            int.TryParse(version.Substring(0, separator), out result);

                        }

                    }

                }

            }

            catch (SecurityException)
            {

                // The user does not have the permissions required to read from the registry key.

            }

            catch (UnauthorizedAccessException)
            {

                // The user does not have the necessary registry rights.

            }



            return result;

        }
        protected static BrowserEmulationVersion GetBrowserEmulationVersion()
        {

            BrowserEmulationVersion result;



            result = BrowserEmulationVersion.Default;



            try
            {

                RegistryKey key;



                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                if (key != null)
                {

                    string programName;

                    object value;



                    programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

                    value = key.GetValue(programName, null);



                    if (value != null)
                    {

                        result = (BrowserEmulationVersion)Convert.ToInt32(value);

                    }

                }

            }

            catch (SecurityException)
            {

                // The user does not have the permissions required to read from the registry key.

            }

            catch (UnauthorizedAccessException)
            {

                // The user does not have the necessary registry rights.

            }



            return result;

        }
        protected static bool IsBrowserEmulationSet()
        {

            return GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;

        }

    }
}