using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    int itemtype = 0;
    [SerializeField] List<Sprite> itemspriteList;
    [SerializeField] List<Sprite> itemcenterGUIList;
    int successCnt = 0;
    int[] itemChances = { 40, 60, 80, 90, 100 };
    public void Init(int layer, Vector3 v)
    {
        this.transform.localPosition = v;
        if(v.x<0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
        sprite.sortingOrder = layer+1;
        int rand = Random.Range(0, 100);
        for (int i = 0; i < 5; i++)
        {
            if (itemChances[i] > rand)
            {
                break;
            }
            itemtype++;
        }
        sprite.sprite = itemspriteList[itemtype];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (itemtype)
            {
                case 0:
                    CircleQTE.Instance.startQTE(40, 1);
                    CircleQTE.Instance.onQTESuccess.AddListener(delegate { successCnt++; });
                    CircleQTE.Instance.onQTEend.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(5 + 5 * (successCnt)); });
                    break;
                case 1:
                    CircleQTE.Instance.startQTE(40, 1);
                    CircleQTE.Instance.onQTESuccess.AddListener(delegate { successCnt++; });
                    CircleQTE.Instance.onQTEend.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(10 + 10 * (successCnt)); });
                    break;
                case 2:
                    CircleQTE.Instance.startQTE(40, 1);
                    CircleQTE.Instance.onQTESuccess.AddListener(delegate { successCnt++; });
                    CircleQTE.Instance.onQTEend.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(10 + 10 * (successCnt)); });
                    break;
                case 3:
                    CircleQTE.Instance.startQTE(40, 1);
                    CircleQTE.Instance.onQTESuccess.AddListener(delegate { successCnt++; });
                    CircleQTE.Instance.onQTEend.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(10 + 10 * (successCnt)); });
                    break;
                case 4:
                    CircleQTE.Instance.startQTE(40, 1);
                    CircleQTE.Instance.onQTESuccess.AddListener(delegate { successCnt++; });
                    CircleQTE.Instance.onQTEend.AddListener(delegate { PlayerScript.Instance.changePlayerHealth(10 + 10 * (successCnt)); });
                    break;
                default:
                    break;
            }
            CircleQTE.Instance.setCenterSprite(itemcenterGUIList[itemtype]);
        }
    }
}
