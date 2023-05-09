using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameMenuController : MonoBehaviour
{
    public TMP_InputField playerName;
    [SerializeField] GameObject gameController;
    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
        gameController.GetComponent<GameController>().setPlayerName(playerName.text);
    }
}
