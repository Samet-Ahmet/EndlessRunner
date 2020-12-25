using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt; // Oyuncunun transformu
    private Vector3 startOffset; // Kamera ile oyuncu arasındaki mesafe
    private Vector3 moveVector; // Kameranın hareket vektörü
    private Vector3 animationOffset = new Vector3(0, 5, 5); // Animasyon başlangıç noktası
    private float transition = 0.0f; // Geçiş değeri
    private float animationDuration = 3.0f; // Animasyon süresi

    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    void Update()
    {
        moveVector = lookAt.position + startOffset;
        // X düzleminde hareket yok
        moveVector.x = 0.0f;
        // Y düzlemindeki hareket alanı 4 ile 6 arası
        moveVector.y = Mathf.Clamp(moveVector.y, 4, 6);

        // Animasyon sonrasındaki hareket
        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        // Oyunun başlangıç animasyonu
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
}