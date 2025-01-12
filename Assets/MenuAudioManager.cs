using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip menuBackground;
    private void Start() {
        musicSource.clip = menuBackground;
        musicSource.Play();
    }
}
