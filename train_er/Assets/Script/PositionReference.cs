using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReference : MonoBehaviour
{
    public static PositionReference Instance { get; private set; }
    public List<Transform> PlayerPositonList = new List<Transform>();
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
