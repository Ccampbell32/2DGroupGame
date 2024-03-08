using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseScene : MonoBehaviour{
    
    #region Initialise Variables
    private GameManager gameManager; //declare instance of GameManager

    [Header("Set Game State")] public GameState gameState = GameState.Gameplay; // The game state to change to (this is set in the inspector)

    [Header("Initialise Player")] 
    public bool initialisePlayer = true; // Set to true to initialise the player
    public GameObject playerPrefab; // The player prefab to be instantiated
    private GameObject player; // The player game object that is instantiated
    public Transform playerSpawnPoint; // The position to instantiate the player at
    #endregion

    #region Initialise Level
    private void Awake(){
        //get the spawn point for the player from the hierarchy - child object called (Player Spwan Point)
        playerSpawnPoint = GameObject.Find("Player Spawn Point").transform;
    }

    void Start(){
        //get the instance of GameManager for use in this script but check if it exists first
        if (GameManager.manager != null){
            //get the instance of GameManager
            gameManager = GameManager.manager;
            //change the game state to the specified game state
            gameManager.ChangeGameState(gameState);
            Debug.Log("GameManager found! " + " - Current State: " + gameManager.CurrentGameState.ToString());
        }
        else{
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }

        if(initialisePlayer){
            InitialisePlayer();
        }
    }

    private void InitialisePlayer(){
        //check if the player exists in the scene - if not then instantiate the player
        if (GameObject.FindWithTag("Player") == null){
            // Instantiate the player at the specified position and rotation
            player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            Debug.Log("Player instantiated!");
        }
        else{
            //get the player from the scene
            player = GameObject.FindWithTag("Player");
            Debug.Log("Player already exists in scene!");
        }
    }
    #endregion
}