using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseScene : MonoBehaviour
{
    #region Initialise Variables

    private GameManager gameManager; //declare instance of GameManager
    
    [Header("Set Game State")]
    public GameState gameState = GameState.Gameplay; // The game state to change to (this is set in the inspector)
    
    #endregion

    #region Initialise Level
    
    void Start()
    {
        //get the instance of GameManager for use in this script but check if it exists first
        if (GameManager.manager != null) {
            //get the instance of GameManager
            gameManager = GameManager.manager;
            //change the game state to the specified game state
            gameManager.ChangeGameState(gameState);
            Debug.Log("GameManager found! " + " - Current State: " + gameManager.CurrentGameState.ToString());
        }
        else {
            Debug.Log("GameManager not found!");
        }

    }

    
    #endregion
    
}
