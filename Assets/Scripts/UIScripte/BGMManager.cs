using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public AudioClip bgmsource;

    private AudioSource audioSource;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public void Awake()
    {

        //musicsource = backMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (instance != null) //배경음악이 재생되고 있다면 패스
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }

    IEnumerator FadeOutCoroutine()
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            audioSource.volume = i;
            yield return waitTime;
        }
    }
    IEnumerator FadeInCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            audioSource.volume = i;
            yield return waitTime;
        }
    }
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine());
    }
    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine());
    }
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;

    }
}
