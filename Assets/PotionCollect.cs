using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCollect : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
   
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

       
    }
     void Start()
     {
        if (gameManager.Potion1Collected == true)
        {
            GameObject.Destroy(gameObject);

        }

        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            gameManager.Potion1Collected = true;

            Destroy(gameObject);
        }
        else
        {

            gameManager.Potion1Collected = false;

        }
    }







}
   