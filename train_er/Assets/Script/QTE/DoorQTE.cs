using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorQTE : MonoBehaviour
{
    public static DoorQTE Instance;
    [SerializeField] Animator anim;
    [SerializeField] Sprite[] doorSprites;
    int correctSwipe;
    [SerializeField] RawImage doorimage;
    int doornum = 0;
    int failcount = 0;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void startQTE()
    {
        PlayerControl.Instance.isQTE = true;
        Debug.Log("in door qte");
        Time.timeScale = 0;
        SpawnDoor();
    }
    void SpawnDoor()
    {
        int r = Random.Range(0, 4);
        doorimage.texture = doorSprites[r].texture;
        switch (r)
        {
            case 0:
                anim.SetTrigger("up");
                correctSwipe = 1;
                break;
            case 1:
                anim.SetTrigger("down");
                correctSwipe = 0;
                break;
            case 2:
                anim.SetTrigger("left");
                correctSwipe = 3;
                break;
            case 3:
                anim.SetTrigger("right");
                correctSwipe = 2;
                break;
        }
    }
    public void startTakingInput()
    {
        Debug.Log("slide now");
        StartCoroutine(slide());
    }
    IEnumerator slide()
    {
        Vector3 fp = Vector3.zero;   //First touch position
        Vector3 lp;   //Last touch position
        float dragDistance = Screen.height * 15 / 100;  //minimum distance for a swipe to be registered
        while (true)
        {
            Debug.Log("wating slide");
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
                            if (lp.x > fp.x)
                            {
                                Debug.Log("Right Swipe");
                                checkSlide(3);
                                break;
                            }
                            else
                            {
                                checkSlide(2);
                                Debug.Log("Left Swipe");
                                break;
                            }
                        }
                        else
                        {
                            if (lp.y > fp.y)
                            {
                                checkSlide(0);
                                Debug.Log("Up Swipe");
                                break;
                            }
                            else
                            {
                                checkSlide(1);
                                Debug.Log("Down Swipe");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Tap");
                    }
                }
            }
            yield return null;
        }
    }
    void checkSlide(int input)
    {
        doornum++;
        if (correctSwipe==input)
        {

        }
        else
        {
            switch(failcount)
            {
                case 0:
                    PlayerScript.Instance.changePlayerHealth(-10);
                    break;
                case 1:
                    PlayerScript.Instance.changePlayerHealth(-20);
                    break;
                case 2:
                    PlayerScript.Instance.changePlayerHealth(-50);
                    break;
                case 3:
                    PlayerScript.Instance.changePlayerHealth(-80);
                    break;
            }
            failcount++;
        }
        if (doornum == 4)
        {
            Time.timeScale = 1;
            this.transform.GetChild(0).gameObject.SetActive(false);
            if(failcount==0)
            {
                PlayerScript.Instance.changePlayerHealth(+20);
            }
            PlayerControl.Instance.isQTE = false;
            failcount = 0;
            doornum = 0;
            return;
        }
        else
        {
            SpawnDoor();
        }
    }
}
