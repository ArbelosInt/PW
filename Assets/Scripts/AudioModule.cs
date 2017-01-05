using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioModule : MonoBehaviour {

    private AudioSource audioSource;
    public List<SoundClip> clips;

    void Start()
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
                audioSource.Play();
            }
        }
    }
}
