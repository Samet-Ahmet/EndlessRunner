using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    public AudioSource menu; // Menü arka plan sesi 
    public GameObject onOff; // Ses açıp kapatma göstergesi
    private bool isMuted = false;

    void Start()
    {
        // GetBool fonksiyonu olmadığı için int tipi kullanıldı (1: true 0: false)
        isMuted = menu.mute = PlayerPrefs.GetInt("isMuted") == 1 ? true : false;
        onOff.SetActive(isMuted);
    }

    // Sesi açıp kapatan fonksiyon
    public void Toggle()
    {
        isMuted = !isMuted;
        onOff.SetActive(isMuted);
        menu.mute = !menu.mute;
        // Ses tercihi hafızaya kaydedildi
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }
}