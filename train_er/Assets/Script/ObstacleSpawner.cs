using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float interval;
    int[] randoms = { 1, 2, 3, 4, 5 };
    List<GameObject> spawnedObjs = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(spawn());
    }
    IEnumerator spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);
            int rand = Random.Range(1, 3);
            shuffle();
            for (int i = 0; i < rand; i++)
            {
                GameObject g = Instantiate(obstaclePrefab);
                spawnedObjs.Add(g);
                g.GetComponent<Obstacle>().Init(randoms[i], interval);
            }
        }
    }
    void shuffle()
    {
        for(int i=0;i<5;i++)
        {
            int rand = Random.Range(0, 4);
            int t = randoms[i];
            randoms[i] = randoms[rand];
            randoms[rand] = t;
        }
    }
}
