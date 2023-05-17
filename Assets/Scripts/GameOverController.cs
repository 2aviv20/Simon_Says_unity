using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] public GameObject gameController;
    [SerializeField] public GameObject gameOverTitle;

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void show()
    {
        FindObjectOfType<AudioManager>().PlaySound(SoundManagerEnum.gameOver);
        gameOverTitle.SetActive(true);
        gameObject.SetActive(true);
        Invoke("hide", 2f);
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
    }
}
