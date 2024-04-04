using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
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
    public string unitname = "Ramesses";
    public int unitLevel;

    public int damage = 2;
    public int damage2 = 3;
    public int damage3 = 4;
    public int damage4 = 5;


    public int maxHP;
    public int currentHP;
    public int CurrentXP;
    public int MaxXP;
    public int XPLevel;
    public Slider XPSlider;
    public GameObject OverworldUI;
    public Slider HealthSlider;

    //Potion collection
    public TMP_Text potionsHeld;
    public int currentamountofPotions;
    public GameObject Potion1;
    public bool Potion1Collected;
    public int CurrentScene;

    //BossDefeated
    public bool BossBeaten;

    /*public PlayerMovement PlayerMovement = null;
    public SpriteRenderer PlayerSprite = null;*/

    //a delegate event to send to freeze enemies
    public delegate void FreezeEnemy(bool t);
    public static event FreezeEnemy OnFreezeEnemyEvent;
    #endregion

    #region Initialise
    public void Awake()
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
        unitLevel = 1;
        PlayerLevel();
        currentHP = maxHP;
        CurrentScene = 0;

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
           

            
            if(battleSystem == null)
            {
                battleSystem = GameObject.FindWithTag("BattleUICanvas");
                Debug.Log("Found Battle System");

            }

            if (GameObject.FindWithTag("BattleSystem") != null)
            {
                battleScript = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
                Debug.Log("Found Battle System");

            }
            else
            {
                Debug.Log("No Battle System");
            }
            if (battleSystem != null)
            {
                battleSystem.SetActive(false);
            }
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
                Debug.Log("Found Player");

            }
            if (OverworldUI == null)
            {
                OverworldUI = GameObject.FindWithTag("OverwordlUICanvas");
                Debug.Log("Found OverworldUI");

            }
            if (HealthSlider == null)
            {
                HealthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
                Debug.Log("Found health Slider");

            }
            if (XPSlider == null)
            {
                XPSlider = GameObject.FindWithTag("XPSlider").GetComponent<Slider>();
                Debug.Log("Found XP Slider");

            }
            if (potionsHeld == null) 
            {
                potionsHeld = GameObject.FindWithTag("BottleAmount").GetComponent<TMP_Text>();

            }
            CurrentGameState = GameState.Overworld;
            //battleSys.gameObject.SetActive(false);
        }
        damage = 2;
        damage2 = 3; 
        damage3 = 4;
        damage4 = 4;
        
        potionsHeld.text = "x" + currentamountofPotions.ToString();

    }

    #endregion

    #region GameStates
    private void Overworld()
    {
        OverworldUI.SetActive(true);

        Potion1 = GameObject.FindWithTag("Potion1");

        if (battleSystem != null)
        {
            battleSystem.SetActive(false);
        }
        player.SetActive(true);
        Debug.Log("BattleSys Deactivated");
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


        if (Potion1 != null)
        {
            Potion1 = GameObject.FindWithTag("Potion1");
            //potion collected
            if (Potion1Collected == true)
            {
                Potion1.gameObject.SetActive(false);
            }
            else
            {
                Potion1.gameObject.SetActive(true);
            }
        }

        //call FreezeEnemies(false); to unfreeze the enemies

    }
    private void GameOver()
    {
        //OverworldRunning = false;

    }
    public void BattleState()
    {

        OverworldUI.SetActive(false);
        player.SetActive(false);
        if (battleSystem != null)
        {
            battleSystem.SetActive(true);
            Debug.Log("Active");
        }
        else
        {
            Debug.Log("No battle system");
        }


        battleScript = battleSystem.GetComponent<BattleSystem>();

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


    #region PlayerLevels
    public void PlayerLevel () 
    {
        if (unitLevel == 1)
        {
            maxHP = 10;
        }

        if (unitLevel == 2)
        {
            maxHP = 12;
        }
        if (unitLevel == 3)
        {
            maxHP = 14;
        }
        if (unitLevel == 4)
        {
            maxHP = 16;
        }
        if (unitLevel == 5)
        {
            maxHP = 18;
        }
        if (unitLevel == 6)
        {
            maxHP = 20;
        }
    }
    
    #endregion
    #region Take Damage 
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        HealthSlider.value = currentHP;
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
        HealthSlider.value = currentHP;
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
        HealthSlider.value = currentHP;
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
        HealthSlider.value = currentHP;
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

    #region HealthBottles
    public void IncreaseBottles(int v)
    {
        currentamountofPotions += v;
        potionsHeld.text = "x" + currentamountofPotions.ToString();
    }


    #endregion

    #region Freeze Enemies
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


