using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    public GameManager gameManager;
    private static bool musicExists = false; // Flag to check if music already exists

    private void Awake()
    {
        if (!musicExists)
        {
            DontDestroyOnLoad(this.gameObject);
            musicExists = true;
           
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }
    private void Update()
    {
        if (gameManager.CurrentGameState == GameState.BattleState) 
        { 
          gameObject.GetComponent<AudioSource>().Pause();
        } 
        else
        {
            gameObject.GetComponent<AudioSource>().UnPause();
        }
        if (gameManager.JustDied == true)
        {
            Destroy(gameObject);
        }
    }
}
