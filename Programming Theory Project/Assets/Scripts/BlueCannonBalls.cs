using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCannonBalls : ShootCannon
{
    public ParticleSystem explosion;
    public override void MatchedCollision()
    {
        // play explosion particle
        explosion = FindObjectOfType<ParticleSystem>();
        explosion.transform.position = transform.position;
        explosion.Play();
        base.MatchedCollision();
        

    }
}
