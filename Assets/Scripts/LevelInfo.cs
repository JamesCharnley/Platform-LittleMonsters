using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] int maxPoints = 0;
    [SerializeField] int nextSceneIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        maxPoints = points.Length;
    }

    public int GetMaxPoints()
    {
        return maxPoints;
    }
    public int GetNextSceneIndex()
    {
        return nextSceneIndex;
    }
}
