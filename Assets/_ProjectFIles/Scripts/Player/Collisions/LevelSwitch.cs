using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    public int sceneBuildIndex;
    public Transform PlayerSpawnPoint;
    public Transform newSpawn;
    public bool IfLevelComplete;
    public PlayerMovement playerMove;
    public GameObject LevelCompleteUI;

    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        if (IfLevelComplete)
        {
            LevelCompleteUI = GameObject.FindWithTag("LevelComplete");
            playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            LevelCompleteUI.SetActive(false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");

        if(collision.tag == "Player")
        {
            if (IfLevelComplete == true)
            {
                StartCoroutine(LevelComplete());
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
