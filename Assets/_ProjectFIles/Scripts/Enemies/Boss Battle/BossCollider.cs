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
            if (GameObject.FindWithTag("BattleSystem") != null)
            {
                battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
            }

        }
        else
        {
            Debug.Log("BattleSystem not found! - Please add to the scene!");
        }


        if (gameManager.BossBeaten == true)
        {
            gameObject.SetActive(false);

        }
        if (gameManager.BossBeaten == true)
        {
            gameObject.SetActive(false);

        }
        else
        {
            gameObject.SetActive(true);
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
