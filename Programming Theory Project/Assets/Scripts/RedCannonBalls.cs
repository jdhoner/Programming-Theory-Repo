using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCannonBalls : ShootCannon
{
    public SpawnManager spawnManager;


    public override void Awake()
    {
        base.Awake();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    public override void MatchedCollision()
    {
        base.MatchedCollision();


        // trigger SpawnManager function that respawns balls in active spawnPos
        foreach (GameObject spawnPos in GameObject.FindGameObjectsWithTag("Spawn Location"))
        {

            if(spawnPos.transform.childCount > 0&& spawnPos.transform.GetChild(0).gameObject!= collidedBall)
            {
                Transform spawnChild = spawnPos.transform.GetChild(0);
                Destroy(spawnChild.gameObject);
                GameObject ball = Instantiate(spawnManager.ballPrefab[spawnManager.RandomCannonball()], spawnPos.transform.position, spawnPos.transform.rotation);
                ball.transform.parent = spawnPos.transform;
            }        
        }



    }

}
