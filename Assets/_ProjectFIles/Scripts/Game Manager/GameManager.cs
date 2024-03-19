using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    #region Variables
    public GameState CurrentGameState; // current game state
    //public bool OverworldRunning = false;
    
    public static GameManager manager;
    public GameObject player;
    
    public GameObject battleSystem; //battle system object - canvas to turn it on/off
    public BattleSystem battleScript; //battle system script to access the battle functions

    //player stats
    public string unitname;
    public int unitLevel;

    public int damage;
    public int damage2;
    public int damage3;
    public int damage4;


    public int maxHP;
    public int currentHP;
    public int CurrentXP;
    public int MaxXP;
    public int XPLevel;
    public Slider XPSlider;
    public GameObject OverworldUI;
    public Slider HealthSlider;


    /*public PlayerMovement PlayerMovement = null;
    public SpriteRenderer PlayerSprite = null;*/

    //a delegate event to send to freeze enemies
    public delegate void FreezeEnemy(bool t);
    public static event FreezeEnemy OnFreezeEnemyEvent;
    #endregion

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
        OverworldUI.SetActive(true);
        Initialise();
        if (battleSystem != null)
        {

            battleSystem.SetActive(false);
            Debug.Log("Not Active");
        }
        else
        {
            Debug.Log("No battle system");
        }

        
        //call FreezeEnemies(false); to unfreeze the enemies

    }
    private void GameOver()
    {
        //OverworldRunning = false;

    }
    private void BattleState()
    {
        OverworldUI.SetActive(false);
        battleSystem.SetActive(true);
        if (battleSystem != null)
        {
            battleSystem.SetActive(true);
            Debug.Log("Active");
        }
        else
        {
            Debug.Log("No battle system");
        }


        //battleScript = gameObject.AddComponent<BattleSystem>(); //not sure what this is for as it adda a compontnete 

        Debug.Log("BattleState");
        
        //call FreezeEnemies(true); to freeze the enemies
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


    #region playerStats
    
    #endregion
    #region Take Damage 
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
    public bool TakeDamage2(int dmg2)
    {
        currentHP -= dmg2;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }

    }
    public bool TakeDamage3(int dmg3)
    {
        currentHP -= dmg3;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }

    }
    public bool TakeDamage4(int dmg4)
    {
        currentHP -= dmg4;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }

    }


    #endregion

    #region freeze enemies
    //---- enemy freeze - send the event to freeze the enemies
    public void FreezeEnemies(bool t)
    {
        //broadcast the event to freeze/unfreeze the enemies
        if (OnFreezeEnemyEvent != null)
        {
            OnFreezeEnemyEvent(t);
        }
    }

    void Update()
    {
        HealthSlider.value = currentHP;
        HealthSlider.maxValue = maxHP;
        XPSlider.value = CurrentXP;
        XPSlider.maxValue = maxHP;
        //test freeze enemies
        if(Input.GetKeyDown(KeyCode.P))
        {
            FreezeEnemies(true);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            FreezeEnemies(false);
        }
    }
    #endregion
}


