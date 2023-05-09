using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] public  GameObject gameOver;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
