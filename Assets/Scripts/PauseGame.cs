using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject scoreContainer;
    public GameObject pauseButton;
    public GameObject muteButton;
    public AudioSource bg; // Arka plan sesi
    public Text scoreText;
    private Score scoreScript;
    private GameObject player;

    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.Find("Player");
        scoreScript = player.GetComponent<Score>();
    }

    // Oyunu durduran fonksiyon
    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        muteButton.SetActive(false);
        scoreContainer.SetActive(false);
        Time.timeScale = 0; // Oyunun zamanını durdur
        scoreText.text = ((int)scoreScript.score).ToString();
        bg.Pause();
    }

    // Oyunu devam ettiren fonksiyon
    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        muteButton.SetActive(true);
        scoreContainer.SetActive(true);
        Time.timeScale = 1; // Oyunun zamanını devam ettir
        bg.Play();
    }
}