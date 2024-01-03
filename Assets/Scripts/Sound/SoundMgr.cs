using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr 
{
    private AudioSource bgmSource;
    private Dictionary<string, AudioClip> clips;

    public SoundMgr()
    {
        clips = new Dictionary<string, AudioClip>();

        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
    }

    public void PlayBgm(string res)
    {
        if(clips.ContainsKey(res) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip);
        }

        bgmSource.clip = clips[res];
        bgmSource.Play();
    }
}
