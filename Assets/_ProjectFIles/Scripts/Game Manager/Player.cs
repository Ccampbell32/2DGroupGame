using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private GameManager gameManager; //GET A REFERENCE TO THIS SO WE CAN ACCESS THE PLAYERS HEALTH AND ATTRIBUTES
    
    [Header("Player Prefab and Spawn Point")]
    public GameObject playerPrefab; // The player prefab to be instantiated
    private GameObject player; // The player game object that is instantiated
    public Transform playerSpawnPoint; // The position to instantiate the player at
    public Quaternion playerRotation; // The rotation to instantiate the player with
    
    [Header("Player Health and Attributes")]
    public float playerHealth; // The players health
    public float playerDamage; // The players damage
    public float playerSpeed; // The players speed

    void Start()
    {
        //get the instance of GameManager for use in this script but check if it exists first
        if (GameManager.manager != null) {
            //get the instance of GameManager
            gameManager = GameManager.manager;
            Debug.Log("GameManager found! " + " - Current State: " + gameManager.CurrentGameState.ToString());
        }
        else {
            Debug.Log("GameManager not found!");
        }
        
    }
    private void InitialisePlayer()
    {
        // Instantiate the player at the specified position and rotation
        player = Instantiate(playerPrefab, playerSpawnPoint.position, playerRotation);
        
        //get and set the players health and attributes from game manager
        playerHealth = gameManager.PlayerCurrentHealth;
        
    }
    
    //an example of how to change the players health - call this public function to take damage
    public void ChangePlayerHealth(float newHealth)
    {
        //change the players health
        playerHealth = newHealth;
        //update the players health in the game manager
        gameManager.PlayerCurrentHealth = playerHealth;
    }
    
    //an example of how to change the players damage - call this public function to change the players damage
    //use this same pattern to pass data to and from the player and the gamemanager
    // example - player collects health pack, player calls a function in the game manager to increase the players health
    /*
    public void ChangePlayerDamage(float newDamage)
    {
        //change the players damage
        playerDamage = newDamage;
        //update the players damage in the game manager
        gameManager.PlayerDamage = playerDamage;
    }
    */
}
