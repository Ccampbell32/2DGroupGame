using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class EnemyMove : MonoBehaviour
{
    public float speed;
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;


    //detection variables
    public PolygonCollider2D detector;
    public Transform playerTransform;
    public bool isChasing;
    Transform target;
    Vector2 moveDirection;
    public GameObject detectionLight;
    public GameObject battleCanvas;

    void Start()
    {
        battleCanvas.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        animator.SetBool("IsMoving (LeftRight)", true);
        detector = GetComponent<PolygonCollider2D>();

        target = GameObject.FindWithTag("Player").transform;

    }

    void Update()
    {


        //chase
        if (isChasing)
        {
            detectionLight.SetActive(false);
            Component.Destroy(detector);

             Vector3 direction = (target.position - transform.position).normalized;
             moveDirection = direction;
            
            

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
        if (isChasing) 
        { 
          rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        
        
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = true;

        }
        
        /*if (isChasing == true && collision.CompareTag("Player")) 
        {
            battleCanvas.gameObject.SetActive(true);


        }*/
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }




}

