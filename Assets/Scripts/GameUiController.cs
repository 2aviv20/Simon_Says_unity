using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] public GameObject gameController;
    [SerializeField] public  GameObject gameOver;
    [SerializeField] public GameObject playAgain;
    [SerializeField] public TextMeshProUGUI timer;
    [SerializeField] public TextMeshProUGUI scoreTextMesh;

    float currentTime = 0f;
    public bool isGameRunning = false;
    private void Awake()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(false);
        playAgain.SetActive(false);

    }

    public void show()
    {
        isGameRunning = true;
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void hideGameOver() {
        gameOver.SetActive(false);
        playAgain.SetActive(false);

    }

    public void showGameOver() {
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.gameOver);
        gameOver.SetActive(true);
        playAgain.SetActive(true);
    }

    public void onPlayAgain() {
        hideGameOver();
        gameController.GetComponent<GameController>().startGame();

    }

    public void restTimer(float time) { 
        currentTime = time;
    }

    public void updateScore(int score)
    {
        scoreTextMesh.text = score.ToString(); 
    }
    void Update()
    {
        //update the game CountDown clock
        if (isGameRunning) {
            currentTime -= Time.deltaTime;
            //display only seconds
            timer.text = currentTime.ToString("0");
            //if current time = 0 the game is over 
            if (currentTime <= 0)
            {
                currentTime = 0;
                gameController.GetComponent<GameController>().gameOver();
                isGameRunning = false;
            }
        }
        

    }
}
