using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
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
    public PlayerMovement playerMove;
    
    public GameObject battleSystem; //battle system object - canvas to turn it on/off
    public BattleSystem battleScript; //battle system script to access the battle functions
    public GameObject GlobalVolume;

    //player stats
    public string unitname = "Ramesses";
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

    //Potion collection
    public TMP_Text potionsHeld;
    public int currentamountofPotions;
    public GameObject Potion1;
    public GameObject Potion2;
    public bool Potion1Collected;
    public bool Potion2Collected;   
    public int CurrentScene;
    public TMP_Text XPText;
    public bool JustDied;
    public GameObject DeathUI;
    public GameObject PlayerDeath;
    public Animator PlayerDeathAnim;

    //Boss
    public bool BossBeaten;
    public bool BossKeyObtained;
    public bool BossDoorOpened;

    public int currentamountofKeys;
    public TMP_Text keysHeld;
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
        XPLevel = 1;
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
        }
        else
        {

            if (GameObject.FindWithTag("BattleSystem") != null)
            {
                battleSystem = GameObject.FindWithTag("BattleUICanvas");
                battleScript = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
                Debug.Log("Found Battle System");
                battleSystem.SetActive(false);
            }
            else
            {
                Debug.Log("No Battle System");
            }         
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            if (OverworldUI == null)
            {
                OverworldUI = GameObject.FindWithTag("OverwordlUICanvas");


            }
            if (HealthSlider == null)
            {
                HealthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
            }
            if (XPSlider == null)
            {
                XPSlider = GameObject.FindWithTag("XPSlider").GetComponent<Slider>();
            }
            if (potionsHeld == null) 
            {
                potionsHeld = GameObject.FindWithTag("BottleAmount").GetComponent<TMP_Text>();

            }
            if (keysHeld == null)
            {
                keysHeld = GameObject.FindWithTag("KeyAmount").GetComponent<TMP_Text>();

            }
            if (XPText == null)
            {
                XPText = GameObject.FindWithTag("XPLevel").GetComponent<TMP_Text>();
            }
            if (playerMove == null)
            {
                playerMove = player.GetComponent<PlayerMovement>();
            }
            if (PlayerDeath == null)
            {
                PlayerDeath = GameObject.FindWithTag("DeathScreen");
                if (PlayerDeath != null)
                {
                    PlayerDeathAnim = GameObject.FindWithTag("DeathScreen").GetComponent<Animator>();
                    PlayerDeath.SetActive(false);
                }
            }
            if (DeathUI == null)
            {
                DeathUI = GameObject.FindWithTag("DeathUI");
                if (DeathUI != null)
                {
                    DeathUI.SetActive(false);
                }

            }
            if (GlobalVolume == null)
            {
                GlobalVolume = GameObject.FindWithTag("GlobalVol");

            }
            {
                
            }
            PlayerLevel();
            XPLevelling();
            
            CurrentGameState = GameState.Overworld;
            
            //battleSys.gameObject.SetActive(false);
        }




        potionsHeld.text = "x" + currentamountofPotions.ToString();
        keysHeld.text = "x" + currentamountofKeys.ToString();
    }

    #endregion
   
    #region GameStates
    private void Overworld()
    {
        GlobalVolume.SetActive(true);
        OverworldUI.SetActive(true);
        PlayerDeath.SetActive(false);
        Potion1 = GameObject.FindWithTag("Potion1");
        Potion2 = GameObject.FindWithTag("Potion2");
        if (battleSystem != null)
        {
            
            battleSystem.SetActive(false);
        }
        player.SetActive(true);
        Initialise();

        if (battleSystem != null)
        {

            battleSystem.SetActive(false);
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
        if (Potion2 != null)
        {
            Potion2 = GameObject.FindWithTag("Potion2");
            //potion2 collected
            if (Potion2Collected == true)
            {
                Potion2.gameObject.SetActive(false);
            }
            else
            {
                Potion2.gameObject.SetActive(true);
            }
        }

        //call FreezeEnemies(false); to unfreeze the enemies
        if (JustDied)
        {
            StartCoroutine(DeathScene());
        }
        else
        {
            DeathUI.SetActive(false);
        }
    }
  
    public IEnumerator DeathScene()
    {
        player.SetActive(false);
        OverworldUI.SetActive(false);
        playerMove.enabled = false;
        PlayerDeath.SetActive(true);
        PlayerDeathAnim.SetBool("IsDead", true);
        yield return new WaitForSeconds(2);
        DeathUI.SetActive(true);
        PlayerDeath.SetActive(false);
        JustDied = false;
        player.SetActive(true);

    }
    private void GameOver()
    {
        //OverworldRunning = false;

    }
    public void BattleState()
    {
        GlobalVolume.SetActive(false);
        OverworldUI.SetActive(false);
        player.SetActive(false);

        if (battleSystem != null)
        {
            battleSystem.SetActive(true);
        }
        else
        {
            battleSystem.SetActive(true);
            
        }


        battleScript = battleSystem.GetComponent<BattleSystem>();

        
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
    public void PlayerLevel() 
    {
        if (unitLevel == 1)
        {
            maxHP = 10;
            MaxXP = 10;
        }

        if (unitLevel == 2)
        {
            maxHP = 12;
            MaxXP = 12;
        }
        if (unitLevel == 3)
        {
            maxHP = 14;
            MaxXP = 14;
        }
        if (unitLevel == 4)
        {
            maxHP = 16;
            MaxXP = 16;
        }
        if (unitLevel == 5)
        {
            maxHP = 18;
            MaxXP = 18;
        }
        if (unitLevel == 6)
        {
            maxHP = 20;
            MaxXP = 20;
        }
    }
    public void XPLevelling()
    {
        if (XPLevel == 1)
        {
         
            MaxXP = 10;
        }

        if (XPLevel == 2)
        {
            
            MaxXP = 12;
        }
        if (XPLevel == 3)
        {
           
            MaxXP = 14;
        }
        if (XPLevel == 4)
        {
            
            MaxXP = 16;
        }
        if (XPLevel == 5)
        {
            
            MaxXP = 18;
        }
        if (XPLevel == 6)
        {
            
            MaxXP = 20;
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

    #region KeysInventory
    public void IncreaseKeys(int v)
    {
        currentamountofKeys += v;
        keysHeld.text = "x" + currentamountofKeys.ToString();
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

        if (HealthSlider != null)
        {
            HealthSlider.value = currentHP;
            HealthSlider.maxValue = maxHP;
        }
        if (XPSlider != null)
        {
            XPSlider.value = CurrentXP;
            XPSlider.maxValue = maxHP;
        }
        //test freeze enemies
        if(Input.GetKeyDown(KeyCode.P))
        {
            FreezeEnemies(true);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            FreezeEnemies(false);
        }
        unitLevel = XPLevel;
        XPText.text = XPLevel.ToString();
        if (CurrentXP >= MaxXP)
        {
            XPLevel += 1;
            CurrentXP -= MaxXP;
        }
    }
    #endregion
    #region XP
    public void AddXP(int xp)
    {
        CurrentXP += xp;
        
    }
    #endregion
    #region Death
    public void resetScene()
    {
        ResetStats();
        OverworldUI.SetActive(true);
        SceneManager.LoadScene(0);
    }
    public void ResetStats()
    {
        playerMove.enabled = true;
        maxHP = 10;
        XPLevel = 1;
        currentHP = maxHP;
        MaxXP = 10;
        CurrentXP = 0;
        BossKeyObtained = false;
        currentamountofKeys = 0;
        currentamountofPotions = 0;
        Potion1Collected = false;
    }
    #endregion
}


