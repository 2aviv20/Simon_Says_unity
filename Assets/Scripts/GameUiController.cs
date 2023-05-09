using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] public GameObject gameController;
    [SerializeField] public  GameObject gameOver;
    [SerializeField] public TextMeshProUGUI timer;
    [SerializeField] public TextMeshProUGUI scoreTextMesh;

    float currentTime = 0f;
    private void Awake()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(false);

    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void hideGameOver() {
        gameOver.SetActive(false);
    }

    public void showGameOver() {
        gameOver.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void restTimer(float time) { 
        currentTime = time;
    }

    public void updateScore(int score)
    {
        scoreTextMesh.text = score.ToString(); 
    }
    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        timer.text = currentTime.ToString("0");
        if (currentTime <= 0) { 
            currentTime= 0;
            gameController.GetComponent<GameController>().gameOver();
        }

    }
}
