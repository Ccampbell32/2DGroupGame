using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public bool isPickedUp;
    public GameManager gameManager;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }

        if (gameManager.BossKeyObtained == true)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            Destroy(gameObject);
            gameManager.BossKeyObtained = true;
        }
    }
}