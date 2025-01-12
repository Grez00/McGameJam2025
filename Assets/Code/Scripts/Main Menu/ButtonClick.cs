using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string _newGameLevel;

    public void NewGame ()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
