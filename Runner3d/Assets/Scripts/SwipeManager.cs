using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    public enum Direction { Left, Right, Up, Down};

    private bool[] swipe = new bool[4];

    private Vector2 startTouch;
    private Vector2 swipeDelta;
    private bool touchMoved;
    private const float SWIPE_THRESHOLD = 50;

    private Vector2 TouchPosition() { return (Vector2)Input.mousePosition; }

    private bool TouchBegan() { return Input.GetMouseButtonDown(0); }

    private bool TouchEnded() { return Input.GetMouseButtonUp(0); }

    private bool GetTouch() { return Input.GetMouseButton(0); }

    private void Awake() { instance = this; }

    private float lastCklickTime = 0.0f;   
    private readonly float doubleTapDelay = 0.3f;

    public delegate void MoveDelegate(bool[] swipes);
    public MoveDelegate MoveEvent;

    public delegate void ClickDelegate(Vector2 pos);
    public ClickDelegate ClickEvent;

    public delegate void DoubleClickDelegate(Vector2 pos);
    public DoubleClickDelegate DoubleClickEvent;

    // Update is called once per frame
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (TouchBegan())
        {
            startTouch = TouchPosition();
            touchMoved = true;
        }
        else if(TouchEnded() && touchMoved == true)
        {
            SendSwipe();
            touchMoved = false;
        }

        swipeDelta = Vector2.zero;
        if(touchMoved && GetTouch())
        {
            swipeDelta = TouchPosition() - startTouch;
        }

        if (swipeDelta.magnitude > SWIPE_THRESHOLD) 
        { 
            if(Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                swipe[(int)Direction.Left] = swipeDelta.x > 0;
                swipe[(int)Direction.Right] = swipeDelta.x < 0;
            }
            else
            {
                swipe[(int)Direction.Up] = swipeDelta.y > 0;
                swipe[(int)Direction.Down] = swipeDelta.y < 0;
            }
            SendSwipe();
        }

    }

    private void SendSwipe()
    {
        if(swipe[0] || swipe[1] || swipe[2] || swipe[3])
        {
           // Debug.Log(swipe[0] + " || " + swipe[1] + " || " + swipe[2] + " || " + swipe[3]);
            MoveEvent?.Invoke(swipe);
        }
        else
        {
            float timeFromLastClick = Time.time - lastCklickTime;
            lastCklickTime = Time.time;
            if (timeFromLastClick < doubleTapDelay)
            {

                 //Debug.Log("DoubleClick");
                DoubleClickEvent?.Invoke(TouchPosition());
            }
             //Debug.Log("Click");
            ClickEvent?.Invoke(TouchPosition());
        }
        Reset();
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        touchMoved = false;
        for (int i = 0; i < swipe.Length; i++)
        {
            swipe[i] = false;
        }
    }
}
