using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;

    public void Start()
    {
        DontDestroyOnLoad(musicsource);
    }

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }

    /* 
    public GameObject BackgroundMusic;
    public void Awake()
     {
         BackgroundMusic = GameObject.Find("Sweet Dreams My Dear Instrumental");
         musicsource = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
         if (musicsource.isPlaying) return; //배경음악이 재생되고 있다면 패스
         else
         {
             musicsource.Play();
             DontDestroyOnLoad(BackgroundMusic); //배경음악 계속 재생하게(이후 버튼매니저에서 조작)

         }
     } */
}