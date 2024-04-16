using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    public GameManager gameManager;
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("OverworldMusic");
        if (musicObj.Length > 1 )
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
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
    }
}
