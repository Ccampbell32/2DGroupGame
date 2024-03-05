using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameState
{
    Battling,
    MainMenu,
    Options,
    Paused,
    Running,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState; // current game state
    public bool OverworldRunning = false;
    public float PlayerMaxHealth = 10;
    public static GameManager manager;
    public PlayerMovement PlayerMovement;
    public SpriteRenderer PlayerSprite;
    void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        } else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Gameplay()
    {
        OverworldRunning = true;
        PlayerMovement.enabled = true;
        PlayerSprite.enabled = true;
    }
    private void GameOver()
    {
        OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void Battling()
    {
        OverworldRunning = false;
        //throw new NotImplementedException();
    }
    private void MainMenu()
    {
        OverworldRunning = false;
        PlayerMovement.enabled = false;
        PlayerSprite.enabled = false;
        
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
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.MainMenu;
        MainMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
