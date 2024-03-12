using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialouge : MonoBehaviour
{
    public GameObject dialougePanel;
    public Text dialougeText;
    public string[] dialouge;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerIsClose)
        {
            if (dialougePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialougePanel.SetActive(true);
                StartCoroutine(Typing());
            }

        }

        if (dialougeText.text == dialouge[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()

    {
        dialougeText.text = "";
        index = 0;
        dialougePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialouge[index].ToCharArray())
        {
            dialougeText.text += letter;    
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if(index < dialouge.Length - 1) 
        {
            index++;
            dialougeText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            playerIsClose = true;
        }    
    }
    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
