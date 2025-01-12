using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectspref";
    private static readonly string AltMusicPref = "AltMusicPref";
    private int firstPlayInt;
    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;
    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0) {
            // default volumes
            backgroundFloat = 0.5f;
            soundEffectsFloat = 0.5f;

            // match sliders
            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;


            // save to PlayerPrefs + the fact that PlayerPrefs now exist
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
            PlayerPrefs.SetInt(AltMusicPref, 0);
        }
        else {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
        }
    }

    public void SaveSoundSettings() 
    {
        // register current settings to prefs
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }

    // save prefs on alt-tab, etc.
    void OnApplicationFocus(bool focusStatus) 
    {
        if(!focusStatus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].volume = backgroundSlider.value;
        }

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }
    }

    public void ResetSound()
    {
        // default volumes
        backgroundFloat = 0.5f;
        soundEffectsFloat = 0.5f;

        // match sliders
        backgroundSlider.value = backgroundFloat;
        soundEffectsSlider.value = soundEffectsFloat;


        // save to PlayerPrefs + the fact that PlayerPrefs now exist
        PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);

        UpdateSound();
    }
}
