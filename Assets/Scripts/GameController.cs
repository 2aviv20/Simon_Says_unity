using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using Unity.VisualScripting;
using static ReadConfig;
using TMPro;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.Search;

public class GameController : MonoBehaviour
{

    public Config[] configArray;
    public Config selectedConfig;
    public int score = 0;
    private string playerName;
    private bool isGameRunning = false;
    private Dictionary<ButtonColorsEnum,GameObject> simonButtonsDict = new Dictionary<ButtonColorsEnum, GameObject>();

    [SerializeField] public GameObject nameMenu;
    [SerializeField] public GameObject gameUi;
    [SerializeField] public GameObject gameOverUi;
    [SerializeField] public GameObject levelSelection;
    [SerializeField] public GameObject leadBoard;
    [SerializeField] public GameObject configController;
    [SerializeField] public GameObject drawButtonsController;




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

    // Start is called before the first frame update
    void Start()
    {

        //1.press on StartGame 

        //2.insert player name 
        //nameMenu.GetComponent<NameMenuController>().show();

        //3.select game level
        //read config file 
        this.configArray = configController.GetComponent<ReadConfig>().loadGameConfig();
        levelSelection.GetComponent<LevelSelectionController>().show();
        //show lead board 
        //leadBoard.GetComponent<LeadBoardController>().show();


        //open settings menu
        //

        //show game ui 
        //gameUi.GetComponent<GameUiController>().show();



    }

    public void startGame() {
        updateScore(0);
        gameUi.GetComponent<GameUiController>().restTimer(selectedConfig.GameDuration);
        isGameRunning = true;
        gameUi.GetComponent<GameUiController>().show();
        gameOverUi.GetComponent<GameOverController>().hide();

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
            GameObject btn = this.simonButtonsDict[sequence[sequence.Count - 1]];
            btn.GetComponent<ButtonController>().highlightState();
        }
        else {
            //repeat mode is on
            foreach (ButtonColorsEnum seq in sequence)
            {
                yield return new WaitForSeconds(speed);

                GameObject btn = this.simonButtonsDict[seq];
                btn.GetComponent<ButtonController>().highlightState();
            }
        }
    
    }


    void nextSequence() {
        int randomNumber = Random.Range(1, selectedConfig.Buttons + 1);
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
        gameOverUi.GetComponent<GameOverController>().show();
        leadBoard.GetComponent<LeadBoardController>().AddScoreEntry(score, playerName);
        isGameRunning = false;
        gameUi.GetComponent<GameUiController>().isGameRunning = false;


    }

    public void onLevelSelected(string levelName) {
        foreach (Config c in configArray) {
            if (c.Level == levelName) {
                selectedConfig = c;
            }
        }
        levelSelection.GetComponent<LevelSelectionController>().hide();
        drawButtonsController.GetComponent<DrawButtons>().drawButtons(selectedConfig);
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
        startGame();
    }

    public void addButtonToList(ButtonColorsEnum buttonColor, GameObject button) { 
        this.simonButtonsDict.Add(buttonColor, button);
    }
}

