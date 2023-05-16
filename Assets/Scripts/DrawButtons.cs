using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class DrawButtons : MonoBehaviour
{
    private List<Transform> transformList;
    [SerializeField] GameObject gameController;
    [SerializeField] public GameObject container;
    [SerializeField] public Transform template;
    private Material[] buttonMaterials;
    // Start is called before the first frame update

    private void Awake()
    {
        transformList = new List<Transform>();
        buttonMaterials = Resources.LoadAll<Material>("ButtonMaterials");
        template.gameObject.SetActive(false);
    }

    public void drawButtons(Config selectedConfig)
    {
        int amountOfButtons = selectedConfig.Buttons;
        int yAngleRotation = 360 / amountOfButtons;
        for (int i = 1; i <= amountOfButtons; i++)
        {
            ButtonColorsEnum buttonColor = (ButtonColorsEnum)i;
            Material normal = getMaterial(buttonColor.ToString() + "_normal");
            Material highlight = getMaterial(buttonColor.ToString() + "_highlight");
            Transform entryTransform = Instantiate(template, container.transform);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.RotateAround(container.GetComponent<Renderer>().bounds.center, new Vector3(0,0,1), yAngleRotation * i);
            entryRectTransform.GetComponent<ButtonController>().setButtonColor(buttonColor, normal, highlight);
            entryTransform.gameObject.SetActive(true);
            transformList.Add(entryTransform);
            //add to object list , used in the gameController
            //simonButtons.Add(transform.gameObject);
            gameController.GetComponent<GameController>().addButtonToList(buttonColor,entryTransform.gameObject);
        }
    }
    void deleteButtonsFromSreen()
    {
        for (int i = transformList.Count - 1; i >= 0; i--)
        {
            Destroy(transformList[i].gameObject);
            transformList.RemoveAt(i);
        }
    }

    private Material getMaterial(string name) {
        foreach (Material m in buttonMaterials) { 
            if (m.name == name){
                return m;
            }
        }
        return  null;
    }
    


}
