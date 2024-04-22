using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialiseScene : MonoBehaviour{
    
    #region Initialise Variables
    public GameManager gameManager; //declare instance of GameManager

    [Header("Set Game State")] 
    public GameState gameState = GameState.Overworld; // The game state to change to (this is set in the inspector)

    [Header("Initialise Player")] 
    public bool initialisePlayer = true; // Set to true to initialise the player
    public GameObject playerPrefab; // The player prefab to be instantiated
    private GameObject player; // The player game object that is instantiated
    [Header("SpawnPoint1")]
    public Transform playerSpawnPoint1; // The position to instantiate the player at
    public int FromScene1;
    [Header("SpawnPoint2")]
    public Transform playerSpawnPoint2;
    public int FromScene2;
    [Header("SpawnPoint3")]
    public Transform playerSpawnPoint3;
    public int FromScene3;
    [Header("SpawnPoint4")]
    public Transform playerSpawnPoint4;
    public int FromScene4;
    public bool FixingStuff;

    #endregion

    #region Initialise Level
    private void Awake(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //get the spawn point for the player from the hierarchy - child object called (Player Spwan Point)
        //check if the player spawn point exists in the scene
        if (GameObject.Find("Player Spawn Point") != null){
            //get the player spawn point
            //playerSpawnPoint = GameObject.Find("Player Spawn Point").transform;
            Debug.Log("Player Spawn Point found!"); 
        }
        else{
            Debug.Log("Player Spawn Point not found! - Please add a Player Spawn Point to the scene if it is not the menu!");
        }
        InitialisePlayer();
        gameManager.CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Start(){
        //get the instance of GameManager for use in this script but check if it exists first
        if (gameManager != null){
            //get the instance of GameManager
            
            //change the game state to the specified game state
            //gameManager.ChangeGameState(gameState);
            Debug.Log("GameManager found! " + " - Current State: " + gameManager.CurrentGameState.ToString());
            //gameManager.Initialise();
        }
        else{
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }
       
        if(initialisePlayer){
            
            
        }
    }

    private void InitialisePlayer(){
        //check if the player exists in the scene - if not then instantiate the player
        if (GameObject.FindWithTag("Player") == null && playerPrefab != null){
            if(gameManager.CurrentScene == FromScene1)
            {
                player = Instantiate(playerPrefab, playerSpawnPoint1.position, playerSpawnPoint1.rotation);
            }
            if(playerSpawnPoint2 != null){
                if (gameManager.CurrentScene == FromScene2)
                {
                    player = Instantiate(playerPrefab, playerSpawnPoint2.position, playerSpawnPoint2.rotation);
                }
            }
            if (playerSpawnPoint3 != null)
            {
                if (gameManager.CurrentScene == FromScene3)
                {
                    player = Instantiate(playerPrefab, playerSpawnPoint3.position, playerSpawnPoint3.rotation);
                }
            }
            if (playerSpawnPoint4 != null)
            {
                if (gameManager.CurrentScene == FromScene4)
                {
                    player = Instantiate(playerPrefab, playerSpawnPoint4.position, playerSpawnPoint4.rotation);
                }
            }           
                if (gameManager.CurrentScene == 0 && FixingStuff == false)
                {
                    player = Instantiate(playerPrefab, playerSpawnPoint1.position, playerSpawnPoint1.rotation);
                }
            
            
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