using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadConfig : MonoBehaviour
{
    public TextAsset textJson;

    [System.Serializable]
    public class ConfigObject{
        string Level;
        int Buttons;
        int PointForStep;
        int GameDuration;
        bool RepeatMode;
        int GameSpeed;
    }

    [System.Serializable]
    public class ConfigList {
        public ConfigObject[] Config;
    }

    public ConfigList myConfigList = new ConfigList();
    // Start is called before the first frame update
    void Start()
    {
        myConfigList = JsonUtility.FromJson<ConfigList>(textJson.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
