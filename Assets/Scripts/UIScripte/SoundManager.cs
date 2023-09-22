using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
     static public SoundManager instance;

    public AudioClip[] soundclips;

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public void Awake()
    {

        //musicsource = backMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        //if (instance!=null) //배경음악이 재생되고 있다면 패스
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    instance = this;
        //}
    }

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(int playMusicTrack)
    {
        source.clip = soundclips[playMusicTrack];
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
