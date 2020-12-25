using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject onOff; // Ses açıp kapatma göstergesi
    public AudioSource bg; // Arka plan sesi
    private GameObject player; // Tilki
    private AudioSource congrats; // Tebrikler sesi
    private AudioSource gameover; // Oyun sona erdi sesi
    private bool isMuted = false;

    void Start()
    {
        player = GameObject.Find("Player");
        congrats = player.GetComponents<AudioSource>()[0];
        gameover = player.GetComponents<AudioSource>()[1];
        // GetBool fonksiyonu olmadığı için int tipi kullanıldı (1: true 0: false)
        isMuted = bg.mute = congrats.mute = gameover.mute = PlayerPrefs.GetInt("isMuted") == 1 ? true : false;
        onOff.SetActive(isMuted);
    }

    // Sesi açıp kapatan fonksiyon
    public void Toggle()
    {
        bg.mute = !bg.mute;
        congrats.mute = !congrats.mute;
        gameover.mute = !gameover.mute;
        isMuted = !isMuted;
        onOff.SetActive(isMuted);
        // Ses tercihi hafızaya kaydedildi
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }

}