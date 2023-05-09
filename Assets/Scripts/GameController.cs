using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using Unity.VisualScripting;
using static ReadConfig;

public class GameController : MonoBehaviour
{
    public Config[] gameConfig;
    public int gameLevel = 0;
    private string playerName;
    private bool isGameRunning = false;

    [SerializeField] public GameObject[] simonButtons;
    [SerializeField] public GameObject nameMenu;
    [SerializeField] public GameObject gameUi;
    [SerializeField] public GameObject levelSelection;
    [SerializeField] public GameObject leadBoard;

    private List<ButtonColorsEnum> startDemoSequence = new List<ButtonColorsEnum> {
        ButtonColorsEnum.red,
        ButtonColorsEnum.green,
        ButtonColorsEnum.yellow,
        ButtonColorsEnum.blue ,
        ButtonColorsEnum.red,
        ButtonColorsEnum.green,
        ButtonColorsEnum.yellow,
        ButtonColorsEnum.blue
    };
    private List<ButtonColorsEnum> gameSequence = new List<ButtonColorsEnum>();
    private List<ButtonColorsEnum> playerInputSequence = new List<ButtonColorsEnum>();

    Dictionary<ButtonColorsEnum, GameObject> buttonsDict = new Dictionary<ButtonColorsEnum, GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        //insert player name 
        nameMenu.GetComponent<NameMenuController>().show();
        //read config file 
        //this.gameConfig = GetComponent<ReadConfig>().loadGameConfig();
        // select game level 

        //this.gameLevel // from user input 

        //start to play 
        foreach (GameObject btn in simonButtons)
        {
            buttonsDict.Add(btn.GetComponent<ButtonController>().colorName, btn);
        }
    }

    void gameLoop() {
        isGameRunning = true;
        gameUi.GetComponent<GameUiController>().show();
        nextSequence();
        //StartCoroutine(playSequence(startDemoSequence));
        StartCoroutine(playSequence(gameSequence, 0.5f));
    }
    IEnumerator playSequence(List<ButtonColorsEnum> sequence, float speed =0.2f)
    {
        yield return new WaitForSeconds(0.3f);
        foreach (ButtonColorsEnum seq in sequence) {
            yield return new WaitForSeconds(0.2f);

            GameObject btn = buttonsDict[seq];
            btn.GetComponent<ButtonController>().highlightState();
        }
    }


    void nextSequence() {
        int randomNumber = Random.Range(1, 7);
        gameSequence.Add((ButtonColorsEnum)randomNumber);
        playerInputSequence.Clear();
    }

    //Invoked from ButtonController.cs OnMouseDown()
    public void onButtonPressed(ButtonColorsEnum color) {
        if (!isGameRunning) { return; }
        playerInputSequence.Add(color);
        if (playerInputSequence.Count == gameSequence.Count) {
            bool keepPlaying = compareSequence(gameSequence, playerInputSequence);
            if (keepPlaying)
            {
                gameLoop();
            }
            else {
                gameOver();
            }

        }
    }

    bool compareSequence(List<ButtonColorsEnum> gameSequence, List<ButtonColorsEnum> playerInputSequence) {
        for (int i = 0; i < gameSequence.Count; i++) {
            if (gameSequence[i] != playerInputSequence[i]) {
                return false;
            }
        }
        return true;
    }

    void gameOver() {
        gameUi.GetComponent<GameUiController>().showGameOver();
        isGameRunning = false;
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.gameOver);

    }

    public void setPlayerName(string playerName) {
        this.playerName = playerName;
        Debug.Log(this.playerName);
        gameLoop();
    }
    // Update is called once per frame
    void Update()
    {


    }
}

