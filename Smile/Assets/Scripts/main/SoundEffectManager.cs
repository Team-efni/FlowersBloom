using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField]
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat("EffectSound", Mathf.Log10(value) * 20);
        if(value < 0.01f) mixer.SetFloat("EffectSound", -80f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
