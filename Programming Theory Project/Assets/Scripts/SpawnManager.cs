using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBalls()
    {
        foreach(GameObject spawnPos in GameObject.FindGameObjectsWithTag("Spawn Location"))
        {
            int spawnedBall = Random.Range(0, ballPrefab.Length);
            Instantiate(ballPrefab[spawnedBall], spawnPos.transform.position, spawnPos.transform.rotation);
        }
    }
}
