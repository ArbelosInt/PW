using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioModule : MonoBehaviour {

    private AudioSource audioSource;
    public List<SoundClip> clips;

    void OnEnable()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < clips.Count; i++)
        {
            if (clips[i].name == name)
            {
                audioSource.clip = clips[i].audio;
				audioSource.loop = false;
                audioSource.Play();
            }
        }
    }

	public void PlayLoopingSound(string name) 
	{
		for (int i = 0; i < clips.Count; i ++)
		{
			if (clips[i].name == name)
			{
				audioSource.clip = clips[i].audio;
				audioSource.loop = true;
				audioSource.Play();
			}
		}
	}

	public void StopSound()
	{
		for (int i = 0; i < clips.Count; i ++) 
		{
			if (clips[i].name == name)
			{
				audioSource.Stop();
			}
		}
	}

	public bool IsPlaying()
	{
		return audioSource.isPlaying;
	}
}
