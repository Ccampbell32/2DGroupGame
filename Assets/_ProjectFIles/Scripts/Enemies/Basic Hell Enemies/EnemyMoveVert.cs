using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class EnemyMoveVert : MonoBehaviour
{
    [Tooltip("SET TO FALSE TO STOP THE ENEMY FROM MOVING - USED FOR BATTLE MODE")]
    public bool canMove = true;
    
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
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        animator.SetBool("IsMoving (Down)", true);
        detectionLightUp.SetActive(false);
        Spotted.gameObject.SetActive(false);

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
            
        }
        //not in chase
        else
        {

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                if (canMove)
                {
                    rb.velocity = new Vector2(0, -speed);
                }
            }
            else
            {
                if (canMove)
                {
                    rb.velocity = new Vector2(0, speed);
                }
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
            if (canMove)
            {
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
            }

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
        if (isChasing && collision.gameObject == player)
        {

            speed = 0;

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
    


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}

