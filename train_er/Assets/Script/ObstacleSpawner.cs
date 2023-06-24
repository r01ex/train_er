using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    int[] randoms = { 0, 1, 2, 3, 4 };
    int[,] difficulties = { { 50, 70, 85, 95, 100 }, { 15, 65, 85, 95, 100 }, { 10, 20, 70, 85, 100 } };
    int[] choose_row_randpool = { 20, 70, 100, 100, 100 };
    int[] lanelock = { 0, 0, 0, 0, 0 };
    
    [SerializeField] List<Sprite> bodySprites;
    [SerializeField] List<Sprite> handSprites;
    [SerializeField] int nofSideObstacle;
    [SerializeField] int nofMidObstacle;
    [SerializeField] GameObject chunkPrefab;

    int baselayer = int.MaxValue-100;
    public static ObstacleSpawner Instance;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        spawn();
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
    public void spawn()
    {

        GameObject chunk = Instantiate(chunkPrefab);
        chunk.GetComponent<Chunk>().Init(baselayer, baselayer + 40);
        int randforRow = UnityEngine.Random.Range(0, 100);
        int NofRow = 1;
        for (int i = 0; i < 5; i++)
        {
            if (choose_row_randpool[i] > randforRow)
            {
                break;
            }
            NofRow++;
        }
        Debug.Log("row number: " + NofRow);
        shuffle();
        Array.Sort(randoms, 0, NofRow);
        int[] selectedRows = new int[NofRow];
        Array.Copy(randoms, selectedRows, NofRow);
        foreach (int e in selectedRows)
        {
            Debug.Log("selected row number: " + e);
        }
        Array.Reverse(selectedRows);
        int currentrow = 0;
        for (int i = 4; i >= 0; i--) //row
        {
            if (currentrow < NofRow && i == selectedRows[currentrow])
            {
                Debug.Log("now spawing row : " + i);
                int rand = UnityEngine.Random.Range(3, 6);
                shuffle();
                for (int j = 0; j < rand; j++) //col
                {
                    Debug.Log("now spawing col : " + randoms[j]);
                    if (lanelock[randoms[j]] > 0)
                    {
                        Debug.Log("lane locked");
                    }
                    else
                    {
                        bool lanelockflag = true;
                        int randsprite;
                        if (randoms[j] == 0 || randoms[j] == 4)
                        {
                            randsprite = UnityEngine.Random.Range(0, nofSideObstacle);
                            if (randsprite == 1) //size2
                            {
                                if(i==0)
                                {
                                    //cannot go in here
                                    lanelockflag = false;
                                }
                                lanelock[randoms[j]] = 2;
                            }
                        }
                        else
                        {
                            randsprite = UnityEngine.Random.Range(0, nofMidObstacle);
                            if(randsprite==1) //size3
                            {
                                if (i <= 1)
                                {
                                    //cannot go in here
                                    lanelockflag = false;
                                }
                                lanelock[randoms[j]] = 3;
                            }
                        }
                        if (lanelockflag == true)
                        {
                            Vector3 desiredPosition = chunk.GetComponent<Chunk>().getpos(i, randoms[j]);
                            GameObject g = Instantiate(obstaclePrefab, chunk.transform, false);
                            g.name = "row " + i + " col " + randoms[j];
                            Transform obstacleTransform = chunk.GetComponent<Chunk>().GetTransform(randoms[j]);
                            obstacleTransform.localPosition = desiredPosition;
                            int randDiff = UnityEngine.Random.Range(1, 100);
                            int diff = 0;
                            for (int k = 0; k < 5; k++)
                            {
                                if (difficulties[PlayerScript.Instance.playerLevel - 1, k] >= randDiff)
                                {
                                    diff = k + 1;
                                    break;
                                }
                            }
                            int layer = baselayer + i;
                            if (randoms[j] == 0 || randoms[j] == 4)
                            {
                                g.GetComponent<Obstacle>().Init(diff, obstacleTransform, layer, bodySprites[randsprite], handSprites[randsprite], true);
                            }
                            else
                            {
                                g.GetComponent<Obstacle>().Init(diff, obstacleTransform, layer, bodySprites[randsprite + nofSideObstacle], handSprites[randsprite + nofSideObstacle], false);
                            }
                        }
                    }

                }
                currentrow++;
            }
            for (int lockindex = 0; lockindex < 5; lockindex++)
            {
                if (lanelock[lockindex] > 0)
                {
                    lanelock[lockindex]--;
                }
            }
        }
        baselayer -= 50;
    }
    void shuffle()
    {
        for(int i=0;i<5;i++)
        {
            int rand = UnityEngine.Random.Range(0, 4);
            int t = randoms[i];
            randoms[i] = randoms[rand];
            randoms[rand] = t;
        }
    }
}
