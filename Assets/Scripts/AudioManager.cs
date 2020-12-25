using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameObject player;
    public GameObject onOff;
    public AudioSource bg;
    private AudioSource congrats;
    private AudioSource gameover;
    private bool isMuted = false;
    // public AudioSource menu;

    void Start()
    {
         player = GameObject.Find("Player");
         congrats = player.GetComponents<AudioSource>()[0];
         gameover = player.GetComponents<AudioSource>()[1];
        // bg.mute = congrats.mute = gameover.mute = menu.mute = PlayerPrefs.GetBool("isMuted");
        isMuted = bg.mute = congrats.mute = gameover.mute = PlayerPrefs.GetInt("isMuted") == 1 ? true : false;
        onOff.SetActive(isMuted);

    }

    public void Toggle()
    {
        bg.mute = !bg.mute;
        congrats.mute = !congrats.mute;
        gameover.mute = !gameover.mute;
        isMuted = !isMuted;
        onOff.SetActive(isMuted);
        // menu.mute = !menu.mute;
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);

    }

}