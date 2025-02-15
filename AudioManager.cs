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

    public void StopSFX()
    {
        GameObject[] sfxObjects = GameObject.FindGameObjectsWithTag("SFX");
        foreach (GameObject obj in sfxObjects)
        {
            AudioSource sfxSource = obj.GetComponent<AudioSource>();
            if (sfxSource != null)
            {
                sfxSource.loop = false;
                sfxSource.Stop();
            }
        }
    }



}
