using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour

{
    [SerializeField] GameObject player;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp)
        {
            Vector3 offset = Vector3(0, 1, 0
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position offset,
            ref vel, smoothTime);
        }
 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !isPickedUp)
        {
            isPickedUp = true;
        }

    }

}

