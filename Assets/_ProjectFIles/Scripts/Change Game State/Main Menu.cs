using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DONT NEED TO DO THIS!! - Check the script in Scripts/Game Manager - InitialiseScene.cs
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject GameManage = GameObject.FindWithTag("GameManager");
        GameManage.GetComponent<GameManager>().ChangeGameState(GameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
