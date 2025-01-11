using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
