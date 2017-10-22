using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void Dormir()
    {

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
		    Application.Quit() ;

        #endif
    }
    public void Recommencer()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}

   
