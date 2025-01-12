using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string _newGameLevel;
    public static int song = 0;
    public AudioSource audioss;

    public void Start()
    {
        audioss1 = audioss;
        songA1 = songA;
        songB1 = songB;
    }
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
        if (song == 0) {
            song = 1;
        }
        else {
            song = 0;
        }
    }
}
