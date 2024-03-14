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
    
    public static GameManager manager;
    public GameObject player;
    
    //define a list for enemyList
    public List<GameObject> enemyList = new List<GameObject>();
    
    public GameObject battleSystem; //battle system object - canvas to turn it on/off
    public BattleSystem battleScript; //battle system script to access the battle functions

    //player stats
    public string unitname;
    public int unitLevel;

    public int damage;
    public int maxHP;
    public int currentHP;


    /*public PlayerMovement PlayerMovement = null;
    public SpriteRenderer PlayerSprite = null;*/

    //Player attributes
    public float playerMaxHealth = 10;
    public float playerCurrentHealth = 10;

    #region Initialise
    void Awake()
    {
        //set the instance of GameManager to this instance and make it persist between scenes
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
        
    }
    
    public void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        //if we are in the menu set the gamestate to menu else find the player and battle system
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            CurrentGameState = GameState.MainMenu;
            Debug.Log("InMenu");
        }
        else
        {
            CurrentGameState = GameState.Overworld;

            player = GameObject.FindWithTag("Player");
            battleSystem = GameObject.FindWithTag("BattleUICanvas");
            Debug.Log("player found");
            Debug.Log("battle system found");
            //battleSys.gameObject.SetActive(false);
        }
    }

    #endregion

    #region GameStates
    private void Overworld()
    {
        battleSystem.SetActive(false);

    }
    private void GameOver()
    {
        //OverworldRunning = false;

    }
    private void BattleState()
    {
        
        battleSystem.SetActive(true);
        Debug.Log("Activate");

        //battleScript = gameObject.AddComponent<BattleSystem>(); //not sure what this is for as it adda a compontnete 

        Debug.Log("BattleState");
    }
    private void MainMenu()
    {
        //OverworldRunning = false;
        Debug.Log("Main Menu");

    }
    private void Options()
    {
        //OverworldRunning = false;

    }
    private void Paused()
    {
        //OverworldRunning = false;

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

    public void PlayerStats()
    {



    }
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }
    }

    
    //---- enemy freeze - this is called from the battle system to freeze the enemies in the scene
    //a function to get a list of all the enemies in the scene and freeze them
    public void FreezeEnemies(bool t)
    {
        //find all enemies in the scene
        // Find all instances of EnemyMoveHoz in the scene
        EnemyMoveHoz[] enemiesWithHozMovement = GameObject.FindObjectsOfType<EnemyMoveHoz>();
        EnemyMoveVert[] enemiesWithVertMovement = GameObject.FindObjectsOfType<EnemyMoveVert>();

        //empty the list
        enemyList.Clear();
        
        // add both enemy types to a lst of game objects
        enemyList = new List<GameObject>();
        foreach (EnemyMoveHoz enemy in enemiesWithHozMovement)
        {
            enemyList.Add(enemy.gameObject);
        }
        foreach (EnemyMoveVert enemy in enemiesWithVertMovement)
        {
            enemyList.Add(enemy.gameObject);
        }
        
        //loop through the list and freeze the enemies
        foreach (GameObject enemy in enemyList)
        {
            if (t)
            {
                enemy.GetComponent<EnemyMoveHoz>().speed= 0;
                enemy.GetComponent<EnemyMoveVert>().speed= 0;
            }
            else
            {
                enemy.GetComponent<EnemyMoveHoz>().speed = 5;
                enemy.GetComponent<EnemyMoveVert>().speed = 5;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FreezeEnemies(true);
        }
    }
}


