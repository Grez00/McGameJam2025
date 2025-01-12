using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    private static readonly string AltMusicPref = "AltMusicPref";
    public string _newGameLevel;

    private int altMusicBool;
    public AudioSource musicReg;
    public AudioSource musicAlt;
    
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
        if (altMusicBool == 0)
        {
            altMusicBool = 1;
        }
        else if (altMusicBool == 1)
        {
            altMusicBool = 0;
        }

        PlayerPrefs.SetInt(AltMusicPref, altMusicBool);

        if (altMusicBool == 1)
        {   
            musicReg.enabled = false;
            musicAlt.enabled = true;
        }
        else if (altMusicBool == 0)
        {
            musicAlt.enabled = false;
            musicReg.enabled = true;
        }
    }
}
