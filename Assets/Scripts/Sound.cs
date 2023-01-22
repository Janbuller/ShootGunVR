using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

	public string Name;

	public AudioClip Clip;

	[Range(0f, 1f)]
	public float Volume = 1.0f;

	public AudioSource Source;

	public void Play() {
		Source.volume = Volume;
		Source.Play();
	}
}
