﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace TrueWoW_Launcher
{
    class ReadSettingsFile
    {
        
        private XmlNode _serverXML;
        private XmlNode _wowPathXML;
        private XmlNode _startUpSoundXML;
        private XmlNode _minimizeToTrayXML;
        private XmlDocument _xmlReader = new XmlDocument();
        private string _serverAddress = "";
        private string _wowPath = "";
        private bool _startUpSound = false;
        private bool _minimizeToTray = false;

        public void Read(string _settingsFileLocation = "localsettings.xml")
        {
            try
            {
                _xmlReader.Load(@Directory.GetCurrentDirectory() + "\\" + _settingsFileLocation);
            }
            //file not found
            catch (FileNotFoundException)
            {
                DialogResult answer = MessageBox.Show("ERROR: localsettings.xml not found in: \n\n'" + Directory.GetCurrentDirectory() + "\\'. \n\n Re-download TrueWoW Launcher from http://www.truewow.org/ ?", "Error!", MessageBoxButtons.YesNo);
                if (answer == DialogResult.No)
                {
                    Environment.Exit(0);
                }
                else
                {
                    System.Diagnostics.Process.Start("http://www.truewow.org");
                    Environment.Exit(0);
                }

            }
            //dir not found
            catch (DirectoryNotFoundException)
            {
                DialogResult answer = MessageBox.Show("ERROR: localsettings.xml not found in: \n\n'" + Directory.GetCurrentDirectory() + "\\'. \n\n Re-download TrueWoW Launcher from http://www.truewow.org/ ?", "Error!", MessageBoxButtons.YesNo);
                if (answer == DialogResult.No)
                {
                    Environment.Exit(0);
                }
                else
                {
                    System.Diagnostics.Process.Start("http://www.truewow.org");
                    Environment.Exit(0);
                }
            }
            //no access
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("ERROR: no read/write access to localsettings.xml in: \n\n '" + Directory.GetCurrentDirectory() + "\\' \n\n Re-run Launcher with Administrator privileges please.", "Error!");
                Environment.Exit(0);
            }

            _serverXML = _xmlReader.SelectSingleNode("/settings/server");
            _serverAddress = _serverXML.InnerText;
            //for testing - using the free web host @ truewowlauncher.netii.net
            //launcher path & resources - truewowlauncher.netii.net/launcher/
            _serverAddress = "truewowlauncher.netii.net";
            _wowPathXML = _xmlReader.SelectSingleNode("/settings/wowPath");
            _wowPath = _wowPathXML.InnerText;
            _startUpSoundXML = _xmlReader.SelectSingleNode("/settings/startUpSound");
            if (_startUpSoundXML.InnerText == "true")
            {
                _startUpSound = true;
            }
            else
            {
                _startUpSound = false;
            }
            _minimizeToTrayXML = _xmlReader.SelectSingleNode("/settings/minimizeToTray");
            if (_minimizeToTrayXML.InnerText == "true")
            {
                _minimizeToTray = true;
            }
            else
            {
                _minimizeToTray = false;
            }
        }

        public string serverAddress() { return _serverAddress; }
        public string wowPath() { return _wowPath; }
        public bool startUpSound() { return _startUpSound; }
        public bool minimizeToTray() { return _minimizeToTray; }
    }
}
