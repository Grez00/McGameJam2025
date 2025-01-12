using UnityEngine;

public class FullscreenController : MonoBehaviour
{
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ResetFullscreen()
    {
        SetFullscreen(true);
    }
}
