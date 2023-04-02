using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float speedMult;
    int startPos;
    int difficulty;
    public void Init(float speedmult, int startpos, int diff)
    {
        speedMult = speedmult;
        startPos = startpos;
        difficulty = diff;
    }
    public void disableObs() //for items
    {
        this.gameObject.GetComponent<Obstacle>().enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Animator>().speed = speedMult;
    }
    public void OnAnimationEnd()
    {
        if(PlayerControl.Instance.currentPos==startPos)
        {
            Debug.Log("contact!");
            //Ãæµ¹
            switch(difficulty - PlayerScript.Instance.playerLevel)
            {
                case 4:
                    PlayerScript.Instance.changePlayerHealth(-80);
                    break;
                case 3:
                    PlayerScript.Instance.changePlayerHealth(-30);
                    break;
                case 2:
                    //qte
                    CircleQTE.Instance.startQTE(40, 1);
                    CircleQTE.Instance.onQTEFail.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(-10); });
                    CircleQTE.Instance.onQTESuccess.AddListener(kick);
                    break;
                case 1:
                    //qte
                    CircleQTE.Instance.startQTE(40, 1.5f);
                    CircleQTE.Instance.onQTEFail.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(-5); });
                    CircleQTE.Instance.onQTESuccess.AddListener(kick);
                    break;
                default:
                    kick();
                    break;
            }
           
        }
        Destroy(this.gameObject); //destroy animation
    }
    void kick()
    {
        Debug.Log("kick!");
        if (difficulty - PlayerScript.Instance.playerLevel > 0)
        {
            PlayerScript.Instance.addPlayerLevel(1.5f);
        }
        else if(difficulty - PlayerScript.Instance.playerLevel < 0)
        {
            PlayerScript.Instance.addPlayerLevel(1);
        }
        else
        {
            PlayerScript.Instance.addPlayerLevel(0.5f);
        }
    }
}
