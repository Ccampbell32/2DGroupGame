using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


#region Game State
public enum GameState
{
    BattleState,
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
    //public bool OverworldRunning = false;
    public BattleSystem battleScript;
    public static GameManager manager;
    public GameObject player;
    public GameObject[] enemies1;
    public GameObject[] enemies2;
    public EnemyMoveHoz enemyMove1;
    public EnemyMoveVert enemyMove2;



    /*public PlayerMovement PlayerMovement = null;
    public SpriteRenderer PlayerSprite = null;*/

    //Player attributes
    public float playerMaxHealth = 10;
    [SerializeField] 
    public float playerCurrentHealth = 10;

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
        enemies1 = GameObject.FindGameObjectsWithTag("Basic Hell Enemy 1");
        enemies2 = GameObject.FindGameObjectsWithTag("Basic Hell Enemy 2");
        //OverworldRunning = true;
        /*PlayerMovement.enabled = true;
        PlayerSprite.enabled = true;*/
        player.GetComponent<PlayerMovement>().enabled = true; 
        Debug.Log("Overworld");

        foreach (GameObject Enemy in enemies1)
        {
            Enemy.GetComponent<EnemyMoveHoz>().enabled = true;           
        }
        foreach (GameObject Enemy2 in enemies2)
        {
            Enemy2.GetComponent<EnemyMoveVert>().enabled = true;           
        }
    }
    private void GameOver()
    {
        //OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void BattleState()
    {
        enemies1 = GameObject.FindGameObjectsWithTag("Basic Hell Enemy 1");
        enemies2 = GameObject.FindGameObjectsWithTag("Basic Hell Enemy 2");
        //battleScript = gameObject.AddComponent<BattleSystem>();
        //OverworldRunning = false;
        //throw new NotImplementedException();
        player.GetComponent<PlayerMovement>().enabled = false; 
        
        foreach (GameObject Enemy in enemies1)
        {
            //Enemy.GetComponent<EnemyMoveHoz>().enabled = false;
            enemyMove1 = Enemy.GetComponent<EnemyMoveHoz>();
            enemyMove1.enabled = false;
        }
        foreach (GameObject Enemy2 in enemies2)
        {
            //Enemy2.GetComponent<EnemyMoveVert>().enabled = false;
            
            enemyMove2 = Enemy2.GetComponent<EnemyMoveVert>();
            enemyMove2.enabled = false;
        }
        Debug.Log("BattleState");
    }
    private void MainMenu()
    {
        //OverworldRunning = false;
        /*PlayerMovement.enabled = false;
        PlayerSprite.enabled = false;*/
        Debug.Log("Main Menu");
        
    }
    private void Options()
    {
        //OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void Paused()
    {
        //OverworldRunning = false;
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
        if (CurrentGameState == GameState.BattleState)
        {
            BattleState();


        }

    }
    #endregion
    public void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            CurrentGameState = GameState.MainMenu;
            Debug.Log("InMenu");
        }
        else 
        {
            //CurrentGameState = GameState.;


        }
        player = GameObject.FindWithTag("Player");
        
    }
    public void update()
    {
        Debug.Log(playerMaxHealth);
    }

    //get and set the players health - call to add health, take damage and check health
    //health can be greater than max health
    public float PlayerCurrentHealth
    {
        get 
        { 
            return playerCurrentHealth; 
        }
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
