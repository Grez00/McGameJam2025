using UnityEngine;
using System.Collections;

public class RandomSoundScript : MonoBehaviour {
	public AudioSource[] audioSources;

    private AudioSource randomSound;

    public int timeBetweenSound;

	// Use this for initialization
	void Start () {

		CallAudio ();
	}


	void CallAudio()
	{
		Invoke ("RandomSoundness", timeBetweenSound);
	}

	void RandomSoundness()
	{
		randomSound = audioSources[Random.Range(0, audioSources.Length)];
		randomSound.Play ();
		CallAudio ();
	}
}