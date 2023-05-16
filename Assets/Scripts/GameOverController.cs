using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] public GameObject gameController;
    [SerializeField] public GameObject gameOverTitle;
    [SerializeField] public GameObject playAgainButton;

    public void hide()
    {
        gameObject.SetActive(false);
        gameOverTitle.SetActive(true);
        playAgainButton.SetActive(true);
    }

    public void show()
    {
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.gameOver);
        gameObject.SetActive(true);
        Debug.Log("show game over");
    }


    public void onPlayAgain()
    {
        hide(); ;
        gameController.GetComponent<GameController>().startGame();

    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
