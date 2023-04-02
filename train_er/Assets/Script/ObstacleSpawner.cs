using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> obstaclePrefabs;
    [SerializeField] float interval;
    int[] randoms = { 0, 1, 2, 3, 4 };
    int[,] difficulties = { { 30, 60, 80, 95, 100 }, { 25, 50, 70, 90, 100 }, { 20, 40, 60, 85, 100 } };
    List<GameObject> spawnedObjs = new List<GameObject>();
    [SerializeField] List<Sprite> difficultySprites;
    private void Start()
    {
        StartCoroutine(spawn());
        StartCoroutine(difficultyIncrease());
    }
    IEnumerator difficultyIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            for (int i=0;i<3;i++)
            {
                difficulties[i, 0]--;
                difficulties[i, 1]-=2;
                difficulties[i, 2]-=2;
                difficulties[i, 3]--;
            }
        }
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
                int randDiff = Random.Range(1, 100);
                int diff = 0;
                for(int j=0;j<5;j++)
                {
                    if(difficulties[PlayerScript.Instance.playerLevel-1,j]>=randDiff)
                    {
                        diff = j + 1;
                        break;
                    }
                }
                
                GameObject g = Instantiate(obstaclePrefabs[randoms[i]]);
                g.GetComponent<SpriteRenderer>().sprite = difficultySprites[diff - 1];
                spawnedObjs.Add(g);
                g.GetComponent<Obstacle>().Init(1, randoms[i], diff);
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
