using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
   
    public void OnInteract()
    {
        Input.GetKeyUp(KeyCode.E);
    }
}
