using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate 4 tronçons

        GameObject go = obstacles[Random.Range(0, obstacles.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
