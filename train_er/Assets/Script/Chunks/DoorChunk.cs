using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChunk : MonoBehaviour
{
    [SerializeField] SpriteRenderer leftdoor;
    [SerializeField] SpriteRenderer rightdoor;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(int layer, float speed)
    {
        leftdoor.sortingOrder = layer;
        rightdoor.sortingOrder = layer;
        animator.speed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void callspawn()
    {
        ObstacleSpawner.Instance.spawn();
    }
    public void destroythis()
    {
        Destroy(this.gameObject);
    }
}
