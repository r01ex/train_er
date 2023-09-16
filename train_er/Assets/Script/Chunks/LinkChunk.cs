using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkChunk : MonoBehaviour
{
    [SerializeField] SpriteRenderer leftlow;
    [SerializeField] SpriteRenderer lefttop;
    [SerializeField] SpriteRenderer rightlow;
    [SerializeField] SpriteRenderer righttop;
    [SerializeField] SpriteRenderer qtedoorframe;
    [SerializeField] SpriteRenderer qtedoorframe_f;
    [SerializeField] SpriteRenderer qtedoorframe_i;
    [SerializeField] Animator animator;
    public void Init(int lowlayer, int toplayer, float speed)
    {
        leftlow.sortingOrder = lowlayer+1;
        rightlow.sortingOrder = lowlayer+1;
        lefttop.sortingOrder = toplayer;
        righttop.sortingOrder = toplayer;
        qtedoorframe.sortingOrder = lowlayer;
        qtedoorframe_f.sortingOrder = lowlayer;
        qtedoorframe_i.sortingOrder = lowlayer;
        animator.speed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void callspawn()
    {
        ObstacleSpawner.Instance.spawn();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void destroythis()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.gameObject.transform.position.x != 0)
            {
                PlayerScript.Instance.changePlayerHealth(-(5 + 5 * PlayerScript.Instance.playerLevel));
            }
            DoorQTE.Instance.transform.GetChild(0).gameObject.SetActive(true);
            DoorQTE.Instance.startQTE();
        }
    }
}
