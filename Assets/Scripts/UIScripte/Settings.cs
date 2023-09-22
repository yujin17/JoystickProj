using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingGams;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickExit()
    {
        settingGams.SetActive(false);
    }

    public void ChangeMusicVolume(float volume)
    {
        //audioSource.volume = volume;
        BGMManager bgmManager = BGMManager.instance.GetComponent<BGMManager>();
        bgmManager.SetMusicVolume(volume);
    }
}
