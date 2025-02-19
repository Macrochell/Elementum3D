using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Serializable]
    public class AudioClipData
    {
        public string clipName;
        public AudioClip clip;
    }

    public List<AudioClipData> audioClips;
    private Dictionary<string, AudioClip> clipDictionary;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        clipDictionary = new Dictionary<string, AudioClip>();
        foreach (var clipData in audioClips)
        {
            clipDictionary[clipData.clipName] = clipData.clip;
        }
    }


    public void PlaySFX(string clipName, bool loop = false)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            GameObject[] sfxObjects = GameObject.FindGameObjectsWithTag("SFX");
            foreach (GameObject obj in sfxObjects)
            {
                AudioSource sfxSource = obj.GetComponent<AudioSource>();
                if (sfxSource != null)
                {
                    if (loop)
                    {
                        sfxSource.clip = clip;
                        sfxSource.loop = true;
                        sfxSource.Play();
                    }
                    else
                    {
                        sfxSource.PlayOneShot(clip);
                    }
                }
            }
        }
    }

 

    public void PlayMusic(string clipName, bool loop = true)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject obj in musicObjects)
            {
                AudioSource musicSource = obj.GetComponent<AudioSource>();
                if (musicSource != null)
                {
                    musicSource.clip = clip;
                    musicSource.loop = loop;
                    musicSource.volume = musicVolume;
                    musicSource.Play();
                }
            }
        }

    }


    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        foreach (GameObject obj in musicObjects)
        {
            AudioSource musicSource = obj.GetComponent<AudioSource>();
            if (musicSource != null)
            {
                musicSource.volume = musicVolume;
            }
        }
    }


    public void SetSFXVolume(float volume)
    {

        sfxVolume = volume;
        GameObject[] sfxObjects = GameObject.FindGameObjectsWithTag("SFX");
        foreach (GameObject obj in sfxObjects)
        {
            AudioSource sfxSource = obj.GetComponent<AudioSource>();
            if (sfxSource != null)
            {
                sfxSource.volume = sfxVolume;
            }
        }
       
    }

}
