using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void resetScene()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
       //gameManager.ChangeGameState(GameState.Overworld);
        gameManager.resetScene();
    }

}
