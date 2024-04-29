using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public GameManager gameManager;

    public BoxCollider2D detector;
    private Animator animator;
    public AudioSource doorOpening;

    private void Awake()
    {
        gameManager = gameManager.GetComponent<GameManager>();


    }
    private void Start()
    {
        detector = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // activate battle UI for this enemy 
        if (collision.gameObject == GameObject.FindWithTag("Player") && gameManager.BossKeyObtained == true)
        {
            
            StartCoroutine(BossDoorOpen());
            
        }
    }

    IEnumerator BossDoorOpen() 
    {
        
        animator.SetBool("Opening", true);
        doorOpening.Play();
        
        yield return new WaitForSeconds(3f);
        gameManager.BossDoorOpened = true;
        Destroy(detector);
        gameManager.currentamountofKeys--;
    }
    private void Update()
    {
        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }
        if (gameManager.BossDoorOpened == true )
        {
            animator.SetBool("isOpened", true);
            if (detector != null)
            {
                detector.enabled = false;
            }
        }
    }


}
