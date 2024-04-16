using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class EnemyMoveHoz : MonoBehaviour
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
    public GameObject detectionLight;
    public GameObject player;
    public GameObject Spotted;

    public GameManager gameManager;
    public AudioSource audioSource; // Reference to the AudioSource component
    
    

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Start()
    {
        // battle mode
 

        //getcomponents
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        animator.SetBool("IsMoving (LeftRight)", true);
        detector = GetComponent<PolygonCollider2D>();
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

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }
   
    void Update()
    {


        //set to chase the player 
        if (isChasing)
        {
            detectionLight.SetActive(false);
            Component.Destroy(detector);
            
             Vector3 direction = (target.position - transform.position).normalized;
             moveDirection = direction;
            speed = 7;

            
            

        }
        //not in chase
        else
        {
            
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
                
            }
            else
            {

                rb.velocity = new Vector2(-speed, 0);
                                
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                StartCoroutine(WaitAndFlip()); // Start waiting coroutine
                currentPoint = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                StartCoroutine(WaitAndFlip()); // Start waiting coroutine
                currentPoint = pointB.transform;
            }
        }
    }
    private void FixedUpdate()
    {
        //set to chase the player
        if (isChasing) 
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            
            rb.velocity = moveDirection * speed;
            
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
            audioSource.Play();
        }
        
       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
         // activate battle UI for this enemy 
        if (collision.gameObject == player )
        {
            speed = 0;

            gameManager.ChangeGameState(GameState.BattleState);
            
            Destroy(gameObject);
        }
    }

    IEnumerator WaitAndFlip()
    {
        speed = 0; // Stop the object
        animator.SetBool("IsMoving (LeftRight)", false); // Set animation to idle
        yield return new WaitForSeconds(4f); // Wait for 4 seconds
        flip(); // Flip 
        speed = 5; // Move again
        animator.SetBool("IsMoving (LeftRight)", true); // Set animation to moving
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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

