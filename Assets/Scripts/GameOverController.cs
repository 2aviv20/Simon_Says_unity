using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverController : MonoBehaviour
{

    [SerializeField] public GameObject gameController;
    [SerializeField] public GameObject gameOverTitle;
    [SerializeField] public GameObject timeIsUpTitle;


    public void hide()
    {
        gameObject.SetActive(false);
        gameOverTitle.SetActive(false);

    }

    public void hideTimeIsUp(){
        gameObject.SetActive(false);
        timeIsUpTitle.SetActive(false);
    }

    public void show()
    {
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.gameOver);
        gameOverTitle.SetActive(true);
        gameObject.SetActive(true);
        Invoke("hide", 2f);
    }

    public void showTimeIsUp()
    {
        gameObject.SetActive(true);
        timeIsUpTitle.SetActive(true);
        Invoke("hideTimeIsUp", 2f);
    }

    public void onPlayAgain()
    {
        hide();
        gameController.GetComponent<GameController>().startGame();

    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        gameOverTitle.SetActive(false);
        timeIsUpTitle.SetActive(false);

    }
}
