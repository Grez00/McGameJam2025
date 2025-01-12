using UnityEngine;
public class AudioManagerScript : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectspref";
    private float backgroundFloat, soundEffectsFloat;
    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectsAudio;
    private static readonly string AltMusicPref = "AltMusicPref";
    private int altMusicBool;
    public AudioSource musicReg;
    public AudioSource musicAlt;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].volume = backgroundFloat;
        }

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsFloat;
        }

        altMusicBool = PlayerPrefs.GetInt(AltMusicPref);

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