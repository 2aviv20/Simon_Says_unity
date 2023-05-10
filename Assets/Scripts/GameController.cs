using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using Unity.VisualScripting;
using static ReadConfig;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviour
{

    public Config[] configArray;
    public Config selectedConfig;
    public int score = 0;
    private string playerName;
    private bool isGameRunning = false;

    [SerializeField] public GameObject[] simonButtons;
    [SerializeField] public GameObject nameMenu;
    [SerializeField] public GameObject gameUi;
    [SerializeField] public GameObject levelSelection;
    [SerializeField] public GameObject leadBoard;
    [SerializeField] public GameObject configController;


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
        //read config file 
        this.configArray = configController.GetComponent<ReadConfig>().loadGameConfig();

        //insert player name 
        //nameMenu.GetComponent<NameMenuController>().show();
        leadBoard.GetComponent<LeadBoardController>().show();

        // select game level 
        //levelSelection.GetComponent<LevelSelectionController>().show();

      



        //start to play 
        foreach (GameObject btn in simonButtons)
        {
            buttonsDict.Add(btn.GetComponent<ButtonController>().colorName, btn);
        }
    }

    public void startGame() {
        updateScore(0);
        gameUi.GetComponent<GameUiController>().restTimer(selectedConfig.GameDuration);
        isGameRunning = true;
        gameUi.GetComponent<GameUiController>().show();
        gameLoop();
    }
    void gameLoop() {
        nextSequence();
        //StartCoroutine(playSequence(startDemoSequence));
        StartCoroutine(playSequence(gameSequence, 0.5f/selectedConfig.GameSpeed));
    }
    IEnumerator playSequence(List<ButtonColorsEnum> sequence, float speed =0.2f)
    {
        yield return new WaitForSeconds(1f);
        // repeat mode is off
        if (!selectedConfig.RepeatMode)
        {
            GameObject btn = buttonsDict[sequence[sequence.Count - 1]];
            btn.GetComponent<ButtonController>().highlightState();
        }
        else {
            //repeat mode is on
            foreach (ButtonColorsEnum seq in sequence)
            {
                yield return new WaitForSeconds(speed);

                GameObject btn = buttonsDict[seq];
                btn.GetComponent<ButtonController>().highlightState();
            }
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
                updateScore(selectedConfig.PointForStep);
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

    public void gameOver() {
        gameUi.GetComponent<GameUiController>().showGameOver();
        updateScore(0);
        isGameRunning = false;
        gameUi.GetComponent<GameUiController>().isGameRunning = false;

    }

    public void onLevelSelected(string levelName) {
        foreach (Config c in configArray) {
            if (c.Level == levelName) {
                selectedConfig = c;
            }
        }
        Debug.Log(selectedConfig.Level + selectedConfig.PointForStep.ToString() + selectedConfig.RepeatMode.ToString());
        levelSelection.GetComponent<LevelSelectionController>().hide();
        nameMenu.GetComponent<NameMenuController>().show();

    }

    public void updateScore(int amount) {
        if (amount == 0)
        {
            score = 0;
        }
        else {
            score += amount;
        }
        gameUi.GetComponent<GameUiController>().updateScore(score);
    }

    public void setPlayerName(string playerName) {
        this.playerName = playerName;
        Debug.Log(this.playerName);
        startGame();
    }
    // Update is called once per frame
    void Update()
    {


    }
}

