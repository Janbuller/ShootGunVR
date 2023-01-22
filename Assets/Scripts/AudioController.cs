using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public Sound[] Sounds;

    public void Awake() {
		foreach (Sound sound in Sounds)
		{
			sound.source = gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.Clip;
		}
    }

	public void Play(string SoundName) {
		foreach (Sound sound in Sounds)
		{
			if(sound.Name == SoundName) {
				sound.Play();
				return;
			}
		}
	}
}
