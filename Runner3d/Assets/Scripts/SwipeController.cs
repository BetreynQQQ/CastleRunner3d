using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, doubleTap;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private float lastCklickTime = 0.0f;
    private readonly float doubleTapDelay = 0.2f;

    private void Update()
    {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = doubleTap = false;       
        #region ��-������
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
            DoubleClick();         
           
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;          
            Reset();
        }       
        #endregion
       

        #region ��������� ������
        if (Input.touches.Length > 0)
        {          
            if (Input.touches[0].phase == TouchPhase.Began)
            {              
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
                DoubleClick();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;               
                Reset();
            }
        }
        #endregion

        //���������� ���������
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //�������� �� ������������ ����������
        if (swipeDelta.magnitude > 50)
        {
            //����������� �����������
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {

                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            Reset();
        }      
    }

    private void DoubleClick()
    {
        //������� �������
        float timeFromLastClick = Time.time - lastCklickTime;
        lastCklickTime = Time.time;
        if (timeFromLastClick < doubleTapDelay)
        {
            doubleTap = true;
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
