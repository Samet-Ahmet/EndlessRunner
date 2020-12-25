using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public DeathMenu deathMenu;
    public AudioSource congrats;
    public AudioSource gameover;
    public float score = 0.0f;
    private bool isDead = false;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 30;
    private int scoreToNextLevel = 10;

    void Start()
    {
        highScoreText.text = "Nice try.\nBut you haven't reached the high score.";
    }

    void Update()
    {
        if (isDead)
            return;
        if (score >= scoreToNextLevel)
            LevelUp();
        score += Time.deltaTime * difficultyLevel; // Skorun artışını zorluk seviyesine göre değiştir
        scoreText.text = ((int)score).ToString();
    }

    // Zorluk seviyesini artıran fonksiyon
    void LevelUp()
    {
        // Maksimum zorluk seviyesine ulaşıldıysa zorluk seviyesini daha fazla artırma
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel = (int)(scoreToNextLevel * 1.5f); // Sonraki zorluk seviyesine ulaşması için gereken skoru belirle
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel); // Hızı artır
    }
    // Karakter öldüğünde çalışan fonksiyon
    public void OnDeath()
    {
        isDead = true;
        // Yüksek skor geçildi mi?
        if (PlayerPrefs.GetFloat("HighScore") < score)
        {
            PlayerPrefs.SetFloat("HighScore", score); // Yeni yüksek skoru hafızaya kaydet
            highScoreText.text = "Congratulations!\nYou have reached the high score!";
            congrats.Play();
        }
        else
        {
            gameover.Play();
        }

        deathMenu.ToggleEndMenu(score); // Karakter öldükten sonra çıkan menüye skoru gönder
    }
}