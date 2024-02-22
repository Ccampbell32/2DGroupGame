using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private int speed = 5;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public PlayerInput playerinput;
    private void Start()
    {
        
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }
    // players movement and animations
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);

        }
        else
        {
            animator.SetBool("IsWalking", false);

        }


    }
    



    private void FixedUpdate()
    {
        
        if (movement.x != 0 || movement.y != 0)
        {
            rb.velocity = movement * speed;
        }


    }
    /*private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("Interact", true);


        }
        else
        {
            animator.SetBool("Interact", false);

        }



    }
    */
}
