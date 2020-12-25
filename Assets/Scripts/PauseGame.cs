using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public AudioSource bg;
    public GameObject pauseMenu;
    public GameObject scoreContainer;
    public Text scoreText;
    private Score scoreScript;
    private GameObject player;
    public GameObject pauseButton;
    public GameObject muteButton;

    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.Find("Player");
        scoreScript = player.GetComponent<Score>();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        muteButton.SetActive(false);
        scoreContainer.SetActive(false);
        Time.timeScale = 0;
        scoreText.text = ((int)scoreScript.score).ToString();
        bg.Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        muteButton.SetActive(true);
        scoreContainer.SetActive(true);
        Time.timeScale = 1;
        bg.Play();
    }
}