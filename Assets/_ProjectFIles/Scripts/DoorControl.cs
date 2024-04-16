using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class DoorControl : MonoBehaviour
{

    public GameObject Door;
    public GameObject Door2;

    public bool doorIsOpening;


    void Update()
    {
       
        if (doorIsOpening == true)
        {
            Door.transform.Translate(Vector3.forward * Time.deltaTime * 5);
            //if the bool is true open the door

            Door2.transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (Door.transform.rotation.z > 2f)
        {
            doorIsOpening = false;

        }

        if (Door2.transform.rotation.z > 1.5f)
        {
            doorIsOpening = false;
        }

    }

    
    void OnMouseDown()
    { //This function will detect the mouse click on a collider

        //if we click on the button door must start to open
        doorIsOpening = true;

    }

}
