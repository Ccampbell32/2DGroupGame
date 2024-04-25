using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    public int sceneBuildIndex;
    public Transform PlayerSpawnPoint;
    public Transform newSpawn;
    private bool IfLevelComplete;
    public PlayerMovement playerMove;
    public GameObject LevelCompleteUI;

    public void Start()
    {
        Debug.Log("Level Switch start");
        if (GameObject.FindWithTag("LevelComplete") != null)
        {
            LevelCompleteUI = GameObject.FindWithTag("LevelComplete");
            playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            LevelCompleteUI.SetActive(false);
            IfLevelComplete = true;
        }
        else
        {
            IfLevelComplete = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");

        if(collision.tag == "Player")
        {
            if (IfLevelComplete)
            {
                LevelComplete();
            }
            else
            {
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            }

           // PlayerSpawnPoint.transform.position = newSpawn.transform.position;

        }
    }

    public IEnumerator LevelComplete()
        {
            playerMove.enabled = false;
            LevelCompleteUI.SetActive(true);
            yield return new WaitForSeconds(2);
            playerMove.enabled = true;
            LevelCompleteUI.SetActive(false);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

}
