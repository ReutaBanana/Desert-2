using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManipulator : MonoBehaviour
{
    private AudioSource music;
    [SerializeField] GameObject SoundOn;
    [SerializeField] GameObject SoundOff;
    // Start is called before the first frame update
    private void Start()
    {
        var gameObject = GameObject.Find("Music");
        music = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
    
    }
    public void StopMusic()
    {
        music.Pause();
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
    }
    public void ResumeMusic()
    {
        music.Play();
        SoundOff.SetActive(false);
        SoundOn.SetActive(true);
    }
   
}
