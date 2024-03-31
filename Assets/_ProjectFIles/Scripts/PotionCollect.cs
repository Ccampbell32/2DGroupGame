using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCollect : MonoBehaviour
{

    public GameManager gameManager;
    private GameManager script;
    public GameObject player;
    public GameObject healthItem;
   
    private void Awake()
    {
        script = gameManager.GetComponent<GameManager>();

       
    }
     void Start()
     {
        if (script.Potion1Collected == true)
        {
            healthItem.SetActive(false);

        }
        else
        {

            healthItem.SetActive(true);
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
    private void Update()
    {
        if (script.Potion1Collected == true)
        {
            healthItem.SetActive(false);

        }
        else
        {

            healthItem.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            gameManager.Potion1Collected = true;
            gameManager.potionsHeld.text = "x 1";
            Destroy(gameObject);
        }
        else
        {

            gameManager.Potion1Collected = false;

        }
    }







}
   