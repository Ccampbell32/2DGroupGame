using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class EnemyMoveVert : MonoBehaviour
{
    
    public float speed;
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;

    //detection variables
    public PolygonCollider2D detector;
    public bool isChasing;
    Transform target;
    Vector2 moveDirection;
    public GameObject detectionLightDown;
    public GameObject detectionLightUp;
    public GameObject player;
    public GameObject Spotted;
    public GameManager gameManager;
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip soundEffect; // Sound effect clip
    private bool hasPlayed = false; // This variable is to track if the sound effect has played

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        animator.SetBool("IsMoving (Down)", true);
        detectionLightUp.SetActive(false);
        Spotted.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player");
        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }

    }

    

    void Update()
    {
        //set to chase the player
        if (isChasing)
        {
            detectionLightDown.SetActive(false);
            detectionLightUp.SetActive(false);
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            speed = 7;
            // Play the sound effect
            audioSource.Play();
            hasPlayed = true;
        }
        //not in chase
        else
        {

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(0, -speed);
            }
            else
            {
                rb.velocity = new Vector2(0, speed);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
                StartCoroutine(WaitAndFlip()); // Start waiting coroutine
                animator.SetBool("IsMoving (Down)", true);
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
                StartCoroutine(WaitAndFlip()); // Start waiting coroutine
                animator.SetBool("IsMoving (Up)", true);
            }
        }

    }
    private void FixedUpdate()
    {
        //set to chase the player
        if (isChasing)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //get the target (Player) from the collision
            target = collision.transform;
            isChasing = true;
            speed = 5;
            Spotted.gameObject.SetActive(true);
        }


    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // activate battle UI for this enemy 
        if (collision.gameObject == player)
        {
            gameManager.ChangeGameState(GameState.BattleState);
            speed = 0;

            Destroy(gameObject);
        }
    }

    // Wait at point before turning
    IEnumerator WaitAndFlip()
    {
      speed = 0; // Stop the object
          
      yield return new WaitForSeconds(4f); // Wait for 4 seconds
      Flip();
      speed = 5; // Move again
      if (speed == 0 && currentPoint == pointA.transform)
      {
            animator.SetFloat("Y", 1);
            
      }

    }
   

    private void Flip()
    {
        if (currentPoint == pointA.transform)
        {
            animator.SetBool("IsMoving (Down)", false);
            
            animator.SetBool("IsMoving (Up)", true);
        }

        if (currentPoint == pointB.transform)
        {
            animator.SetBool("IsMoving (Up)", false);
            animator.SetFloat("Y", -1);
            animator.SetBool("IsMoving (Down)", true);

        }
       if (animator.GetBool("IsMoving (Up)") == true)
       {
            detectionLightDown.SetActive(false);
            detectionLightUp.SetActive(true);
        }
        if (animator.GetBool("IsMoving (Down)") == true)
        {
            detectionLightUp.SetActive(false);
            detectionLightDown.SetActive(true);
            
        }
    }
    
    private void FreezeEnemy(bool t)
    {
        if (t)
        {
            //freeze the rigidbody2D X and Y
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            //YOU CAN SET THE ANIMATION TO IDLE HERE
        }
        else
        {
            //unfreeze the rigidbody2D X and Y
            rb.constraints = RigidbodyConstraints2D.None;
            //YOU CAN SET THE ANIMATION TO WALK HERE
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
    
    
    #region Event Subscriptions
    //------------------EVENT SUBSCRIPTIONS------------------
//event subscriptions OnEnable and OnDisable
    private void OnEnable()
    {
        //subscribe to the event
        GameManager.OnFreezeEnemyEvent += FreezeEnemy;
    }
    private void OnDisable()
    {
        //unsubscribe to the event
        GameManager.OnFreezeEnemyEvent -= FreezeEnemy;
    }
    #endregion
}

