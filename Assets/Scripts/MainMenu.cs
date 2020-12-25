using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        // Hafızadan en yüksek skoru al
        highScoreText.text = "Highscore: " + ((int)PlayerPrefs.GetFloat("HighScore")).ToString();
    }

    // Oyun sahnesini yükle
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
}