using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{

    public Config[] configArray;
    public Config selectedConfig;
    public int score = 0;
    public string playerName;
    private bool isGameRunning = false;
    private Dictionary<ButtonColorsEnum, GameObject> simonButtonsDict = new Dictionary<ButtonColorsEnum, GameObject>();

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

    public void startGame()
    {
        //reset the sequence lists 
        playerInputSequence.Clear();
        gameSequence.Clear();
        //reset score 
        updateScore(0);
        //reset timer 
        gameUi.GetComponent<GameUiController>().restTimer(selectedConfig.GameDuration);
        isGameRunning = true;
        gameUi.GetComponent<GameUiController>().show();
        //hide game over title
        gameOverUi.GetComponent<GameOverController>().hide();

        gameLoop();
    }
    void gameLoop()
    {
        nextSequence();
        StartCoroutine(playSequence(gameSequence, 0.5f / selectedConfig.GameSpeed));
    }
    IEnumerator playSequence(List<ButtonColorsEnum> sequence, float speed = 0.2f)
    {
        yield return new WaitForSeconds(1f);
        // repeat mode is off
        if (!selectedConfig.RepeatMode)
        {
            GameObject btn = this.simonButtonsDict[sequence[sequence.Count - 1]];
            btn.GetComponent<ButtonController>().highlightState();
        }
        else
        {
            //repeat mode is on
            foreach (ButtonColorsEnum seq in sequence)
            {
                yield return new WaitForSeconds(speed);

                GameObject btn = this.simonButtonsDict[seq];
                btn.GetComponent<ButtonController>().highlightState();
            }
        }

    }


    void nextSequence()
    {
        int randomNumber = Random.Range(1, selectedConfig.Buttons + 1);
        gameSequence.Add((ButtonColorsEnum)randomNumber);
        playerInputSequence.Clear();
    }

    //Invoked from ButtonController.cs OnMouseDown()
    public void onButtonPressed(ButtonColorsEnum color)
    {
        if (!isGameRunning) { return; }
        playerInputSequence.Add(color);
        
        //check if last player input equal the game Sequence at the same position 
        if (playerInputSequence[playerInputSequence.Count - 1] != gameSequence[playerInputSequence.Count - 1]) {
            gameOver();
        }
        //if its the last user input in sequence , move to next sequence 
        if (playerInputSequence.Count == gameSequence.Count)
        {
            updateScore(selectedConfig.PointForStep);
            gameLoop();
        }
    }

    public void gameOver()
    {
        gameOverUi.GetComponent<GameOverController>().show();
        leadBoard.GetComponent<LeadBoardController>().AddScoreEntry(score, playerName);
        isGameRunning = false;
        gameUi.GetComponent<GameUiController>().isGameRunning = false;
        gameUi.GetComponent<GameUiController>().restTimer(0);
    }

    public void onLevelSelected(string levelName)
    {
        foreach (Config c in configArray)
        {
            if (c.Level == levelName)
            {
                selectedConfig = c;
            }
        }
        levelSelection.GetComponent<LevelSelectionController>().hide();
        drawButtonsController.GetComponent<DrawButtons>().drawButtons(selectedConfig);
        nameMenu.GetComponent<NameMenuController>().show();

    }

    public void updateScore(int amount)
    {
        if (amount == 0)
        {
            score = 0;
        }
        else
        {
            score += amount;
        }
        gameUi.GetComponent<GameUiController>().updateScore(score);
    }

    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
        startGame();
    }

    public void addButtonToList(ButtonColorsEnum buttonColor, GameObject button)
    {
        this.simonButtonsDict.Add(buttonColor, button);
    }
}

