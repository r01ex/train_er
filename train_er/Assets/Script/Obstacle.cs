using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    int currentpos = 3;
    float intervalSec = 1.0f;
    float alpha = 0.2f;
    public void Init(int pos, float interval)
    {
        this.transform.position = PositionReference.Instance.ObjPositionList[pos].position;
        currentpos = pos;
        intervalSec = interval;
    }
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = PositionReference.Instance.ObjPositionList[3].position;
        //currentpos = 3;
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalSec);
            Debug.Log(currentpos);
            try
            {
                this.transform.position = PositionReference.Instance.ObjPositionList[currentpos + 7].position;
                currentpos += 7;
                alpha += 0.2f;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, alpha);
            }
            catch
            {
                break;
            }
        }
        if (PlayerControl.Instance.currentPos + 1 == currentpos % 7)
        {
            Debug.Log("crash!");
        }
        Destroy(this.gameObject);
    }
    public void setInterval(float interval)
    {
        intervalSec = interval;
    }
}
