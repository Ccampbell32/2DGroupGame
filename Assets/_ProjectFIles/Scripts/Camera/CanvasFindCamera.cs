using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class CanvasFindCamera : MonoBehaviour
{
    public Canvas canvas;
    public Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        //Camera = GameObject.FindWithTag("MainCamera");
        //canvas.worldCamera = Camera.Main;
        canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
