using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public int currentPos = 2;
    public static PlayerControl Instance;
    public bool isQTE = false;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; 
    }

    void Update()
    {
        if (isQTE == false)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    lp = touch.position;

                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {
                            if ((lp.x > fp.x))
                            {
                                Debug.Log("Right Swipe");
                                try
                                {
                                    this.transform.position = PositionReference.Instance.PlayerPositonList[currentPos + 1].position;
                                    currentPos++;
                                }
                                catch
                                {
                                    Debug.Log("already rightmost");
                                }
                            }
                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                                try
                                {
                                    this.transform.position = PositionReference.Instance.PlayerPositonList[currentPos - 1].position;
                                    currentPos--;
                                }
                                catch
                                {
                                    Debug.Log("already leftmost");
                                }
                            }
                        }
                        else
                        {
                            if (lp.y > fp.y)
                            {
                                Debug.Log("Up Swipe");
                            }
                            else
                            {
                                Debug.Log("Down Swipe");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Tap");
                    }
                }
            }
        }
    }
}
