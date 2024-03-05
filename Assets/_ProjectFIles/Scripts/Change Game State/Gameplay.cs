using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject GameManage = GameObject.FindWithTag("GameManager");
        GameManage.GetComponent<GameManager>().ChangeGameState(GameState.Gameplay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
