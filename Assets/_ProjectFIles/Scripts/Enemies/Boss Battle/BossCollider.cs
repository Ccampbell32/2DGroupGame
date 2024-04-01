using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
    public BattleSystem battleSystem;

    private void Awake()
    {
        
        battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
    }
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }
        if (battleSystem != null)
        {
            battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>(); 

        }
        else
        {
            Debug.Log("BattleSystem not found! - Please add to the scene!");
        }


        if (gameManager.BossBeaten == true)
        {
            gameObject.SetActive(false);

        }


    }
    private void Update()
    {
        if (gameManager.BossBeaten == true)
        {
            gameObject.SetActive(false);

        }
        else
        {
            gameObject.SetActive(true);
        }
        if (battleSystem.state == BattleState.WON)
        {
            gameManager.BossBeaten = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Destroy(gameObject);
            gameManager.ChangeGameState(GameState.BattleState);
        }
    }
}
