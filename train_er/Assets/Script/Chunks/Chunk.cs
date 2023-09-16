using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] SpriteRenderer leftlow;
    [SerializeField] SpriteRenderer lefttop;
    [SerializeField] SpriteRenderer rightlow;
    [SerializeField] SpriteRenderer righttop;
    [SerializeField] Animator animator;

    [SerializeField] List<Transform> topTransforms;
    [SerializeField] List<Transform> lowTransforms;
    [SerializeField] Transform itemleftlow;
    [SerializeField] Transform itemlefttop;
    [SerializeField] Transform itemrightlow;
    [SerializeField] Transform itemrighttop;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(int lowlayer, int toplayer,float speed)
    {
        leftlow.sortingOrder = lowlayer;
        rightlow.sortingOrder = lowlayer;
        lefttop.sortingOrder = toplayer;
        righttop.sortingOrder = toplayer;
        animator.speed = speed;
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
    public Vector3 getpos(int col, int row)
    {
        Vector3 vec = topTransforms[row].localPosition - lowTransforms[row].localPosition;
        return lowTransforms[row].localPosition + (4 - col) * vec.normalized * vec.magnitude * 0.2f;
    }
    public Vector3 getRandomPosforItem()
    {
        Vector3 vec = itemlefttop.localPosition - itemleftlow.localPosition;
        return itemleftlow.localPosition + Random.Range(0f, 1f) * vec.normalized * vec.magnitude;
    }
    public Transform GetTransform(int row)
    {
        return lowTransforms[row];
    }
}
