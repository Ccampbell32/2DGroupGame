using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsClick : MonoBehaviour
{
    public int sceneBuildIndex;

    void start()
    {
        


    }
        
    public void PlayButton()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);


    }
    public void QuitButton()
    {
        Application.Quit();
    }



    

       
       
            


        
    
}
