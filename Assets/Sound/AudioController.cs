using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource,soundSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfxSound, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Not Found");
        }

        else
        {
            soundSource.PlayOneShot(s.clip);
        }
    }
}
