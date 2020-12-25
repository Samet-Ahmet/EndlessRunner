using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public static bool tap;
    public static bool swipeLeft;
    public static bool swipeRight;
    public static bool swipeUp;
    public static bool swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch; // Kaydırmanın başlangıç noktası
    private Vector2 swipeDelta; // Kaydırma vektörü

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            // Dokunma başlangıç evresinde mi?
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = isDraging = true;
                startTouch = Input.touches[0].position;
            }
            // Dokunma bitiş evresinde mi?
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // Kaydırma mesafesini hesapla
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        // Ölü alanı geçti mi?
        if (swipeDelta.magnitude > 125)
        {
            // Hangi yön?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Sol ya da sağ
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                // Yukarı ya da aşağı
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            Reset();
        }
    }

    // Kaydırmayı sıfırla
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}