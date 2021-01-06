using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class MenuAudioManager : MonoBehaviour
{
    public AudioSource menu; // Menü arka plan sesi 
    public GameObject onOff; // Ses açıp kapatma göstergesi
    private bool isMuted = false;
    public Slider volumeSlider;

    void Start()
    {
        // GetBool fonksiyonu olmadığı için int tipi kullanıldı (1: true 0: false)
        isMuted = menu.mute = PlayerPrefs.GetInt("isMuted") == 1 ? true : false;
        onOff.SetActive(isMuted);
        // Başlangıçta kayıtlı ses seviyesine ayarla
        SetVolume();
        volumeSlider.value = GetVolume();
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

    // Slider değerini alan fonksiyon
    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        SetVolume();
    }

    // Kayıtlı ses seviyesini alan fonksiyon
    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("volume");
    }

    // Ses seviyesini ayarlayan fonksiyon
    public void SetVolume()
    {
        menu.volume = GetVolume();
    }
}