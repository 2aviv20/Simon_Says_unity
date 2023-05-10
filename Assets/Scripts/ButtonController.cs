using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{

    public Material normal, highlight;
    public ButtonColorsEnum colorName;

    bool ishighlight;

    private void Start()
    {
        this.GetComponent<Renderer>().material = normal;
        ishighlight = false;
    }

    public void setButtonColor(ButtonColorsEnum colorName, Material normal, Material highlight) {
                this.normal = normal;
                this.highlight = highlight;
                this.colorName= colorName;
                this.GetComponent<Renderer>().material = normal;
    }
    void OnMouseOver()
    {

    }

    void OnMouseExit()
    {

    }

    private void OnMouseDown()
    {
        if (!ishighlight)
        {
            highlightState();
            FindObjectOfType<GameController>().onButtonPressed(colorName);
        }
    }

    public void highlightState() {
        this.GetComponent<Renderer>().material = highlight;
        FindObjectOfType<AudioManager>().PlaySound(colorNameToSound(colorName));
        ishighlight = true;
        Invoke("normalState", 0.3f);
    }

    public SoundManagerEnum colorNameToSound(ButtonColorsEnum colorName) {
        switch(colorName){
            case ButtonColorsEnum.red:
                return SoundManagerEnum.red;
            case ButtonColorsEnum.green:
                return SoundManagerEnum.green;
            case ButtonColorsEnum.blue: 
                return SoundManagerEnum.blue;
            case ButtonColorsEnum.orange:
                return SoundManagerEnum.orange;
            case ButtonColorsEnum.pink:
                return SoundManagerEnum.pink;
            case ButtonColorsEnum.yellow:
                return SoundManagerEnum.yellow;
        }
        return SoundManagerEnum.none;
    }

    private void normalState() {
        this.GetComponent<Renderer>().material = normal;
        ishighlight = false;
    }
}
