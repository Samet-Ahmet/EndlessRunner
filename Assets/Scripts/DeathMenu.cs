using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Text scoreText;
    public Image backgroundImg;
    public GameObject scoreContainer;
    public GameObject pauseButton;
    public GameObject muteButton;
    private bool isShowned = false; // Ölüm ekranı aktif mi?
    private float transition = 0.0f;

    void Start()
    {
        gameObject.SetActive(false); // Menü başlangıçta gözükmesin
    }

    void Update()
    {
        // Menü gözükmüyorsa hiçbir şey yapma
        if (!isShowned)
            return;

        // Ekran geçiş animasyonu
        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.5f), transition);
    }

    // Ölüm menüsünü aktif hale getiren fonksiyon
    public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        pauseButton.SetActive(false);
        muteButton.SetActive(false);
        scoreContainer.SetActive(false);
        scoreText.text = ((int)score).ToString();
        isShowned = true;
    }

    // Sahneyi yeniden yükle
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Ana menüye dön
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}