using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{

    public AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];


    public void Init()
    {

        GameObject root = GameObject.Find("Sound");

        if (root == null)
        {

            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for (int i = 0; i < soundNames.Length - 1; i++)
            {


                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;

            }

            audioSources[(int)Define.Sound.Bgm].loop = true;
        }


    }


    public void Clear()
    {

        foreach (AudioSource audioSource in audioSources)
        {

            audioSource.clip = null;
            audioSource.Stop();

        }


    }

    public void Play(Define.Sound type, AudioClip clip, float pitch)
    {

        if (type == Define.Sound.Bgm)
        {

            AudioSource audiosource = audioSources[(int)Define.Sound.Bgm];
            if (audiosource.isPlaying)
            {
                audiosource.Pause();
            }
            audiosource.pitch = pitch;
            audiosource.clip = clip;
            audiosource.Play();
        }
        else if (type == Define.Sound.D2_Effect)
        {



            AudioSource audiosource = audioSources[(int)Define.Sound.D2_Effect];
            audiosource.pitch = pitch;
            audiosource.PlayOneShot(clip);//´Ü¹ß¼º

        }

    }

   
    public void Change_Sound_Value(Define.Sound type, float volume_value)
    {

        if (type == Define.Sound.Bgm)
        {

            AudioSource audiosource = audioSources[(int)Define.Sound.Bgm];
            audiosource.volume = volume_value;

        }
        else if (type == Define.Sound.D2_Effect)
        {
            AudioSource audiosource = audioSources[(int)Define.Sound.D2_Effect];
            audiosource.volume = volume_value;

        }

    }

}
