using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEngine.UI.Image imageSource;
    [SerializeField] private Sprite normalMode, pressedMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        imageSource.sprite = pressedMode;
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.menuButtonCick);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageSource.sprite = normalMode;
    }

    public void ButtonClicked() {
        Debug.Log("Menu Button Was Clicked");

        FindObjectOfType<NameMenuController>().hide();
    }
}
