using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject Spawning = GameObject.FindWithTag("Spawner");
        GameObject Spawning = GameObject.Find("Floor");
        if (Spawning == null)
        {
            Debug.Log("No Spawner");
        }
        else
        {
            Spawner SpawnPoint = Spawning.GetComponent<Spawner>();
            transform.position = new Vector2(SpawnPoint.X, SpawnPoint.Y);
            Debug.Log("G");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
