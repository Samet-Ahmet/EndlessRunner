using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    public AudioSource menu;
    public GameObject onOff;
    private bool isMuted = false;

    void Start()
    {
        isMuted = menu.mute = PlayerPrefs.GetInt("isMuted") == 1 ? true : false;
        onOff.SetActive(isMuted);
    }

    public void Toggle()
    {
        isMuted = !isMuted;
        onOff.SetActive(isMuted);
        menu.mute = !menu.mute;
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }
}