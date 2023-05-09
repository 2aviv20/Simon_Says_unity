using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using static ReadConfig;

public class LevelSelectionController : MonoBehaviour
{
    private List<Transform> transformList;
    public Config[] gameConfig;
    [SerializeField] public GameObject gameController;
    [SerializeField] public Transform entryContainer;
    [SerializeField] public Transform entryTemplate;
    private void Awake()
    {
        gameObject.SetActive(false);
        transformList = new List<Transform>();

    }

    public void show() {
        gameObject.SetActive(true);
        float templateHeight = 20f;
        gameConfig = gameController.GetComponent<GameController>().configArray;
        for (int i = 0; i < gameConfig.Length; i++) {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * gameConfig.Length * i);
            entryTransform.gameObject.SetActive(true);
            entryTransform.Find("LevelName").GetComponent<TextMeshProUGUI>().text = gameConfig[i].Level;
            transformList.Add(entryTransform);
        }
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
