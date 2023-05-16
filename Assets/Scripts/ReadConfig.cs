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
    public class ConfigList
    {
        public Config[] config;
    }

    [System.Serializable]
    [XmlRoot("ConfigXml")]
    public class ConfigXml
    {
        [XmlArray("MyArray")]
        [XmlArrayItem("Item")]
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
        string path = Application.dataPath + xmlFilePath;

        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigXml));
            StreamReader reader = new StreamReader(path);
            ConfigXml data = (ConfigXml)serializer.Deserialize(reader);
            reader.Close();
            return data.config;
        }
        else
        {
            Debug.Log("File does not exist.");
        }
        return null;
    }
}
