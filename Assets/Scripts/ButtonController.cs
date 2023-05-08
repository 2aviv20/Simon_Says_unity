using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{

    [SerializeField] public Material normal, highlight;
    [SerializeField] public ButtonColorsEnum colorName = ButtonColorsEnum.blue;

    bool ishighlight;

    private void Start()
    {
        this.GetComponent<Renderer>().material = normal;
        ishighlight = false;

    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.  " + transform.tag);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject. " + transform.tag);
    }

    private void OnMouseDown()
    {
        if (!ishighlight)
        {
            highlightState();
        }
    }

    public void highlightState() {
        this.GetComponent<Renderer>().material = highlight;
        FindObjectOfType<AudioManager>().PlaySound(colorName);
        ishighlight = true;
        Invoke("normalState", 0.3f);
    }

    private void normalState() {
        this.GetComponent<Renderer>().material = normal;
        ishighlight = false;
    }
}
