using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenController : MonoBehaviour
{
    public void ChangeFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ResetFullscreen()
    {
        Screen.fullScreen = true;

        GameObject fullscreenToggle;
        fullscreenToggle = GameObject.Find("FullscreenToggle");
        fullscreenToggle.GetComponent<Toggle>().isOn = true;
    }
}
