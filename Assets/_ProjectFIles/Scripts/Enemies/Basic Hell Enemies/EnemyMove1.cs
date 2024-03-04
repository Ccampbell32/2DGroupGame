using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyMove1 : MonoBehaviour
{
    public float speed;
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        animator.SetBool("IsMoving (Up)", true);
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(0, speed);

        }
        else
        {
            rb.velocity = new Vector2(0, -speed);
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

    IEnumerator WaitAndFlip()
    {
        speed = 0; // Stop the object
        if (animator.GetBool("IsMoving (Up)") == true)
        {
            animator.SetBool("IsMoving (Up)", false);
            yield return new WaitForSeconds(4f); // Wait for 4 seconds

        }
        animator.SetBool("IsMoving (Up)", false); // Set animation to idle
        if (animator.GetBool("IsMoving (Down)") == true)
        {
            animator.SetBool("IsMoving (Down)", false);
            yield return new WaitForSeconds(4f); // Wait for 4 seconds

        }

        speed = 5; // Move again
        
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}

