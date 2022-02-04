using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    Vector2 swipeStart;
    Vector2 swipeEnd;
    float minimumDistance = 20;

    public static event System.Action<SwipeDirection> OnSwipe = delegate { };

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEnd = touch.position;
                ProcessSwipe();
            }
        }

        //mouse touch simulation
        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeEnd = Input.mousePosition;
            ProcessSwipe();
        }
    }

    void ProcessSwipe()
    {
        float distance = Vector2.Distance(swipeStart, swipeEnd);
        if (distance > minimumDistance)
        {
            if (IsVerticalSwipe())
            {
                if (swipeEnd.y > swipeStart.y)
                {
                    //up
                    OnSwipe(SwipeDirection.Up);
                    PlayerMovement.instance.JumpAction();
                    //Debug.Log("Going Up? ============================================================================");
                    PlayerMovement.instance.JumpAction();
                }
                else
                {
                    //down
                    OnSwipe(SwipeDirection.Down);
                    //Debug.Log("Going Down? ============================================================================");
                }
            }
            else
            {
                if (swipeEnd.x > swipeStart.x)
                {
                    //right
                    OnSwipe(SwipeDirection.Right);
                    //Debug.Log("Going Right? ============================================================================");
                    PlayerMovement.instance.MoveRight();
                }
                else
                {
                    //left
                    OnSwipe(SwipeDirection.Left);
                    //Debug.Log("Going Left? ============================================================================");
                    PlayerMovement.instance.MoveLeft();
                }
            }
        }
    }

    bool IsVerticalSwipe()
    {
        float vertical = Mathf.Abs(swipeEnd.y - swipeStart.y);
        float horizontal = Mathf.Abs(swipeEnd.x - swipeStart.x);
        if (vertical > horizontal)
            return true;
        return false;
    }

}
