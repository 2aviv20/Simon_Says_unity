using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;


public class GameController : MonoBehaviour
{
    //enum ButtonColorsEnum = Enums.ButtonColorsEnum;
    [SerializeField] public GameObject[] simonButtons;
    private List<ButtonColorsEnum> sequence = new List<ButtonColorsEnum> {
        ButtonColorsEnum.red,
        ButtonColorsEnum.green,
        ButtonColorsEnum.yellow,
        ButtonColorsEnum.blue ,
        ButtonColorsEnum.red,
        ButtonColorsEnum.green,
        ButtonColorsEnum.yellow,
        ButtonColorsEnum.blue
    };
    Dictionary<ButtonColorsEnum, GameObject> buttonsDict = new Dictionary<ButtonColorsEnum, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject btn in simonButtons)
        {
            buttonsDict.Add(btn.GetComponent<ButtonController>().colorName, btn);
        }
        StartCoroutine(runDemo(simonButtons));
    }

    IEnumerator runDemo(GameObject[] simonButtons)
    {
        foreach (ButtonColorsEnum seq in sequence) {
            yield return new WaitForSeconds(0.2f);

            GameObject btn = buttonsDict[seq];
            btn.GetComponent<ButtonController>().highlightState();
        }
    }

    
    // Update is called once per frame
    void Update()
    {


    }
}

