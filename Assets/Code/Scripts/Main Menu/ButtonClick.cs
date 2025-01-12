using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string _newGameLevel;
<<<<<<< Updated upstream
    public static int song = 0;
    public AudioSource audioss;
=======

    private bool altMusicBool = false;
    public AudioSource musicReg;
    public AudioSource musicAlt;
    
>>>>>>> Stashed changes
    public void NewGame ()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ToggleMusic()
    {
        altMusicBool = !altMusicBool;

        if (altMusicBool)
        {   
            musicReg.enabled = false;
            musicAlt.enabled = true;
        }
        else
        {
            musicAlt.enabled = false;
            musicReg.enabled = true;
        }
    }
}
