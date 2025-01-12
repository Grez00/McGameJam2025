using UnityEngine;
public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip gameBackground;
    private void Start() {
        musicSource.clip = gameBackground;
        musicSource.Play();
    }
}