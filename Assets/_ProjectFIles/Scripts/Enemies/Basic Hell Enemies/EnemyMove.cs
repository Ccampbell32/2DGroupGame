using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody rb = GetComponent<Rigidbody>();
        moveenemyright();

    }
    // speed in right and left direction
    void moveenemyright()
    {
      transform.Translate(speed * Time.deltaTime, 0, 0);

        transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
        animator.SetBool("IsMoving", true);
        
    }

    void moveenemyleft()
    {
        transform.Translate(-speed * -Time.deltaTime, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        animator.SetBool("IsMoving", true);
    }


}
