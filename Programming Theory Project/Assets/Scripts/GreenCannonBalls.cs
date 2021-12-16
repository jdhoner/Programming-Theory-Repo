using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCannonBalls : ShootCannon
{
    [SerializeField] AudioSource sound;
    [SerializeField] GameObject cam;

    public override void MatchedCollision()
    {
        cam = GameObject.Find("Main Camera");
        sound = cam.GetComponent<AudioSource>();
        sound.Play();
        base.MatchedCollision();
    }

}
