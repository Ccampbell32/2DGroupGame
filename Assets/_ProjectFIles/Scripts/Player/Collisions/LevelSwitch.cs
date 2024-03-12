using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");

        if (collision.tag == "Player");
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);


        }
    }

}
