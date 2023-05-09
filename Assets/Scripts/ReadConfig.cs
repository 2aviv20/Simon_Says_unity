using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;


public class ReadConfig : MonoBehaviour
{
    public string jsonFilePath;
    public string xmlFilePath;

    [System.Serializable]
    public class Config
    {
        public string Level;
        public int Buttons;
        public int PointForStep;
        public int GameDuration;
        public bool RepeatMode;
        public int GameSpeed;
    }

    [System.Serializable]
    public class ConfigList
    {
        public Config[] config;
    }

    public Config[] loadGameConfig() {
        if (jsonFilePath != "" && File.Exists(Application.dataPath + jsonFilePath))
        {
            return readJsonConfig();
             
        }
        else {
            if (xmlFilePath != "" && File.Exists(Application.dataPath + xmlFilePath))
            {
                return readXmlConfig();
            }
        }
        Debug.LogError("Config files are missing");
        return null;
        
    }

    public Config[] readJsonConfig() {
        string json = File.ReadAllText(Application.dataPath + jsonFilePath);
        ConfigList gameConfig = JsonUtility.FromJson<ConfigList>(json);
        return gameConfig.config;
    }

    public Config[] readXmlConfig() {
        //string xmlString = File.ReadAllText(Application.dataPath + xmlFilePath);
        XmlSerializer serializer = new XmlSerializer(typeof(ConfigList));
        StreamReader reader = new StreamReader(Application.dataPath + xmlFilePath);
        ConfigList gameConfig = (ConfigList)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return gameConfig.config;
    }
}
