using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public AudioSource bg;
    public Animator animator;
    private CharacterController controller;
    private Vector3 moveVector; // Oyuncunun hareket vektörü
    private int desiredLane = 1; // 0:sol, 1:orta, 2:sağ
    private bool isDead = false;
    private float speed = 6.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 3.0f;
    private float jumpForce = 6.8f;
    private float laneDistance = 2.0f; // Şeritler arasındaki mesafe
    private float startTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time; // Sahnenin başlangıç zamanı alındı
    }

    void Update()
    {
        // Karakter öldüyse hareket edemesin
        if (isDead)
            return;
        // Animasyon süresi boyunca kontroller devre dışı olsun
        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero; // Hareket vektörü sıfırlandı

        verticalVelocity -= gravity * Time.deltaTime; // Yer çekimi etkisi oluşturuldu
        if ((Swipe.swipeUp || Input.GetKeyDown(KeyCode.W)) && transform.position.y <= 0.1f)
        {
            verticalVelocity = jumpForce; // Karakter yerdeyse dikey hızına zıplama kuvveti uygula
        }
        if ((Swipe.swipeDown || Input.GetKeyDown(KeyCode.S)) && transform.position.y > 0.1f)
        {
            verticalVelocity = -jumpForce; // Karakter havadaysa dikey hızına eksi zıplama kuvveti uygula
        }
        if (Swipe.swipeRight || Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            // Daha fazla sağa gitme
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Swipe.swipeLeft || Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            // Daha fazla sola gitme
            if (desiredLane == -1)
                desiredLane = 0;
        }

        // Karakterin nerede olacağını hesapla
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        // Hedef pozisyona ulaşana kadar geçiş yap
        if (transform.position != targetPosition)
        {
            Vector3 difference = targetPosition - transform.position;
            Vector3 moveDirection = difference.normalized * 25 * Time.deltaTime;

            if (moveDirection.sqrMagnitude < difference.magnitude)
                controller.Move(moveDirection);
            else
                controller.Move(difference);
        }

        moveVector.z = speed;
        moveVector.y = verticalVelocity;
        // Karakteri hareket ettir
        controller.Move(moveVector * Time.deltaTime);
    }
    // Hızı artıran fonksiyon
    public void SetSpeed(float modifier)
    {
        speed = 6.0f + modifier;
    }

    // Çarpışma kontrolünü yapan fonksiyon
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "Block")
            Death();
    }

    // Karakter ölünce gerekli işleri yapan fonksiyon
    private void Death()
    {
        isDead = true;
        bg.Stop();
        GetComponent<Score>().OnDeath();
        animator.SetBool("IsDead", true);
    }

}