using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    public Animator animator;

    private float speed = 6.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 3.0f;
    private float jumpForce = 6.8f;
    private float startTime;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.0f;//The distance between tow lanes
    // private bool isSliding = false; //never used

    private bool isDead = false;
    public AudioSource bg;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
        //bg = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        verticalVelocity -= gravity * Time.deltaTime;
        if ((Swipe.swipeUp || Input.GetKeyDown(KeyCode.W)) && transform.position.y <= 0.1f)
        {
            verticalVelocity = jumpForce;
        }
        if ((Swipe.swipeDown || Input.GetKeyDown(KeyCode.S)) && transform.position.y > 0.1f)
        {
            verticalVelocity = -jumpForce;
        }
        if (Swipe.swipeRight || Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Swipe.swipeLeft || Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        //transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
        moveVector.z = speed;
        moveVector.y = verticalVelocity;
        //Move Player
        controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }
    //It is being valled every time our capsule hits something
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "Block")
            Death();
    }

    private void Death()
    {
        isDead = true;
        bg.Stop();
        GetComponent<Score>().OnDeath();
        animator.SetBool("IsDead", true);
    }
}