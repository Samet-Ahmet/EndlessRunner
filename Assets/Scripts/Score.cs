using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 20;
    private int scoreToNextLevel = 10;
    private bool isDead = false;
    public Text scoreText;
    public DeathMenu deathMenu;
    public Text highScoreText;
    public AudioSource congrats;
    public AudioSource gameover;
    public float score = 0.0f;

    void Start()
    {
        highScoreText.text = "Nice try.\nBut you haven't reached the high score.";
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        if (score >= scoreToNextLevel)
            LevelUp();
        score += Time.deltaTime * difficultyLevel; // TODO: daha hızlı puan verme
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        if (PlayerPrefs.GetFloat("HighScore") < score)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highScoreText.text = "Congratulations!\nYou have reached the high score!";
            congrats.Play();
        }
        else
        {
            gameover.Play();
        }

        deathMenu.ToggleEndMenu(score);
    }
}