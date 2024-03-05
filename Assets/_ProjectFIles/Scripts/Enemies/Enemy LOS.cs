using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLOS : MonoBehaviour
{
    public float distance;
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);


        }
        else 
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        
        
        }
    }
}
