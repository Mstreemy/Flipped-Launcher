using IniParser;
using IniParser.Model;
using System;
using System.IO;

namespace Flipped.Utilities
{
    // Code Might Be Bad Never Really Touched INI Files
    public static class SavedData
    {
        public static void WriteToConfig(string sectionName, string pathKey, string newValue)
        {
            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dataFolder = Path.Combine(baseFolder, "FlippedConfiguration");
            Directory.CreateDirectory(dataFolder);
            string filePath = Path.Combine(dataFolder, "Settings.ini");

            FileIniDataParser parser = new FileIniDataParser();

            IniData iniData;
            if (File.Exists(filePath))
            {
                iniData = parser.ReadFile(filePath);
            }
            else
            {
                iniData = new IniData();
            }

            iniData[sectionName][pathKey] = newValue;
            parser.WriteFile(filePath, iniData, null);
        }

        public static string ReadValue(string sectionName, string pathKey)
        {
            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dataFolder = Path.Combine(baseFolder, "FlippedConfiguration");
            string filePath = Path.Combine(dataFolder, "Settings.ini");

            FileIniDataParser parser = new FileIniDataParser();

            if (File.Exists(filePath))
            {
                IniData iniData = parser.ReadFile(filePath);
                return iniData[sectionName][pathKey];
            }
            else
            {
                return null;
            }
        }

        public static void RemoveKey(string sectionName, string pathKey)
        {
            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dataFolder = Path.Combine(baseFolder, "FlippedConfiguration");
            string filePath = Path.Combine(dataFolder, "Settings.ini");

            FileIniDataParser parser = new FileIniDataParser();

            if (File.Exists(filePath))
            {
                IniData iniData = parser.ReadFile(filePath);
                if (iniData.Sections.ContainsSection(sectionName) && iniData[sectionName].ContainsKey(pathKey))
                {
                    iniData[sectionName].RemoveKey(pathKey);
                    parser.WriteFile(filePath, iniData, null);
                }
            }
        }
    }
}