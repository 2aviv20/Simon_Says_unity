using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] public GameObject gameController;
    [SerializeField] public GameObject configDropDown;
    [SerializeField] public GameObject gameConfig;

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void onConfigSourceChanged() {
        TMP_Dropdown dropdown = configDropDown.GetComponent<TMP_Dropdown>();
        

        if (dropdown != null)
        {
            // Access the value of the Dropdown component
            int selectedIndex = dropdown.value;
            string selectedValue = dropdown.options[selectedIndex].text;
            switch (selectedValue) {
                case "json":
                    gameController.GetComponent<GameController>().configArray = gameConfig.GetComponent<ReadConfig>().readJsonConfig();
                    break;
                case "xml":
                    gameController.GetComponent<GameController>().configArray = gameConfig.GetComponent<ReadConfig>().readXmlConfig();
                    break;
            }
            gameController.GetComponent<GameController>().selectedConfig = gameController.GetComponent<GameController>().configArray[0];
        }
    }
}
