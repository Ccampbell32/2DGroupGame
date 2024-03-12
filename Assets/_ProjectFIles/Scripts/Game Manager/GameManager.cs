using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Game State
public enum GameState
{
    Battling,
    MainMenu,
    Options,
    Paused,
    Overworld,
    GameOver
}
#endregion


public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState; // current game state
    public bool OverworldRunning = false;
    public BattleSystem battleScript;

    public static GameManager manager;

    /*public PlayerMovement PlayerMovement = null;
    public SpriteRenderer PlayerSprite = null;*/
    
    //Player attributes
    [SerializeField] float playerMaxHealth = 10;
    [SerializeField] public float playerCurrentHealth = 10;

    #region Initialise
    void Awake()
    {
        //set the instance of GameManager to this instance and make it persist between scenes
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        } else if (manager != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region GameStates
    private void Overworld()
    {
        OverworldRunning = true;
        /*PlayerMovement.enabled = true;
        PlayerSprite.enabled = true;*/
        Debug.Log("Overworld");
    }
    private void GameOver()
    {
        OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void BattleState()
    {
        battleScript = gameObject.AddComponent<BattleSystem>();
        OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void MainMenu()
    {
        OverworldRunning = false;
        /*PlayerMovement.enabled = false;
        PlayerSprite.enabled = false;*/
        Debug.Log("Main Menu");
        
    }
    private void Options()
    {
        OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void Paused()
    {
        OverworldRunning = false;
        //throw new NotImplementedException();
    }

    // Method to change the game state
    public void ChangeGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        if (CurrentGameState == GameState.MainMenu)
        {
            MainMenu();
        }
        if (CurrentGameState == GameState.Overworld)
        {
            Overworld();
        }
    }
    #endregion


    //get and set the players health - call to add health, take damage and check health
    //health can be greater than max health
    public float PlayerCurrentHealth
    {
        get { return playerCurrentHealth; }
        set 
        { 
            if (value > playerMaxHealth)
            {
                playerCurrentHealth = playerMaxHealth;
            }
            else
            {
                playerCurrentHealth = value; 
            }
        }
    }
    
}
