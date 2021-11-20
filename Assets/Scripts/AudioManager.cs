using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool muted;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();

            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
        muted = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("Background Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }

        if (!s.Source.isPlaying)
            s.Source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }

        if (s.Source.isPlaying)
            s.Source.Stop();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return false;
        }

        return s.Source.isPlaying;
    }

    public void MuteAudio()
    {
        foreach (Sound s in sounds)
        {
            s.Source.mute = true;
        }
    }


    public void UnmuteAudio()
    {
        foreach (Sound s in sounds)
        {
            s.Source.mute = false;
        }
    }
}
