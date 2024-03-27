using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    void Start()
    {
        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }
        if (gameManager.BossBeaten == true)
        {
            gameObject.SetActive(false);

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
