using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPlayer : MonoBehaviour
{
    public CinemachineVirtualCamera Camera;
    public Transform tFollowTarget;
    
    // Start is called before the first frame update
    void Start()
    {

        GameObject Player = GameObject.FindWithTag("Player");
        tFollowTarget = Player.transform;
        Camera.Follow = tFollowTarget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
