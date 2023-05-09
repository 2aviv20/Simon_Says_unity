using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectionButtonController : MonoBehaviour
{

    [SerializeField] GameObject gameController;
    public void onClick()
    {
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.menuButtonCick);
        string levelName = gameObject.GetComponent<TextMeshProUGUI>().text;
        gameController.GetComponent<GameController>().onLevelSelected(levelName);
    }
}
